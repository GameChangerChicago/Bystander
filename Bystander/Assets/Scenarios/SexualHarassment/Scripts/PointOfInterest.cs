using UnityEngine;
using System.Collections;

public class PointOfInterest : MonoBehaviour
{
    private SHGameManager _myGameManager;
    private GameObject _myMiniComic;

    public GameObject MyMiniComic;
    public Transform InstantiationTransform;
    public float ComicViewTime;

    void Start()
    {
        _myGameManager = FindObjectOfType<SHGameManager>();
    }

    void Update()
    {

    }

    void OnMouseDown()
    {
        ShowComic();
    }

    private void ShowComic()
    {
        _myMiniComic = (GameObject)Instantiate(MyMiniComic, InstantiationTransform.position, Quaternion.identity);
        Invoke("ShowQuiz", ComicViewTime);
    }

    private void ShowQuiz()
    {
        Destroy(_myMiniComic);
    }
}
