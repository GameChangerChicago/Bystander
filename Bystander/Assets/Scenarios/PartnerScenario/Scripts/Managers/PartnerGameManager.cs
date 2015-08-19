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
    public Subtitle CurrentSubtitle;
    public AudioSource VirgilAudioSource;
    public bool InteractionEnabled;

    private CursorHandler _cursorHandler;
    private GinaStates _currentGinaState = GinaStates.BACK;
    private HollyStates _currentHollyState = HollyStates.EXPLAINING;
    private string _affect = "";
    private bool _helpfulPrimer = false,
                 _finalBool = false;//This is definitely hacky but I don't know if I'll fix it...

    protected bool win
    {
        get
        {
            if (DialogueLua.GetVariable("M1").AsInt > 0 &&
                DialogueLua.GetVariable("M2").AsInt > 0 &&
                DialogueLua.GetVariable("M3").AsInt > 0 &&
                DialogueLua.GetVariable("M4").AsInt > 0 &&
                DialogueLua.GetVariable("M5").AsInt > 0 &&
                DialogueLua.GetVariable("M6").AsInt > 0 &&
                DialogueLua.GetVariable("M7").AsInt > 0)
            {
                _win = true;
                HollyAnimator.SetBool("Win", true);
            }
            else
                _win = false;

            return _win;
        }

        set
        {
            _win = value;
        }
    }
    private bool _win;

    protected int wrongAnswerCount
    {
        get
        {
            return _wrongAnswerCount;
        }
        set
        {
            if (value > 1)
                VirgilAudioSource.Play();

            _wrongAnswerCount = value;
        }
    }
    private int _wrongAnswerCount;

    void Start()
    {
        _cursorHandler = FindObjectOfType<CursorHandler>();
    }

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
                if (DialogueLua.GetVariable("FacingGina").AsBool && Input.GetKeyDown(KeyCode.Mouse0) && InteractionEnabled && !win)
                {
                    Debug.Log("NOW!!");
                    _affect = "";
                    DialogueLua.SetVariable("Affect", "");
                    GinaAnimator.SetBool("FacingGina", true);
                    _currentGinaState = GinaStates.THINKING;
                }//The bullshit starts here...
                else if (Input.GetKeyDown(KeyCode.Mouse0) && InteractionEnabled && win && _helpfulPrimer)
                {
                    GinaAnimator.SetBool("FacingGina", true);
                    GinaAnimator.SetBool("Helpful", true);
                    _currentGinaState = GinaStates.HELPFUL;
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
                    Invoke("RecetAnimProperties", 1);
                }
                else if (_affect == "Helpful")
                {
                    _currentGinaState = GinaStates.HELPFUL;
                    GinaAnimator.SetBool("Helpful", true);
                    Invoke("RecetAnimProperties", 1);
                }
                else if (_affect == "Unhelpful")
                {
                    _currentGinaState = GinaStates.UNHELPFUL;
                    GinaAnimator.SetBool("Unhelpful", true);
                    Invoke("RecetAnimProperties", 1);
                }
                break;
            case GinaStates.LISTENING:
                if (!DialogueLua.GetVariable("FacingGina").AsBool && Input.GetKeyDown(KeyCode.Mouse0) && InteractionEnabled)
                {
                    _currentGinaState = GinaStates.BACK;
                    GinaAnimator.SetBool("FacingGina", false);
                    GinaAnimator.SetBool("DecisionMade", false);
                }
                break;
            case GinaStates.HELPFUL:
                if (!win)
                {
                    if (!DialogueLua.GetVariable("FacingGina").AsBool)
                    {
                        _currentGinaState = GinaStates.BACK;
                        GinaAnimator.SetBool("FacingGina", false);
                        GinaAnimator.SetBool("DecisionMade", false);
                    }
                }
                else
                {
                    if (!DialogueLua.GetVariable("FacingGina").AsBool && !_finalBool)
                    {
                        _currentGinaState = GinaStates.BACK;
                        GinaAnimator.SetBool("FacingGina", false);
                        GinaAnimator.SetBool("DecisionMade", false);
                        _finalBool = true;
                    }

                    if (!DialogueLua.GetVariable("FacingGina").AsBool && Input.GetKeyDown(KeyCode.Mouse0) && InteractionEnabled && _finalBool)
                    {
                        _currentGinaState = GinaStates.BACK;
                        GinaAnimator.SetBool("FacingGina", false);
                    }
                }
                
                break;
            case GinaStates.UNHELPFUL:
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
                if (DialogueLua.GetVariable("FacingGina").AsBool && Input.GetKeyDown(KeyCode.Mouse0) && InteractionEnabled)
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
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        if (win)
                        {
                            _affect = "Helpful";
                            HollyAnimator.SetBool("Win", false);
                        }
                    }

                    if (_affect == "Listening" && Input.GetKeyDown(KeyCode.Mouse0) && InteractionEnabled)
                    {
                        _currentHollyState = HollyStates.EXPLAINING;
                        HollyAnimator.SetBool("Explaining", true);
                        int rand = Random.Range(0, 2);
                        HollyAnimator.SetInteger("ExplainRandomizer", rand);
                    }
                    else if (_affect == "Helpful")
                    {
                        _currentHollyState = HollyStates.HELPFUL;
                        HollyAnimator.SetBool("Helpful", true);
                        HollyAnimator.SetBool("FacingGina", false);
                    }
                    else if (_affect == "Unhelpful")
                    {
                        _currentHollyState = HollyStates.UNHELPFUL;
                        HollyAnimator.SetBool("Unhelpful", true);
                        HollyAnimator.SetBool("FacingGina", false);
                    }
                }
                break;
            case HollyStates.HELPFUL:
                if (Input.GetKeyDown(KeyCode.Mouse0) && InteractionEnabled && !_helpfulPrimer)
                    _helpfulPrimer = true;
                else if (Input.GetKeyDown(KeyCode.Mouse0) && _helpfulPrimer && win)
                {
                    DialogueManager.StopConversation();
                    Application.LoadLevel("PostPartner");
                    _currentHollyState = HollyStates.BACK;
                    _affect = "";
                    HollyAnimator.SetBool("Helpful", false);
                    HollyAnimator.SetBool("FacingGina", true);
                    _helpfulPrimer = false;
                }
                else if (Input.GetKeyDown(KeyCode.Mouse0) && InteractionEnabled && _helpfulPrimer && !win)
                {
                    _currentHollyState = HollyStates.EXPLAINING;
                    HollyAnimator.SetBool("Explaining", true);
                    int rand = Random.Range(0, 2);
                    HollyAnimator.SetInteger("ExplainRandomizer", rand);
                    _helpfulPrimer = false;
                }
                break;
            case HollyStates.UNHELPFUL:
                if (Input.GetKeyDown(KeyCode.Mouse0) && InteractionEnabled)
                {
                    _currentHollyState = HollyStates.EXPLAINING;
                    HollyAnimator.SetBool("Explaining", true);
                    int rand = Random.Range(0, 2);
                    HollyAnimator.SetInteger("ExplainRandomizer", rand);
                    wrongAnswerCount++;
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