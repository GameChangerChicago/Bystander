using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour
{

		public bool isLeo, isJace;
		private MaleGameManager gameManager;
		private Animator animator;	
		// Use this for initialization
		void Start ()
		{
	
				animator = this.GetComponent<Animator> ();
				gameManager = FindObjectOfType<MaleGameManager> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
	
//				if ((isLeo || isJace) && (gameManager.isGameState (GameState.Intro) || gameManager.isGameState (GameState.Outro)))
//						animator.Play ("Idle");
//				else
//						animator.Play ("Walk");
//					

				//Plays the animations for objects that aren't Leo and Jace based on the current Game State
				if (!isLeo || !isJace) {

						switch (gameManager.State ()) {

						case GameState.Intro:
							//	animator.Play (this.name + "_Intro");
								Debug.Log (gameManager.State () + " animation");
								break;
						case GameState.Q1:
								//animator.Play ("Q1");
								Debug.Log (gameManager.State () + " animation");
								break;
						case GameState.Q2:
								//animator.Play ("Q2");
								Debug.Log (gameManager.State () + " animation");
								break;
						case GameState.Q3:
								//animator.Play ("Q3");
								Debug.Log (gameManager.State () + " animation");
								break;
						case GameState.Q4:
								//animator.Play ("Q4");
								Debug.Log (gameManager.State () + " animation");
								break;
						case GameState.Q5:
								//animator.Play ("Q5");
								Debug.Log (gameManager.State () + " animation");
								break;
						case GameState.Q6:
								//animator.Play ("Q6");
								Debug.Log (gameManager.State () + " animation");
								break;
						case GameState.Q7:
								//animator.Play ("Q7");
								Debug.Log (gameManager.State () + " animation");
								break;
						case GameState.Outro:
								//animator.Play ("Outro");
								Debug.Log (gameManager.State () + " animation");
								break;

			
						}

				}
		}
}

