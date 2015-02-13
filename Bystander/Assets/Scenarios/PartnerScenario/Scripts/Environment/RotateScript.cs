using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (this.gameObject.name == "Sun")
        	this.gameObject.transform.Rotate (Vector3.forward * -.111f);
    	else if (this.gameObject.name == "Wheel1")
        	this.gameObject.transform.Rotate (Vector3.forward * -.222f);
    	else if (this.gameObject.name == "Wheel2")
        	this.gameObject.transform.Rotate (Vector3.forward * .666f);
    	else if (this.gameObject.name == "Wheel3")
        	this.gameObject.transform.Rotate (Vector3.forward * -.444f);
	
	}
}
