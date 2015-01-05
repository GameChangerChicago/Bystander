using UnityEngine;
using System.Collections;

public class PartyGameManager : MonoBehaviour
{
    public GameObject DialogBox1,
                      DialogBox2,
                      LivingRoom,
                      Kitchen,
                      BackPoarch,
                      LivingRoom2,
                      Hallway;
    public Transform[] MainPanels;
    public int MaxClicks;
    public bool SectionCompleted = false;

    protected Transform currentMainPanel
    {
        get
        {
            _currentMainPanel = MainPanels[_interactableMomentCount];
            return _currentMainPanel;
        }
        set
        {
            Debug.LogWarning("No one should be trying to set currentMainPanel");
        }
    }
    private Transform _currentMainPanel;

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
    private GameObject _currentPrefab;
    private float _cameraTravelTime;
    private int _clickCount = 0,
                _stringIndex = 0,
                _stringsShown = 0,
                _interactableMomentCount = 0;
    private bool _usingBox1 = true;

    void Start()
    {
        _myCameraManager = FindObjectOfType<PartyCameraManager>();
        _virgil = FindObjectOfType<PartyVirgil>();
        _currentPrefab = GameObject.Find("LivingRoom");
    }

    public void PlayerClicked(bool importantProp, bool hasDialog, float cameraTravelTime, Vector3 myPanelPos, float camSize, float viewTime)
    {
        _cameraTravelTime = cameraTravelTime;

        if (!hasDialog)
        {
            _clickCount++;
            StartCoroutine(_myCameraManager.ReturnCamera(currentMainPanel.position, _cameraTravelTime + viewTime));
            Invoke("VirgilHandler", (_cameraTravelTime * 2) + viewTime);
        }

        if (importantProp)
            SectionCompleted = true;

        _myCameraManager.SetCameraToMove(myPanelPos, _cameraTravelTime, camSize);
    }

    public void FinishDialog()
    {
        _clickCount++;
        StartCoroutine(_myCameraManager.ReturnCamera(currentMainPanel.position, 0));
        Invoke("VirgilHandler", _cameraTravelTime);
    }

    public void FinsihInteractiveSegment()
    {
        if (SectionCompleted)
        {
            Debug.Log("Load new prefab");
            //This will delete the prefab and instantiate the next prefab
            //This will likely be a switch statement
        }
        else
        {
            GameObject.Destroy(_currentPrefab);

            switch (_currentInteractiveMoment)
            {
                case InteractiveMoments.LivingRoom:
                    Instantiate(LivingRoom);
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
    }

    public IEnumerator PlayCloseUpAnimation(Animation currentAnimation, int clickCount, float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);

        currentAnimation.clip = currentAnimation.GetClip(currentAnimation.gameObject.name + "_" + clickCount);
        currentAnimation.Play();
    }

    private void VirgilHandler()
    {
        if (_clickCount >= MaxClicks)
        {
            if (SectionCompleted)
                _virgil.Appear(true);
            else
                _virgil.Appear(false);
        }
    }
}