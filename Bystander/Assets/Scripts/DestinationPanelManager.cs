using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestinationPanelManager : MonoBehaviour {

	private MaleGameManager gameManager; 
	private GameState gameState;
	public List<GameObject> blackBox;
	public float alphaTime;
	private int i;

	// Use this for initialization
	void Start () {

		gameManager = FindObjectOfType<MaleGameManager> ();
		gameState = gameManager.State ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	

			if (!gameManager.isGameState(GameState.Intro) && !gameManager.isGameState(GameState.Q1) && !gameManager.isGameState (gameState)) {
								gameState = gameManager.State ();
								i = Random.Range (0, blackBox.Count);
								StartCoroutine("FadeBox");
						
				}


	}


	IEnumerator FadeBox ()
	{

		//blackBox[i].GetComponent<SpriteRenderer>().color = new Color (0,0,0, blackBox[i].GetComponent<SpriteRenderer>().color.a - (2f * Time.deltaTime));

		float alpha = blackBox [i].GetComponent<SpriteRenderer> ().color.a;
		Debug.Log (alpha);
		for (float t = 0f; t < 1.0f; t += Time.deltaTime/ alphaTime ) {
						Color c = new Color (0, 0, 0, Mathf.Lerp(alpha, 0, t));
						blackBox[i].GetComponent<SpriteRenderer>().color = c;
						yield return null; 
				}

		blackBox.RemoveAt (i);
		//Destroy (blackBox[i]);


	}
}
