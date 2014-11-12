using UnityEngine;
//using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FaithScript : MonoBehaviour {


	public UILabel mySpeechText; 

	private int faithLinesCounter;
	Dictionary<int, string> faithLines = new Dictionary<int, string>();

	void Start () {
	
		faithLines.Add (2, "I don't know how to talk about it ...fklajdslkfj"); 
		faithLines.Add (4, "adsfhkadfj   It's just one of those things...fajslkdfjlakjdf"); 
		faithLines.Add (6, "I'm distracted, can't concentrateajskldfjladjflkajdflk"); 
		faithLines.Add (8, "I don't know if I should. dklajlksdjflkajdlksfjlkadf"); 
		faithLines.Add (10,"It's not so simple."); 
		faithLines.Add (12, "It's subtle. But it's there. It's everywhere. See for yourself."); 
	}
	
	public void displayCorrectDialogue(){
		faithLinesCounter++;
		string temp = null;

		if (faithLinesCounter > 12) {
			//Move on to next scene
			Debug.Log ("Should be sending new state to GrandManaager");
			GrandMaster_SexHasManager sexHasManagerScript = GameObject.FindGameObjectWithTag ("GameManagerOBJ").GetComponent ("GrandMaster_SexHasManager") as GrandMaster_SexHasManager;
			sexHasManagerScript.SexHasState = SEXHAS_STATE.PANEL;
			faithLinesCounter = 0;
		}

		if (faithLines.TryGetValue(faithLinesCounter, out temp)) {
			mySpeechText.text = faithLines [faithLinesCounter];
		}
	}
}
