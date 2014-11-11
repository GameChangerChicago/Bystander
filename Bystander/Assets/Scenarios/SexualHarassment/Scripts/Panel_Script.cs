using UnityEngine;
using System.Collections;

public class Panel_Script : MonoBehaviour {

	public Transform mySituationToLoad;
	public GameObject target;
	public static readonly float offset = 20.0f;
	private float speed;
	private Vector3 newPosition;

	void Start(){
		int rand1 = Random.Range(0,2);
		float rand2 = Random.Range(10f,500f);

		speed = 3.0f;

		if (rand1 == 0) {
			transform.position = new Vector3 (transform.position.x + rand2, transform.position.y, 0);
		}
		else
			transform.position = new Vector3 (transform.position.x, transform.position.y+ rand2, 0);
	}
		
	void Update(){
		transform.position = Vector3.Lerp(transform.position, target.transform.position, speed*Time.deltaTime);
	}

	void OnMouseDown(){
		loadMySituation ();
	}

	public void loadMySituation (){
		//load the scene/situation prefab specific to this script
	}
}
