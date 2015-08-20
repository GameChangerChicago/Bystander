using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {

	public GameObject regularSprite, selectedSprite;
    private CursorHandler _cursorHandler;
	private HubWorldManager hb_Manager;
	private HB_GameState gameState;
	public string level;

	// Use this for initialization
	void Start () {
        _cursorHandler = FindObjectOfType<CursorHandler>();
		hb_Manager = FindObjectOfType<HubWorldManager> ();
		//gameState = hb_Manager.isGameState();
		regularSprite.GetComponent<SpriteRenderer>().enabled = true;
		selectedSprite.GetComponent<SpriteRenderer>().enabled = false;



	}


	void OnMouseOver(){
        _cursorHandler.ChangeCursor(0);
		selectedSprite.GetComponent<SpriteRenderer>().enabled = true;
		Debug.Log ("WTF 2");

		}

	void OnMouseExit(){
        _cursorHandler.ChangeCursor(1);
		selectedSprite.GetComponent<SpriteRenderer>().enabled = false;
		Debug.Log ("WTF 3");

	}

	void OnMouseDown(){

		if (level == "CLOSE")
						Application.Quit ();
		else
			Application.LoadLevel (level);
		//Application.LoadLevel (hb_Manager.NextLevel());
		//hb_Manager.prepNextLevel ();
	}

}