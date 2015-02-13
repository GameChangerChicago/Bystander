using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class VirgilScript : MonoBehaviour {
    public GrayscaleEffect grayscaleEffect;
    bool _restart;
    bool VirgilTalking;
	// Use this for initialization
	void Start () {
       
        _restart = DialogueLua.GetVariable("Repeat").AsBool;
	}
	
	// Update is called once per frame
	void Update () {
        _restart = DialogueLua.GetVariable("Repeat").AsBool;
        if (_restart)
        {
            grayscaleEffect.enabled = true;
           
        }
        else 
        {
            grayscaleEffect.enabled = false;
        }
	
	}



}
