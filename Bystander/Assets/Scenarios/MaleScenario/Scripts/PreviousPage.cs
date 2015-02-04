using UnityEngine;
using System.Collections;

public class PreviousPage : MonoBehaviour {

	private WebSiteManager siteManager;

	// Use this for initialization
	void Start () {
	
		siteManager = FindObjectOfType<WebSiteManager>();

	}
	
	void OnMouseDown(){

		//siteManager.PreviousPageHandler();

		}
}
