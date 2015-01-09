using UnityEngine;
using System.Collections;

public class QuizButton : MonoBehaviour
{
    private SHGameManager _myGameManager;
    private SHVigilHandler _myVirgil;
    private GameObject _myQuiz;

    //These are the most important bools for the quiz buttons
    public bool CorrectAnswer,
                GameWinner;

    void Start()
    {
        //Standard initialization junk
        _myGameManager = FindObjectOfType<SHGameManager>();
        _myVirgil = FindObjectOfType<SHVigilHandler>();
        _myQuiz = this.transform.parent.gameObject;
    }

    //This method handles what happens when the player clicks one of the quiz buttons
    void OnMouseDown()
    {
        if (CorrectAnswer)
        {
            //If the player correctly found the instance of sexual harrasment
            if (GameWinner)
                _myVirgil.GameWinner = true;

            //Tells the virgil handler that the player was correct then calls ShowStringSegment
            _myVirgil.IsCorrect = true;
            _myVirgil.ShowStringSegment();
        }
        else
        {
            //Tells the virgil handler that the player was wrong then  calls ShowStringSegment
            _myVirgil.IsCorrect = false;
            _myVirgil.ShowStringSegment();
        }

        //Moves the quiz out of sight and reach
        _myQuiz.transform.position = new Vector3(0, 0, 500);
    }
}
