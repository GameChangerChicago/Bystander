using UnityEngine;
using System.Collections;

public class QuizButton : MonoBehaviour
{
    private SHGameManager _myGameManager;

    public bool CorrectAnswer;

    void Start()
    {
        _myGameManager = FindObjectOfType<SHGameManager>();
    }

    void OnMouseDown()
    {
        if (CorrectAnswer)
            Debug.Log("Good Job!");
        else
            Debug.Log("You Lose!");
    }
}
