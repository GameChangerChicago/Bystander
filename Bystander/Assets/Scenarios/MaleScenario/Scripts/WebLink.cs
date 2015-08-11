using UnityEngine;
using System.Collections;

public class WebLink : MonoBehaviour
{
    public WebMenuTransition MyWebMenu;
		public Node webPage;
		private WebSiteManager siteManager;
        private CursorHandler _cursorHandler;

		void Start ()
		{
            _cursorHandler = FindObjectOfType<CursorHandler>();
				siteManager = FindObjectOfType<WebSiteManager> ();
				siteManager.AddWebLink (webPage);

		}
	
		void OnMouseDown ()
		{

				siteManager.NavigationHandler (webPage);
            if(MyWebMenu != null)
                MyWebMenu.TransitionToMenu();

		}


        void OnMouseEnter()
        {
            _cursorHandler.ChangeCursor(0);
        }

        void OnMouseExit()
        {
            _cursorHandler.ChangeCursor(1);
        }
}
