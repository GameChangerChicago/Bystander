﻿using UnityEngine;
using System.Collections;

public class InterventionManager : MonoBehaviour
{
	private AudioManager _audioManager;
    private SHGameManager _myGameManager;
    private SHVigilHandler _myVirgil;
    private Animator _myAnimator;
    private TextMesh _myText;
    private BoxCollider[] _allSelectorColliders = new BoxCollider[5];
    private Vector3[] _interventionPositions = new Vector3[5];
    private string _interventionText;
    private bool _interventionActive;

    void Start()
    {
        _myGameManager = FindObjectOfType<SHGameManager>();
		_audioManager = FindObjectOfType<AudioManager>();
        _myVirgil = FindObjectOfType<SHVigilHandler>();
        _myAnimator = GetComponentInChildren<Animator>();
        _myText = this.GetComponentInChildren<TextMesh>();

        for (int i = 0; i < 5; i++)
        {
            _allSelectorColliders[i] = _myGameManager.AllSelectors[i].GetComponent<BoxCollider>();
        }

        _interventionPositions[0] = new Vector3(26.78879f, -35.70682f, -15.32112f);
        _interventionPositions[1] = new Vector3(16.9052f, -84.01974f, -15.32112f);
        _interventionPositions[2] = new Vector3(15.63024f, -235.9857f, -15.32112f);
        _interventionPositions[3] = new Vector3(16.7015f, -190.4575f, -15.32112f);
        _interventionPositions[4] = new Vector3(17.23712f, -138.5017f, -15.32112f);
    }

    void OnMouseUp()
    {
        if (_interventionActive)
        {
			_audioManager.StopAmbience();
            RemoveIntervention();
            _myGameManager.HideCurrentPOIs();
            StartCoroutine(_myGameManager.ReenablePOIs());
        }
    }

    public void InterventionSetup(ButtonType currentType)
    {
        _interventionText = Resources.Load("SHText/InterventionText_" + _myGameManager.CurrentMicroScenario.ToString()).ToString();
        string interventionText = "";
        char buttonTypeChar = '&';
        bool startPointFound = false;
        _interventionActive = true;

        //This makes sure that only faith will intervene in the bathroom
        if (_myGameManager.CurrentMicroScenario == MicroScenarios.Bathroom)
            _myAnimator.SetBool("IsFaith", true);
        else
            _myAnimator.SetBool("IsFaith", !_myAnimator.GetBool("IsFaith"));

        for (int i = 0; i < _interventionText.Length; i++)
        {
            if (!startPointFound)
            {
                switch (currentType)
                {
                    case ButtonType.CheckIn:
                        if (_interventionText[i] == '!')
                        {
                            startPointFound = true;
                            buttonTypeChar = _interventionText[i];
                            _myAnimator.SetInteger("InterventionIndex", 0);
                        }
                        break;
                    case ButtonType.Empathy:
                        if (_interventionText[i] == '@')
                        {
                            startPointFound = true;
                            buttonTypeChar = _interventionText[i];
                            _myAnimator.SetInteger("InterventionIndex", 1);
                        }
                        break;
                    case ButtonType.Friends:
                        if (_interventionText[i] == '#')
                        {
                            startPointFound = true;
                            buttonTypeChar = _interventionText[i];
                            _myAnimator.SetInteger("InterventionIndex", 2);
                        }
                        break;
                    case ButtonType.IStatement:
                        if (_interventionText[i] == '$')
                        {
                            startPointFound = true;
                            buttonTypeChar = _interventionText[i];
                            _myAnimator.SetInteger("InterventionIndex", 3);
                        }
                        break;
                    case ButtonType.SilentStare:
                        if (_interventionText[i] == '%')
                        {
                            startPointFound = true;
                            buttonTypeChar = _interventionText[i];
                            _myAnimator.SetInteger("InterventionIndex", 4);
                        }
                        break;
                    default:
                        Debug.LogWarning("Hmm, seems like you got here with a 'yes' or 'no' button type. I'm not sure how that happened...");
                        break;
                }
            }
            else
            {
                if (_interventionText[i] != buttonTypeChar)
                    interventionText += _interventionText[i];
                else
                    break;
            }
        }

        StringFormatter(interventionText);

        switch (_myGameManager.CurrentMicroScenario)
        {
            case MicroScenarios.Hallway:
                this.transform.position = _interventionPositions[0];
                break;
            case MicroScenarios.Classroom:
                this.transform.position = _interventionPositions[1];
                break;
            case MicroScenarios.Bathroom:
                this.transform.position = _interventionPositions[2];
                break;
            case MicroScenarios.Gym:
                this.transform.position = _interventionPositions[3];
                break;
            case MicroScenarios.Library:
                this.transform.position = _interventionPositions[4];
                break;
            default:
                Debug.Log("This shouldn't even be possible. If I were you, I'd check the \"MicroScenarios\" enum and see if there's something funky going on.");
                break;
        }
    }

    //This method works the same way that the one in the Party Scenario does
    //It keeps string lines within the bounds of the dialog box
    private void StringFormatter(string lineContent)
    {
        string currentWord = "";
        bool isFirstWord = true;
        Renderer currentRenderer;

        currentRenderer = _myText.GetComponent<Renderer>();
        _myText.text = currentWord;

        for (int i = 0; i < lineContent.Length; i++)
        {
            if (lineContent[i] != ' ')
            {
                currentWord = currentWord + lineContent[i];
            }
            else
            {
                if (isFirstWord)
                {
                    _myText.text = _myText.text + currentWord;

                    isFirstWord = false;
                }
                else
                {
                    _myText.text = _myText.text + " " + currentWord;
                }

                if (currentRenderer.bounds.extents.x > 12f)
                {
                    _myText.text = _myText.text.Remove(_myText.text.Length - (currentWord.Length + 1));
                    _myText.text = _myText.text + "\n" + currentWord;
                }
                currentWord = "";
            }
        }
    }

    private void RemoveIntervention()
    {
        this.transform.position = new Vector3(5000, -5000, -5000);
        _interventionActive = false;
        _myGameManager.SectionComplete = true;

        for (int i = 0; i < 5; i++)
        {
            if (!_myGameManager.AllSelectors[i].Selected)
                _allSelectorColliders[i].enabled = true;
        }
        //_myVirgil.ShowStringSegment();
    }
}