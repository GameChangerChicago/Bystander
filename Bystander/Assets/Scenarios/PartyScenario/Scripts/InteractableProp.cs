using UnityEngine;
using System.Collections;

public class InteractableProp : MonoBehaviour
{
    public bool ImportantProp,
                HasDialog,
                HasMultipleSteps;
    public int MaxClicks;
    public AudioClip MySFX;

    private int _myClickCount = 0;
    private PartyGameManager _myGameManager;

    void Start()
    {
        _myGameManager = FindObjectOfType<PartyGameManager>();
    }

    void OnMouseDown()
    {
        _myGameManager.PlayerClicked(ImportantProp);

        if (HasMultipleSteps)
        {
            if (MaxClicks != _myClickCount)
            {
                animation.clip = animation.GetClip(this.name + _myClickCount);
                _myClickCount++;
                animation.Play();
            }
        }
        else
            animation.Play();
    }
}