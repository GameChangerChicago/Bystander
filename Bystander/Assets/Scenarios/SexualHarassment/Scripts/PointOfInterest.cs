using UnityEngine;
using System.Collections;

public class PointOfInterest : MonoBehaviour
{
    private SHGameManager _myGameManager;
    private SHVigilHandler _myVirgil;
    private GameObject _myMiniComic;
    private string _virgilString;

    protected GameObject myMiniComic;

    public Transform InstantiationTransform;
    public float ComicViewTime;
    public bool IsSexualHarassment,
                ComicShown = false;

    void Start()
    {
        //Standard instantiation junk. UP, UP, DOWN, DOWN, LEFT, RIGHT, LEFT, RIGHT. WHATCHU GONNA DO NOW? A, B, START! -Konami
        _myGameManager = FindObjectOfType<SHGameManager>();
        _myVirgil = FindObjectOfType<SHVigilHandler>();

        //The .txt used for virgil's responses and the prefabs for the mini comic are named after the PointOfInterest in question
        _virgilString = Resources.Load("SHText/VirgilDialog_" + this.name).ToString();
        myMiniComic = Resources.Load("Prefabs/MiniComic_" + this.name) as GameObject;
    }

    //If the player clicks and the comic isn't already being shown then the ShowComic is called
    void OnMouseDown()
    {
        if (!ComicShown)
            ShowComic();
    }

    //This method simply instantiates the mini comic in question and starts the Invoke for ShowQuiz
    private void ShowComic()
    {
        _myMiniComic = (GameObject)Instantiate(myMiniComic, InstantiationTransform.position, Quaternion.identity);
        Invoke("ShowQuiz", ComicViewTime);
        ComicShown = true;
    }

    //ShowQuiz handles showing the quiz and setting up it's quiz buttons
    private void ShowQuiz()
    {
        //These lines destroy the minicomic and moves the quiz into postion
        GameObject quiz = GameObject.Find("Quiz");
        Destroy(_myMiniComic);
        quiz.transform.position = InstantiationTransform.position;

        //Set's the correct dialog string in Virgil
        _myVirgil.DialogString = _virgilString;

        //This for loop sets the impertinant bools for each QuizButton based on whether the point of interest was sexual harassment
        foreach (QuizButton qb in quiz.GetComponentsInChildren<QuizButton>())
        {
            if (IsSexualHarassment)
            {
                //If the answer is  yes and it is a case of sexual harrasment then the qb will be marked as a Game winner
                if (qb.name == "Answer Yes")
                {
                    qb.CorrectAnswer = true;
                    qb.GameWinner = true;
                }
                else
                    qb.CorrectAnswer = false;
            }
            else
            {
                if (qb.name == "Answer Yes")
                    qb.CorrectAnswer = false;
                else
                    qb.CorrectAnswer = true;
            }
        }
    }
}
