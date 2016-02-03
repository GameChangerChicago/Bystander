using UnityEngine;
using System.Collections;

public class PlayerSelection : MonoBehaviour
{
    public GameObject[] SelectObjects;
	public AudioClip ClickSound;
    public PolygonCollider2D[] HallwayColliders;

    private PartyGameManager _myGameManager;
    private CursorHandler _cursorManager;
	private AudioManager _audioManager;
    private SpriteRenderer _idleSprite;
    private Camera _myCamera;
    private bool _selectionMade;

    void Start()
    {
        _myCamera = Camera.main;
		_audioManager = FindObjectOfType<AudioManager>();
        _myGameManager = FindObjectOfType<PartyGameManager>();
        _cursorManager = FindObjectOfType<CursorHandler>();
    }

    void Update()
    {
        if (_myGameManager.IsFinalInteractiveMoment())
        {
            Collider2D overlapCircle = Physics2D.OverlapCircle(_myCamera.ScreenToWorldPoint(Input.mousePosition), 0.01f);

            if (overlapCircle && overlapCircle.transform != null)
            {
                if (overlapCircle.gameObject == SelectObjects[0])
                {
                    if (!_selectionMade)
                        _cursorManager.ChangeCursor(0);
                    SelectObjects[0].GetComponent<Animator>().SetBool("MousedOver", true);
                    SelectObjects[1].GetComponent<Animator>().SetBool("MousedOver", false);

                    if (Input.GetKeyUp(KeyCode.Mouse0))
					{
                        StartCoroutine(SelectionMade(true));
						_audioManager.PlaySFX(ClickSound, 0.7f, false);
					}
                }
                else if (overlapCircle.gameObject == SelectObjects[1])
                {
                    if (!_selectionMade)
                        _cursorManager.ChangeCursor(0);
                    SelectObjects[1].GetComponent<Animator>().SetBool("MousedOver", true);
                    SelectObjects[0].GetComponent<Animator>().SetBool("MousedOver", false);

                    if (Input.GetKeyUp(KeyCode.Mouse0))
					{
                        StartCoroutine(SelectionMade(false));
						_audioManager.PlaySFX(ClickSound, 0.7f, false);
					}
                }
                else
                {
                    _cursorManager.ChangeCursor(1);
                    SelectObjects[0].GetComponent<Animator>().SetBool("MousedOver", false);
                    SelectObjects[1].GetComponent<Animator>().SetBool("MousedOver", false);
                }
            }
            else
            {
                _cursorManager.ChangeCursor(1);
                SelectObjects[0].GetComponent<Animator>().SetBool("MousedOver", false);
                SelectObjects[1].GetComponent<Animator>().SetBool("MousedOver", false);
            }
        }
    }

    private IEnumerator SelectionMade(bool isCoral)
    {
        _selectionMade = true;

        if (isCoral)
        {
            _cursorManager.ChangeCursor(1);
            _myGameManager.ChoseCoral = true;
            SelectObjects[0].GetComponent<Animator>().SetBool("Selected", true);
            SelectObjects[1].GetComponent<Animator>().SetBool("NotSelected", true);
        }
        else
        {
            _cursorManager.ChangeCursor(1);
            _myGameManager.ChoseCoral = false;
            SelectObjects[1].GetComponent<Animator>().SetBool("Selected", true);
            SelectObjects[0].GetComponent<Animator>().SetBool("NotSelected", true);
        }

        yield return new WaitForSeconds(1);

        for (int i = 0; i < 4; i++)
        {
            HallwayColliders[i].enabled = true;
        }

        Destroy(this.gameObject);
    }
}