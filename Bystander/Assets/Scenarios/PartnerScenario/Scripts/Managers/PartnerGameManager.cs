﻿using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public enum GinaStates
{
    THINKING,
    TRANSITIONING,
    HELPFUL,
    UNHELPFUL,
    LISTENING,
    BACK
}

public enum HollyStates
{
    EXPLAINING,
    HELPFUL,
    UNHELPFUL,
    BACK
}

public class PartnerGameManager : MonoBehaviour
{
    public Animator GinaAnimator,
                    HollyAnimator;

    private GinaStates _currentGinaState = GinaStates.BACK;
    private HollyStates _currentHollyState = HollyStates.EXPLAINING;
    private string _affect = "";

    void Update()
    {
        UpdateGinaAnimationParams();
        UpdateHollyAnimationParams();
    }

    private void UpdateGinaAnimationParams()
    {
        switch (_currentGinaState)
        {
            case GinaStates.BACK:
                if (DialogueLua.GetVariable("FacingGina").AsBool && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    _affect = "";
                    DialogueLua.SetVariable("Affect", "");
                    GinaAnimator.SetBool("FacingGina", true);
                    _currentGinaState = GinaStates.THINKING;
                }
                break;
            case GinaStates.THINKING:
                _affect = DialogueLua.GetVariable("Affect").AsString;

                if (_affect != "")
                {
                    _currentGinaState = GinaStates.TRANSITIONING;
                    GinaAnimator.SetBool("DecisionMade", true);
                }
                break;
            case GinaStates.TRANSITIONING:
                if (_affect == "Listening")
                {
                    _currentGinaState = GinaStates.LISTENING;
                    GinaAnimator.SetBool("Listening", true);
                    Invoke("ResetAnimProperites", 1);
                }
                else if (_affect == "Helpful")
                {
                    _currentGinaState = GinaStates.HELPFUL;
                    GinaAnimator.SetBool("Helpful", true);
                    Invoke("ResetAnimProperites", 1);
                }
                else if (_affect == "Unhelpful")
                {
                    _currentGinaState = GinaStates.UNHELPFUL;
                    GinaAnimator.SetBool("Unhelpful", true);
                    Invoke("ResetAnimProperites", 1);
                }
                break;
            case GinaStates.LISTENING:
                //Invoke("Flip", 1.5f);
                if (!DialogueLua.GetVariable("FacingGina").AsBool)
                {
                    _currentGinaState = GinaStates.BACK;
                    GinaAnimator.SetBool("FacingGina", false);
                    GinaAnimator.SetBool("DecisionMade", false);
                }
                break;
            case GinaStates.HELPFUL:
                //Invoke("Flip", 2.15f);
                if (!DialogueLua.GetVariable("FacingGina").AsBool)
                {
                    _currentGinaState = GinaStates.BACK;
                    GinaAnimator.SetBool("FacingGina", false);
                    GinaAnimator.SetBool("DecisionMade", false);
                }
                break;
            case GinaStates.UNHELPFUL:
                //Invoke("Flip", 2.15f);
                if (!DialogueLua.GetVariable("FacingGina").AsBool)
                {
                    _currentGinaState = GinaStates.BACK;
                    GinaAnimator.SetBool("FacingGina", false);
                    GinaAnimator.SetBool("DecisionMade", false);
                }
                break;
            default:
                Debug.Log("Fool, that Gina state doesn't even exists");
                break;
        }
    }

    private void UpdateHollyAnimationParams()
    {
        switch (_currentHollyState)
        {
            case HollyStates.EXPLAINING:
                if (DialogueLua.GetVariable("FacingGina").AsBool && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    _currentHollyState = HollyStates.BACK;
                    HollyAnimator.SetBool("FacingGina", true);
                    HollyAnimator.SetBool("Explaining", false);
                    HollyAnimator.SetBool("Helpful", false);
                    HollyAnimator.SetBool("Unhelpful", false);
                }
                else
                {
                    HollyAnimator.SetBool("FacingGina", false);
                }
                break;
            case HollyStates.BACK:
                if (!DialogueLua.GetVariable("FacingGina").AsBool)
                {
                    if (_affect == "Listening")
                    {
                        _currentHollyState = HollyStates.EXPLAINING;
                        HollyAnimator.SetBool("Explaining", true);
                    }
                    else if (_affect == "Helpful")
                    {
                        _currentHollyState = HollyStates.HELPFUL;
                        HollyAnimator.SetBool("Helpful", true);
                    }
                    else if (_affect == "Unhelpful")
                    {
                        _currentHollyState = HollyStates.UNHELPFUL;
                        HollyAnimator.SetBool("Unhelpful", true);
                    }
                }
                break;
            case HollyStates.HELPFUL:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    _currentHollyState = HollyStates.EXPLAINING;
                    HollyAnimator.SetBool("Explaining", true);
                }
                break;
            case HollyStates.UNHELPFUL:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    _currentHollyState = HollyStates.EXPLAINING;
                    HollyAnimator.SetBool("Explaining", true);
                }
                break;
            default:
                Debug.Log("Fool, that Holly state doesn't even exists");
                break;
        }
    }

    private void RecetAnimProperties()
    {
        GinaAnimator.SetBool("Listening", false);
        GinaAnimator.SetBool("Helpful", false);
        GinaAnimator.SetBool("Unhelpful", false);
    }
}