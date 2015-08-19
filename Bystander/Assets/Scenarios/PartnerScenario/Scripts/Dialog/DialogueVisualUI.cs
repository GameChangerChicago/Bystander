using UnityEngine;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.UnityGUI;

public class DialogueVisualUI : UnityDialogueUI
{
    protected PartnerGameManager myGameManager;

    protected bool ClickEnabled
    {
        get
        {
            return _clickEnabled;
        }
        set
        {
            if (value != _clickEnabled)
            {
                if (value)
                    _cursorHandler.ChangeCursor(1);
                else
                    _cursorHandler.ChangeCursor(2);

                _clickEnabled = value;
            }
        }
    }
    private bool _clickEnabled = true;

    private CursorHandler _cursorHandler;
    private GUIButton[] _guiButtons;
    private float _width,
                  _height;
    private bool _button1MouseOver,
                 _button2MouseOver,
                 _button3MouseOver,
                 _button4MouseOver;

    // This is a reference to the NPC subtitle fade effect. If the subtitle panel is already
    // visible, we want to manually disable the fade effect so the panel stays visible.
    public FadeEffect npcBorderFadeEffect;

    // We use this bool to remember whether the subtitle panel is already visible:
    private bool isSubtitleVisible = false;

    // Make sure npcBorderFadeEffect is assigned:
    public override void Awake()
    {
        _guiButtons = FindObjectsOfType<GUIButton>();
        _cursorHandler = FindObjectOfType<CursorHandler>();
        base.Awake();
        myGameManager = FindObjectOfType<PartnerGameManager>();
        if (npcBorderFadeEffect == null) Debug.LogError(string.Format("{0}: NPC Border Fade Effect is unassigned!", DialogueDebug.Prefix));
    }

    public override void Update()
    {
        base.Update();

        for (int i = 0; i < _guiButtons.Length; i++)
        {
            float xOffset = 0,
                  yOffset = 0;

            if (_guiButtons[i].name == "Button 1")
            {
                xOffset = Screen.width * -0.031f;
                yOffset = Screen.height * 0.401f;
            }
            if (_guiButtons[i].name == "Button 2")
            {
                xOffset = Screen.width * 0.031f;
                yOffset = Screen.height * 0.4f;
            }
            if (_guiButtons[i].name == "Button 3")
            {
                xOffset = Screen.width * 0.165f;
                yOffset = Screen.height * 0.31f;
            }
            if (_guiButtons[i].name == "Button 4")
            {
                xOffset = Screen.width * -0.164f;
                yOffset = Screen.height * 0.311f;
            }

            if (Input.mousePosition.x < _guiButtons[i].rect.xMax - xOffset &&
                Input.mousePosition.x > _guiButtons[i].rect.xMin - xOffset &&
                Input.mousePosition.y < _guiButtons[i].rect.yMax + yOffset &&
                Input.mousePosition.y > _guiButtons[i].rect.yMin + yOffset)
            {
                if (_guiButtons[i].name == "Button 1")
                    _button1MouseOver = true;
                if (_guiButtons[i].name == "Button 2")
                    _button2MouseOver = true;
                if (_guiButtons[i].name == "Button 3")
                    _button3MouseOver = true;
                if (_guiButtons[i].name == "Button 4")
                    _button4MouseOver = true;
            }
            else
            {
                if (_guiButtons[i].name == "Button 1")
                    _button1MouseOver = false;
                if (_guiButtons[i].name == "Button 2")
                    _button2MouseOver = false;
                if (_guiButtons[i].name == "Button 3")
                    _button3MouseOver = false;
                if (_guiButtons[i].name == "Button 4")
                    _button4MouseOver = false;
            }
        }

        if (_button1MouseOver || _button2MouseOver || _button3MouseOver || _button4MouseOver)
            _cursorHandler.ChangeCursor(0);
        else
            _cursorHandler.ChangeCursor(1);

        //Debug.Log("Mouse Pos: " + Input.mousePosition);
    }

    // When the UI has just been opened, the subtitle panel is not visible:
    public override void Open()
    {
        base.Open();
        isSubtitleVisible = false;
    }
    
    // If the subtitle panel is already visible, disable the fade effect:
    public override void ShowSubtitle(Subtitle subtitle)
    {
        if (npcBorderFadeEffect != null) npcBorderFadeEffect.enabled = !isSubtitleVisible;
        isSubtitleVisible = true;
        base.ShowSubtitle(subtitle);
    }

    public override void OnContinue()
    {
        if (ClickEnabled)
        {
            ClickEnabled = false;
            myGameManager.InteractionEnabled = false;
            Invoke("EnableClick", 1.25f);
            base.OnContinue();
        }
    }

    public override void OnClick(object data)
    {
        if (ClickEnabled)
        {
            ClickEnabled = false;
            myGameManager.InteractionEnabled = false;
            Invoke("EnableClick", 1.25f);
            base.OnClick(data);
        }
    }

    public override void ShowResponses(Subtitle subtitle, Response[] responses, float timeout)
    {
        //Scramble the responses but keeps "Keep Listening" at the bottom
        if (responses.Length > 3)
        {
            List<int> responseIndex = new List<int>();
            responseIndex.Add(0);
            responseIndex.Add(1);
            responseIndex.Add(2);

            Response[] responseRef = new Response[4] { responses[0], responses[1], responses[2], responses[3] };

            for (int i = 0; i < responseRef.Length; i++)
            {
                if (i == 2)
                {
                    responses[i] = responseRef[3];
                }
                else
                {
                    int randIndex;

                    if (responseIndex.Count > 1)
                        randIndex = Random.Range(0, responseIndex.Count);
                    else
                        randIndex = 0;

                    responses[i] = responseRef[responseIndex[randIndex]];
                    responseIndex.RemoveAt(randIndex);
                }
            }
        }

        base.ShowResponses(subtitle, responses, timeout);
    }

    public void EnableClick()
    {
        ClickEnabled = true;
        myGameManager.InteractionEnabled = true;
    }
}