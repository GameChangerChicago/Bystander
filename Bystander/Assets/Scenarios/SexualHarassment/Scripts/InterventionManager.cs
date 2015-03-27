using UnityEngine;
using System.Collections;

public class InterventionManager : MonoBehaviour
{
    private SHGameManager _myGameManager;
    private SHVigilHandler _myVirgil;
    private Animator _myAnimator;
    private TextMesh _myText;
    private Vector3[] _interventionPositions = new Vector3[5];
    private string _interventionText;

    void Start()
    {
        _myGameManager = FindObjectOfType<SHGameManager>();
        _myVirgil = FindObjectOfType<SHVigilHandler>();
        _myAnimator = GetComponentInChildren<Animator>();
        _myText = this.GetComponentInChildren<TextMesh>();

        _interventionPositions[0] = new Vector3(26.78879f, -35.70682f, -15.32112f);
    }

    public void InterventionSetup(ButtonType currentType)
    {
        _interventionText = Resources.Load("SHText/InterventionText_" + _myGameManager.CurrentMicroScenario.ToString()).ToString();
        string interventionText = "";
        char buttonTypeChar = '&';
        bool startPointFound = false;

        for (int i = 0; i < _interventionText.Length; i++)
        {
            if (!startPointFound)
            {
                switch (currentType)
                {
                    case ButtonType.CheckIn:
                        if (_interventionText[i] == '!')
                        {
                            startPointFound = true;
                            buttonTypeChar = _interventionText[i];
                            _myAnimator.SetInteger("InterventionIndex", 0);
                        }
                        break;
                    case ButtonType.Empathy:
                        if (_interventionText[i] == '@')
                        {
                            startPointFound = true;
                            buttonTypeChar = _interventionText[i];
                            _myAnimator.SetInteger("InterventionIndex", 1);
                        }
                        break;
                    case ButtonType.Friends:
                        if (_interventionText[i] == '#')
                        {
                            startPointFound = true;
                            buttonTypeChar = _interventionText[i];
                            _myAnimator.SetInteger("InterventionIndex", 2);
                        }
                        break;
                    case ButtonType.IStatement:
                        if (_interventionText[i] == '$')
                        {
                            startPointFound = true;
                            buttonTypeChar = _interventionText[i];
                            _myAnimator.SetInteger("InterventionIndex", 3);
                        }
                        break;
                    case ButtonType.SilentStare:
                        if (_interventionText[i] == '%')
                        {
                            startPointFound = true;
                            buttonTypeChar = _interventionText[i];
                            _myAnimator.SetInteger("InterventionIndex", 4);
                        }
                        break;
                    default:
                        Debug.LogWarning("Hmm, seems like you got here with a 'yes' or 'no' button type. I'm not sure how that happened...");
                        break;
                }
            }
            else
            {
                if (_interventionText[i] != buttonTypeChar)
                    interventionText += _interventionText[i];
                else
                    break;
            }
        }

        StringFormatter(interventionText);

        switch (_myGameManager.CurrentMicroScenario)
        {
            case MicroScenarios.Hallway:
                this.transform.position = _interventionPositions[0];
                break;
            case MicroScenarios.Classroom:
                this.transform.position = _interventionPositions[1];
                break;
            case MicroScenarios.Bathroom:
                this.transform.position = _interventionPositions[2];
                break;
            case MicroScenarios.Gym:
                this.transform.position = _interventionPositions[3];
                break;
            case MicroScenarios.Library:
                this.transform.position = _interventionPositions[4];
                break;
            default:
                Debug.Log("This shouldn't even be possible. If I were you, I'd check the \"MicroScenarios\" enum and see if there's something funky going on.");
                break;
        }
        Invoke("RemoveIntervention", 3);
    }

    //This method works the same way that the one in the Party Scenario does
    //It keeps string lines within the bounds of the dialog box
    private void StringFormatter(string lineContent)
    {
        string currentWord = "";
        bool isFirstWord = true;
        Renderer currentRenderer;

        currentRenderer = _myText.GetComponent<Renderer>();
        _myText.text = currentWord;

        for (int i = 0; i < lineContent.Length; i++)
        {
            if (lineContent[i] != ' ')
            {
                currentWord = currentWord + lineContent[i];
            }
            else
            {
                if (isFirstWord)
                {
                    _myText.text = _myText.text + currentWord;

                    isFirstWord = false;
                }
                else
                {
                    _myText.text = _myText.text + " " + currentWord;
                }

                if (currentRenderer.bounds.extents.x > 7.5f)
                {
                    _myText.text = _myText.text.Remove(_myText.text.Length - (currentWord.Length + 1));
                    _myText.text = _myText.text + "\n" + currentWord;
                }
                currentWord = "";
            }
        }
    }

    private void RemoveIntervention()
    {
        this.transform.position = new Vector3(5000, -5000, -5000);
        _myVirgil.ShowStringSegment();
    }
}