using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour
{

		private MaleGameManager gameManager;
		private string inputField;
		private bool questionAnswered;
		private int screenWidth, screenHeight;


	void Start ()
	{
		screenWidth = Screen.width / 2;
		screenHeight = Screen.height / 2;
		gameManager = FindObjectOfType<MaleGameManager> ();
		inputField = "Answer";
		questionAnswered = false;
	}


	void OnGUI ()
	{


		if(!gameManager.isGameState(GameState.Intro) && !gameManager.isGameState(GameState.Outro))
		{
			if (Event.current.keyCode == KeyCode.Return && Event.current.type == EventType.KeyDown && !questionAnswered) {
					gameManager.CheckAnswer (inputField.ToUpper ());
					inputField = "";
				    questionAnswered = true;
					


			}

        if (Event.current.type == EventType.KeyUp)
        {
            questionAnswered = false;

        }


			inputField = GUI.TextField (new Rect (screenWidth - 120, screenHeight - 45 , 125, 30), inputField, 15);
		}
	}

	void Update(){




	}
	
	private void IntroText ()
	{
			Debug.Log ("Here is some intro text");
			
	}


}


