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
    private Vector3 _myInitialSize;

    void Start()
    {
        _myGameManager = FindObjectOfType<PartyGameManager>();
        TextAsset rawText = Resources.Load("PartyDialogText/TestDialog") as TextAsset;
        _dialog = rawText.text;
        _myInitialSize = this.GetComponent<BoxCollider>().size;
    }

    void OnMouseDown()
    {
        Debug.Log(MaxClicks + " != " + _myClickCount);
        if (MaxClicks != _myClickCount)
        {
            if (HasMultipleSteps)
            {
                animation.clip = animation.GetClip(this.name + _myClickCount);
                _myClickCount++;
                animation.Play();
            }
            else
                animation.Play();

            if (!HasDialog)
                StartCoroutine(_myGameManager.PlayerClicked(ImportantProp, animation.clip.length));
            else
            {
                if (_myGameManager.DialogHandler(ImportantProp, _dialog, DialogSections, _myInitialSize, this.GetComponent<BoxCollider>()))
                    _myClickCount++;
            }
        }
    }
}