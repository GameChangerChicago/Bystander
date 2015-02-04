using UnityEngine;
using System.Collections;




public class MaleGameManager : MonoBehaviour {

private GUIManager guiManager;
private string correctAnswer;

	//Enum to track game state - Intro, question one, question two etc.
	private enum GameState{
		Intro, 
		Q1, 
		Q2, 
		Q3, 
		Q4, 
		Q5, 
		Q6, 
		Q7, 
		Outro
	};

	//Holds correct answers for each question
	private string[] answerArray	= new string[]{ 
		"BELIEVE", 
		"23%", 
		"2 MILLION",
		"STATE",
		"CONSENT",
		"THREATENED",
		"ALCOHOL",
		"1-800-656-HOPE"
	};


	private GameState gameState;
	private bool questionAnswered;


	void Start(){

		guiManager = FindObjectOfType<GUIManager>();
		gameState = GameState.Intro;
		Invoke ("StartGame", 10f);



	}


	void StartGame(){

		guiManager.IntroText();
		questionAnswered = false;
		gameState = GameState.Q1;
		correctAnswer = "BELIEVE";
		

		}

	//if the player's answer is correct, we change the game state and set the correct answer
	//for the new question
	public void CheckAnswer(string a){

		if (a.Equals (correctAnswer)) {

			questionAnswered = true;

			switch (a) {

				case "BELIEVE":
					Debug.Log ("The Answer is Believe! Moving to next game State");
					correctAnswer = answerArray[1];
					questionAnswered = false;
					gameState = GameState.Q2;
					break;
				case "23%":
					Debug.Log ("The Answer is 23%! Moving to next game State");
					correctAnswer = answerArray[2];
					questionAnswered = false;
					gameState = GameState.Q3;
					break;
				case "2 MILLION":
					Debug.Log ("Correct!  You are so smart");
					correctAnswer = answerArray[3];
					questionAnswered = false;
					//Since this is a two part question, we will not change the game state
					break;
				case "STATE":
					Debug.Log ("Correct! You are really smart");
					correctAnswer = answerArray[4];
					gameState = GameState.Q4;
					break;
				case "CONSENT":
					Debug.Log ("Yep yep!");
					correctAnswer = answerArray[5];
					gameState = GameState.Q5;
					break;
				case "THREATENED":
					Debug.Log ("Uh-huh!");
					correctAnswer = answerArray[6];
					gameState = GameState.Q6;
					break;
				case "ALCOHOL":
					Debug.Log ("Yes~");
					correctAnswer = answerArray[7];
					gameState = GameState.Q7;
					break;
				case "1-800-656-HOPE":
					Debug.Log ("Say what!");
					gameState = GameState.Outro;
					break;
				}

			} else
				Debug.Log ("Wrong Answer. Try Again");

		}




}


