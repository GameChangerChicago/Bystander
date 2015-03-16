using UnityEngine;
using System.Collections;

public class LinkHighlight : MonoBehaviour {

	private Color defaultColor, highlightColor; 

	// Use this for initialization
	void Start () {
	
		defaultColor = this.GetComponent<SpriteRenderer> ().color;
		highlightColor = Color.cyan;

	}


	void OnMouseEnter()
	{
		GetComponent<SpriteRenderer> ().color = highlightColor;

		}

	void OnMouseExit(){

		GetComponent<SpriteRenderer> ().color = defaultColor;

		}

	
	// Update is called once per frame
	void Update () {
	
	}
}
