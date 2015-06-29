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
            //_myCameraManager.SetCameraToMove(DialogBoxLocations[_stringsShown].parent.position, 0.3f, _myCameraManager.camera.orthographicSize);
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
            _myGameManager.FinsihInteractiveSegment(0, false);
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

        //Each virgil dialog has a portion for when you're successful and when you're not
        if (IsVirgil && _stringIndex == 0 && !_myGameManager.SectionCompleted)
        {
            //If the player is unsuccessful then _stringIndex is set to where the unsuccessful dialog starts and sets the number of sections the unsuccessful portion has
            _stringIndex = _unsuccessfulDialogIndex;
            DialogSections = UnsuccessfulDialogSections;
        }
        else if (IsVirgil && _stringIndex == 0 && _myGameManager.SectionCompleted) //Otherwise the DialogSections is set to the number of sections the successful portion has
            DialogSections = SuccessfulDialogSections;

        //This for loop handles the tricky part of breaking the string from the .txt file into it's individual dialog sections
        for (int i = _stringIndex; i < _dialog.Length; i++)
        {
            //I seperate the various dialog sections with '|'; As long as the character isn't a '|' it will be added to currentString
            if (_dialog[i] != '|')
            {
                currentString = currentString + _dialog[i];
            }
            else
            {
                //If the character is a '|' than the _stringIndex is set to the point just after the '|'; it also adds one to _stringsShown
                _stringIndex = i + 1;
                _stringsShown++;

                //By this point currentString will be filled out so it gets put into StringFormatter
                StringFormatter(currentString);

                //In non virgil convos each dialog section has a seperate location and size; this sets the location and size based on the transforms stored in DialogBoxLocations
                if (!IsVirgil)
                {
                    DialogBox.transform.position = DialogBoxLocations[_stringsShown - 1].position;
                    DialogBox.transform.localScale = DialogBoxLocations[_stringsShown - 1].lossyScale;
                }
                else //Virgil dialogs are always in the same spot so there is only ever one transform located in DialogBoxLocations
                {
                    DialogBox.transform.position = DialogBoxLocations[0].position;
                    DialogBox.transform.localScale = DialogBoxLocations[0].lossyScale;
                }

                //These set the dialog box sprite to face left or right based on the bool FaceLeft
                if (FaceLeft && DialogBox.GetComponentInChildren<SpriteRenderer>().transform.localScale.x < 0)
                    DialogBox.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector3(DialogBox.GetComponentInChildren<SpriteRenderer>().transform.localScale.x * -1, DialogBox.GetComponentInChildren<SpriteRenderer>().transform.localScale.y, DialogBox.GetComponentInChildren<SpriteRenderer>().transform.localScale.z);
                else if (!FaceLeft && DialogBox.GetComponentInChildren<SpriteRenderer>().transform.localScale.x > 0)
                    DialogBox.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector3(DialogBox.GetComponentInChildren<SpriteRenderer>().transform.localScale.x * -1, DialogBox.GetComponentInChildren<SpriteRenderer>().transform.localScale.y, DialogBox.GetComponentInChildren<SpriteRenderer>().transform.localScale.z);

                //This turns the DialogBox renderers on
                DialogBox.GetComponent<Renderer>().enabled = true;
                DialogBox.GetComponentInChildren<SpriteRenderer>().enabled = true;

                //This ends the for loop here and since _stirngIndex has been set to start after the '|' the next time we run through the for loop it will start there
                break;
            }
        }
    }

    //This method takes current string and makes sure it doesn't run outhside the bounds of the dialog box
    private void StringFormatter(string lineContent)
    {
        string currentWord = "";
        bool isFirstWord = true;
        Renderer currentRenderer;

        currentRenderer = DialogBox.GetComponent<Renderer>();
        DialogBox.GetComponent<TextMesh>().text = currentWord;

        for (int i = 0; i < lineContent.Length; i++)
        {
            //As long as the char isn't a ' ' then it will be added to currentWord
            if (lineContent[i] != ' ')
            {
                currentWord = currentWord + lineContent[i];
            }
            else
            {
                //If currentWord is the first word it is added to the to the dialog box
                if (isFirstWord)
                {
                    DialogBox.GetComponent<TextMesh>().text = DialogBox.GetComponent<TextMesh>().text + currentWord;

                    isFirstWord = false;
                }
                else // Otherwise it adds the current word with a space before the word.
                {
                    DialogBox.GetComponent<TextMesh>().text = DialogBox.GetComponent<TextMesh>().text + " " + currentWord;
                }

                //If after adding the word the line extends past the TextBounds then the word will be added with a line break
                if (currentRenderer.bounds.extents.x > TextBounds)
                {
                    DialogBox.GetComponent<TextMesh>().text = DialogBox.GetComponent<TextMesh>().text.Remove(DialogBox.GetComponent<TextMesh>().text.Length - (currentWord.Length + 1));
                    DialogBox.GetComponent<TextMesh>().text = DialogBox.GetComponent<TextMesh>().text + "\n" + currentWord;
                }

                //Resets the current word each time
                currentWord = "";
            }
        }
    }
}
