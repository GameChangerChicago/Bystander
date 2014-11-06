using UnityEngine;
using System.Collections;

public class GuiDanceOfficeManager : MonoBehaviour {


	//This script registers mouseClicks any where on the screen to advance the dialogue
	//Implemented.

	//This script contains a state machine that dictates the states of the scene.
	//Everytime the int guidanceSceneState variable changes...it's property variable triggers
	//the getter and setter methods in the STEVE and FAITH scripts to update their dialogue. 

	public GameObject faith;
	public GameObject steve;

	private FaithScript faithScript;
	private SteveScript steveScript;

	private int guidanceSceneState;
	public int GuidanceSceneState {

		get
		{
			return guidanceSceneState;
		}
		set {
			//trigger faith script to increase to appropriate dialogue choice

			guidanceSceneState = value;

			if (guidanceSceneState > 9) {

			}

			//faithScript.displayCorrectDialogue (guidanceSceneState);
			//steveScript.displayCorrectDialogue (guidanceSceneState);
		}
	}

	void Start () {
		faithScript = faith.GetComponent<FaithScript>();
		steveScript = steve.GetComponent<SteveScript>();
	}


	void OnMouseDown(){
		GuidanceSceneState++;
		Debug.Log ("guidanceScene State is : "+GuidanceSceneState);
	}

}
