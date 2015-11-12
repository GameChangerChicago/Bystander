using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour
{
    public GameObject SelectedSprite;

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
	private Color backgroundColor;
	private GameObject loadingBackground;

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
		loadingBackground = GameObject.Find ("LoadingScreen");
		backgroundColor = loadingBackground.GetComponent<SpriteRenderer> ().color = new Color (0,0,0,1);


		StartCoroutine (FadeLoadingScreen (1f, 0f, 3f));

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
                if (Level != "")
                {
                    if (Level != "Close")
                    {
						StartCoroutine(DisplayLoadingScreen(Level));
                    }
                    else
                        Application.Quit();
                }
                else if (!CloseMenuButton)
                {
                    Invoke("ToggleScenarios", 0.1f);
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
            if (_camera.transform.position.y > 0)
                _camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y - 15.5f, _camera.transform.position.z);
            else
                _camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y + 15.5f, _camera.transform.position.z);
        }
        else if (SubMenuIndex == 1)
        {
            if (_camera.transform.position.y > 0)
                _camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y - 31.4734f, _camera.transform.position.z);
            else
                _camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y + 31.4734f, _camera.transform.position.z);
        }
    }

	IEnumerator DisplayLoadingScreen (string Level){

		StartCoroutine(FadeLoadingScreen(0f, 1f, 2f));
		AsyncOperation async = Application.LoadLevelAsync (Level);

		while (!async.isDone) {

						yield return null;
				}

		if (async.isDone) {
			StartCoroutine(FadeLoadingScreen(1f, 0f, 2f));
			               yield return new WaitForSeconds(2f);
			Application.LoadLevel(Level);

				}


				
	}

	IEnumerator FadeLoadingScreen(float currentAlpha, float finalAlpha, float time){

		yield return new WaitForSeconds (1f);
				for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time) {
						Color newColor = new Color (0, 0, 0, Mathf.Lerp (currentAlpha, finalAlpha, t));
						loadingBackground.GetComponent<SpriteRenderer> ().color = newColor;
				
						yield return null;

		
				}
		}


	}
		



		                                                                

		                                                                 
