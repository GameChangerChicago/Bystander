using UnityEngine;
using System.Collections;

public class ConvoHandler : MonoBehaviour
{
    public bool IsVirgil;
    public int DialogSections;
    public float TextBounds;
    public GameObject DialogBox;
    public Transform[] DialogBoxLocations;

    private int _stringsShown,
                _stringIndex;
    private string _dialog;
    private PartyGameManager _myGameManager;
    private PartyCameraManager _myCameraManager;

    void Start()
    {
        _myGameManager = FindObjectOfType<PartyGameManager>();
        _myCameraManager = FindObjectOfType<PartyCameraManager>();
        TextAsset rawText = Resources.Load("PartyDialogText/TestDialog") as TextAsset;
        _dialog = rawText.text;
    }

    void OnMouseDown()
    {
        if (_stringsShown < DialogSections && !IsVirgil)
        {
            _myCameraManager.SetCameraToMove(DialogBoxLocations[_stringsShown].parent.position, 0.3f, _myCameraManager.camera.orthographicSize);
            StartCoroutine(DialogHandler(0.3f));
        }
        else if (_stringsShown < DialogSections && IsVirgil)
            StartCoroutine(DialogHandler(0));
        else
            _myGameManager.FinishDialog();
    }

    public IEnumerator DialogHandler(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        string currentString = "";

        if (_dialog.Length == _stringIndex)
            _stringsShown++;

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
                    DialogBox.transform.position = DialogBoxLocations[_stringsShown - 1].position;
                    DialogBox.transform.localScale = DialogBoxLocations[_stringsShown - 1].lossyScale;
                }
                else
                {
                    DialogBox.transform.position = new Vector3(0, 0, 0);
                    DialogBox.transform.localScale = new Vector3(1, 1, 1);
                }
                DialogBox.GetComponent<Renderer>().enabled = true;
                DialogBox.GetComponentInChildren<SpriteRenderer>().enabled = true;

                break;
            }
        }

        if (DialogSections < _stringsShown)
        {
            DialogBox.GetComponent<Renderer>().enabled = false;
            DialogBox.GetComponentInChildren<SpriteRenderer>().enabled = false;
            _myGameManager.FinishDialog();
            _stringIndex = 0;
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
