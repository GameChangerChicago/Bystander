using UnityEngine;
using System.Collections;

public class QuizButton : MonoBehaviour
{
    private CursorHandler _cursorHanlder;
    private SHGameManager _myGameManager;
    private InterventionManager _myInterventionManager;
    private SHVigilHandler _myVirgil;
    private QuizHandler _otherQuiz;
	private AudioManager _audioManager;
    private GameObject _myQuiz,
					   _currentMiniComic;
	public AudioClip HoverSound,
					 ClickSound;
    private ButtonType _myButtonType;

    protected GameObject currentMiniComic;
    protected bool mousedOver
    {
        get
        {
            return _mousedOver;
        }
        set
        {
            if (_mousedOver != value)
            {
                if (value)
				{
					_audioManager.PlaySFX(HoverSound, 0.05f, true);
                    _cursorHanlder.ChangeCursor(0);
				}
                else
				{
					_audioManager.StopSFX();
                    _cursorHanlder.ChangeCursor(1);
				}

                _mousedOver = value;
            }
        }
    }
    private bool _mousedOver;

    //These are the most important bools for the quiz buttons
    public bool CorrectAnswer,
                GameWinner;

    void Start()
    {
        //Standard initialization junk
        _myGameManager = FindObjectOfType<SHGameManager>();
		_audioManager = FindObjectOfType<AudioManager>();
        _myInterventionManager = FindObjectOfType<InterventionManager>();
        _myVirgil = FindObjectOfType<SHVigilHandler>();
        _cursorHanlder = FindObjectOfType<CursorHandler>();
        _myQuiz = this.transform.parent.gameObject;

        //Looks for the QuizHandler that isn't this button's parent and sets _otherQuiz to what ever that is
        QuizHandler[] quizes = FindObjectsOfType<QuizHandler>();
        for (int i = 0; i < quizes.Length; i++)
        {
            if (_myQuiz != quizes[i].gameObject)
                _otherQuiz = quizes[i];
        }

        //Sets button type
        GetButtonType();
    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(_myGameManager.CurrentCameraManager.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition), Vector3.forward, out hit))
        {
            if (hit.transform.gameObject == this.gameObject)
                mousedOver = true;
        }
        else
        {
            mousedOver = false;
        }
    }

    //This method handles what happens when the player clicks one of the quiz buttons
    void OnMouseUp()
    {
        if (_myButtonType == ButtonType.Yes || _myButtonType == ButtonType.No)
        {
			_audioManager.PlaySFX(ClickSound, 0.7f, false);
            if (CorrectAnswer)
            {
                //Tells the virgil handler that the player was correct
                _myVirgil.IsCorrect = true;

                //If the player correctly found the instance of sexual harrasment...
                if (GameWinner)
                {
                    //we let _myVirgil know that it's a game winner and then we show the "Which type" quiz
                    _myVirgil.GameWinner = true;
                    _otherQuiz.ShowQuiz(_myQuiz.transform.position, true, false);

                    //When calling this method with 'false' it plays the intervention audio
                    _myVirgil.PlayAudio(false);
                }
                else //otherwise we start the virgil dialog by calling ShowStringSegment
                {
                    //If we call ShowDialog with 'true' it will bring up the virgil dialog box with the 'correct' dialog
                    _myVirgil.ShowDialog(true);
                }
            }
            else
            {
                //Tells the virgil handler that the player was wrong then calls ShowStringSegment
                _myVirgil.IsCorrect = false;
                //_myVirgil.ShowStringSegment();
                _myGameManager.CurrentPOI.ComicShown = false;
                if (_myGameManager.CurrentPOI.CompanionPOI != null)
                    _myGameManager.CurrentPOI.CompanionPOI.ComicShown = false;
                _myVirgil.ShowDialog(false);

                //When calling this method with 'true' it plays the wrong answer audio
                _myVirgil.PlayAudio(true);
            }
        }
        else
        {
            //Tells the Intervention Manager to show the type of intervention. InterventionSetup() handles all that.
            _myInterventionManager.InterventionSetup(_myButtonType);
            //_myVirgil.DialogString = Resources.Load("SHText/VirgilDialog_" + _myGameManager.CurrentPOI.name + "_" + this.name).ToString();

            //This grays out the answer and disables it's box collider so you can't select it
            this.GetComponent<TextMesh>().color = Color.gray;
            this.GetComponent<BoxCollider>().enabled = false;
        }

        //Moves the quiz out of sight and reach
        _myQuiz.transform.position = new Vector3(0, 0, 500);
    }

    //This method looks at the name of the QuizButton and assigns it a ButtonType based on that
    //ButtonTypes 1-5 will be renamed to whatever they end up being but I don't know what that will be yet so:
    //Que sara sera; what ever will be will be;
    private void GetButtonType()
    {
        switch (this.name)
        {
            case "Answer Yes":
                _myButtonType = ButtonType.Yes;
                break;
            case "Answer No":
                _myButtonType = ButtonType.No;
                break;
            case "Check In":
                _myButtonType = ButtonType.CheckIn;
                break;
            case "Empathy":
                _myButtonType = ButtonType.Empathy;
                break;
            case "Silent Stare":
                _myButtonType = ButtonType.SilentStare;
                break;
            case "I Statement":
                _myButtonType = ButtonType.IStatement;
                break;
            case "Friends":
                _myButtonType = ButtonType.Friends;
                break;
            default:
                Debug.Log("This quiz button shouldn't be named what it is. Check that and get back to me.");
                break;
        }
    }
}
