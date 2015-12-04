using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;

public class HintManager : MonoBehaviour
{
    public SpriteRenderer InstructionSprite;
    public ConversationTrigger MyTrigger;
    public PartnerGameManager MyPartnerGameManager;
    public PartyGameManager MyPartyGameManager;
    public GameObject MyDialogueManager;
    public float HintDelay,
                 InitialDelay;

    protected bool fadingIn
    {
        get
        {
            return _fadingIn;
        }
        set
        {
            if (value)
            {
                List<Camera> allCameras = new List<Camera>();
                
                allCameras.AddRange(FindObjectsOfType<Camera>());

                if (allCameras.Count > 2)
                {
                    for (int i = 0; i < allCameras.Count; i++)
                    {
                        //this is for the sh scene
                    }
                }
                else
                {
                    _currentCamera = Camera.main;

                    InstructionSprite.transform.localScale = new Vector3(_currentCamera.orthographicSize * 0.19f, _currentCamera.orthographicSize * 0.19f, 1);
                }
            }

            _fadingIn = value;
        }
    }

    DatabaseManager _databaseManager;
    private SHCameraSelector[] _SHCameraSelector;
    private CursorHandler _cursorHandler;
    private Camera _currentCamera;
    private float _hintTimer;
    private bool _timerActive = true,
                 _initialHintShown,
                 _fadingIn,
                 _fadingOut;

    void Start()
    {
        _cursorHandler = FindObjectOfType<CursorHandler>();

        if (Application.loadedLevelName == "SexualHarassment")
            _SHCameraSelector = FindObjectsOfType<SHCameraSelector>();
    }

    void Update()
    {
        if (_timerActive)
            _hintTimer += Time.deltaTime;

        if (_hintTimer > InitialDelay && !_initialHintShown)
        {
            _hintTimer = 0;
            _timerActive = false;
            _initialHintShown = true;
            fadingIn = true;
        }

        if (_hintTimer > HintDelay && _initialHintShown)
        {
            _hintTimer = 0;
            _timerActive = false;
            fadingIn = true;
            HintDelay += 10;

            if (MyTrigger != null)
            {
                MyDialogueManager.SetActive(false);
                MyPartnerGameManager.enabled = false;
            }
        }

        if (!_timerActive && !fadingIn && Input.GetKeyUp(KeyCode.Mouse0))
        {
            if(MyPartyGameManager)
                StartCoroutine(MyPartyGameManager.EnableAllProps(0.01f));

            _timerActive = true;
            _fadingOut = true;
        }

        if (Input.anyKeyDown && _initialHintShown)
        {
            _hintTimer = 0;
        }

        Fading();
    }

    private void Fading()
    {
        if (fadingIn && InstructionSprite.color.a == 0 && Application.loadedLevelName == "MaleScenario")
            FindObjectOfType<GUIManager>().ShowTextBar = false;

        if (fadingIn && Application.loadedLevelName == "PartyScenario")
        {
            _cursorHandler.ChangeCursor(1);
            MyPartyGameManager.DisableAllProps();
        }

        if (fadingIn && InstructionSprite.color.a < 1)
        {
            InstructionSprite.color = new Color(1, 1, 1, InstructionSprite.color.a + (2f * Time.deltaTime));

            if (InstructionSprite.color.a > 1)
            {
                InstructionSprite.color = new Color(1, 1, 1, 1);
                fadingIn = false;

                if (Application.loadedLevelName == "SexualHarassment")
                {
                    for (int i = 0; i < _SHCameraSelector.Length; i++)
                    {
                        _SHCameraSelector[i].enabled = false;
                    }
                }
            }
        }

        if (_fadingOut && InstructionSprite.color.a > 0)
        {
            InstructionSprite.color = new Color(1, 1, 1, InstructionSprite.color.a - (3f * Time.deltaTime));

            if (InstructionSprite.color.a < 0)
            {
                InstructionSprite.color = new Color(1, 1, 1, 0);
                _fadingOut = false;

                if (Application.loadedLevelName == "SexualHarassment")
                {
                    for (int i = 0; i < _SHCameraSelector.Length; i++)
                    {
                        _SHCameraSelector[i].enabled = true;
                    }
                }

                if (Application.loadedLevelName == "MaleScenario")
                    FindObjectOfType<GUIManager>().ShowTextBar = true;

                if (MyTrigger != null)
                {
                    MyTrigger.TryStartConversation(MyTrigger.actor);
                    MyPartnerGameManager.enabled = true;
                    MyDialogueManager.SetActive(true);
                }
            }
        }
    }
}