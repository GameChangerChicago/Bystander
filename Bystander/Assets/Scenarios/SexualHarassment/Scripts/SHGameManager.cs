using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This enum will need to be updated so that One-Five are named what the five intervention types end up being named.
public enum ButtonType
{
    Yes,
    No,
    CheckIn,
    Empathy,
    SilentStare,
    IStatement,
    Friends
}

public enum MicroScenarios
{
    Hallway,
    Classroom,
    Bathroom,
    Gym,
    Library
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


    public int WrongAnswerCounter
    {
        get
        {
            return wrongAnswerCounter;
        }
        set
        {
            if (value > 2)
            {
                Debug.Log("Virgil Shows up");
                wrongAnswerCounter = 0;
            }

            wrongAnswerCounter = value;
        }
    }
    protected int wrongAnswerCounter;

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

            sectionComplete = value;
        }
    }
    protected bool sectionComplete;

    public PointOfInterest CurrentPOI;
    public MicroScenarios CurrentMicroScenario;
    public bool FocusedOnPOI;

    void Start()
    {
        AnswersSelected.Add(ButtonType.CheckIn, false);
        AnswersSelected.Add(ButtonType.Empathy, false);
        AnswersSelected.Add(ButtonType.SilentStare, false);
        AnswersSelected.Add(ButtonType.IStatement, false);
        AnswersSelected.Add(ButtonType.Friends, false);
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