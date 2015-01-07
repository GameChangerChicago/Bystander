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
        _myGameManager = FindObjectOfType<SHGameManager>();
        _myVirgil = FindObjectOfType<SHVigilHandler>();
        _virgilString = Resources.Load("SHText/VirgilDialog_" + this.name).ToString();
        myMiniComic = Resources.Load("Prefabs/MiniComic_" + this.name) as GameObject;
    }

    void OnMouseDown()
    {
        if (!ComicShown)
            ShowComic();
    }

    private void ShowComic()
    {
        _myMiniComic = (GameObject)Instantiate(myMiniComic, InstantiationTransform.position, Quaternion.identity);
        Invoke("ShowQuiz", ComicViewTime);
        ComicShown = true;
    }

    private void ShowQuiz()
    {
        GameObject quiz = GameObject.Find("Quiz");
        Destroy(_myMiniComic);
        quiz.transform.position = InstantiationTransform.position;
        _myVirgil.DialogString = _virgilString;

        foreach (QuizButton qb in quiz.GetComponentsInChildren<QuizButton>())
        {
            if (IsSexualHarassment)
            {
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
