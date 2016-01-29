using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour
{
    public GameObject SelectedSprite;
	public AudioClip HoverSound,
					 ClickSound;

    public string Level;
    public int SubMenuIndex;
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
					_audioManager.PlaySFX(HoverSound, true);
                }
                else
                {
                    _cursorHandler.ChangeCursor(1);
                    SelectedSprite.GetComponent<SpriteRenderer>().enabled = false;
					_audioManager.StopSFX();
                }

                _mousedOver = value;
            }
        }
    }
    private bool _mousedOver;

    private GameManager _gameManager;
	private AudioManager _audioManager;
    private Camera _camera;
    private CursorHandler _cursorHandler;
    private HubWorldManager hb_Manager;
    private HB_GameState gameState;
    private Collider2D _myCollider;
	private Color backgroundColor;
	private GameObject loadingBackground;
	private GameObject selectionScreen;
    // Use this for initialization
   


	void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
		_audioManager = _gameManager.GetComponent<AudioManager>();
        _camera = Camera.main;
        _cursorHandler = FindObjectOfType<CursorHandler>();
        //hb_Manager = FindObjectOfType<HubWorldManager>();
        //gameState = hb_Manager.isGameState();
        SelectedSprite.GetComponent<SpriteRenderer>().enabled = false;
        _myCollider = this.GetComponent<BoxCollider2D>();
		//loadingBackground = GameObject.Find ("LoadingScreen");
		//backgroundColor = loadingBackground.GetComponent<SpriteRenderer> ().color = new Color (0,0,0,1);
		selectionScreen = GameObject.Find("Episode Selection");

		//StartCoroutine (FadeLoadingScreen (1f, 0f, 3f));

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
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
				_audioManager.PlaySFX(ClickSound, false);
                if (Level != "")
                {
                    if (Level != "Close")
                    {
                        StartCoroutine(_gameManager.LoadingHandler(Level));
                    }
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
        if (SubMenuIndex == 0)
        {
			Debug.Log("hello?");

            if (_camera.transform.position.y > 0)

				_camera.transform.position = new Vector3(selectionScreen.transform.position.x, selectionScreen.transform.position.y, -10);
			else
				_camera.transform.position = new Vector3(selectionScreen.transform.position.x, selectionScreen.transform.position.y, -10);
//                _camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y - 15.5f, _camera.transform.position.z);
//            else
//                _camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y + 15.5f, _camera.transform.position.z);
        }
//        else if (SubMenuIndex == 1)
//        {
//            if (_camera.transform.position.y > 0)
//                _camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y - 31.4734f, _camera.transform.position.z);
//            else
//                _camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y + 31.4734f, _camera.transform.position.z);
//        }
    }
	}