using UnityEngine;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.UnityGUI;

public class DialogueVisualUI : UnityDialogueUI
{
    private bool _clickEnabled = true;

    // This is a reference to the NPC subtitle fade effect. If the subtitle panel is already
    // visible, we want to manually disable the fade effect so the panel stays visible.
    public FadeEffect npcBorderFadeEffect;

    // We use this bool to remember whether the subtitle panel is already visible:
    private bool isSubtitleVisible = false;

    // Make sure npcBorderFadeEffect is assigned:
    public override void Awake()
    {
        base.Awake();
        if (npcBorderFadeEffect == null) Debug.LogError(string.Format("{0}: NPC Border Fade Effect is unassigned!", DialogueDebug.Prefix));
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
        if (_clickEnabled)
        {
            _clickEnabled = false;
            Invoke("EnableClick", 1.25f);
            base.OnContinue();
        }
    }

    public override void OnClick(object data)
    {
        if(_clickEnabled)
        {
            _clickEnabled = false;
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
        _clickEnabled = true;
    }
}