using UnityEngine;
using System.Collections;

public class InteractableProp : MonoBehaviour
{
    public bool MousedOver
    {
        get
        {
            return _mousedOver;
        }

        set
        {
            if (_mousedOver != value)
            {
                if (value)
                {
                    MouseOverObject.SetActive(true);
                    _cursorHandler.ChangeCursor(0);

                    for (int i = 0; i < FindObjectsOfType<InteractableProp>().Length; i++)
                    {
                        if (FindObjectsOfType<InteractableProp>()[i] != this)
                            FindObjectsOfType<InteractableProp>()[i].MousedOver = false;
                    }
                }
                else
                {
                    MouseOverObject.SetActive(false);

                    if (!_clicked && !Physics2D.OverlapCircle(_myCamera.ScreenToWorldPoint(Input.mousePosition), 0.01f))
                    {
                        _cursorHandler.ChangeCursor(1);
                    }
                    else
                        _clicked = false;
                }

                _mousedOver = value;
            }
        }
    }
    private bool _mousedOver;

    public bool ImportantProp,
                HasMultipleSteps,
                AnimationChanges,
                HasCloseupAnimation,
                HasCutscene,
                InfiniteClicks,
                Disabled,
                Exit;
    public int MaxClicks,
               DialogSections;
    public float CameraMoveTime,
                 CameraSize,
                 ViewTime,
                 CamRotation;
    public Transform MyPanelPos;
    public AudioSource BGMSource;
    public AudioClip[] MyBGM,
                       MySFX;
    public Animator CloseUpAnimator;
    public GameObject MouseOverObject;

    private bool _firstDialog,
                 _clicked,
                 _hasCutscene;
    private int _myClickCount = 0,
                _SFXIndex;
    private string _dialog;
    private Animator _myAnimator;
    private PartyGameManager _myGameManager;
    private CutSceneManager _myCSManager;
    private CursorHandler _cursorHandler;
    private Camera _myCamera;
    private Collider2D _overlapCircle;

    void Start()
    {
        _myGameManager = FindObjectOfType<PartyGameManager>();
        _myAnimator = GetComponent<Animator>();
        _myCamera = Camera.main;
        _cursorHandler = FindObjectOfType<CursorHandler>();

        if (HasCloseupAnimation)
            CloseUpAnimator.speed = 0;

        _myCSManager = MyPanelPos.gameObject.GetComponent<CutSceneManager>();

        if (_myCSManager != null)
        {
            _hasCutscene = true;
        }
    }

    void Update()
    {
        _overlapCircle = Physics2D.OverlapCircle(_myCamera.ScreenToWorldPoint(Input.mousePosition), 0.01f);

        if (_overlapCircle && _overlapCircle.transform.parent != null && MaxClicks != _myClickCount && !Disabled)
        {
            if (_overlapCircle.transform.parent.parent == this.transform)
                MousedOver = true;
        }
        else
            MousedOver = false;

        if (MousedOver)
            ClickHandler();
    }

    private void ClickHandler()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _clicked = true;

            //If an Interactable Props changes animations while off screen
            if (AnimationChanges)
                Invoke("ChangeAnimation", CameraMoveTime);

            //This calls PlayerClicked sending all of the impertinent data
            _myGameManager.PlayerClicked(ImportantProp, _hasCutscene, CameraMoveTime, MyPanelPos.position, CameraSize, ViewTime, CamRotation);

            //If the interactable prop has dialog than this calls the first DialogHandler
            if (!_hasCutscene)
                StartCoroutine(_myGameManager.FinsihInteractiveSegment((2 * CameraMoveTime) + ViewTime, Exit));

            //If the interactable prop has dialog than this calls PlayCloseUpAnimation
            if (HasCloseupAnimation && !InfiniteClicks)
            {
                StartCoroutine(_myGameManager.PlayCloseUpAnimation(CloseUpAnimator, _myClickCount, CameraMoveTime));
            }
            else if (HasCloseupAnimation)
            {
                StartCoroutine(_myGameManager.PlayCloseUpAnimation(CloseUpAnimator, 0, CameraMoveTime));
            }

            //This bit handles background music changes
            if (MyBGM.Length != 0)
            {
                Invoke("ChangeBGM", CameraMoveTime);
            }

            if (_hasCutscene)
            {
                _myCSManager.enabled = true;
            }

            _myClickCount++;

            if (InfiniteClicks)
                if (_myClickCount == MaxClicks)
                    _myClickCount = 0;
        }
    }

    private void ChangeAnimation()
    {
        _myAnimator.Play(this.name + "_" + _myClickCount);
    }

    private void ChangeBGM()
    {
        if (BGMSource.clip == MyBGM[0])
        {
            BGMSource.clip = MyBGM[1];
            BGMSource.Play();
        }
        else
        {
            BGMSource.clip = MyBGM[0];
            BGMSource.Play();
        }
    }

    public void ResetProp()
    {
        _myClickCount = 0;

        if (AnimationChanges)
            _myAnimator.Play(this.name + "_0");

        if (HasCloseupAnimation)
        {
            CloseUpAnimator.StartPlayback();
            CloseUpAnimator.Play(CloseUpAnimator.name + "_0");
            CloseUpAnimator.speed = 0;
        }

        if (HasCutscene)
        {
            CloseUpAnimator.SetInteger("AnimatorIndex", 0);
            _myCSManager.enabled = false;
            _myCSManager.CurrentStep = 0;
        }
    }
}