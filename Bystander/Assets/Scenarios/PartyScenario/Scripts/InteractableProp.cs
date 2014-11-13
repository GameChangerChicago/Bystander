using UnityEngine;
using System.Collections;

public class InteractableProp : MonoBehaviour
{
    public bool ImportantProp,
                HasDialog,
                HasMultipleSteps;
    public int MaxClicks,
               DialogSections;
    public AudioClip MySFX;

    private int _myClickCount = 0;
    private string _dialog;
    private PartyGameManager _myGameManager;

    void Start()
    {
        _myGameManager = FindObjectOfType<PartyGameManager>();
        TextAsset rawText = Resources.Load("DialogText/TestDialog") as TextAsset;
        _dialog = rawText.text;
    }

    void OnMouseDown()
    {
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

        if (!HasDialog)
            StartCoroutine(_myGameManager.PlayerClicked(ImportantProp, animation.clip.length));
        else
            _myGameManager.DialogHandler(ImportantProp, _dialog, DialogSections);
    }
}