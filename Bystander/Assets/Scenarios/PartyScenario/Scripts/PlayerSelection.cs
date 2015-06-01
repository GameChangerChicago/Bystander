using UnityEngine;
using System.Collections;

public class PlayerSelection : MonoBehaviour
{
    public GameObject SelectObject;

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

        if (overlapCircle && overlapCircle.transform.parent != null)
        {
            if (overlapCircle.transform.parent.transform == this.transform)
            {
                SelectObject.SetActive(true);

                if (Input.GetKeyUp(KeyCode.Mouse0))
                    SelectionMade();
            }
        }
    }

    private void SelectionMade()
    {
        if (this.name == "Coral")
            _myGameManager.ChoseCoral = true;

        Debug.Log("I suggest feather touch. You have selected " + this.name);
    }
}