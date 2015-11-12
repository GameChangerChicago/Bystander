using UnityEngine;
using System.Collections;

public class Highlight : MonoBehaviour {

	public Sprite spriteHighlight;
	public bool hover = false; 


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseEnter(){

		GetComponent<SpriteRenderer> ().sprite = spriteHighlight;
		hover = true; 

		}

	void OnMouseExit(){

		GetComponent<SpriteRenderer> ().sprite = spriteHighlight;
		hover = false; 

		}
}
