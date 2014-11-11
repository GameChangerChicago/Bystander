using UnityEngine;
using System.Collections;

public class Panel_Script : MonoBehaviour {


	public GameObject target;
	public static readonly float offset = 20.0f;
	public float speed = 10.0f;
	private Vector3 newPosition;

	void Start(){
		int rand1 = Random.Range(0,2);
		float rand2 = Random.Range(10f,200f);

		if (rand1 == 0) {
			transform.position = new Vector3 (transform.position.x + rand2, transform.position.y, 0);
		}
		else
			transform.position = new Vector3 (transform.position.x, transform.position.y+ rand2, 0);

	}
		
	void Update(){
		transform.position = Vector3.Lerp(transform.position, target.transform.position, 3*Time.deltaTime);
	}
}
