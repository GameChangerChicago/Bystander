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

    private Dictionary<MicroScenarios, PointOfInterest[]> PointsOfInterestPerMicroScenario = new Dictionary<MicroScenarios, PointOfInterest[]>();
    private SHCameraSelector[] _allSelectors;
    private SHVigilHandler _myVirgil;
    private int _sectionsCompleted;

    //This property looks at all of the SHCameraManagers and checks to see which one is the current camera then returns said SHCameraManager
    public SHCameraManager CurrentCameraManager
    {
        get
        {
            SHCameraManager[] cameraManagers = FindObjectsOfType<SHCameraManager>();

            for (int i = 0; i < cameraManagers.Length; i++)
            {
                if (cameraManagers[i].IsCurrentCamera)
                {
                    _currentCameraManager = cameraManagers[i];
                    break;
                }
                else
                    _currentCameraManager = cameraManagers[0];
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
                _sectionsCompleted++;

                //Checks to see if all sections are complete; if so then we'll load the epilogue
                if (_sectionsCompleted == 5)
                    Application.LoadLevel("PostHarassment");
                else //Otherwise we add one to _sectionsComplete and brings us back to the hub world
                    CurrentCameraManager.ReturnToHub();
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
        _allSelectors = FindObjectsOfType<SHCameraSelector>();
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
        PointsOfInterestPerMicroScenario.Add(MicroScenarios.Hallway, GameObject.Find("Hallway").GetComponentsInChildren<PointOfInterest>());
        PointsOfInterestPerMicroScenario.Add(MicroScenarios.Classroom, GameObject.Find("Classroom").GetComponentsInChildren<PointOfInterest>());
        PointsOfInterestPerMicroScenario.Add(MicroScenarios.Library, GameObject.Find("Library").GetComponentsInChildren<PointOfInterest>());
        PointsOfInterestPerMicroScenario.Add(MicroScenarios.Gym, GameObject.Find("Gym").GetComponentsInChildren<PointOfInterest>());
        PointsOfInterestPerMicroScenario.Add(MicroScenarios.Bathroom, GameObject.Find("Bathroom").GetComponentsInChildren<PointOfInterest>());
        foreach (PointOfInterest poi in GameObject.FindObjectsOfType<PointOfInterest>())
        {
            poi.enabled = false;
            poi.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }

    public void ActivatePOIs()
    {
        for (int i = 0; i < PointsOfInterestPerMicroScenario[CurrentMicroScenario].Length; i++)
        {
            PointsOfInterestPerMicroScenario[CurrentMicroScenario][i].enabled = true;
            for (int j = 0; j < PointsOfInterestPerMicroScenario[CurrentMicroScenario][i].GetComponentsInChildren<SpriteRenderer>().Length; j++)
            {
                if (PointsOfInterestPerMicroScenario[CurrentMicroScenario][i].GetComponentsInChildren<SpriteRenderer>()[j] != PointsOfInterestPerMicroScenario[CurrentMicroScenario][i].MouseOverSprite)
                    PointsOfInterestPerMicroScenario[CurrentMicroScenario][i].GetComponentsInChildren<SpriteRenderer>()[j].enabled = true;
            }
        }
    }

    public void DeselectPOIs(PointOfInterest poi)
    {
        for (int i = 0; i < PointsOfInterestPerMicroScenario[CurrentMicroScenario].Length; i++)
        {
            if (poi != PointsOfInterestPerMicroScenario[CurrentMicroScenario][i])
                PointsOfInterestPerMicroScenario[CurrentMicroScenario][i].MouseOverSprite.enabled = false;
        }
    }

    public void HideCurrentPOIs()
    {
        for (int i = 0; i < PointsOfInterestPerMicroScenario[CurrentMicroScenario].Length; i++)
        {
            for (int j = 0; j < PointsOfInterestPerMicroScenario[CurrentMicroScenario][i].GetComponentsInChildren<SpriteRenderer>().Length; j++)
                PointsOfInterestPerMicroScenario[CurrentMicroScenario][i].GetComponentsInChildren<SpriteRenderer>()[j].enabled = false;

            PointsOfInterestPerMicroScenario[CurrentMicroScenario][i].enabled = false;
        }
    }

    public IEnumerator ReenablePOIs()
    {
        yield return new WaitForSeconds(0.05f);
        FocusedOnPOI = false;
    }
}