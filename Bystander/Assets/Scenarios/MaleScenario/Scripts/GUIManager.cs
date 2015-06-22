using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour
{

		private MaleGameManager gameManager;
		private string inputField;
		private bool questionAnswered;
		private int screenWidth, screenHeight;
		public GUISkin maleSkin;


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
		GUI.skin = maleSkin;

//		GUI.SetNextControlName("inputField");
//		if (UnityEngine.Event.current.type == EventType.Repaint)
//				if (GUI.GetNameOfFocusedControl == "inputField")
//						inputField = "";



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

        if (Input.GetKeyDown(KeyCode.Mouse0))
            inputField = GUI.TextField(new Rect(screenWidth - 200, screenHeight - 45, 250, 50), inputField, 15);
		}
	}


}




