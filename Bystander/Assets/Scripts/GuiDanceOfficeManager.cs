using UnityEngine;
using System.Collections;

public class GuiDanceOfficeManager : MonoBehaviour {


	//This script registers mouseClicks any where on the screen to advance the dialogue
	//Implemented.

	//This script contains a state machine that dictates the states of the scene.
	//Everytime the int guidanceSceneState variable changes...it's property variable triggers
	//the getter and setter methods in the STEVE and FAITH scripts to update their dialogue. 


	private SteveScript steveScript;
	private FaithScript faithScript;

	private int guidanceSceneState;
	public int GuidanceSceneState {

		get
		{
			//Some other code
			return guidanceSceneState;
		}
		set {
			//Some other code
			guidanceSceneState = value;
		}
	}

	void Start () {
		faithScript = GetComponent<FaithScript>();
		steveScript = GetComponent<SteveScript>();
	}


	void OnMouseDown(){
		GuidanceSceneState++;
		Debug.Log ("guidanceScene State is : "+GuidanceSceneState);
	}

}
