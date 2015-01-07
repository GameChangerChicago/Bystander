using UnityEngine;
using System.Collections;

public class SHVigilHandler : MonoBehaviour
{
    protected bool isVisible
    {
        get
        {
            if (!_isVisible)
            {
                //Turns on all renderers for Virgil
                this.GetComponent<Renderer>().enabled = true;
                foreach (Renderer r in this.GetComponentsInChildren<Renderer>())
                {
                    r.enabled = true;
                }

                //Finds where the "wrong choice" dialog begins
                for (int i = 0; i < DialogString.Length; i++)
                {
                    if (DialogString[i] == '-')
                        _splitPoint = i;
                }

                //Sets where to start reading the string from
                if (IsCorrect)
                {
                    _stringIndex = 1;
                    _endPoint = _splitPoint;
                }
                else
                {
                    _stringIndex = _splitPoint + 1;
                    _endPoint = DialogString.Length;
                }

                //Turns on the collider for mouse clicks
                _myCollider.enabled = true;

                //Sets visible to true so that the preceding code is only run once
                _isVisible = true;
            }

            return _isVisible;
        }

        set
        {
            if (!value)
            {
                //Turns all renderers for Virgil off
                this.GetComponent<Renderer>().enabled = false;
                foreach (Renderer r in this.GetComponentsInChildren<Renderer>())
                {
                    r.enabled = false;
                }

                //Turns collider off
                _myCollider.enabled = false;
            }

            _isVisible = value;
        }
    }
    private bool _isVisible = false;

    private SHGameManager _myGameManager;
    private BoxCollider _myCollider;
    private TextMesh _myText;
    private int _stringIndex,
                _endPoint,
                _splitPoint;

    public string DialogString;
    public bool IsCorrect,
                GameWinner;

    void Start()
    {
        _myGameManager = FindObjectOfType<SHGameManager>();
        _myText = this.GetComponentInChildren<TextMesh>();
        _myCollider = this.GetComponent<BoxCollider>();
    }

    void OnMouseDown()
    {
        ShowStringSegment();
    }

    public void ShowStringSegment()
    {
        if (isVisible)
        {
            if (_stringIndex < _endPoint)
                DialogHandler();
            else
            {
                if (IsCorrect && GameWinner)
                {
                    isVisible = false;
                    _myGameManager.SectionComplete = true;
                }
                else
                {
                    //This is where I need add code that resets the sexual harassment moment/memory
                    isVisible = false;
                    ResetPOI();
                }
            }
        }
    }

    public void DialogHandler()
    {
        string currentString = "";

        for (int i = _stringIndex; i < _endPoint; i++)
        {
            if (DialogString[i] != '|')
            {
                currentString = currentString + DialogString[i];
            }
            else
            {
                _stringIndex = i + 1;

                break;
            }
        }

        StringFormatter(currentString);
    }

    private void StringFormatter(string lineContent)
    {
        string currentWord = "";
        bool isFirstWord = true;
        Renderer currentRenderer;

        currentRenderer = _myText.GetComponent<Renderer>();
        _myText.GetComponent<TextMesh>().text = currentWord;

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

                if (currentRenderer.bounds.extents.x > 3.5f)
                {
                    _myText.text = _myText.text.Remove(_myText.text.Length - (currentWord.Length + 1));
                    _myText.text = _myText.text + "\n" + currentWord;
                }
                currentWord = "";
            }
        }
    }

    private void ResetPOI()
    {
        PointOfInterest[] POIs = FindObjectsOfType<PointOfInterest>();

        for (int i = 0; i < POIs.Length; i++)
        {
            POIs[i].ComicShown = false;
        }
    }
}