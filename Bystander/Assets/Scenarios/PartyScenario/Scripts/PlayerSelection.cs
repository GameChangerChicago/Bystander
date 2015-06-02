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
                RendererHandler(SelectObjects[0]);
                SelectObjects[0].GetComponent<Animator>().SetBool("MousedOver", true);

                if (Input.GetKeyUp(KeyCode.Mouse0))
                    SelectionMade(true);
            }
            else if (overlapCircle.gameObject == SelectObjects[1])
            {
                RendererHandler(SelectObjects[1]);
                SelectObjects[1].GetComponent<Animator>().SetBool("MousedOver", true);

                if (Input.GetKeyUp(KeyCode.Mouse0))
                    SelectionMade(false);
            }
        }
    }

    private void SelectionMade(bool isCoral)
    {
        if (this.name == "Coral")
            _myGameManager.ChoseCoral = true;

        Debug.Log("I suggest feather touch. You have selected " + this.name);
    }

    private void RendererHandler(GameObject selectObject)
    {
        if (SelectObjects[0] == selectObject)
        {
            SelectObjects[0].GetComponent<SpriteRenderer>().sortingOrder = 2;
            SpriteRenderer[] otherRenderers = SelectObjects[0].GetComponentsInChildren<SpriteRenderer>();

            for (int i = 0; i < otherRenderers.Length; i++)
            {
                if (otherRenderers[i].name == "FighterSelectFrameSprite")
                    otherRenderers[i].sortingOrder = 4;
                else
                    otherRenderers[i].sortingOrder = 3;
            }

            SelectObjects[1].GetComponent<SpriteRenderer>().sortingOrder = 1;
            otherRenderers = SelectObjects[1].GetComponentsInChildren<SpriteRenderer>();

            for (int i = 0; i < otherRenderers.Length; i++)
            {
                if (otherRenderers[i].name == "FighterSelectFrameSprite")
                    otherRenderers[i].sortingOrder = 3;
                else
                    otherRenderers[i].sortingOrder = 2;
            }
        }
        else
        {
            SelectObjects[1].GetComponent<SpriteRenderer>().sortingOrder = 2;
            SpriteRenderer[] otherRenderers = SelectObjects[1].GetComponentsInChildren<SpriteRenderer>();

            for (int i = 0; i < otherRenderers.Length; i++)
            {
                if (otherRenderers[i].name == "FighterSelectFrameSprite")
                    otherRenderers[i].sortingOrder = 4;
                else
                    otherRenderers[i].sortingOrder = 3;
            }

            SelectObjects[0].GetComponent<SpriteRenderer>().sortingOrder = 1;
            otherRenderers = SelectObjects[0].GetComponentsInChildren<SpriteRenderer>();

            for (int i = 0; i < otherRenderers.Length; i++)
            {
                if (otherRenderers[i].name == "FighterSelectFrameSprite")
                    otherRenderers[i].sortingOrder = 3;
                else
                    otherRenderers[i].sortingOrder = 2;
            }
        }
    }
}