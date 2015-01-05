using UnityEngine;
using System.Collections;

public class PartyGameManager : MonoBehaviour
{
    public GameObject DialogBox1,
                      DialogBox2;
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

    private PartyCameraManager _myCameraManager;
    private PartyVirgil _virgil;
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
            Debug.Log("Load new prefab");
        else
            Debug.Log("Restart current interactive segment");
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