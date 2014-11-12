using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {


	private Vector3 target;
	private float slideAmt = 12.0f;
	public static readonly float speed = 3.0f;


	void Update(){
		transform.position = Vector3.Lerp (transform.position, target, speed * Time.deltaTime);
	}


	public void moveBackToGuidance(){
		target = new Vector3(0, 0, -10);
	}

	public void moveToPanel(){
		target = new Vector3 (0, -12, -10);
	}
		
	public void moveToSituHall(){
		target = new Vector3(0,-24,-10);
	}
	public void moveToSituBlackTop(){
		target = new Vector3(0,-36,-10);
	}
	public void moveToSituClass(){
		target = new Vector3(0,-48,-10);
	}
	public void moveToSituBthrm(){
		target = new Vector3(0,-60,-10);
	}
	public void moveToSituCaf(){
		target = new Vector3(0,-72,-10);
	}
}
