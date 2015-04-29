using UnityEngine;
using System.Collections;

public class LinkHighlight : MonoBehaviour {

	private Color defaultColor, highlightColor; 
	public bool hover;

	// Use this for initialization
	void Start () {
	
		defaultColor = this.GetComponent<SpriteRenderer> ().color;
		highlightColor = Color.cyan;
		hover = false;

	}


	void OnMouseEnter()
	{
		GetComponent<SpriteRenderer> ().color = highlightColor;
		hover = true;

		}

	void OnMouseExit(){

		GetComponent<SpriteRenderer> ().color = defaultColor;
		hover = false;

		}

	
	// Update is called once per frame
	void Update () {
	
	}

	public bool isHover(){

		return hover;
}
}
