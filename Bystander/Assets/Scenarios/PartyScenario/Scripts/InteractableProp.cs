using UnityEngine;
using System.Collections;

public class InteractableProp : MonoBehaviour
{
    public bool ImportantProp,
                HasDialog,
                HasMultipleSteps,
                AnimationChanges,
                HasCloseupAnimation;
    public int MaxClicks,
               DialogSections;
    public float CameraMoveTime,
                 CameraSize,
                 ViewTime;
    public Transform MyPanelPos;
    public AudioClip MySFX;
    public Animation CloseUpAnimation;

    private bool _firstDialog;
    private int _myClickCount = 0;
    private string _dialog;
    private PartyGameManager _myGameManager;

    void Start()
    {
        _myGameManager = FindObjectOfType<PartyGameManager>();
    }

    void OnMouseDown()
    {
        if (MaxClicks != _myClickCount)
        {
            //Interactable Props with multiple steps need to call different animations each time
            if (HasMultipleSteps)
            {
                animation.clip = animation.GetClip(this.name + _myClickCount);
                Invoke("ChangeAnimation", CameraMoveTime);
            } //Otherwise, don't worry about it
            else if (AnimationChanges)
                Invoke("ChangeAnimation", CameraMoveTime);

            //This calls PlayerClicked sending all of the impertinent data
            _myGameManager.PlayerClicked(ImportantProp, HasDialog, CameraMoveTime, MyPanelPos.position, CameraSize, ViewTime);

            //If the interactable prop has dialog than this calls the first DialogHandler
            if(HasDialog)
            {
                ConvoHandler currentCloseUpConvo = GameObject.Find("CloseUpPanel_" + this.name).GetComponent<ConvoHandler>();
                StartCoroutine(currentCloseUpConvo.DialogHandler(CameraMoveTime));
                _myClickCount++;
            }

            //If the interactable prop has dialog than this calls PlayCloseUpAnimation
            if (HasCloseupAnimation)
            {
                StartCoroutine(_myGameManager.PlayCloseUpAnimation(CloseUpAnimation, _myClickCount, CameraMoveTime));
                _myClickCount++;
            }
        }
    }

    private void ChangeAnimation()
    {
        animation.Play();
    }
}