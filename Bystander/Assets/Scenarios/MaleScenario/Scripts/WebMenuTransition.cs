using UnityEngine;
using System.Collections;

public class WebMenuTransition : MonoBehaviour
{
		public GameObject menuToTransitionTo;
		private WebMenuTransition menu;
		public bool isMenu;
		public bool isOpen;


		// Use this for initialization
		void Start ()
		{

				menu = menuToTransitionTo.GetComponent<WebMenuTransition> ();



				if (isMenu) {
						this.isOpen = false;
				} else {
						this.isOpen = true;
				}


							
				

	
		}
	
		// Update is called once per frame
		void Update ()
		{

//				if (isMenu && isOpen)
//						Debug.Log ("Test");

//		if (isMenu && isOpen && UnityEngine.Input.GetMouseButton (0))
//						TransitionToMenu ();

		}

		void OnMouseDown ()
		{

				
				TransitionToMenu ();


		}

		public void TransitionToMenu ()
		{
				
		isOpen = !isOpen;
		menu.isOpen = !menu.isOpen;
		
		
				//Turns on the new menus sprite renderers and box colliders
				SpriteRenderer[] rs = menuToTransitionTo.GetComponentsInChildren<SpriteRenderer> ();
				BoxCollider2D[] bc = menuToTransitionTo.GetComponentsInChildren<BoxCollider2D> ();
		
		
				foreach (SpriteRenderer r in rs) 
						r.enabled = true;
				foreach (BoxCollider2D b in bc)
						b.enabled = true;
		
		
				//Turns off this game objects sprite renderers and box colliders
				rs = this.GetComponentsInChildren<SpriteRenderer> ();
				bc = this.GetComponentsInChildren<BoxCollider2D> ();
		
				foreach (SpriteRenderer r in rs)
						r.enabled = false;
				foreach (BoxCollider2D b in bc)
						b.enabled = false;
		
				Debug.Log (isOpen);
		}

	void OnMouseEnter(){
		
	}
	
	void OnMouseOver(){

	}
	
	void OnMouseExit(){
}

}
