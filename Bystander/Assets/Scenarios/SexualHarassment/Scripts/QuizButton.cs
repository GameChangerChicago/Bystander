using UnityEngine;
using System.Collections;

public class QuizButton : MonoBehaviour
{
    private SHGameManager _myGameManager;
    private SHVigilHandler _myVirgil;
    private GameObject _myQuiz;

    public bool CorrectAnswer;

    void Start()
    {
        _myGameManager = FindObjectOfType<SHGameManager>();
        _myVirgil = FindObjectOfType<SHVigilHandler>();
        _myQuiz = this.transform.parent.gameObject;
    }

    void OnMouseDown()
    {
        if (CorrectAnswer)
        {
            _myVirgil.IsCorrect = true;
            _myVirgil.ShowStringSegment();
        }
        else
        {
            _myVirgil.IsCorrect = false;
            _myVirgil.ShowStringSegment();
        }

        _myQuiz.transform.position = new Vector3(0, 0, 500);
    }
}
