using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WebSiteManager : MonoBehaviour
{

		public List<Node> webPageList;
		private Node currentWebPage;

		public void AddWebLink (Node page)
		{
				webPageList.Add (page);
	
		}

		public void NavigationHandler (Node nextPage)
		{
				currentWebPage = nextPage;
				EnableCurrentPage ();


		}

		private void EnableCurrentPage ()
		{

				for (int i = 0; i <webPageList.Count; i++) {
		
						SpriteRenderer[] rs = webPageList [i].GetComponentsInChildren<SpriteRenderer> ();
		
						if (webPageList [i].Equals (currentWebPage)) {
			
								foreach (SpriteRenderer r in rs)
										r.enabled = true;
			
								continue;
						}

						foreach (SpriteRenderer r in rs) {
								r.enabled = false;
						}
		
				}


		}
}



	

	




