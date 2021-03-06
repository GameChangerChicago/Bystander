﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PartyGameManager : MonoBehaviour
{
    public bool CameraMoving
    {
        get
        {
            return _cameraMoving;
        }
        set
        {
            if (value != _cameraMoving)
            {
				Debug.Log(value);
                if (value)
                {
                    for (int i = 0; i < _propsPerIM[_currentInteractiveMoment].Length; i++)
                    {
                        _propsPerIM[_currentInteractiveMoment][i].Disabled = true;
                    }
                }
                else if(!_exit)
                {
                    for (int i = 0; i < _propsPerIM[_currentInteractiveMoment].Length; i++)
                    {
                        _propsPerIM[_currentInteractiveMoment][i].Disabled = false;
                    }
                }
                else if (_exit)
                {
                    _exit = false;
                }

                _cameraMoving = value;
            }
        }
    }
    private bool _cameraMoving;

    //HACK: This will need to updated once I finish the other final comics
    public bool ChoseCoral
    {
        get
        {
            return _choseCoral;
        }
        set
        {
            if (value)
            {
                CoralBryanProp.SetActive(true);
                CoralAnishaProp.SetActive(true);
                DavidBryanProp.SetActive(false);
                DavidBryanProp2.SetActive(false);
            }
            else
            {
                DavidBryanProp.SetActive(true);
                DavidBryanProp2.SetActive(true);
                CoralBryanProp.SetActive(false);
                CoralAnishaProp.SetActive(false);
            }

            _choseCoral = value;
        }
    }
    private bool _choseCoral;

    public GameObject CurrentSection
    {
        get
        {
            return _currentSection;
        }
    }

    //These five game objects are the various prefabs that make up the 5 levels
    public GameObject LivingRoom,
                      Kitchen,
                      BackPoarch,
                      LivingRoom2,
                      Hallway,
                      CoralBryanProp,
                      DavidBryanProp,
                      CoralAnishaProp,
                      DavidBryanProp2;
    public SpriteRenderer[] BystanderPortraits;
    public int MaxClicks;
    public bool SectionCompleted = false;

    //This is an enum used to keep track of what level we are on
    private enum InteractiveMoments
    {
        LivingRoom,
        Kitchen,
        BackPoarch,
        LivingRoom2,
        Hallway
    };

    private Dictionary<InteractiveMoments, InteractableProp[]> _propsPerIM = new Dictionary<InteractiveMoments, InteractableProp[]>();
    private InteractableProp[] _allProps;
    private GameManager _gameManager;
    private InteractiveMoments _currentInteractiveMoment = InteractiveMoments.LivingRoom;
    private PartyCameraManager _myCameraManager;
    private PartyVirgil _virgil;
    private HintManager _hintManager;
    private SpriteRenderer _currentBystanderPortrait;
    private GameObject _currentSection;
    private float _cameraTravelTime;
    private int _clickCount = 0;
    private bool _exit;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _myCameraManager = FindObjectOfType<PartyCameraManager>();
        _virgil = FindObjectOfType<PartyVirgil>();
        _currentSection = GameObject.Find("LivingRoom1");
        _currentBystanderPortrait = BystanderPortraits[0];
        _hintManager = FindObjectOfType<HintManager>();

        InitializeProps();
    }

    private void InitializeProps()
    {
        _propsPerIM.Add(InteractiveMoments.LivingRoom, LivingRoom.GetComponentsInChildren<InteractableProp>());
        _propsPerIM.Add(InteractiveMoments.Kitchen, Kitchen.GetComponentsInChildren<InteractableProp>());
        _propsPerIM.Add(InteractiveMoments.BackPoarch, BackPoarch.GetComponentsInChildren<InteractableProp>());
        _propsPerIM.Add(InteractiveMoments.LivingRoom2, LivingRoom2.GetComponentsInChildren<InteractableProp>());
        _propsPerIM.Add(InteractiveMoments.Hallway, Hallway.GetComponentsInChildren<InteractableProp>());
        _allProps = FindObjectsOfType<InteractableProp>();
    }

    //This method is called whenever an interactable prop is clicked
    //It's kind of an all purpose method handling the various types of interactable props
    public void PlayerClicked(bool importantProp, bool hasCutscene, float cameraTravelTime, Vector3 myPanelPos, float camSize, float viewTime, float camRotation)
    {
        _cameraTravelTime = cameraTravelTime;

        if (!hasCutscene)
        {
            _clickCount++;
            StartCoroutine(_myCameraManager.ReturnCamera(_currentSection.transform.position, _cameraTravelTime + viewTime, 0));
            //Invoke("VirgilHandler", (_cameraTravelTime * 2) + viewTime);
        }

        if (importantProp)
            SectionCompleted = true;
        else
            _currentBystanderPortrait.color = new Color(1, 1, 1, 1f - (float)_clickCount / (float)MaxClicks);

        _myCameraManager.SetCameraToMove(myPanelPos, _cameraTravelTime, camSize, camRotation, importantProp);
    }

    //This is called when a dialog has reached its end
    public void FinishDialog()
    {
        _clickCount++;
        StartCoroutine(_myCameraManager.ReturnCamera(_currentSection.transform.position, 0, 0));
        //Invoke("VirgilHandler", _cameraTravelTime);
    }

    //This method is called the max clicks has been reached
    public IEnumerator FinsihInteractiveSegment(float waitTime, bool exit)
    {
        yield return new WaitForSeconds(waitTime);

        if (SectionCompleted) //If you've clicked the correct interactable prop
        {
            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < _propsPerIM[_currentInteractiveMoment].Length; i++)
            {
                _propsPerIM[_currentInteractiveMoment][i].Disabled = true;
            }

            //Sets the next prefab or ends the scenario
            switch (_currentInteractiveMoment)
            {
                case InteractiveMoments.LivingRoom:
                    //This should actually instantiate the Kitchen but that pfab hasn't been made yet
                    //Each one of these also needs to set the Max click count as it may be different for each Interactive moment
                    _currentInteractiveMoment = InteractiveMoments.Kitchen;
                    _currentSection = Kitchen;
                    _myCameraManager.MovingToNext = true;
                    _myCameraManager.SetCameraToMove(Kitchen.transform.position, 3, 19, 0, false);
                    _currentBystanderPortrait = BystanderPortraits[1];
                    MaxClicks = 3;
                    break;
                case InteractiveMoments.Kitchen:
                    _currentInteractiveMoment = InteractiveMoments.BackPoarch;
                    _currentSection = BackPoarch;
                    _myCameraManager.MovingToNext = true;
                    _myCameraManager.SetCameraToMove(BackPoarch.transform.position, 3, 19, 0, false);
                    _currentBystanderPortrait = BystanderPortraits[2];
                    MaxClicks = 2;
                    break;
                case InteractiveMoments.BackPoarch:
                    _currentInteractiveMoment = InteractiveMoments.LivingRoom2;
                    _currentSection = LivingRoom2;
                    _myCameraManager.MovingToNext = true;
                    _myCameraManager.SetCameraToMove(LivingRoom2.transform.position, 3, 19, 0, false);
                    _currentBystanderPortrait = BystanderPortraits[3];
                    MaxClicks = 2;
                    break;
                case InteractiveMoments.LivingRoom2:
                    _currentInteractiveMoment = InteractiveMoments.Hallway;
                    _currentSection = Hallway;
                    _myCameraManager.MovingToNext = true;
                    _hintManager.transform.localScale = new Vector3(4.45f, 4.45f, 1);
                    _myCameraManager.SetCameraToMove(Hallway.transform.position, 3, 24, 0, false);
                    MaxClicks = 1;
                    break;
                case InteractiveMoments.Hallway:
                    StartCoroutine(_gameManager.LoadingHandler("PostParty"));
                    break;
                default:
                    Debug.Log("There are only 5 Interactive moments. You should check _currentInteractiveMoment.");
                    break;
            }
            SectionCompleted = false;
            _clickCount = 0;
        }
        else if (_clickCount >= MaxClicks || exit) //If you never clicked the correct interactable prop or you hit the exit sign
        {
            _exit = exit;
            _virgil.VirgilReset();
            _currentSection = GameObject.Find("LivingRoom1");
            _currentInteractiveMoment = InteractiveMoments.LivingRoom;
            MaxClicks = 6;
            _clickCount = 0;
        }
    }

    //Some iteractable props have an animation that plays in the close up panel this method handles that
    public IEnumerator PlayCloseUpAnimation(Animator currentAnimator, int clickCount, float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);

        if (currentAnimator.speed == 0) //Initially and when props are reset, the animator's speed is set to 0; this set's it back to 1
        {
            currentAnimator.speed = 1;
            currentAnimator.Play(currentAnimator.gameObject.name + "_" + clickCount);
        }
        else //Otherwise we will play the next animation
        {
            currentAnimator.Play(currentAnimator.gameObject.name + "_" + clickCount);
        }
    }

    public void DisableAllProps()
    {
        for (int i = 0; i < _propsPerIM[_currentInteractiveMoment].Length; i++)
        {
            _propsPerIM[_currentInteractiveMoment][i].Disabled = true;
        }
    }

    public IEnumerator EnableAllProps(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        for (int i = 0; i < _allProps.Length; i++)
        {
            _allProps[i].Disabled = false;
        }
    }

    public void ResetProps()
    {
        for (int i = 0; i < _allProps.Length; i++)
        {
            _allProps[i].ResetProp();
        }

        _myCameraManager.ResetCam();

        for (int i = 0; i < BystanderPortraits.Length; i++)
        {
            BystanderPortraits[i].color = new Color(1, 1, 1, 1);
        }
    }

    public bool IsFinalInteractiveMoment()
    {
        if (_currentInteractiveMoment == InteractiveMoments.Hallway)
            return true;
        else
            return false;
    }
}