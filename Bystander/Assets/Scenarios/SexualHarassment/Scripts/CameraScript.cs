using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {


	private Vector3 target;
	private float slideAmt = 12.0f;
	public static readonly float speed = 3.0f;

	private bool bMoveToPanel;
	private bool bMoveToSitu1;
	private bool bMoveToSitu2;
	private bool bMoveToSitu3;


	void Update(){
		if (bMoveToPanel) {
			transform.position = Vector3.Lerp (transform.position, target, speed * Time.deltaTime);
		}
		else if (bMoveToSitu1) {
			transform.position = Vector3.Lerp (transform.position, target, speed * Time.deltaTime);
		}
		else if (bMoveToSitu2) {
			transform.position = Vector3.Lerp (transform.position, target, speed * Time.deltaTime);
		}
		else if (bMoveToSitu3) {
			transform.position = Vector3.Lerp (transform.position, target, speed * Time.deltaTime);
		}
	}



	public void moveToPanel(){
		bMoveToPanel = true;
		target = new Vector3 (0, -12, -10);
	}

	public void moveToSitu1(){
		bMoveToPanel = false;
		bMoveToSitu1 = true;
		target = new Vector3(0,-24,-10);
	}

	public void moveBackToGuidance(){
		target = new Vector3(0, 0, -10);
		transform.position = Vector3.Lerp(transform.position, target, speed*Time.deltaTime);

	}
}
