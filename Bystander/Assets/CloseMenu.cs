using UnityEngine;
using System.Collections;

public class CloseMenu : MonoBehaviour {

	private WebMenuTransition menu;
	public bool isMenu, isOpen;


	// Use this for initialization
	void Start () {
	
		menu = GetComponentInParent<WebMenuTransition> ();

			//FindObjectOfType<WebMenuTransition> ();
		isMenu = menu.isMenu;
		isOpen = menu.isOpen;
	}


	void Update(){

		isOpen = menu.isOpen;

		}
	
	// Update is called once per frame
	void OnMouseEnter(){

		}

	void OnMouseOver(){

		if(isMenu && isOpen)
			Debug.Log ("Test 2");

		}

	void OnMouseExit(){


		if (isMenu && isOpen)
						menu.TransitionToMenu ();
	


		}
}
