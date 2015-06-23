using UnityEngine;
using System.Collections;

public enum AniState
{
	CloseUp,
	
	Profile,
	
	Back,
}
;

public class AnimationManager : MonoBehaviour
{



		public bool isLeo, isJace;
		private MaleGameManager gameManager;
		//private Animator animator;	
		// Use this for initialization

		public GameObject bg_SidewaysScroll,
				bg_SidewaysFlatScroll,
				bg_TrackScroll,
				bg_BackwardsScroll,
				char_LeoAndJace;
		private Animator sideScroll, trackScroll, backwardsScroll, flatSideScroll, LeoJace;
		private GameState gameState;
		private int i;
		private GameObject activeBackground;
		public static AniState aniState;

		void Start ()
		{
	
				gameManager = FindObjectOfType<MaleGameManager> ();
				flatSideScroll = bg_SidewaysFlatScroll.GetComponent<Animator> ();
				sideScroll = bg_SidewaysScroll.GetComponent<Animator> ();
				trackScroll = bg_TrackScroll.GetComponent<Animator> ();
				backwardsScroll = bg_BackwardsScroll.GetComponent<Animator> ();
				LeoJace = char_LeoAndJace.GetComponent<Animator> ();
				aniState = AniState.Profile;
				gameState = gameManager.State ();
				
				i = 0;
		}
	
		// Update is called once per frame
		void Update ()
		{

	
				//Plays the animations for objects that aren't Leo and Jace based on the current Game State
				if (!gameManager.isGameState (gameState)) {

						gameState = gameManager.State ();
						
						i++;


						if (i == 0) {

				aniState = AniState.CloseUp;
								bg_SidewaysScroll.SetActive (false);
								//activeBackground = bg_SidewaysScroll;
								//activeBackground.SetActive(true);
								bg_BackwardsScroll.SetActive (false);
								bg_SidewaysFlatScroll.SetActive (true);
								LeoJace.Play ("ProfileStationary");
				
								



						} else if (i == 1) {

							
				aniState = AniState.Profile;
								bg_SidewaysScroll.SetActive (true);
								LeoJace.Play ("WalkTogether");
								//bg_TrackScroll.SetActive(false);
								bg_BackwardsScroll.SetActive (false);
								bg_SidewaysFlatScroll.SetActive (false);
							



								
						} else if (i == 2) {

				aniState = AniState.Back;
								bg_SidewaysScroll.SetActive (false);
								LeoJace.Play ("WalkingBacks");
								//bg_TrackScroll.SetActive(false);
								bg_BackwardsScroll.SetActive (true);
								bg_SidewaysFlatScroll.SetActive (false);
								



						} else
								i = 0;

			
				}

		//Debug.Log (aniState);
		}


	public AniState State(){

		return aniState;
		}

	public bool isAniState(AniState state){

		if (aniState == state)
						return true;
				else
						return false;

}

}



