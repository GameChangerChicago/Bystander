using UnityEngine;
//using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SteveScript : MonoBehaviour {

	public UILabel mySpeechText; 

	private int steveLinesCounter;

	public GameObject myManager;
	private GuiDanceOfficeManager myManagerScript;
	private List<string> steveLines = new List<string>();

	// Use this for initialization
	void Start () {

		myManagerScript = myManager.GetComponent<GuiDanceOfficeManager>();

		steveLines.Add ("Wha ");
		steveLines.Add ("What's the matter? ajsdlfkjalkdjflkjlaksdfjlajdlfjlajljsdlfjlkajldjfljalkdsjflkjakldfjkljaldjflkjalkjsdlkfjlkjalkjsldkfjlkjalksjdlfkjlkjlka"); 
		steveLines.Add ("(Clears throat)*"); 
		steveLines.Add ("Well, here we are. At the counselor's office"); 
		steveLines.Add ("Step up. It's time to shed some light on this."); 
		steveLines.Add ("Step up. It's time to shed some light on this."); 
		steveLines.Add ("I can only lead you so far."); 
		steveLines.Add ("Do you see it a lot? In the halls and stuff?"); 
		steveLines.Add ("Do you see it a lot? In the halls and stuff?"); 

	}

	public void displayCorrectDialogue(){
		steveLinesCounter++;
		if (steveLinesCounter < steveLines.Count) {
			Debug.Log ("We are inside of displayCorrectDialogue for faith");
			mySpeechText.text = steveLines [steveLinesCounter];
		}

	}
}
