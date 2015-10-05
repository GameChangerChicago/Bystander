using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour
{
    public GameObject SelectedSprite;
    public string Level;
    public bool ScenerioButton,
                CloseMenuButton;

    protected bool mousedOver
    {
        get
        {
            return _mousedOver;
        }

        set
        {
            if (_mousedOver != value)
            {
                if (value)
                {
                    _cursorHandler.ChangeCursor(0);
                    SelectedSprite.GetComponent<SpriteRenderer>().enabled = true;
                }
                else
                {
                    _cursorHandler.ChangeCursor(1);
                    SelectedSprite.GetComponent<SpriteRenderer>().enabled = false;
                }

                _mousedOver = value;
            }
        }
    }
    private bool _mousedOver;

    private GameManager _gameManager;
    private Camera _camera;
    private CursorHandler _cursorHandler;
    private HubWorldManager hb_Manager;
    private HB_GameState gameState;
    private Collider2D _myCollider;

    // Use this for initialization
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _camera = FindObjectOfType<Camera>();
        _cursorHandler = FindObjectOfType<CursorHandler>();
        //hb_Manager = FindObjectOfType<HubWorldManager>();
        //gameState = hb_Manager.isGameState();
        SelectedSprite.GetComponent<SpriteRenderer>().enabled = false;
        _myCollider = this.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Physics2D.OverlapPoint(_camera.ScreenToWorldPoint(Input.mousePosition)) &&
            Physics2D.OverlapPoint(_camera.ScreenToWorldPoint(Input.mousePosition)) == _myCollider)
        {
            mousedOver = true;
        }
        else
        {
            mousedOver = false;
        }

        if (mousedOver)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (Level != "")
                {
                    if (Level != "Close")
                        Application.LoadLevel(Level);
                    else
                        Application.Quit();
                }
                else if (!CloseMenuButton)
                {
                    ToggleScenarios();
                }
                else
                {
                    _gameManager.TogglePauseMenu();
                }

                if (ScenerioButton)
                    _gameManager.SingleScenarioMode = true;
            }
        }
    }

    private void ToggleScenarios()
    {
        if (_camera.transform.position.y > 0)
            _camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y - 15.5f, _camera.transform.position.z);
        else
            _camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y + 15.5f, _camera.transform.position.z);
    }
}