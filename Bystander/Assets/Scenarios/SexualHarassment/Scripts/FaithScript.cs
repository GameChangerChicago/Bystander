using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FaithScript : MonoBehaviour {


	//public 
	public Text mySpeechText; 
	public GameObject myManager;
	private GuiDanceOfficeManager myManagerScript;
	private List<string> faithLines = new List<string>();

	// Use this for initialization
	void Start () {

		myManagerScript = myManager.GetComponent<GuiDanceOfficeManager>();

		faithLines.Add ("I don't know how to talk about it ...fklajdslkfj"); 
		faithLines.Add ("I don't know how to talk about itaksdjflkadfjlajdflkjaldfjlakjdfljl"); 
		faithLines.Add ("adsfhkadfj   It's just one of those things...fajslkdfjlakjdf"); 
		faithLines.Add ("adsfhkadfj   It's just one of those things...fajslkdfjlakjdf"); 
		faithLines.Add ("adsfhkadfj   It's just one of those things...fajslkdfjlakjdf"); 
		faithLines.Add ("I'm distracted, can't concentrateajskldfjladjflkajdflk"); 
		faithLines.Add ("I don't know if I should. dklajlksdjflkajdlksfjlkadf"); 
		faithLines.Add ("I don't know if I should. dklajlksdjflkajdlksfjlkadf"); 
		faithLines.Add ("It's subtle. But it's there. It's everywhere. See for yourself."); 

	}
	
	public void displayCorrectDialogue(int speechNum){
		if (speechNum < faithLines.Count) {
			Debug.Log ("We are inside of displayCorrectDialogue for faith");
			mySpeechText.text = faithLines [speechNum];
		}

	}
}
