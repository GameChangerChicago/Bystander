using UnityEngine;
//using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FaithScript : MonoBehaviour {





	//public GUIStyle myGUIStyle;

	//public GUIText mySpeechText; 

	public UILabel mySpeechText; 

	private int faithLinesCounter;


	public GameObject myManager;
	private GuiDanceOfficeManager myManagerScript;
	//private List<string> faithLines = new List<string>();
	Dictionary<int, string> faithLines = new Dictionary<int, string>();


	// Use this for initialization
	void Start () {

		myManagerScript = myManager.GetComponent<GuiDanceOfficeManager>();

		faithLines.Add (2, "I don't know how to talk about it ...fklajdslkfj"); 
		faithLines.Add (4, "adsfhkadfj   It's just one of those things...fajslkdfjlakjdf"); 
		faithLines.Add (6, "I'm distracted, can't concentrateajskldfjladjflkajdflk"); 
		faithLines.Add (8, "I don't know if I should. dklajlksdjflkajdlksfjlkadf"); 
		faithLines.Add (10,"I don't know if I should. dklajlksdjflkajdlksfjlkadf"); 
		faithLines.Add (12, "It's subtle. But it's there. It's everywhere. See for yourself."); 

	}
	
	public void displayCorrectDialogue(){
		faithLinesCounter++;
		string temp = null;

		if (faithLines.TryGetValue(faithLinesCounter, out temp)) {
			mySpeechText.text = faithLines [faithLinesCounter];
		}
	}
}
