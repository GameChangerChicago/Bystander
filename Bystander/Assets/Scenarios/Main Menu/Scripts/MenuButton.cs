using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour
{
    public GameObject SelectedSprite;
    public string Level;
    public bool ScenerioButton;
    private GameManager _gameManager;
    private Camera _camera;
    private CursorHandler _cursorHandler;
    private HubWorldManager hb_Manager;
    private HB_GameState gameState;

    // Use this for initialization
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _camera = FindObjectOfType<Camera>();
        _cursorHandler = FindObjectOfType<CursorHandler>();
        //hb_Manager = FindObjectOfType<HubWorldManager>();
        //gameState = hb_Manager.isGameState();
        SelectedSprite.GetComponent<SpriteRenderer>().enabled = false;
    }


    void OnMouseOver()
    {
        _cursorHandler.ChangeCursor(0);
        SelectedSprite.GetComponent<SpriteRenderer>().enabled = true;
    }

    void OnMouseExit()
    {
        _cursorHandler.ChangeCursor(1);
        SelectedSprite.GetComponent<SpriteRenderer>().enabled = false;
    }

    void OnMouseDown()
    {
        if (Level != "")
        {
            if(Level != "Close")
                Application.LoadLevel(Level);
            else
                Application.Quit();
        }
        else
        {
            ToggleScenarios();
        }

        if (ScenerioButton)
            _gameManager.SingleScenarioMode = true;
    }

    private void ToggleScenarios()
    {
        if (_camera.transform.position.y > 0)
            _camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y - 15.5f, _camera.transform.position.z);
        else
            _camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y + 15.5f, _camera.transform.position.z);
    }
}