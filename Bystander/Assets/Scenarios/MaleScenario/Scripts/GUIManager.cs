using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class GUIManager : MonoBehaviour
{

		private MaleGameManager gameManager;
		private string inputField;
		private bool questionAnswered,
                     textFieldActive;
		private int screenWidth, screenHeight;
		public GUISkin maleSkin;
        public bool ShowTextBar = true;


	void Start ()
	{
		screenWidth = Screen.width / 2;
		screenHeight = Screen.height / 2;
		gameManager = FindObjectOfType<MaleGameManager> ();
		inputField = "";
		questionAnswered = false;


	}


    void OnGUI()
    {
        GUI.skin = maleSkin;

        //		GUI.SetNextControlName("inputField");
        //		if (UnityEngine.Event.current.type == EventType.Repaint)
        //				if (GUI.GetNameOfFocusedControl == "inputField")
        //						inputField = "";



        if (!gameManager.isGameState(GameState.Intro) && !gameManager.isGameState(GameState.Outro))
        {
            if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Mouse0) && !Input.GetKeyDown(KeyCode.Escape))
                GUI.FocusControl("InputField");
            if (Event.current.keyCode == KeyCode.Return && Event.current.type == EventType.KeyDown && !questionAnswered)
            {
                Debug.Log(inputField);
//			if(gameManager.isGameState(GameState.Q8)){
					string m_inputField = Regex.Replace(inputField, @"\W+" , "");
                Debug.Log(m_inputField);
					gameManager.CheckAnswer(m_inputField.ToUpper());
					inputField = "";
					questionAnswered = true;

//				}else{
//
//
//					gameManager.CheckAnswer(inputField.ToUpper());
//					inputField = "";
//					questionAnswered = true;
//					//					if(inputField.ToUpper()== "656 HOPE" || inputField.ToUpper() == "656-HOPE" || inputField.ToUpper() == "656HOPE")
//					//						gameManager.CheckAnswer("656HOPE");
//					//						inputField = "";
//					//						questionAnswered = true;
//					
//				}
//

            }

            if (Event.current.type == EventType.KeyUp)
            {
                questionAnswered = false;
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
                textFieldActive = true;

            if (textFieldActive && ShowTextBar)
            {
                GUI.SetNextControlName("InputField");
                inputField = GUI.TextField(new Rect(screenWidth - 200, screenHeight - 45, 250, 50), inputField, 15);
            }
        }
    }


}




