﻿using UnityEngine;
using System.Collections;

public class CutSceneManager : MonoBehaviour
{
    protected bool clickDisabled
    {
        get
        {
            return _clickDisabled;
        }
        set
        {
            if (value != _clickDisabled)
            {
                if (value)
                {
                    _cursorHandler.ChangeCursor(2);
                }
                else
                {
                    _cursorHandler.ChangeCursor(1);
                }

                _clickDisabled = value;
            }
        }
    }
    private bool _clickDisabled;

    public Step[] Page;
    public AudioSource IntroAudio,
                       StepSoundEffect;
    public int CurrentStep;

    private GameManager _gameManager;
    private PartyGameManager _partyGameManager;
    private PartyCameraManager _partyCameraManager;
    private CursorHandler _cursorHandler;
    private Rect _rect,
                 _rectDiff;
    private Vector2 _pos;
    private float _camTravelTime,
                  _camSize,
                  _camSizeDiff,
                  _camRotation,
                  _camRotationDiff;
    private bool _movingCamera,
                 _introAudioFinished = true,
                 _started;

    void Start()
    {
        _cursorHandler = FindObjectOfType<CursorHandler>();
        _gameManager = FindObjectOfType<GameManager>();

        if (IntroAudio != null)
        {
            _cursorHandler.ChangeCursor(2);
            _introAudioFinished = false;
        }

        if (Page[Page.Length - 1].PartyCameraTravelTime > 0)
        {
            _partyGameManager = FindObjectOfType<PartyGameManager>();
            _partyCameraManager = FindObjectOfType<PartyCameraManager>();
        }
        
        if (Page[CurrentStep].InitialAutoStep)
        {
            CurrentStep--;
            _cursorHandler.ChangeCursor(2);
            _clickDisabled = true;
            Invoke("AutoStep", Page[CurrentStep + 1].InitialDelay);

            //DELETE THIS SOON
            //As long as it doesn't break anything...
            //if (!Page[CurrentStep + 2].AutoStep)
            //{
            //    if (Page[CurrentStep + 1].ClickDelay > Page[CurrentStep + 1].CamTravelTime)
            //        Invoke("ReEnableClicking", Page[CurrentStep + 1].ClickDelay);
            //    else
            //        Invoke("ReEnableClicking", Page[CurrentStep + 1].CamTravelTime);
            //}
        }

        _started = true;
    }

    void OnEnable()
    {
        if (_started)
        {
            if (Page[CurrentStep].InitialAutoStep)
            {
                CurrentStep--;
                _cursorHandler.ChangeCursor(2);
                _clickDisabled = true;
                Invoke("AutoStep", Page[CurrentStep + 1].InitialDelay);

                //DELETE THIS SOON
                //As long as it doesn't break anything...
                //if (!Page[CurrentStep + 2].AutoStep)
                //{
                //    if (Page[CurrentStep + 1].InitialDelay > Page[CurrentStep + 1].CamTravelTime)
                //        Invoke("ReEnableClicking", Page[CurrentStep + 1].InitialDelay);
                //    else
                //        Invoke("ReEnableClicking", Page[CurrentStep + 1].CamTravelTime);
                //}
            }
        }
    }

    void OnMouseDown()
    {
        if (!clickDisabled && _introAudioFinished && Page[CurrentStep].SceneToLoad == "")
        {
            clickDisabled = true;

            if (Page[CurrentStep].CamTravelTime > 0)
                SetCameraToMove();

            if (Page[CurrentStep].MyAnimator != null && !Page[CurrentStep].PlayAnimImmediately)
                Invoke("FireAnimation", Page[CurrentStep].CamTravelTime);
            else if (Page[CurrentStep].MyAnimator != null)
                FireAnimation();

            if (Page[CurrentStep].StepClip != null && !Page[CurrentStep].PlayAudioImmediately)
                Invoke("PlaySoundEffect", Page[CurrentStep].CamTravelTime + Page[CurrentStep].AudioDelay);
            else if (Page[CurrentStep].StepClip != null)
                PlaySoundEffect();

            if (Page[CurrentStep].MyTextMesh != null)
                ChangeDialog();

            if (Page[CurrentStep].PartyCameraTravelTime > 0)
            {
                StartCoroutine(_partyCameraManager.ReturnCamera(_partyGameManager.CurrentSection.transform.position, 0, 0));
                StartCoroutine(_partyGameManager.FinsihInteractiveSegment(Page[CurrentStep].PartyCameraTravelTime, false));
            }

            if (CurrentStep + 1 < Page.Length)
            {
                if (Page[CurrentStep].ClickDelay > Page[CurrentStep].CamTravelTime)
                {
                    Invoke("ReEnableClicking", Page[CurrentStep].ClickDelay + 0.05f);

                    if (!Page[CurrentStep + 1].AutoStep)
                        Invoke("AdvanceStep", Page[CurrentStep].ClickDelay + 0.025f);
                    else
                        Invoke("AutoStep", Page[CurrentStep].ClickDelay + 0.025f);
                }
                else
                {
                    Invoke("ReEnableClicking", Page[CurrentStep].CamTravelTime + 0.05f);

                    if (!Page[CurrentStep + 1].AutoStep)
                        Invoke("AdvanceStep", Page[CurrentStep].CamTravelTime + 0.025f);
                    else
                        Invoke("AutoStep", Page[CurrentStep].CamTravelTime + 0.025f);
                }
            }
        }
        else if (!clickDisabled && Page[CurrentStep].SceneToLoad != "")
        {
            //The only scenes who's second character is 'o' are the post cutscenes
            //So basically what I'm doing with that second condition is seeing if this is a post cutscene
            if (_gameManager.SingleScenarioMode && Application.loadedLevelName[1] == 'o')
            {
				StartCoroutine(_gameManager.LoadingHandler("Final_MainMenu"));
                _gameManager.SingleScenarioMode = false;
            }
            else
            {
                if (Page[CurrentStep].SceneToLoad == "CLOSE")
                    Application.Quit();
                else
                {
                    StartCoroutine(_gameManager.LoadingHandler(Page[CurrentStep].SceneToLoad));
                }
            }
        }
    }

    void Update()
    {
        if (IntroAudio != null && IntroAudio.time >= IntroAudio.clip.length && !_introAudioFinished)
        {
            if (CurrentStep > -1)
            {
                if (Page[CurrentStep].AutoStep)
                {
                    CurrentStep--;
                    AutoStep();
                }
            }

            _introAudioFinished = true;
        }

        if (_movingCamera)
            MoveCameraTo();
    }

    private void FireAnimation()
    {
        if (Page[CurrentStep].AnimatorIndex != 0)
            Page[CurrentStep].MyAnimator.SetInteger("AnimatorIndex", Page[CurrentStep].AnimatorIndex);
        Page[CurrentStep].MyAnimator.enabled = true;
    }

    private void PlaySoundEffect()
    {
        StepSoundEffect.clip = Page[CurrentStep].StepClip;
        StepSoundEffect.Play();
    }

    private void ChangeDialog()
    {
        if (!Page[CurrentStep].TextBox.activeSelf)
            Page[CurrentStep].TextBox.SetActive(true);

        StringFormatter(Page[CurrentStep].MyText);
    }

    //This method takes current string and makes sure it doesn't run outhside the bounds of the dialog box
    private void StringFormatter(string lineContent)
    {
        string currentWord = "";
        bool isFirstWord = true;
        GameObject dialogBox = Page[CurrentStep].TextBox;
        TextMesh dBTextMesh = dialogBox.GetComponentInChildren<TextMesh>();
        MeshRenderer currentRenderer = dialogBox.GetComponentInChildren<MeshRenderer>();

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
                    dBTextMesh.text = dBTextMesh.text + currentWord;

                    isFirstWord = false;
                }
                else // Otherwise it adds the current word with a space before the word.
                {
                    dBTextMesh.text = dBTextMesh.text + " " + currentWord;
                }

                //If after adding the word the line extends past the TextBounds then the word will be added with a line break
                if (Page[CurrentStep].CamRotation == 90 || Page[CurrentStep].CamRotation == -90 || Page[CurrentStep].CamRotation == 270 || Page[CurrentStep].CamRotation == -270)
                {
                    if (currentRenderer.bounds.extents.y > Page[CurrentStep].TextBounds)
                    {
                        dBTextMesh.text = dBTextMesh.text.Remove(dBTextMesh.text.Length - (currentWord.Length));
                        dBTextMesh.text = dBTextMesh.text + "\n" + currentWord;
                    }
                }
                else
                {
                    if (currentRenderer.bounds.extents.x > Page[CurrentStep].TextBounds)
                    {
                        dBTextMesh.text = dBTextMesh.text.Remove(dBTextMesh.text.Length - (currentWord.Length));
                        dBTextMesh.text = dBTextMesh.text + "\n" + currentWord;
                    }
                }

                //Resets the current word each time
                currentWord = "";
            }
        }
    }

    //This method initializes the moving process; setting impertinant fields and also invokes StopMoving
    public void SetCameraToMove()
    {
        _pos = Page[CurrentStep].CamLocation;
        _rect = Page[CurrentStep].CamRectangle;
        _camTravelTime = Page[CurrentStep].CamTravelTime;

        if (this.GetComponent<AspectRatioHandler>().IsMacAspect)
            _camSize = Page[CurrentStep].CamSize + (Page[CurrentStep].CamSize * 0.11f);
        else
            _camSize = Page[CurrentStep].CamSize;

        _camSizeDiff = Mathf.Abs(this.camera.orthographicSize - _camSize);
        _camRotation = Page[CurrentStep].CamRotation;
        _camRotationDiff = Mathf.Abs(this.transform.rotation.z - Page[CurrentStep].CamRotation);
        _rectDiff = new Rect(Mathf.Abs(this.camera.rect.x - _rect.x), Mathf.Abs(this.camera.rect.y - _rect.y), Mathf.Abs(this.camera.rect.width - _rect.width), Mathf.Abs(this.camera.rect.height - _rect.height));
        _movingCamera = true;
        Invoke("StopMoving", Page[CurrentStep].CamTravelTime);
    }

    //This method moves and resizes the camera for each page section
    private void MoveCameraTo()
    {
        float travelTimeModifier = 1f,
              currentRotation = this.transform.rotation.z;
        
        //Initializing MoveTo values
        Hashtable MoveValues = new Hashtable();
        MoveValues.Add("x", _pos.x);
        MoveValues.Add("y", _pos.y);
        MoveValues.Add("z", this.transform.position.z);
        MoveValues.Add("time", Page[CurrentStep].CamTravelTime);
        MoveValues.Add("easetype", iTween.EaseType.easeOutQuad);

        //Initializing RotateTo values
        Hashtable RotateValues = new Hashtable();
        RotateValues.Add("z", _camRotation);
        RotateValues.Add("time", Page[CurrentStep].CamTravelTime);
        RotateValues.Add("easetype", iTween.EaseType.easeOutQuad);

        //Using the iTween method MoveTo we set an object to move, a location, and a speed; iTween handles the rest
        //iTween.MoveTo(this.gameObject, new Vector3(_pos.x, _pos.y, this.transform.position.z), _camTravelTime);
        iTween.MoveTo(this.gameObject, MoveValues);

        iTween.RotateTo(this.gameObject, RotateValues);

        //If the current camera size is greater than _camSize then we know we need to decrease the size of the camera
        if (this.camera.orthographicSize > _camSize)
        {
            this.camera.orthographicSize -= _camSizeDiff / (_camTravelTime / Time.deltaTime * travelTimeModifier);
            _gameManager.PauseMenu.transform.localScale = new Vector3(_gameManager.PauseMenu.transform.localScale.x - ((_camSizeDiff * 0.06f) / (_camTravelTime / Time.deltaTime * travelTimeModifier)), _gameManager.PauseMenu.transform.localScale.y - ((_camSizeDiff * 0.06f) / (_camTravelTime / Time.deltaTime * travelTimeModifier)), _gameManager.PauseMenu.transform.localScale.z);

            //Eventually the camera size will be slightly smaller than the desired size; once this happens we simply set the size to _camSize
            if (this.camera.orthographicSize < _camSize)
                this.camera.orthographicSize = _camSize;
        }
        else if (this.camera.orthographicSize < _camSize) //If the current camera size is less than _camSize then we know we need to increase the size of the camera
        {
            this.camera.orthographicSize += _camSizeDiff / (_camTravelTime / Time.deltaTime * travelTimeModifier);
            _gameManager.PauseMenu.transform.localScale = new Vector3(_gameManager.PauseMenu.transform.localScale.x - ((_camSizeDiff * 0.06f) / (_camTravelTime / Time.deltaTime * travelTimeModifier)), _gameManager.PauseMenu.transform.localScale.y - ((_camSizeDiff * 0.06f) / (_camTravelTime / Time.deltaTime * travelTimeModifier)), _gameManager.PauseMenu.transform.localScale.z);

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
        //GameObject.Destroy(Page[CurrentStep]);
        CurrentStep++;
    }

    private void AutoStep()
    {
        clickDisabled = true;
        CurrentStep++;

        if (Page[CurrentStep].SceneToLoad == "")
        {
            if (Page[CurrentStep].CamTravelTime > 0)
                SetCameraToMove();

            if (Page[CurrentStep].MyAnimator != null && !Page[CurrentStep].PlayAnimImmediately)
                Invoke("FireAnimation", Page[CurrentStep].CamTravelTime);
            else if (Page[CurrentStep].MyAnimator != null)// && !clickDisabled)
                FireAnimation();

            if (Page[CurrentStep].StepClip != null && !Page[CurrentStep].PlayAudioImmediately)
                Invoke("PlaySoundEffect", Page[CurrentStep].CamTravelTime + Page[CurrentStep].AudioDelay);
            else if (Page[CurrentStep].StepClip != null)
                PlaySoundEffect();

            if (Page[CurrentStep].MyTextMesh != null)
                ChangeDialog();

            if (CurrentStep + 1 <= Page.Length)
            {
                if (Page[CurrentStep].ClickDelay > Page[CurrentStep].CamTravelTime)
                {
                    Invoke("ReEnableClicking", Page[CurrentStep].ClickDelay + 0.05f);

                    if (!Page[CurrentStep + 1].AutoStep)
                        Invoke("AdvanceStep", Page[CurrentStep].ClickDelay + 0.025f);
                    else
                        Invoke("AutoStep", Page[CurrentStep].ClickDelay + 0.025f);
                }
                else
                {
                    Invoke("ReEnableClicking", Page[CurrentStep].CamTravelTime + 0.05f);

                    if (!Page[CurrentStep + 1].AutoStep)
                        Invoke("AdvanceStep", Page[CurrentStep].CamTravelTime + 0.025f);
                    else
                        Invoke("AutoStep", Page[CurrentStep].CamTravelTime + 0.025f);
                }
            }

            clickDisabled = true;
        }
        else if (Page[CurrentStep].SceneToLoad != "")
        {
            //The only scenes who's second character is 'o' are the post cutscenes
            //So basically what I'm doing with that second condition is seeing if this is a post cutscene
            if (_gameManager.SingleScenarioMode && Application.loadedLevelName[1] == 'o')
            {
                StartCoroutine(_gameManager.LoadingHandler("Final_MainMenu"));
                _gameManager.SingleScenarioMode = false;
            }
            else
            {
                if (Page[CurrentStep].SceneToLoad == "CLOSE")
                    Application.Quit();
                else
                {
                    StartCoroutine(_gameManager.LoadingHandler(Page[CurrentStep].SceneToLoad));
                }
            }
        }
    }

    private void ReEnableClicking()
    {
        if (!Page[CurrentStep].AutoStep)
            clickDisabled = false;
    }
}