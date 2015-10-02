using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    static bool singleScenarioMode;
    public bool SingleScenarioMode
    {
        get
        {
            return singleScenarioMode;
        }
        set
        {
            singleScenarioMode = value;
        }
    }

    protected bool audioPaused
    {
        get
        {
            return _audioPaused;
        }
        set
        {
            if (_audioPaused != value)
            {
                if (value)
                {
                    for (int i = 0; i < _allAudioSources.Length; i++)
                    {
                        _allAudioSources[i].Pause();
                    }
                }
                else
                {
                    for (int i = 0; i < _allAudioSources.Length; i++)
                    {
                        _allAudioSources[i].Play();
                    }
                }

                _audioPaused = value;
            }
        }
    }
    private bool _audioPaused;

    public GameObject PauseMenu;
    public int CursorIndexAtPause;

    private AudioSource[] _allAudioSources;
    private CursorHandler _cursorHandler;
    private GUIManager _guiManager;
    private Camera _camera;
    
    void Start()
    {
        _cursorHandler = FindObjectOfType<CursorHandler>();
        _allAudioSources = FindObjectsOfType<AudioSource>();
        _camera = Camera.main;
        if (PauseMenu != null)
        {
            PauseMenu.transform.localScale = new Vector3(PauseMenu.transform.localScale.x + ((_camera.orthographicSize - 17) * 0.06f),
                                                         PauseMenu.transform.localScale.x + ((_camera.orthographicSize - 17) * 0.06f),
                                                         PauseMenu.transform.localScale.z);
        }

        if (Application.loadedLevelName == "MaleScenario")
        {
            _guiManager = FindObjectOfType<GUIManager>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        if (Time.timeScale > 0)
        {
            if (Application.loadedLevelName == "MaleScenario")
            {
                _guiManager.ShowTextBar = false;
            }

            audioPaused = true;
            CursorIndexAtPause = _cursorHandler.CursorIndex;
            _cursorHandler.ChangeCursor(1);
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            if (Application.loadedLevelName == "MaleScenario")
            {
                _guiManager.ShowTextBar = true;
            }

            audioPaused = false;
            PauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
}