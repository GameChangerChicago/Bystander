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
                HasCloseupAnimation;
    public int MaxClicks,
               DialogSections;
    public float CameraMoveTime,
                 CameraSize,
                 ViewTime;
    public Transform MyPanelPos;
    public AudioClip[] MySFX;
    public Animator CloseUpAnimator;
    public GameObject MouseOverObject;

    private bool _firstDialog;
    private int _myClickCount = 0;
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
    }

    void Update()
    {
        _overlapCircle = Physics2D.OverlapCircle(_myCamera.ScreenToWorldPoint(Input.mousePosition), 0.01f);

        if (_overlapCircle && _overlapCircle.transform.parent != null && MaxClicks != _myClickCount)
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
            _myGameManager.PlayerClicked(ImportantProp, HasDialog, CameraMoveTime, MyPanelPos.position, CameraSize, ViewTime);

            //If the interactable prop has dialog than this calls the first DialogHandler
            if (HasDialog)
            {
                ConvoHandler currentCloseUpConvo = GameObject.Find("CloseUpPanel_" + this.name).GetComponent<ConvoHandler>();
                StartCoroutine(currentCloseUpConvo.DialogHandler(CameraMoveTime));
            }

            //If the interactable prop has dialog than this calls PlayCloseUpAnimation
            if (HasCloseupAnimation)
            {
                StartCoroutine(_myGameManager.PlayCloseUpAnimation(CloseUpAnimator, _myClickCount, CameraMoveTime));
            }

            if (MySFX[0] != null)
            {
                if (FindObjectOfType<AudioSource>().clip == MySFX[0])
                    FindObjectOfType<AudioSource>().clip = MySFX[1];
                else
                    FindObjectOfType<AudioSource>().clip = MySFX[0];

                FindObjectOfType<AudioSource>().Play();
            }

            _myClickCount++;
        }
    }

    private void ChangeAnimation()
    {
        _myAnimator.Play(this.name + "_" + _myClickCount);
    }
}