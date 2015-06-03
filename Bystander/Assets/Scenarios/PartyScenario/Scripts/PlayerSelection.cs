using UnityEngine;
using System.Collections;

public class PlayerSelection : MonoBehaviour
{
    protected bool mouseOverCoral
    {
        get
        {
            return _mouseOverCoral;
        }

        set
        {
            if (value != _mouseOverCoral)
            {
                if (value)
                    _mouseOverDavid = false;
                if (!value)

                _mouseOverCoral = value;
            }
        }
    }
    private bool _mouseOverCoral;
    protected bool mouseOverDavid
    {
        get
        {
            return _mouseOverDavid;
        }

        set
        {
            if (value != _mouseOverDavid)
            {
                if (value)
                    _mouseOverCoral = false;
                if (!value)

                _mouseOverDavid = value;
            }
        }
    }
    private bool _mouseOverDavid;

    public GameObject[] SelectObjects;

    private PartyGameManager _myGameManager;
    private SpriteRenderer _idleSprite;
    private Camera _myCamera;

    void Start()
    {
        _myCamera = Camera.main;
        _myGameManager = FindObjectOfType<PartyGameManager>();
    }

    void Update()
    {
        Collider2D overlapCircle = Physics2D.OverlapCircle(_myCamera.ScreenToWorldPoint(Input.mousePosition), 0.01f);

        if (overlapCircle && overlapCircle.transform != null)
        {
            if (overlapCircle.gameObject == SelectObjects[0])
            {
                SelectObjects[0].GetComponent<Animator>().SetBool("MousedOver", true);
                SelectObjects[1].GetComponent<Animator>().SetBool("MousedOver", false);

                if (Input.GetKeyUp(KeyCode.Mouse0))
                    StartCoroutine(SelectionMade(true));
            }
            else if (overlapCircle.gameObject == SelectObjects[1])
            {
                SelectObjects[1].GetComponent<Animator>().SetBool("MousedOver", true);
                SelectObjects[0].GetComponent<Animator>().SetBool("MousedOver", false);

                if (Input.GetKeyUp(KeyCode.Mouse0))
                    StartCoroutine(SelectionMade(false));
            }
            else
            {
                SelectObjects[0].GetComponent<Animator>().SetBool("MousedOver", false);
                SelectObjects[1].GetComponent<Animator>().SetBool("MousedOver", false);
            }
        }
        else
        {
            SelectObjects[0].GetComponent<Animator>().SetBool("MousedOver", false);
            SelectObjects[1].GetComponent<Animator>().SetBool("MousedOver", false);
        }
    }

    private IEnumerator SelectionMade(bool isCoral)
    {
        if (isCoral)
        {
            _myGameManager.ChoseCoral = true;
            SelectObjects[0].GetComponent<Animator>().SetBool("Selected", true);
            SelectObjects[1].GetComponent<Animator>().SetBool("NotSelected", true);
        }
        else
        {
            _myGameManager.ChoseCoral = false;
            SelectObjects[1].GetComponent<Animator>().SetBool("Selected", true);
            SelectObjects[0].GetComponent<Animator>().SetBool("NotSelected", true);
        }

        yield return new WaitForSeconds(1);

        Destroy(this.gameObject);
    }
}