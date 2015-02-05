﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This enum will need to be updated so that One-Five are named what the five intervention types end up being named.
public enum ButtonType
{
    Yes,
    No,
    One,
    Two,
    Three,
    Four,
    Five
}

public class SHGameManager : MonoBehaviour
{
    static Dictionary<ButtonType, bool> AnswersSelected = new Dictionary<ButtonType, bool>();

    private int _sectionsCompleted;

    //This property looks at all of the SHCameraManagers and checks to see which one is the current camera then returns said SHCameraManager
    protected SHCameraManager currentCameraManager
    {
        get
        {
            for (int i = 0; i < FindObjectsOfType<SHCameraManager>().Length; i++)
            {
                if (FindObjectsOfType<SHCameraManager>()[i].IsCurrentCamera)
                {
                    _currentCameraManager = FindObjectsOfType<SHCameraManager>()[i];
                    break;
                }
            }

            return _currentCameraManager;
        }
    }
    private SHCameraManager _currentCameraManager;

    //This property, when set to true, calls ReturnToHub in whichever camera is the current camera.
    public bool SectionComplete
    {
        get
        {
            return sectionComplete;
        }
        set
        {
            if (value)
            {
                //Checks to see if all sections are complete; if so then we'll load the epilogue
                if (_sectionsCompleted == 5)
                    Debug.Log("Load Epilogue");
                else //Otherwise we add one to _sectionsComplete and brings us back to the hub world
                {
                    _sectionsCompleted++;
                    currentCameraManager.ReturnToHub();
                }
            }
        }
    }
    protected bool sectionComplete;

    public PointOfInterest CurrentPOI;

    void Start()
    {
        AnswersSelected.Add(ButtonType.One, false);
        AnswersSelected.Add(ButtonType.Two, false);
        AnswersSelected.Add(ButtonType.Three, false);
        AnswersSelected.Add(ButtonType.Four, false);
        AnswersSelected.Add(ButtonType.Five, false);
    }

    public bool CheckAnswer(ButtonType button)
    {
        if (!AnswersSelected[button])
        {
            AnswersSelected[button] = true;
            return true;
        }
        else
            return false;
    }
}