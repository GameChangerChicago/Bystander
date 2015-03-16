using UnityEngine;
using System.Collections;
using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
	
		private MaleGameManager gameManager;
		private Camera camera;
		private bool isLerping;
		private float defaultSize, introSize, camTravelSpeed;
		private Vector3 defaultPosition, introCamPosition, startPosition;
	
	
		// Use this for initialization
		void Start ()
		{
		
				gameManager = FindObjectOfType<MaleGameManager> ();
				camera = Camera.main;
		
				
		
		}

	
		//
		public void MoveCamera ()
		{
//		
				
						camera.GetComponent<Animator> ().Play ("TrackOut");
			Debug.Log("Hello?");
				

				if (gameManager.isGameState (GameState.Outro))
						camera.GetComponent<Animator> ().Play ("TrackInLocation");

		}
	
	

}

