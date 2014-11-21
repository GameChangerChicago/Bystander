using UnityEngine;
using System.Collections;

public class QuizButton : MonoBehaviour
{
    private SHGameManager _myGameManager;
    private GameObject _myQuiz;

    public bool CorrectAnswer;

    void Start()
    {
        _myGameManager = FindObjectOfType<SHGameManager>();
        _myQuiz = this.transform.parent.gameObject;
    }

    void OnMouseDown()
    {
        if (CorrectAnswer)
            Debug.Log("Good Job!");
        else
            Debug.Log("You Lose!");

        _myQuiz.transform.position = new Vector3(0, 0, 500);
    }
}
