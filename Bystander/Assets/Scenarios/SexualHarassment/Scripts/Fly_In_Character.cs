using UnityEngine;
using System.Collections;

public class Fly_In_Character : MonoBehaviour {

	public GameObject target;
	private float speed;

	void Start(){
		int rand1 = Random.Range(0,2);
		float rand2 = Random.Range(10f,50f);
		speed = 30.0f;

		if (rand1 == 0) {
			transform.position = new Vector3 (transform.position.x + rand2, transform.position.y, 0);
		}
		else
			transform.position = new Vector3 (transform.position.x, transform.position.y+ rand2, 0);
	}

	void Update(){
		transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed*Time.deltaTime);
	}


}
