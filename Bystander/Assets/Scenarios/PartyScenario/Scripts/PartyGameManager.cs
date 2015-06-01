using UnityEngine;
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
                if (value)
                {
                    for (int i = 0; i < PropsPerIM[_currentInteractiveMoment].Length; i++)
                    {
                        PropsPerIM[_currentInteractiveMoment][i].Disabled = true;
                    }
                }
                else
                {
                    for (int i = 0; i < PropsPerIM[_currentInteractiveMoment].Length; i++)
                    {
                        PropsPerIM[_currentInteractiveMoment][i].Disabled = false;
                    }
                }

                _cameraMoving = value;
            }
        }
    }
    private bool _cameraMoving;

    //These five game objects are the various prefabs that make up the 5 levels
    public GameObject LivingRoom,
                      Kitchen,
                      BackPoarch,
                      LivingRoom2,
                      Hallway;
    public int MaxClicks;
    public bool SectionCompleted = false,
                ChoseCoral;

    //This is an enum used to keep track of what level we are on
    private enum InteractiveMoments
    {
        LivingRoom,
        Kitchen,
        BackPoarch,
        LivingRoom2,
        Hallway
    };

    private Dictionary<InteractiveMoments, InteractableProp[]> PropsPerIM = new Dictionary<InteractiveMoments, InteractableProp[]>();
    private InteractiveMoments _currentInteractiveMoment = InteractiveMoments.LivingRoom;
    private PartyCameraManager _myCameraManager;
    private PartyVirgil _virgil;
    private GameObject _currentSection;
    private float _cameraTravelTime;
    private int _clickCount = 0;

    void Start()
    {
        _myCameraManager = FindObjectOfType<PartyCameraManager>();
        _virgil = FindObjectOfType<PartyVirgil>();
        _currentSection = GameObject.Find("LivingRoom1");

        InitializeProps();
    }

    private void InitializeProps()
    {
        PropsPerIM.Add(InteractiveMoments.LivingRoom, LivingRoom.GetComponentsInChildren<InteractableProp>());
        PropsPerIM.Add(InteractiveMoments.Kitchen, Kitchen.GetComponentsInChildren<InteractableProp>());
        PropsPerIM.Add(InteractiveMoments.BackPoarch, BackPoarch.GetComponentsInChildren<InteractableProp>());
        PropsPerIM.Add(InteractiveMoments.LivingRoom2, LivingRoom2.GetComponentsInChildren<InteractableProp>());
        PropsPerIM.Add(InteractiveMoments.Hallway, Hallway.GetComponentsInChildren<InteractableProp>());
    }

    //This method is called whenever an interactable prop is clicked
    //It's kind of an all purpose method handling the various types of interactable props
    public void PlayerClicked(bool importantProp, bool hasDialog, float cameraTravelTime, Vector3 myPanelPos, float camSize, float viewTime, float camRotation)
    {
        _cameraTravelTime = cameraTravelTime;

        if (!hasDialog)
        {
            _clickCount++;
            StartCoroutine(_myCameraManager.ReturnCamera(_currentSection.transform.position, _cameraTravelTime + viewTime, 0));
            //Invoke("VirgilHandler", (_cameraTravelTime * 2) + viewTime);
        }

        if (importantProp)
            SectionCompleted = true;

        _myCameraManager.SetCameraToMove(myPanelPos, _cameraTravelTime, camSize, camRotation);
    }

    //This is called when a dialog has reached its end
    public void FinishDialog()
    {
        _clickCount++;
        StartCoroutine(_myCameraManager.ReturnCamera(_currentSection.transform.position, 0, 0));
        //Invoke("VirgilHandler", _cameraTravelTime);
    }

    //This method is called the max clicks has been reached
    public IEnumerator FinsihInteractiveSegment(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (SectionCompleted) //If you've clicked the correct interactable prop
        {
            for (int i = 0; i < PropsPerIM[_currentInteractiveMoment].Length; i++)
            {
                PropsPerIM[_currentInteractiveMoment][i].Disabled = true;
            }

            //Sets the next prefab or ends the scenario
            switch (_currentInteractiveMoment)
            {
                case InteractiveMoments.LivingRoom:
                    //This should actually instantiate the Kitchen but that pfab hasn't been made yet
                    //Each one of these also needs to set the Max click count as it may be different for each Interactive moment
                    _currentInteractiveMoment = InteractiveMoments.Kitchen;
                    _currentSection = Kitchen;
                    _myCameraManager.SetCameraToMove(Kitchen.transform.position, 3, 19, 0);
                    break;
                case InteractiveMoments.Kitchen:
                    _currentInteractiveMoment = InteractiveMoments.BackPoarch;
                    _currentSection = BackPoarch;
                    _myCameraManager.SetCameraToMove(BackPoarch.transform.position, 3, 19, 0);
                    break;
                case InteractiveMoments.BackPoarch:
                    _currentInteractiveMoment = InteractiveMoments.LivingRoom2;
                    _currentSection = LivingRoom2;
                    _myCameraManager.SetCameraToMove(LivingRoom2.transform.position, 3, 19, 0);
                    break;
                case InteractiveMoments.LivingRoom2:
                    _currentInteractiveMoment = InteractiveMoments.Hallway;
                    _currentSection = Hallway;
                    _myCameraManager.SetCameraToMove(Hallway.transform.position, 3, 24, 0);
                    break;
                case InteractiveMoments.Hallway:
                    Debug.Log("You win?");
                    break;
                default:
                    Debug.Log("There are only 5 Interactive moments. You should check _currentInteractiveMoment.");
                    break;
            }
            SectionCompleted = false;
            _clickCount = 0;
        }
        else if (_clickCount >= MaxClicks) //If you never clicked the correct interactable prop
        {
            for (int i = 0; i < PropsPerIM[_currentInteractiveMoment].Length; i++)
            {
                PropsPerIM[_currentInteractiveMoment][i].ResetProp();
            }

            _clickCount = 0;
        }

        _virgil = FindObjectOfType<PartyVirgil>();
    }

    //Some iteractable props have an animation that plays in the close up panel this method handles that
    public IEnumerator PlayCloseUpAnimation(Animator currentAnimator, int clickCount, float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);

        if (currentAnimator.speed == 0) //Initially and when props are reset, the animator's speed is set to 0; this set's it back to 1
            currentAnimator.speed = 1;
        else //Otherwise we will play the next animation
            currentAnimator.Play(currentAnimator.gameObject.name + "_" + clickCount);
    }

    //This method brings up virgil and disables the interactable props
    //private void VirgilHandler()
    //{
    //    if (_clickCount >= MaxClicks)
    //    {
    //        _virgil.Appear();

    //        InteractableProp[] props = FindObjectsOfType<InteractableProp>();

    //        for (int i = 0; i < props.Length; i++)
    //        {
    //            props[i].GetComponent<BoxCollider>().enabled = false;
    //        }
    //    }
    //}
}