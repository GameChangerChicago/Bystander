using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WebSiteManager : MonoBehaviour
{

		public List<Node> webPageList;
		//private Stack<Node> previousPageList, nextPageList;
		public Node currentWebPage, previousPage;


	void Start(){
		//currentWebPage = GameObject.Find ("Home Page");
	}

		public void AddWebLink (Node page)
		{
			webPageList.Add (page);
	
		}

		public void NavigationHandler (Node nextPage)
		{
			currentWebPage = nextPage;
			EnableCurrentPage ();

		}

		//Cycles through all webpages in the list and hides them from view
		private void EnableCurrentPage ()
		{

				for (int i = 0; i <webPageList.Count; i++) {
		
						SpriteRenderer[] rs = webPageList [i].GetComponentsInChildren<SpriteRenderer> ();
						BoxCollider2D[] bc = webPageList[i].GetComponentsInChildren<BoxCollider2D>(); 
						
				if (webPageList [i].Equals (currentWebPage)) {
			
								foreach (SpriteRenderer r in rs)
										r.enabled = true;

								foreach(BoxCollider2D b in bc)
										b.enabled = true;
			
								continue;
						}

						foreach (SpriteRenderer r in rs) {
								r.enabled = false;

						foreach(BoxCollider2D b in bc)
					b.enabled = false;
						}
		
				}


		}
}



	

	




