using UnityEngine;
using System.Collections;

public class PartyGameManager : MonoBehaviour
{
    public GameObject DialogBox1,
                      DialogBox2;
    public int MaxClicks;

    private PartyCameraManager _myCameraManager;
    private Virgil _virgil;
    private int _clickCount = 0,
                _stringIndex = 0,
                _stringsShown = 0;
    private bool _sectionCompleted = false,
                 _usingBox1 = true;

    void Start()
    {
        _myCameraManager = FindObjectOfType<PartyCameraManager>();
        _virgil = FindObjectOfType<Virgil>();
    }

    public IEnumerator PlayerClicked(bool importantProp, float animationLength)
    {
        _clickCount++;
        if (importantProp)
            _sectionCompleted = true;

        yield return new WaitForSeconds(animationLength);
        VirgilHandler();
    }

    public void DialogHandler(bool importantProp, string dialog, int dialogCount)
    {
        string currentString = "";

        if (importantProp)
            _sectionCompleted = true;

        if (dialog.Length == _stringIndex)
            _stringsShown++;

        for (int i = _stringIndex; i < dialog.Length; i++)
        {
            if (dialog[i] != '|')
            {
                currentString = currentString + dialog[i];
            }
            else
            {
                _stringIndex = i + 1;
                _stringsShown++;

                if (_usingBox1)
                {
                    StringFormatter(currentString);
                    DialogBox1.GetComponent<Renderer>().enabled = true;
                    DialogBox1.GetComponentInChildren<SpriteRenderer>().enabled = true;
                }
                else
                {
                    StringFormatter(currentString);
                    DialogBox2.GetComponent<Renderer>().enabled = true;
                    DialogBox2.GetComponentInChildren<SpriteRenderer>().enabled = true;
                }

                _usingBox1 = !_usingBox1;

                break;
            }
        }

        if (dialogCount < _stringsShown)
        {
            _clickCount++;
            DialogBox1.GetComponent<Renderer>().enabled = false;
            DialogBox1.GetComponentInChildren<SpriteRenderer>().enabled = false;
            DialogBox2.GetComponent<Renderer>().enabled = false;
            DialogBox2.GetComponentInChildren<SpriteRenderer>().enabled = false;
            VirgilHandler();
            _stringIndex = 0;
        }
    }

    private void StringFormatter(string lineContent)
    {
        string currentWord = "";
        bool isFirstWord = true;
        Renderer currentRenderer;

        if (_usingBox1)
        {
            currentRenderer = DialogBox1.GetComponent<Renderer>();
            DialogBox1.GetComponent<TextMesh>().text = currentWord;
        }
        else
        {
            currentRenderer = DialogBox2.GetComponent<Renderer>();
            DialogBox2.GetComponent<TextMesh>().text = currentWord;
        }

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
                    if (_usingBox1)
                        DialogBox1.GetComponent<TextMesh>().text = DialogBox1.GetComponent<TextMesh>().text + currentWord;
                    else
                        DialogBox2.GetComponent<TextMesh>().text = DialogBox2.GetComponent<TextMesh>().text + currentWord;

                    isFirstWord = false;
                }
                else
                {
                    if(_usingBox1)
                        DialogBox1.GetComponent<TextMesh>().text = DialogBox1.GetComponent<TextMesh>().text + " " + currentWord;
                    else
                        DialogBox2.GetComponent<TextMesh>().text = DialogBox2.GetComponent<TextMesh>().text + " " + currentWord;
                }

                if (currentRenderer.bounds.extents.x > 5f)
                {
                    if (_usingBox1)
                    {
                        DialogBox1.GetComponent<TextMesh>().text = DialogBox1.GetComponent<TextMesh>().text.Remove(DialogBox1.GetComponent<TextMesh>().text.Length - (currentWord.Length + 1));
                        DialogBox1.GetComponent<TextMesh>().text = DialogBox1.GetComponent<TextMesh>().text + "\n" + currentWord;
                    }
                    else
                    {
                        DialogBox2.GetComponent<TextMesh>().text = DialogBox2.GetComponent<TextMesh>().text.Remove(DialogBox2.GetComponent<TextMesh>().text.Length - (currentWord.Length + 1));
                        DialogBox2.GetComponent<TextMesh>().text = DialogBox2.GetComponent<TextMesh>().text + "\n" + currentWord;
                    }
                }
                currentWord = "";
            }
        }
    }

    private void VirgilHandler()
    {
        if (_clickCount >= MaxClicks)
        {
            if (_sectionCompleted)
                _virgil.Appear(true);
            else
                _virgil.Appear(false);
        }
    }
}