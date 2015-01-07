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
        string txtName = "";
        if (IsVirgil)
            txtName = this.name;
        else
        {
            for (int i = 13; i < this.name.Length; i++)
            {
                txtName += this.name[i];
            }
        }
        string sceneName = "";

        for (int i = 0; i < this.transform.parent.name.Length; i++)
        {
            if (this.transform.parent.name[i] == '(')
                break;
            else
                sceneName += this.transform.parent.name[i];
        }

        TextAsset rawText = Resources.Load("PartyDialogText/" + txtName + "_" + sceneName) as TextAsset;
        _dialog = rawText.text;

        if (IsVirgil)
        {
            for (int i = 0; i < _dialog.Length; i++)
            {
                if (_dialog[i] == '\\')
                    _unsuccessfulDialogIndex = i + 1;
            }
        }
    }

    void OnMouseDown()
    {
        if (_stringsShown < DialogSections && !IsVirgil && !_cameraMoving)
        {
            _myCameraManager.SetCameraToMove(DialogBoxLocations[_stringsShown].parent.position, 0.3f, _myCameraManager.camera.orthographicSize);
            StartCoroutine(DialogHandler(0.3f));
            _cameraMoving = true;
        }
        else if (_stringsShown < DialogSections && IsVirgil)
            StartCoroutine(DialogHandler(0));
        else if (IsVirgil)
        {
            DialogBox.GetComponent<Renderer>().enabled = false;
            DialogBox.GetComponentInChildren<SpriteRenderer>().enabled = false;
            _stringIndex = 0;
            _stringsShown = 0;
            _myGameManager.FinsihInteractiveSegment();
        }
        else if (!_cameraMoving)
        {
            DialogBox.GetComponent<Renderer>().enabled = false;
            DialogBox.GetComponentInChildren<SpriteRenderer>().enabled = false;
            _stringIndex = 0;
            _stringsShown = 0;
            _myGameManager.FinishDialog();
        }
    }

    public IEnumerator DialogHandler(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        _cameraMoving = false;

        string currentString = "";

        if (_dialog.Length == _stringIndex)
            _stringsShown++;

        if (IsVirgil && _stringIndex == 0 && !_myGameManager.SectionCompleted)
        {
            _stringIndex = _unsuccessfulDialogIndex;
            DialogSections = UnsuccessfulDialogSections;
        }
        else if (IsVirgil && _stringIndex == 0 && _myGameManager.SectionCompleted)
            DialogSections = SuccessfulDialogSections;

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
