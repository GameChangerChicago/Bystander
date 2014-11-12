using UnityEngine;
using System.Collections;

public class MoveToNextScene : MonoBehaviour {

	public SEXHAS_STATE gameState;

	void OnMouseDown(){
		Debug.Log ("Should be moving to new scene.");
		GrandMaster_SexHasManager sexHasManagerScript = GameObject.FindGameObjectWithTag ("GameManagerOBJ").GetComponent ("GrandMaster_SexHasManager") as GrandMaster_SexHasManager;
		sexHasManagerScript.SexHasState = gameState;
		sexHasManagerScript.bInstantiated = false;
	}
}
