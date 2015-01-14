using UnityEngine;
using System.Collections;

public class QuizButton : MonoBehaviour
{
    private SHGameManager _myGameManager;
    private SHVigilHandler _myVirgil;
    private QuizHandler _otherQuiz;
    private GameObject _myQuiz;
    private ButtonType _myButtonType;

    enum ButtonType
    {
        Yes,
        No,
        One,
        Two,
        Three,
        Four,
        Five
    }

    //These are the most important bools for the quiz buttons
    public bool CorrectAnswer,
                GameWinner;

    void Start()
    {
        //Standard initialization junk
        _myGameManager = FindObjectOfType<SHGameManager>();
        _myVirgil = FindObjectOfType<SHVigilHandler>();
        _myQuiz = this.transform.parent.gameObject;
        QuizHandler[] quizes = FindObjectsOfType<QuizHandler>();

        for (int i = 0; i < quizes.Length; i++)
        {
            if (_myQuiz != quizes[i].gameObject)
                _otherQuiz = quizes[i];
        }

        GetButtonType();
    }

    //This method handles what happens when the player clicks one of the quiz buttons
    void OnMouseDown()
    {
        if (_myButtonType == ButtonType.Yes || _myButtonType == ButtonType.No)
        {
            if (CorrectAnswer)
            {
                //Tells the virgil handler that the player was correct then calls ShowStringSegment
                _myVirgil.IsCorrect = true;

                //If the player correctly found the instance of sexual harrasment
                if (GameWinner)
                {
                    _myVirgil.GameWinner = true;
                    StartCoroutine(_otherQuiz.ShowQuiz(0, _myQuiz.transform.position, null, true, false));
                }
                else
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
            //GameObject myMiniComic = Resources.Load("Prefabs/MiniComic_" + _myGameManager.CurrentPOI.name + "_" + this.name) as GameObject;
            Debug.Log("Load: " + "Prefabs/MiniComic_" + _myGameManager.CurrentPOI.name + "_" + this.name);
        }

        //Moves the quiz out of sight and reach
        _myQuiz.transform.position = new Vector3(0, 0, 500);
    }

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
