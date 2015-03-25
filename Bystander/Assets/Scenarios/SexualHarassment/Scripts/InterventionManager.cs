using UnityEngine;
using System.Collections;

public class InterventionManager : MonoBehaviour
{
    private SHGameManager _myGameManager;
    private TextMesh _myText;
    //private Vector3[] _interventionPositions = new Vector3[3];
    private string _interventionText;

    void Start()
    {
        _myGameManager = FindObjectOfType<SHGameManager>();
        _myText = this.GetComponentInChildren<TextMesh>();

        //_interventionPositions[0] = new Vector3 ()
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
                        }
                        break;
                    case ButtonType.Empathy:
                        if (_interventionText[i] == '@')
                        {
                            startPointFound = true;
                            buttonTypeChar = _interventionText[i];
                        }
                        break;
                    case ButtonType.Friends:
                        if (_interventionText[i] == '#')
                        {
                            startPointFound = true;
                            buttonTypeChar = _interventionText[i];
                        }
                        break;
                    case ButtonType.IStatement:
                        if (_interventionText[i] == '$')
                        {
                            startPointFound = true;
                            buttonTypeChar = _interventionText[i];
                        }
                        break;
                    case ButtonType.SilentStare:
                        if (_interventionText[i] == '%')
                        {
                            startPointFound = true;
                            buttonTypeChar = _interventionText[i];
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
        //I don't want this to be so hard coded in the future but it's fine for now
        this.transform.position = new Vector3(26.78879f, -35.70682f, -15.32112f);
        Invoke("RemoveIntervention", 3);
        //Also needs to display the test somewhere
        //Beyond even that is the load the correct animation part
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
    }
}