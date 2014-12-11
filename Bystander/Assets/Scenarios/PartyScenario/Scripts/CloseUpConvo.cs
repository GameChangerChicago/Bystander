using UnityEngine;
using System.Collections;

public class CloseUpConvo : MonoBehaviour
{
    public int DialogSections;
    public GameObject DialogBox;
    public Transform[] DialogBoxLocations;

    private int _stringsShown,
                _stringIndex;
    private string _dialog;
    private PartyGameManager _myGameManager;

    void Start()
    {
        _myGameManager = FindObjectOfType<PartyGameManager>();
        TextAsset rawText = Resources.Load("PartyDialogText/TestDialog") as TextAsset;
        _dialog = rawText.text;
    }

    void OnMouseDown()
    {
        DialogHandler(0);
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

                if (currentRenderer.bounds.extents.x > 5f)
                {
                    DialogBox.GetComponent<TextMesh>().text = DialogBox.GetComponent<TextMesh>().text.Remove(DialogBox.GetComponent<TextMesh>().text.Length - (currentWord.Length + 1));
                    DialogBox.GetComponent<TextMesh>().text = DialogBox.GetComponent<TextMesh>().text + "\n" + currentWord;
                }
                currentWord = "";
            }
        }
    }
}
