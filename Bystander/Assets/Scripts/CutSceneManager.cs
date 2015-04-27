﻿using UnityEngine;
using System.Collections;

public class CutSceneManager : MonoBehaviour
{
    public Step[] Page;
    public AudioSource IntroAudio;

    private Rect _rect,
                 _rectDiff;
    private Vector2 _pos;
    private float _camTravelTime,
                  _camSize,
                  _camSizeDiff;
    private int _currentStep;
    private bool _movingCamera,
                 _clickDisabled,
                 _introAudioFinished;

    void OnMouseDown()
    {
        if (!_clickDisabled && _introAudioFinished && Page[_currentStep].SceneToLoad == "")
        {
            if (Page[_currentStep].CamTravelTime > 0)
                SetCameraToMove();

            if (Page[_currentStep].MyAnimator != null && !Page[_currentStep].PlayImmediately)
                Invoke("FireAnimation", Page[_currentStep].CamTravelTime);
            else if (Page[_currentStep].MyAnimator != null && !_clickDisabled)
                FireAnimation();

            if (Page[_currentStep].MyTextMesh != null)
                ChangeDialog();

            Invoke("AdvanceStep", Page[_currentStep].CamTravelTime + 0.05f);
            if (Page[_currentStep].ClickDelay > Page[_currentStep].CamTravelTime)
                Invoke("ReEnableClicking", Page[_currentStep].ClickDelay);
            else
                Invoke("ReEnableClicking", Page[_currentStep].CamTravelTime);

            _clickDisabled = true;
        }
        else if (Page[_currentStep].SceneToLoad != "")
            Application.LoadLevel(Page[_currentStep].SceneToLoad);
    }

    void Update()
    {
        if (IntroAudio != null && !IntroAudio.isPlaying)
            _introAudioFinished = true;
        if (_movingCamera)
            MoveCameraTo();
    }

    private void FireAnimation()
    {
        if (Page[_currentStep].AnimatorIndex != 0)
            Page[_currentStep].MyAnimator.SetInteger("AnimatorIndex", Page[_currentStep].AnimatorIndex);
        Page[_currentStep].MyAnimator.enabled = true;
    }

    private void ChangeDialog()
    {
        if (!Page[_currentStep].TextBox.active)
            Page[_currentStep].TextBox.SetActive(true);

        StringFormatter(Page[_currentStep].MyText);
    }

    //This method takes current string and makes sure it doesn't run outhside the bounds of the dialog box
    private void StringFormatter(string lineContent)
    {
        string currentWord = "";
        bool isFirstWord = true;
        MeshRenderer currentRenderer;
        GameObject dialogBox = Page[_currentStep].TextBox;

        currentRenderer = dialogBox.GetComponentInChildren<MeshRenderer>();
        dialogBox.GetComponentInChildren<TextMesh>().text = currentWord;

        for (int i = 0; i < lineContent.Length; i++)
        {
            //As long as the char isn't a ' ' then it will be added to currentWord
            if (lineContent[i] != ' ')
            {
                currentWord += lineContent[i];
            }
            else
            {
                //If currentWord is the first word it is added to the to the dialog box
                if (isFirstWord)
                {
                    dialogBox.GetComponentInChildren<TextMesh>().text = dialogBox.GetComponentInChildren<TextMesh>().text + currentWord;

                    isFirstWord = false;
                }
                else // Otherwise it adds the current word with a space before the word.
                {
                    dialogBox.GetComponentInChildren<TextMesh>().text = dialogBox.GetComponentInChildren<TextMesh>().text + " " + currentWord;
                }

                //If after adding the word the line extends past the TextBounds then the word will be added with a line break
                if (currentRenderer.bounds.extents.x > Page[_currentStep].TextBounds)
                {
                    dialogBox.GetComponentInChildren<TextMesh>().text = dialogBox.GetComponentInChildren<TextMesh>().text.Remove(dialogBox.GetComponentInChildren<TextMesh>().text.Length - (currentWord.Length));
                    dialogBox.GetComponentInChildren<TextMesh>().text = dialogBox.GetComponentInChildren<TextMesh>().text + "\n" + currentWord;
                }

                //Resets the current word each time
                currentWord = "";
            }
        }
    }

    //This method initializes the moving process; setting impertinant fields and also invokes StopMoving
    public void SetCameraToMove()
    {
        _pos = Page[_currentStep].CamLocation;
        _rect = Page[_currentStep].CamRectangle;
        _camTravelTime = Page[_currentStep].CamTravelTime;
        _camSize = Page[_currentStep].CamSize;
        _camSizeDiff = Mathf.Abs(this.camera.orthographicSize - Page[_currentStep].CamSize);
        _rectDiff = new Rect(Mathf.Abs(this.camera.rect.x - _rect.x), Mathf.Abs(this.camera.rect.y - _rect.y), Mathf.Abs(this.camera.rect.width - _rect.width), Mathf.Abs(this.camera.rect.height - _rect.height));
        _movingCamera = true;
        Invoke("StopMoving", Page[_currentStep].CamTravelTime);
    }

    //This method moves and resizes the camera for each page section
    private void MoveCameraTo()
    {
        float travelTimeModifier = 1f;
        Hashtable Values = new Hashtable();
        Values.Add("x", _pos.x);
        Values.Add("y", _pos.y);
        Values.Add("z", this.transform.position.z);
        Values.Add("time", Page[_currentStep].CamTravelTime);
        Values.Add("easetype", iTween.EaseType.easeOutQuad);

        //Using the iTween method MoveTo we set an object to move, a location, and a speed; iTween handles the rest
        //iTween.MoveTo(this.gameObject, new Vector3(_pos.x, _pos.y, this.transform.position.z), _camTravelTime);
        iTween.MoveTo(this.gameObject, Values);

        //If the current camera size is greater than _camSize then we know we need to decrease the size of the camera
        if (this.camera.orthographicSize > _camSize)
        {
            this.camera.orthographicSize -= _camSizeDiff / (_camTravelTime / Time.deltaTime * travelTimeModifier);

            //Eventually the camera size will be slightly smaller than the desired size; once this happens we simply set the size to _camSize
            if (this.camera.orthographicSize < _camSize)
                this.camera.orthographicSize = _camSize;
        }
        else if (this.camera.orthographicSize < _camSize) //If the current camera size is less than _camSize then we know we need to increase the size of the camera
        {
            this.camera.orthographicSize += _camSizeDiff / (_camTravelTime / Time.deltaTime * travelTimeModifier);

            //Eventually the camera size will be slightly larger than the desired size; once this happens we simply set the size to _camSize
            if (this.camera.orthographicSize > _camSize)
                this.camera.orthographicSize = _camSize;
        }

        //These lines handle the rectangle changes
        float x = this.camera.rect.x;
        float y = this.camera.rect.y;
        float width = this.camera.rect.width;
        float height = this.camera.rect.height;
        if (x > _rect.x)
        {
            x -= _rectDiff.x / (_camTravelTime / Time.deltaTime * travelTimeModifier);
        }
        else
        {
            x = _rect.x;
        }
        if (y > _rect.y)
        {
            y -= _rectDiff.y / (_camTravelTime / Time.deltaTime * travelTimeModifier);
        }
        else
        {
            y = _rect.y;
        }
        if (width > _rect.width)
        {
            width -= _rectDiff.width / (_camTravelTime / Time.deltaTime * travelTimeModifier);
        }
        else
        {
            width = _rect.width;
        }
        if (height > _rect.height)
        {
            height -= _rectDiff.height / (_camTravelTime / Time.deltaTime * travelTimeModifier);
        }
        else
        {
            height = _rect.height;
        }
        this.camera.rect = new Rect(x, y, width, height);
    }

    private void StopMoving()
    {
        _movingCamera = false;
    }

    private void AdvanceStep()
    {
        _currentStep++;
    }

    private void ReEnableClicking()
    {
        _clickDisabled = false;
    }
}