using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class VirgilScript : MonoBehaviour
{
    protected bool restart
    {
        get
        {
            return _restart;
        }
        set
        {
            if (value != _restart)
            {
                if (value)
                {
                    grayscaleEffect.enabled = true;
                    //_multiDialogUi.ChangeDialog("DialogueVisualUIVirgil");
                }
                else
                {
                    grayscaleEffect.enabled = false;
                    //_multiDialogUi.ChangeDialog("DialogueVisualUIDefault");
                }
                _restart = value;
            }
        }
    }
    private bool _restart;

    private MultiDialogUI _multiDialogUi;

    public GrayscaleEffect grayscaleEffect;
    // Use this for initialization
    void Start()
    {
        _restart = DialogueLua.GetVariable("Repeat").AsBool;
        _multiDialogUi = FindObjectOfType<MultiDialogUI>();
    }

    // Update is called once per frame
    void Update()
    {
        restart = DialogueLua.GetVariable("Repeat").AsBool;
    }
}
