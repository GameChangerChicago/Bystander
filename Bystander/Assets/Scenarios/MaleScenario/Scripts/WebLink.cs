using UnityEngine;
using System.Collections;

public class WebLink : MonoBehaviour
{
    public WebMenuTransition MyWebMenu;
		public Node webPage;
		private WebSiteManager siteManager;

		void Start ()
		{
		
				siteManager = FindObjectOfType<WebSiteManager> ();
				siteManager.AddWebLink (webPage);

		}
	
		void OnMouseDown ()
		{

				siteManager.NavigationHandler (webPage);
            if(MyWebMenu != null)
                MyWebMenu.TransitionToMenu();

		}




}
