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

                    for (int i = 0; i < FindObjectsOfType<InteractableProp>().Length; i++)
                    {
                        if (FindObjectsOfType<InteractableProp>()[i] != this)
                            FindObjectsOfType<InteractableProp>()[i].MousedOver = false;
                    }
                }
                else
                    MouseOverObject.SetActive(false);

                _mousedOver = value;
            }
        }
    }
    private bool _mousedOver;

    public bool ImportantProp,
                HasDialog,
                HasMultipleSteps,
                AnimationChanges,
                HasCloseupAnimation,
                InfiniteClicks,
                Disabled;
    public int MaxClicks,
               DialogSections;
    public float CameraMoveTime,
                 CameraSize,
                 ViewTime,
                 CamRotation;
    public Transform MyPanelPos;
    public AudioClip[] MyBGM,
                       MySFX;
    public Animator CloseUpAnimator;
    public GameObject MouseOverObject;

    private bool _firstDialog;
    private int _myClickCount = 0,
                _SFXIndex;
    private string _dialog;
    private Animator _myAnimator;
    private PartyGameManager _myGameManager;
    private Camera _myCamera;
    private Collider2D _overlapCircle;

    void Start()
    {
        _myGameManager = FindObjectOfType<PartyGameManager>();
        _myAnimator = GetComponent<Animator>();
        _myCamera = Camera.main;

        if (CloseUpAnimator != null)
            CloseUpAnimator.speed = 0;
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
            //If an Interactable Props changes animations while off screen
            if (AnimationChanges)
                Invoke("ChangeAnimation", CameraMoveTime);

            //This calls PlayerClicked sending all of the impertinent data
            _myGameManager.PlayerClicked(ImportantProp, HasDialog, CameraMoveTime, MyPanelPos.position, CameraSize, ViewTime, CamRotation);

            //If the interactable prop has dialog than this calls the first DialogHandler
            if (HasDialog)
            {
                ConvoHandler currentCloseUpConvo = GameObject.Find(this.name + "Comic").GetComponent<ConvoHandler>();
                StartCoroutine(currentCloseUpConvo.DialogHandler(CameraMoveTime));
            }
            else
                StartCoroutine(_myGameManager.FinsihInteractiveSegment((2 * CameraMoveTime) + ViewTime + 0.5f));

            //If the interactable prop has dialog than this calls PlayCloseUpAnimation
            if (HasCloseupAnimation)
            {
                StartCoroutine(_myGameManager.PlayCloseUpAnimation(CloseUpAnimator, _myClickCount, CameraMoveTime));
            }

            //This bit handles background music changes
            if (MyBGM.Length != 0)
            {
                Invoke("ChangeBGM", CameraMoveTime);
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
        if (FindObjectOfType<AudioSource>().clip == MyBGM[0])
        {
            FindObjectOfType<AudioSource>().clip = MyBGM[1];
            FindObjectOfType<AudioSource>().Play();
        }
        else
        {
            FindObjectOfType<AudioSource>().clip = MyBGM[0];
            FindObjectOfType<AudioSource>().Play();
        }
    }

    public void ResetProp()
    {
        _myClickCount = 0;

        if (AnimationChanges)
            _myAnimator.Play(this.name + "_0");

        if (HasCloseupAnimation)
        {
            CloseUpAnimator.Play(CloseUpAnimator.name + "_0");
            CloseUpAnimator.speed = 0;
            //CloseUpAnimator.enabled = false;
        }
    }
}