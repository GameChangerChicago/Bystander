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

				if (Event.current.keyCode == KeyCode.Return && Event.current.type == EventType.KeyDown && !questionAnswered) {
						gameManager.CheckAnswer (inputField.ToUpper ());
						Debug.Log ("Test");
                        questionAnswered = true;
				}

                if (Event.current.type == EventType.KeyUp)
                {
                    questionAnswered = false;
                }

				inputField = GUI.TextField (new Rect (475, 300, 150, 30), inputField, 15);




		}
		
		public void IntroText ()
		{
				Debug.Log ("Here is some intro text");
				
		}


}


