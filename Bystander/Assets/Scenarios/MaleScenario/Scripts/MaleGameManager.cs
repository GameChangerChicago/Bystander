using UnityEngine;
using System.Collections;

//Enum to track game state - Intro, question one, question two etc.
//Moved for global accessability
public enum GameState
{
		Intro, 
		Q1, 
		Q2, 
		Q3, 
		Q4, 
		Q5, 
		Q6, 
		Q7,
		Q8,
		Outro,
}
;

public class MaleGameManager : MonoBehaviour
{

		
		private string correctAnswer;
		private bool questionAnswered;
		public static GameState gameState;
		private CameraManager cameraManager;
	private SoundManager soundManager;
    private GameManager _gameManager;

		//MASON NOTE: This array is redundant. In the switch you can just have correctAnswer = a. I left a commented out example below in the "BELIEVE" case.
		//Holds correct answers for next question asked
		private string[] answerArray = new string[]{ 
		"BELIEVE", 
		"23",
		"STATE",
		"CONSENT",
		"THREATENED",
		"ALCOHOL",
		"TRUSTED",
		"656HOPE"
	};

		void Start ()
		{
            _gameManager = FindObjectOfType<GameManager>();
				gameState = GameState.Intro;
				cameraManager = FindObjectOfType<CameraManager> ();
				soundManager = FindObjectOfType<SoundManager> ();
				//correctAnswer = answerArray[0];
				
            if (cameraManager.camera.aspect < 1.61f)
                cameraManager.GetComponent<Animator>().Play("CameraIntroMac");
		}

		public IEnumerator StartGame ()
		{

		yield return new WaitForSeconds (3);
            //Mason: This looks at the aspect ratio and calls the correct animation accordingly
        if (cameraManager.camera.aspect < 1.61f)
            cameraManager.GetComponent<Animator>().Play("TrackOutMac");
        else
            cameraManager.GetComponent<Animator>().Play("TrackOut");
				
		//waiting for the camera animation to stop	
		yield return new WaitForSeconds (5);
				
				questionAnswered = false;
				gameState = GameState.Q1;
				correctAnswer = answerArray[0];

		}

	public IEnumerator EndGame()
	{

		yield return new WaitForSeconds (5);
		cameraManager.GetComponent<Animator> ().Play ("TrackInLocation");

		yield return new WaitForSeconds (5);
			//gameState = GameState.Outro;


        StartCoroutine(_gameManager.LoadingHandler("PostMale"));

		}

		public bool isGameState (GameState state)
		{

				if (gameState == state)
						return true;
				else
						return false;
	
		}

		public GameState State ()
		{
				return gameState;
		}

		public bool isAnswered ()
		{
				return questionAnswered;
		}

		
		//if the player's answer is correct, we change the game state and set the correct answer
		//for the new question
		public void CheckAnswer (string a)
		{

				if (a.Equals(correctAnswer)) {

						questionAnswered = true;

						switch (a) {

						case "BELIEVE":
								
								//correctAnswer = a;
								gameState = GameState.Q2;				
								correctAnswer = answerArray [1];
								questionAnswered = false;
								
								//Debug.Log (correctAnswer);
								break;
						case "23":
								
								correctAnswer = answerArray [2];
								questionAnswered = false;
								gameState = GameState.Q3;
								//Debug.Log (correctAnswer);
								break;
//						case "2 MILLION":
//								Debug.Log ("Correct!  You are so smart");
//								correctAnswer = answerArray [3];
//								questionAnswered = false;
//								gameState = GameState.Q4;
//								//Debug.Log (correctAnswer);
//								break;
						case "STATE":
								
								correctAnswer = answerArray [3];
								gameState = GameState.Q4;
								//Debug.Log (correctAnswer);
								break;
						case "CONSENT":
								
								correctAnswer = answerArray [4];
								gameState = GameState.Q5;
								//Debug.Log (correctAnswer);
								break;
						case "THREATENED":
								
								correctAnswer = answerArray [5];
								gameState = GameState.Q6;
								//Debug.Log (correctAnswer);
								break;
						case "ALCOHOL":
								
								correctAnswer = answerArray [6];
								gameState = GameState.Q7;
								//Debug.Log (correctAnswer);
								break;
						case "TRUSTED":
								
								correctAnswer = answerArray [7];
								gameState = GameState.Q8;
								//Debug.Log (correctAnswer);
								break;
						case "656HOPE":	
								
								gameState = GameState.Outro;
							//	cameraManager.GetComponent<Animator> ().Play ("TrackInLocation");
								//Debug.Log (correctAnswer);
								break;
					
			}
			
		} else {
			Debug.Log ("Wrong Answer. Try Again");
						questionAnswered = false;
						soundManager.PlayVirgilIncorrect();
						
				}
						

		}

		



}


