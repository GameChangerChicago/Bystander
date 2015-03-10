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
		Outro
}
;

public class MaleGameManager : MonoBehaviour
{

		
		private string correctAnswer;
		private bool questionAnswered;
		public static GameState gameState;
		private CameraManager cameraManager;

		//MASON NOTE: This array is redundant. In the switch you can just have correctAnswer = a. I left a commented out example below in the "BELIEVE" case.
		//Holds correct answers for next question asked
		private string[] answerArray = new string[]{ 
		"BELIEVE", 
		"23%", 
		"2 MILLION",
		"STATE",
		"CONSENT",
		"THREATENED",
		"ALCOHOL",
		"1-800-656-HOPE"
	};

		void Start ()
		{
				gameState = GameState.Intro;
				cameraManager = FindObjectOfType<CameraManager> ();
				//Invoke ("StartGame", 5f);
		}

		public void StartGame ()
		{
				questionAnswered = false;
				gameState = GameState.Q1;
				correctAnswer = "BELIEVE";

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

				if (a.Equals (correctAnswer)) {

						questionAnswered = true;

						switch (a) {

						case "BELIEVE":
								Debug.Log ("The Answer is Believe! Moving to next game State");
								//correctAnswer = a;
								correctAnswer = answerArray [1];
								questionAnswered = false;
								gameState = GameState.Q2;
								Debug.Log (correctAnswer);
								break;
						case "23%":
								Debug.Log ("The Answer is 23%! Moving to next game State");
								correctAnswer = answerArray [2];
								questionAnswered = false;
								gameState = GameState.Q3;
								Debug.Log (correctAnswer);
								break;
						case "2 MILLION":
								Debug.Log ("Correct!  You are so smart");
								correctAnswer = answerArray [3];
								questionAnswered = false;
								gameState = GameState.Q4;
								Debug.Log (correctAnswer);
								break;
						case "STATE":
								Debug.Log ("Correct! You are really smart");
								correctAnswer = answerArray [4];
								gameState = GameState.Q5;
								Debug.Log (correctAnswer);
								break;
						case "CONSENT":
								Debug.Log ("Yep yep!");
								correctAnswer = answerArray [5];
								gameState = GameState.Q6;
								Debug.Log (correctAnswer);
								break;
						case "THREATENED":
								Debug.Log ("Uh-huh!");
								correctAnswer = answerArray [6];
								gameState = GameState.Q7;
								Debug.Log (correctAnswer);
								break;
						case "ALCOHOL":
								Debug.Log ("Yes~");
								correctAnswer = answerArray [7];
								gameState = GameState.Q8;
								Debug.Log (correctAnswer);
								break;
						case "1-800-656-HOPE":
								Debug.Log ("Say what!");
								gameState = GameState.Outro;
								Debug.Log (correctAnswer);
								break;
						}

				} else
						Debug.Log ("Wrong Answer. Try Again");

		}

		



}


