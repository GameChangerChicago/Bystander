using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	private MaleGameManager gameManager;
	private Camera camera;
	private bool isLerping; 
	private float defaultSize, introSize, camTravelSpeed;
	private Vector3 defaultPosition, introCamPosition, startPosition;


	// Use this for initialization
	void Start () {

		gameManager = FindObjectOfType<MaleGameManager>();
		camera = Camera.main;

		isLerping = false;

		defaultSize = 22.25f;
		introSize = 10f;
		camTravelSpeed = 4f;

		introCamPosition = new Vector3 (-19, 11, -20);
		defaultPosition =  new Vector3 (0,0, -20);


		camera.transform.position = introCamPosition;
		camera.orthographicSize = introSize;
	
	}
	
	// Update is called once per frame
	void Update () {
	
		MoveCamera();
	}


	//
	void MoveCamera(){

		isLerping = true;
		startPosition = camera.transform.position;	
		Invoke("StopCamera", camTravelSpeed);
	}


	void FixedUpdate(){

		if(gameManager.isGameState(GameState.Q1) && isLerping){
			camera.transform.position = Vector3.Lerp(startPosition, defaultPosition, Time.deltaTime);

			if(camera.orthographicSize < defaultSize)
				camera.orthographicSize += (defaultSize - introSize) / (camTravelSpeed / Time.deltaTime);
		}

		if(gameManager.isGameState (GameState.Outro) && isLerping){

			camera.transform.position = Vector3.Lerp (startPosition, introCamPosition, 3f*Time.deltaTime);

			if(camera.orthographicSize > introSize)
				camera.orthographicSize -=(defaultSize - introSize) / (1f/ Time.deltaTime);

		}



	}

	void StopCamera(){
		isLerping = false;
	}
}
