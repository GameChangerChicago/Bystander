using UnityEngine;
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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            UpdateAnimationParams();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            
        }
    }

    private void UpdateAnimationParams()
    {
        switch (_currentGinaState)
        {
            case GinaStates.LISTENING:
                //pklksalfjd[alskd
                break;
            case GinaStates.THINKING:
                Debug.Log(_affect);
                if (_affect != "")
                {
                    _currentGinaState = GinaStates.TRANSITIONING;
                    GinaAnimator.SetBool("DecisionMade", true);
                }
                break;
            case GinaStates.HELPFUL:
                Debug.Log("Helpful");
                break;
            case GinaStates.UNHELPFUL:
                Debug.Log("Unhelpful");
                break;
            case GinaStates.TRANSITIONING:
                if (_affect == "Listening")
                {
                    _currentGinaState = GinaStates.LISTENING;
                    GinaAnimator.SetBool("Listening", true);
                }
                else if (_affect == "Helpful")
                {
                    _currentGinaState = GinaStates.HELPFUL;
                    GinaAnimator.SetBool("Helpful", true);
                }
                else if (_affect == "Unhelpful")
                {
                    _currentGinaState = GinaStates.UNHELPFUL;
                    GinaAnimator.SetBool("Unhelpful", true);
                }

                GinaAnimator.SetBool("DecisionMade", false);
                break;
            case GinaStates.BACK:
                if (DialogueLua.GetVariable("FacingGina").AsBool)
                {
                    GinaAnimator.SetBool("FacingGina", true);
                    _currentGinaState = GinaStates.THINKING;
                    //_affect = DialogueLua.GetVariable("")
                    //_affect = "";
                    //GinaAnimator.SetBool("Listening", false);
                    //GinaAnimator.SetBool("Helpful", false);
                    //GinaAnimator.SetBool("Unhelpful", false);
                }
                break;
            default:
                Debug.Log("Fool, that Gina state doesn't even exists");
                break;
        }

        switch (_currentHollyState)
        {
            case HollyStates.EXPLAINING:
                if (DialogueLua.GetVariable("FacingGina").AsBool)
                {
                    HollyAnimator.SetBool("FacingGina", true);
                    _currentHollyState = HollyStates.BACK;
                    //_affect = "";
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
                Debug.Log("Helpful");
                break;
            case HollyStates.UNHELPFUL:
                Debug.Log("Unhelpful");
                break;
            default:
                Debug.Log("Fool, that Holly state doesn't even exists");
                break;
        }
    }


}