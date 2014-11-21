using UnityEngine;
using System.Collections;

public class PointOfInterest : MonoBehaviour
{
    private SHGameManager _myGameManager;
    private GameObject _myMiniComic;
    private bool _comicShown = false;

    public GameObject MyMiniComic;
    public Transform InstantiationTransform;
    public float ComicViewTime;
    public bool isSexualHarassment;

    void Start()
    {
        _myGameManager = FindObjectOfType<SHGameManager>();
    }

    void Update()
    {

    }

    void OnMouseDown()
    {
        if (!_comicShown)
            ShowComic();
    }

    private void ShowComic()
    {
        _comicShown = true;
        _myMiniComic = (GameObject)Instantiate(MyMiniComic, InstantiationTransform.position, Quaternion.identity);
        Invoke("ShowQuiz", ComicViewTime);
    }

    private void ShowQuiz()
    {
        GameObject quiz = GameObject.Find("Quiz");
        Destroy(_myMiniComic);
        quiz.transform.position = InstantiationTransform.position;

        foreach (QuizButton qb in quiz.GetComponentsInChildren<QuizButton>())
        {
            if (isSexualHarassment)
            {
                if (qb.name == "Answer Yes")
                    qb.CorrectAnswer = true;
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
