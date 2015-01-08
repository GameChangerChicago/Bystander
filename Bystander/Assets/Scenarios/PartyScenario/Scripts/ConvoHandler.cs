using UnityEngine;
using System.Collections;

public class ConvoHandler : MonoBehaviour
{
    public bool IsVirgil,
                FaceLeft;
    public int DialogSections,
               SuccessfulDialogSections,
               UnsuccessfulDialogSections;
    public float TextBounds;
    public GameObject DialogBox;
    public Transform[] DialogBoxLocations;

    private bool _cameraMoving;
    private int _stringsShown,
                _stringIndex,
                _unsuccessfulDialogIndex;
    private string _dialog;
    private PartyGameManager _myGameManager;
    private PartyCameraManager _myCameraManager;

    void Start()
    {
        _myGameManager = FindObjectOfType<PartyGameManager>();
        _myCameraManager = FindObjectOfType<PartyCameraManager>();

        //These lines are used to get the correct name for the .txt file
        string txtName = "";
        if (IsVirgil) //If the object is virgil than txtName will be set to Virgil
            txtName = this.name;
        else //If the object isn't virgil then txtName will be set to the the game object's name starting at index 13 in the name. This is because we want to skip "CloseUpPanel_"
        {
            for (int i = 13; i < this.name.Length; i++)
            {
                txtName += this.name[i];
            }
        }

        //These lines are used to get the correct lvl for the .txt file
        string sceneName = "";
        for (int i = 0; i < this.transform.parent.name.Length; i++)
        {
            //The scene name might have "(Clone)" at the end of it so the script looks for a '(' which is when it stops taking down the name
            if (this.transform.parent.name[i] == '(')
                break;
            else
                sceneName += this.transform.parent.name[i];
        }

        //Uses txtName and sceneName to load the correct .txt then pulls the string from that .txt
        TextAsset rawText = Resources.Load("PartyDialogText/" + txtName + "_" + sceneName) as TextAsset;
        _dialog = rawText.text;

        //Virgil has multiple lines; one if you are correct and one if you're not; these lines find the point where the two split
        if (IsVirgil)
        {
            for (int i = 0; i < _dialog.Length; i++)
            {
                if (_dialog[i] == '\\')
                    _unsuccessfulDialogIndex = i + 1;
            }
        }
    }

    //This is where most of the controlling happens. The meat and potatoes if you will.
    void OnMouseDown()
    {
        //These if and else ifs will call DialogHandler in various ways or end the convo or lvl
        if (_stringsShown < DialogSections && !IsVirgil && !_cameraMoving) //For typical convos that still have unshown dialog sections and the camera isn't moving
        {
            _myCameraManager.SetCameraToMove(DialogBoxLocations[_stringsShown].parent.position, 0.3f, _myCameraManager.camera.orthographicSize);
            StartCoroutine(DialogHandler(0.3f));
            _cameraMoving = true;
        }
        else if (_stringsShown < DialogSections && IsVirgil) //For virgil convos that still have unshown dialog sections
            StartCoroutine(DialogHandler(0));
        else if (IsVirgil) //For virgil convos that have shown all dialog sections
        {
            DialogBox.GetComponent<Renderer>().enabled = false;
            DialogBox.GetComponentInChildren<SpriteRenderer>().enabled = false;
            _stringIndex = 0;
            _stringsShown = 0;
            _myGameManager.FinsihInteractiveSegment();
        }
        else if (!_cameraMoving) //For typical convos that have shown all dialog sections
        {
            DialogBox.GetComponent<Renderer>().enabled = false;
            DialogBox.GetComponentInChildren<SpriteRenderer>().enabled = false;
            _stringIndex = 0;
            _stringsShown = 0;
            _myGameManager.FinishDialog();
        }
    }

    //This method's primary function is to chop up the various dialog sections from the .txt
    public IEnumerator DialogHandler(float waitTime) //It's an IEnumerator because sometimes we want to wait for the camera to finish moving
    {
        yield return new WaitForSeconds(waitTime);

        _cameraMoving = false;

        string currentString = "";

        //This keeps track of the number of strings shown
        if (_dialog.Length == _stringIndex)
            _stringsShown++;


        if (IsVirgil && _stringIndex == 0 && !_myGameManager.SectionCompleted)
        {
            _stringIndex = _unsuccessfulDialogIndex;
            DialogSections = UnsuccessfulDialogSections;
        }
        else if (IsVirgil && _stringIndex == 0 && _myGameManager.SectionCompleted)
            DialogSections = SuccessfulDialogSections;

        //This for loop handles the tricky part of breaking the string from the .txt file into it's individual dialog sections
        for (int i = _stringIndex; i < _dialog.Length; i++)
        {
            if (_dialog[i] != '|')
            {
                currentString = currentString + _dialog[i];
            }
            else
            {
                _stringIndex = i + 1;
                _stringsShown++;

                StringFormatter(currentString);
                if (!IsVirgil)
                {
                    Debug.Log(_stringsShown);
                    DialogBox.transform.position = DialogBoxLocations[_stringsShown - 1].position;
                    DialogBox.transform.localScale = DialogBoxLocations[_stringsShown - 1].lossyScale;
                }
                else
                {
                    DialogBox.transform.position = DialogBoxLocations[0].position;
                    DialogBox.transform.localScale = DialogBoxLocations[0].lossyScale;
                }

                if (FaceLeft && DialogBox.GetComponentInChildren<SpriteRenderer>().transform.localScale.x < 0)
                    DialogBox.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector3(DialogBox.GetComponentInChildren<SpriteRenderer>().transform.localScale.x * -1, DialogBox.GetComponentInChildren<SpriteRenderer>().transform.localScale.y, DialogBox.GetComponentInChildren<SpriteRenderer>().transform.localScale.z);
                else if (!FaceLeft && DialogBox.GetComponentInChildren<SpriteRenderer>().transform.localScale.x > 0)
                    DialogBox.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector3(DialogBox.GetComponentInChildren<SpriteRenderer>().transform.localScale.x * -1, DialogBox.GetComponentInChildren<SpriteRenderer>().transform.localScale.y, DialogBox.GetComponentInChildren<SpriteRenderer>().transform.localScale.z);

                DialogBox.GetComponent<Renderer>().enabled = true;
                DialogBox.GetComponentInChildren<SpriteRenderer>().enabled = true;

                break;
            }
        }
    }

    private void StringFormatter(string lineContent)
    {
        string currentWord = "";
        bool isFirstWord = true;
        Renderer currentRenderer;

        currentRenderer = DialogBox.GetComponent<Renderer>();
        DialogBox.GetComponent<TextMesh>().text = currentWord;

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
                    DialogBox.GetComponent<TextMesh>().text = DialogBox.GetComponent<TextMesh>().text + currentWord;

                    isFirstWord = false;
                }
                else
                {
                    DialogBox.GetComponent<TextMesh>().text = DialogBox.GetComponent<TextMesh>().text + " " + currentWord;
                }

                if (currentRenderer.bounds.extents.x > TextBounds)
                {
                    DialogBox.GetComponent<TextMesh>().text = DialogBox.GetComponent<TextMesh>().text.Remove(DialogBox.GetComponent<TextMesh>().text.Length - (currentWord.Length + 1));
                    DialogBox.GetComponent<TextMesh>().text = DialogBox.GetComponent<TextMesh>().text + "\n" + currentWord;
                }
                currentWord = "";
            }
        }
    }
}
