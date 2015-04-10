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

    private Dictionary<MicroScenarios, PointOfInterest[]> PointsOfViewPerMicroScenario = new Dictionary<MicroScenarios, PointOfInterest[]>();
    private SHVigilHandler _myVirgil;
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
                //If we call ShowDialog with 'false' it will bring up the virgil dialog box with the 'wrong' dialog
                _myVirgil.ShowDialog(false);
                wrongAnswerCounter = 0;
            }
            else
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
        _myVirgil = FindObjectOfType<SHVigilHandler>();
        AnswersSelected.Add(ButtonType.CheckIn, false);
        AnswersSelected.Add(ButtonType.Empathy, false);
        AnswersSelected.Add(ButtonType.SilentStare, false);
        AnswersSelected.Add(ButtonType.IStatement, false);
        AnswersSelected.Add(ButtonType.Friends, false);
        InitializePointsOfView();
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

    private void InitializePointsOfView()
    {
        PointsOfViewPerMicroScenario.Add(MicroScenarios.Hallway, GameObject.Find("Hallway").GetComponentsInChildren<PointOfInterest>());
        PointsOfViewPerMicroScenario.Add(MicroScenarios.Classroom, GameObject.Find("Classroom").GetComponentsInChildren<PointOfInterest>());
        PointsOfViewPerMicroScenario.Add(MicroScenarios.Library, GameObject.Find("Library").GetComponentsInChildren<PointOfInterest>());
        //PointsOfViewPerMicroScenario.Add(MicroScenarios.Classroom, GameObject.Find("Classroom").GetComponentsInChildren<PointOfInterest>());
        //PointsOfViewPerMicroScenario.Add(MicroScenarios.Classroom, GameObject.Find("Classroom").GetComponentsInChildren<PointOfInterest>());
        foreach (PointOfInterest poi in GameObject.FindObjectsOfType<PointOfInterest>())
        {
            poi.enabled = false;
            poi.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }

    public void ActivatePOIs()
    {
        for (int i = 0; i < PointsOfViewPerMicroScenario[CurrentMicroScenario].Length; i++)
        {
            PointsOfViewPerMicroScenario[CurrentMicroScenario][i].enabled = true;
            for (int j = 0; j < PointsOfViewPerMicroScenario[CurrentMicroScenario][i].GetComponentsInChildren<SpriteRenderer>().Length; j++)
            {
                if (PointsOfViewPerMicroScenario[CurrentMicroScenario][i].GetComponentsInChildren<SpriteRenderer>()[j] != PointsOfViewPerMicroScenario[CurrentMicroScenario][i].MouseOverSprite)
                    PointsOfViewPerMicroScenario[CurrentMicroScenario][i].GetComponentsInChildren<SpriteRenderer>()[j].enabled = true;
            }
        }
    }
}