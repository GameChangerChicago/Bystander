using UnityEngine;
using System.Collections;

public class WebLink : MonoBehaviour
{

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


		}




}
