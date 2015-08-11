using UnityEngine;
using System.Collections;

public class WebMenuTransition : MonoBehaviour
{
		public GameObject menuToTransitionTo;
		private WebMenuTransition menu;
		private LinkHighlight[] menuOptions;
		private BoxCollider2D[] menuOptionsColliders;
        private CursorHandler _cursorHandler;
		public bool isMenu;
		public bool isOpen;
	private bool isClicked;


		// Use this for initialization
		void Start ()
		{
				menu = menuToTransitionTo.GetComponent<WebMenuTransition> ();
                _cursorHandler = FindObjectOfType<CursorHandler>();
				
		if(isMenu)
			menuOptions = menu.GetComponentsInChildren<LinkHighlight> ();



				if (isMenu) {
						this.isOpen = false;
				} else {
						this.isOpen = true;
				}


				

        //for (int i = 0; i < menuOptions.Length; i++)
        //                menuOptionsColliders[i] = menuOptions [i].GetComponent<BoxCollider2D> ();
					
							
				

	
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
				if (isMenu)
						isClicked = true;


		}

		public void TransitionToMenu ()
		{
				
		isOpen = !isOpen;
		//menu.isOpen = !menu.isOpen;

		if(isMenu)
			isClicked = !isClicked;
		
		
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
        _cursorHandler.ChangeCursor(0);
	}
	
	void OnMouseOver(){

	}
	
	void OnMouseExit(){
        _cursorHandler.ChangeCursor(1);
//		if(isMenu && isClicked){
//		for (int i = 0; i < menuOptions.Length; i++)
//						if (menuOptions [i].isHover ())
//								break;
//						else
//								TransitionToMenu ();


//
//}
			   }
}
