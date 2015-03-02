using UnityEngine;
using System.Collections;

public class BGOffset : MonoBehaviour
{

		private float scrollSpeed;
		private MaleGameManager gameManager;

		void Start ()
		{
				gameManager = FindObjectOfType<MaleGameManager> ();

		}
	
		void Update ()
		{

				if (gameManager.isGameState (GameState.Intro) || gameManager.isGameState (GameState.Outro)) 
						scrollSpeed = 0;
				else 
						scrollSpeed = .05f;


				float x = Mathf.Repeat (Time.time * scrollSpeed, 1);
				Vector2 offset = new Vector2 (x, 0);
				renderer.sharedMaterial.SetTextureOffset ("_MainTex", offset);
		}
				
		
	

}