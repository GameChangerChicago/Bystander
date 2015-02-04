using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour
{

		private MaleGameManager gameManager;

		
		private string inputField;
		private bool questionAnswered;

		void Start ()
		{
			gameManager = FindObjectOfType<MaleGameManager> ();
			inputField = "Answer";
			questionAnswered = false;
		}

		void OnGUI ()
		{
			inputField = GUI.TextField (new Rect (475, 300, 150, 30), inputField, 15);

			//If the player presses enter, check if their answer is correct
			if (Event.current.keyCode == KeyCode.Return) {
					gameManager.CheckAnswer(inputField.ToUpper());
						Debug.Log ("Test");			
					}
			

			}
	

		public void IntroText ()
		{
				Debug.Log ("Here is some intro text");
				
		}


}


