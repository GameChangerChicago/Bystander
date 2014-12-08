using UnityEngine;
using System.Collections;

public class InteractableProp : MonoBehaviour
{
    public bool ImportantProp,
                HasDialog,
                HasMultipleSteps,
                AnimationChanges;
    public int MaxClicks,
               DialogSections;
    public float CameraMoveTime,
                 CameraSize;
    public Vector3 MyPanelPos;
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
        if (MaxClicks != _myClickCount)
        {
            if (HasMultipleSteps)
            {
                animation.clip = animation.GetClip(this.name + _myClickCount);
                _myClickCount++;
                Invoke("ChangeAnimation", CameraMoveTime);
            }
            else if (AnimationChanges)
                Invoke("ChangeAnimation", CameraMoveTime);


            
            _myGameManager.PlayerClicked(ImportantProp, HasDialog, CameraMoveTime, MyPanelPos, CameraSize);

            if(HasDialog)
            {
                if (_myGameManager.DialogHandler(ImportantProp, _dialog, DialogSections, _myInitialSize, this.GetComponent<BoxCollider>()))
                    _myClickCount++;
            }
        }
    }

    private void ChangeAnimation()
    {
        animation.Play();
    }
}