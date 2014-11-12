using UnityEngine;
using System.Collections;

public class MoveToNextScene : MonoBehaviour {


	//int rand1 = Random.Range(0,2);

	void OnMouseDown(){
		Debug.Log ("Should be moving to new scene.");
		CameraScript camScript = Camera.main.GetComponent ("CameraScript") as CameraScript;
		camScript.moveToSitu1 ();
	}
}
