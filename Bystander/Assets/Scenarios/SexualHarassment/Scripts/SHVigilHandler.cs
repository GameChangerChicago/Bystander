using UnityEngine;
using System.Collections;

public class SHVigilHandler : MonoBehaviour
{
    public Transform[] MicroScenarioTransforms = new Transform[5];
    public bool IsCorrect,
                GameWinner;

    protected bool playWrongAnswer
    {
        get
        {
            if (_wrongCount < 3)
            {
                _wrongCount++;
                _playWrongAnswer = false;
            }
            else
            {
                _wrongCount = 0;
                _playWrongAnswer = true;
            }
            return _playWrongAnswer;
        }
        set
        {
            _playWrongAnswer = value;
        }
    }
    private bool _playWrongAnswer = false;

    protected bool playIntervention
    {
        get
        {
            if (_interveneCount < 3)
            {
                _interveneCount++;
                playIntervention = false;
            }
            else
            {
                _interveneCount = 0;
                _playIntervention = true;
            }
            return _playIntervention;
        }
        set
        {
            _playIntervention = value;
        }
    }
    private bool _playIntervention = false;

    private SHGameManager _myGameManager;
    private AudioSource _myAudioSource;
    private AudioClip _wrongAnswerClip,
                      _interventionClip;
    private BoxCollider _myCollider;
    private TextMesh _myText;
    private string _correctString,
                   _wrongString;
    private int _stringIndex,
                _endPoint,
                _splitPoint,
                _wrongCount = 3,
                _interveneCount = 3;
    private bool _isVisible = false;

    void Start()
    {
        //Standard initialization junk
        _myGameManager = FindObjectOfType<SHGameManager>();
        _myText = this.GetComponentInChildren<TextMesh>();
        _myCollider = this.GetComponent<BoxCollider>();
        _myAudioSource = GetComponentInChildren<AudioSource>();
        _wrongAnswerClip = Resources.Load("Sounds/04-Reminder") as AudioClip;
        _interventionClip = Resources.Load("Sounds/05-Interventions") as AudioClip;
        _correctString = "That's right! But let's try to find an instance of sexual harassment. ";
        _wrongString = "It seems like you're having some trouble. Remember, we're trying to find an instance of sexual harassment. ";
    }

    //Clicking anywhere will call ShowSpringSegment
    void OnMouseUp()
    {
        FinishDialog();
    }

    public void PlayAudio(bool playVirgilWrong)
    {
        if (playVirgilWrong && playWrongAnswer)
        {
            _wrongCount = 0;
            _myAudioSource.clip = _wrongAnswerClip;
            _myAudioSource.Play();
        }
        else if (!playVirgilWrong && playIntervention)
        {
            _interveneCount = 0;
            _myAudioSource.clip = _interventionClip;
            _myAudioSource.Play();
        }
    }

    public void ShowDialog(bool correct)
    {
        if (correct)
            StringFormatter(_correctString);
        else
            StringFormatter(_wrongString);

        switch (_myGameManager.CurrentMicroScenario)
        {
            case MicroScenarios.Hallway:
                this.transform.position = MicroScenarioTransforms[0].position;
                break;
            case MicroScenarios.Classroom:
                this.transform.position = MicroScenarioTransforms[1].position;
                break;
            case MicroScenarios.Bathroom:
                this.transform.position = MicroScenarioTransforms[4].position;
                break;
            case MicroScenarios.Gym:
                this.transform.position = MicroScenarioTransforms[3].position;
                break;
            case MicroScenarios.Library:
                this.transform.position = MicroScenarioTransforms[2].position;
                break;
            default:
                Debug.Log("There is no such Micro Scenario. Something very strange is happening...");
                break;
        }
    }

    private void FinishDialog()
    {
        this.transform.position = new Vector3(500, 500, 500);
        StartCoroutine(_myGameManager.ReenablePOIs());
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

                if (currentRenderer.bounds.extents.x > 11f)
                {
                    _myText.text = _myText.text.Remove(_myText.text.Length - (currentWord.Length + 1));
                    _myText.text = _myText.text + "\n" + currentWord;
                }
                currentWord = "";
            }
        }
    }
}