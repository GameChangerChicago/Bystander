using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
                _relaventAudioSources.Clear();

                for (int i = 0; i < _allAudioSources.Length; i++)
                {
                    if (_allAudioSources[i].isPlaying)
                        _relaventAudioSources.Add(_allAudioSources[i]);
                }

                if (value)
                {
                    for (int i = 0; i < _relaventAudioSources.Count; i++)
                    {
                        _relaventAudioSources[i].Pause();
                    }
                }
                else
                {
                    for (int i = 0; i < _relaventAudioSources.Count; i++)
                    {
                        _relaventAudioSources[i].Play();
                    }
                }

                _audioPaused = value;
            }
        }
    }
    private bool _audioPaused;

    public GameObject PauseMenu,
                      DialogManager;
    public Camera LoadingCamera;

    private AudioSource[] _allAudioSources;
    private List<AudioSource> _relaventAudioSources = new List<AudioSource>();
    private CursorHandler _cursorHandler;
    private PartnerGameManager _partnerGM;
    private PartyGameManager _partyGM;
    private GUIManager _guiManager;
    private Camera _camera;
    private int _cursorIndexAtPause;
    
    void Start()
    {
        _cursorHandler = FindObjectOfType<CursorHandler>();
        _allAudioSources = FindObjectsOfType<AudioSource>();
        _partnerGM = FindObjectOfType<PartnerGameManager>();
        _partyGM = FindObjectOfType<PartyGameManager>();
        _camera = Camera.main;

        if (Application.loadedLevelName == "MaleScenario")
        {
            _guiManager = FindObjectOfType<GUIManager>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			if(Application.loadedLevelName != "Final_MainMenu")
            	TogglePauseMenu();
			else
				Application.Quit();
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

            if (_partnerGM != null)
            {
                DialogManager.SetActive(false);
                _partnerGM.enabled = false;
            }

            if (_partyGM != null)
            {
                _partyGM.DisableAllProps();
            }

            audioPaused = true;
            _cursorIndexAtPause = _cursorHandler.CursorIndex;
            _cursorHandler.ChangeCursor(1);
            PauseMenu.SetActive(true);
            PauseMenu.transform.localScale = new Vector3(_camera.orthographicSize * 0.06f, _camera.orthographicSize * 0.06f, PauseMenu.transform.localScale.z);
            Time.timeScale = 0;
        }
        else
        {
            if (Application.loadedLevelName == "MaleScenario")
            {
                _guiManager.ShowTextBar = true;
            }

            if (_partnerGM != null)
            {
                DialogManager.SetActive(true);
                _partnerGM.enabled = true;
            }

            if (_partyGM != null)
            {
                StartCoroutine(_partyGM.EnableAllProps(0));
            }

            _cursorHandler.ChangeCursor(_cursorIndexAtPause);
            audioPaused = false;
            PauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public IEnumerator LoadingHandler(string sceneToLoad)
    {
        LoadingCamera.enabled = true;
        AsyncOperation async = Application.LoadLevelAsync(sceneToLoad);
        yield return async;
    }
}