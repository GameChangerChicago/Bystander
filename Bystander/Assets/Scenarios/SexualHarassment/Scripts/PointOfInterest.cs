using UnityEngine;
using System.Collections;

public class PointOfInterest : MonoBehaviour
{
    private SHGameManager _myGameManager;
    private SHVigilHandler _myVirgil;
    private QuizHandler _myQuiz;
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
        _myQuiz = GameObject.Find("Quiz True False").GetComponent<QuizHandler>();

        //The .txt used for virgil's responses and the prefabs for the mini comic are named after the PointOfInterest in question
		Debug.Log ("SHText/VirgilDialog_" + this.name);
		_virgilString = Resources.Load("SHText/VirgilDialog_" + this.name).ToString();
        myMiniComic = Resources.Load("Prefabs/MiniComic_" + this.name) as GameObject;
    }

    //If the player clicks and the comic isn't already being shown then the ShowComic is called
    void OnMouseDown()
    {
        if (!ComicShown)
        {
            ShowComic();
            _myGameManager.CurrentPOI = this.GetComponent<PointOfInterest>();
        }
    }

    //This method simply instantiates the mini comic in question and starts the Invoke for ShowQuiz
    private void ShowComic()
    {
        _myMiniComic = (GameObject)Instantiate(myMiniComic, InstantiationTransform.position, Quaternion.identity);
        StartCoroutine(_myQuiz.ShowQuiz(ComicViewTime, InstantiationTransform.position, _virgilString, IsSexualHarassment, true));
        Invoke("HideComic", ComicViewTime);
        ComicShown = true;
    }

    private void HideComic()
    {
        Destroy(_myMiniComic);
    }
}
