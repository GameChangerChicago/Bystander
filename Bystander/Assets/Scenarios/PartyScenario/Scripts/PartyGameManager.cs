using UnityEngine;
using System.Collections;

public class PartyGameManager : MonoBehaviour
{
    //These five game objects are the various prefabs that make up the 5 levels
    public GameObject LivingRoom,
                      Kitchen,
                      BackPoarch,
                      LivingRoom2,
                      Hallway;
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
    }

    //This method is called whenever an interactable prop is clicked
    //It's kind of an all purpose method handling the various types of interactable props
    public void PlayerClicked(bool importantProp, bool hasDialog, float cameraTravelTime, Vector3 myPanelPos, float camSize, float viewTime, float camRotation)
    {
        _cameraTravelTime = cameraTravelTime;

        if (!hasDialog)
        {
            _clickCount++;
            StartCoroutine(_myCameraManager.ReturnCamera(_currentSection.transform.position, _cameraTravelTime + viewTime));
            Invoke("VirgilHandler", (_cameraTravelTime * 2) + viewTime);
        }

        if (importantProp)
            SectionCompleted = true;

        _myCameraManager.SetCameraToMove(myPanelPos, _cameraTravelTime, camSize, camRotation);
    }

    //This is called when a dialog has reached its end
    public void FinishDialog()
    {
        _clickCount++;
        StartCoroutine(_myCameraManager.ReturnCamera(_currentSection.transform.position, 0));
        Invoke("VirgilHandler", _cameraTravelTime);
    }

    //This method is called the max clicks has been reached
    public void FinsihInteractiveSegment()
    {
        if (SectionCompleted) //If you've clicked the correct interactable prop
        {
            //Sets the next prefab or ends the scenario
            switch (_currentInteractiveMoment)
            {
                case InteractiveMoments.LivingRoom:
                    //This should actually instantiate the Kitchen but that pfab hasn't been made yet
                    //Each one of these also needs to set the Max click count as it may be different for each Interactive moment
                    _myCameraManager.SetCameraToMove(Kitchen.transform.position, 3, 19, 0);
                    _currentInteractiveMoment = InteractiveMoments.Kitchen;
                    _currentSection = Kitchen;
                    break;
                case InteractiveMoments.Kitchen:
                    _myCameraManager.SetCameraToMove(BackPoarch.transform.position, 3, 19, 0);
                    _currentInteractiveMoment = InteractiveMoments.BackPoarch;
                    _currentSection = BackPoarch;
                    break;
                case InteractiveMoments.BackPoarch:
                    _myCameraManager.SetCameraToMove(LivingRoom2.transform.position, 3, 19, 0);
                    _currentInteractiveMoment = InteractiveMoments.LivingRoom2;
                    _currentSection = LivingRoom2;
                    break;
                case InteractiveMoments.LivingRoom2:
                    Debug.Log("Done");
                    break;
                case InteractiveMoments.Hallway:
                    Debug.Log("Hallway");
                    break;
                default:
                    Debug.Log("There are only 5 Interactive moments. You should check _currentInteractiveMoment.");
                    break;
            }
        }
        else //If you never clicked the correct interactable prop
        {
            //Sets the same prfab to be loaded effectively reseting the level
            switch (_currentInteractiveMoment)
            {
                case InteractiveMoments.LivingRoom:
                    break;
                case InteractiveMoments.Kitchen:
                    Debug.Log("Kitchen");
                    break;
                case InteractiveMoments.BackPoarch:
                    Debug.Log("Back Poarch");
                    break;
                case InteractiveMoments.LivingRoom2:
                    Debug.Log("Living Room2");
                    break;
                case InteractiveMoments.Hallway:
                    Debug.Log("Hallway");
                    break;
                default:
                    Debug.Log("There are only 5 Interactive moments. You should check _currentInteractiveMoment.");
                    break;
            }
        }

        _virgil = FindObjectOfType<PartyVirgil>();
        _clickCount = 0;
    }

    //Some iteractable props have an animation that plays in the close up panel this method handles that
    public IEnumerator PlayCloseUpAnimation(Animator currentAnimator, int clickCount, float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);

        if (currentAnimator.enabled != true)
            currentAnimator.enabled = true;
        else
            currentAnimator.Play(currentAnimator.gameObject.name + "_" + clickCount);
    }

    //This method brings up virgil and disables the interactable props
    private void VirgilHandler()
    {
        if (_clickCount >= MaxClicks)
        {
            _virgil.Appear();

            InteractableProp[] props = FindObjectsOfType<InteractableProp>();

            for (int i = 0; i < props.Length; i++)
            {
                props[i].GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}