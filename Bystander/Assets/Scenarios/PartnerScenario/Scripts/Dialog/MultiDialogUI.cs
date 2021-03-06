﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.UnityGUI;

//[System.Serializable]
public class MultiDialogUI : DialogueVisualUI
{
    public UnityDialogueControls[] DialogUiSets;
    public string[] DialogUiNames;

    private Dictionary<string, UnityDialogueControls> _dialogUiSets = new Dictionary<string, UnityDialogueControls>();

    void Start()
    {
        myGameManager = FindObjectOfType<PartnerGameManager>();

        for (int i = 0; i < DialogUiSets.Length; i++)
        {
            _dialogUiSets.Add(DialogUiNames[i], DialogUiSets[i]);
        }

        dialogue = DialogUiSets[0];
        ChangeDialog("DialogueVisualUIDefault");
    }

    public void ChangeDialog(string setUpName)
    {
        Close();
        dialogue = _dialogUiSets[setUpName];
        Open();
        ShowSubtitle(myGameManager.CurrentSubtitle);
    }
}