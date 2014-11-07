using UnityEngine;
using System.Collections;

public class PartyGameManager : MonoBehaviour
{
    public int MaxClicks;

    private PartyCameraManager _myCameraManager;
    private int _clickCount = 0;
    private bool _sectionCompleted = false;

    void Start()
    {
        _myCameraManager = FindObjectOfType<PartyCameraManager>();
    }

    public void PlayerClicked(bool importantProp)
    {
        _clickCount++;
        if (importantProp)
            _sectionCompleted = true;

        VirgilHandler();
    }

    private void VirgilHandler()
    {
        Debug.Log(_clickCount + " >= " + MaxClicks);
        if (_clickCount >= MaxClicks)
        {
            if (_sectionCompleted)
                Debug.Log("Cue Virgil Good");
            else
                Debug.Log("Cue Virgil Bad");
        }
    }
}
