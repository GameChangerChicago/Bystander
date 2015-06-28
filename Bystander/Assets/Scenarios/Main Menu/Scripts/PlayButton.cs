using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {

	public GameObject regularSprite, selectedSprite;
	private HubWorldManager hb_Manager;
	private HB_GameState gameState;
	public string level;

	// Use this for initialization
	void Start () {

		hb_Manager = FindObjectOfType<HubWorldManager> ();
		//gameState = hb_Manager.isGameState();
		regularSprite.GetComponent<SpriteRenderer>().enabled = true;
		selectedSprite.GetComponent<SpriteRenderer>().enabled = false;



	}


	void OnMouseOver(){

		selectedSprite.GetComponent<SpriteRenderer>().enabled = true;
		Debug.Log ("WTF 2");

		}

	void OnMouseExit(){

		selectedSprite.GetComponent<SpriteRenderer>().enabled = false;
		Debug.Log ("WTF 3");

	}

	void OnMouseDown(){

		Application.LoadLevel (level);
		//Application.LoadLevel (hb_Manager.NextLevel());
		//hb_Manager.prepNextLevel ();
	}

}