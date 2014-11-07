using UnityEngine;
using System.Collections;

public class PartyGameManager : MonoBehaviour
{
    public int MaxClicks;

    private PartyCameraManager _myCameraManager;
    private Virgil _virgil;
    private int _clickCount = 0;
    private bool _sectionCompleted = false;

    void Start()
    {
        _myCameraManager = FindObjectOfType<PartyCameraManager>();
        _virgil = FindObjectOfType<Virgil>();
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
        if (_clickCount >= MaxClicks)
        {
            if (_sectionCompleted)
                _virgil.Appear(true);
            else
                _virgil.Appear(false);
        }
    }
}
