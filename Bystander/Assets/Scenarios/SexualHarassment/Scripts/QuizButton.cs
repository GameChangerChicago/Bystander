using UnityEngine;
using System.Collections;

public class QuizButton : MonoBehaviour
{
    private SHGameManager _myGameManager;
    private SHVigilHandler _myVirgil;
    private QuizHandler _otherQuiz;
    private GameObject _myQuiz,
                       _currentMiniComic;
    private ButtonType _myButtonType;

    //These are the most important bools for the quiz buttons
    public bool CorrectAnswer,
                GameWinner;

    void Start()
    {
        //Standard initialization junk
        _myGameManager = FindObjectOfType<SHGameManager>();
        _myVirgil = FindObjectOfType<SHVigilHandler>();
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

    //This method handles what happens when the player clicks one of the quiz buttons
    void OnMouseDown()
    {
        if (_myButtonType == ButtonType.Yes || _myButtonType == ButtonType.No)
        {
            if (CorrectAnswer)
            {
                //Tells the virgil handler that the player was correct
                _myVirgil.IsCorrect = true;

                //If the player correctly found the instance of sexual harrasment...
                if (GameWinner)
                {
                    //we let _myVirgil know that it's a game winner and then we show the "Which type" quiz
                    _myVirgil.GameWinner = true;
                    StartCoroutine(_otherQuiz.ShowQuiz(0, _myQuiz.transform.position, null, true, false));
                }
                else //otherwise we start the virgil dialog by calling ShowStringSegment
                    _myVirgil.ShowStringSegment();
            }
            else
            {
                //Tells the virgil handler that the player was wrong then  calls ShowStringSegment
                _myVirgil.IsCorrect = false;
                _myVirgil.ShowStringSegment();
            }
        }
        else
        {
            //This wont work until i have more minicomics
            //GameObject _currentMiniComic = Resources.Load("Prefabs/MiniComic_" + _myGameManager.CurrentPOI.name + "_" + this.name) as GameObject;
            //_myVirgil.DialogString = Resources.Load("SHText/VirgilDialog_" + _myGameManager.CurrentPOI.name + "_" + this.name).ToString();
            Debug.Log("Load: " + "Prefabs/MiniComic_" + _myGameManager.CurrentPOI.name + "_" + this.name);
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
            case "Answer 1":
                _myButtonType = ButtonType.One;
                break;
            case "Answer 2":
                _myButtonType = ButtonType.Two;
                break;
            case "Answer 3":
                _myButtonType = ButtonType.Three;
                break;
            case "Answer 4":
                _myButtonType = ButtonType.Four;
                break;
            case "Answer 5":
                _myButtonType = ButtonType.Five;
                break;
            default:
                Debug.Log("This quiz button shouldn't be named what it is. Check that and get back to me.");
                break;
        }
    }
}
