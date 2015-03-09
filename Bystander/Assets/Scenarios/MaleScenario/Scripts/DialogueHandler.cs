using UnityEngine;
using System.Collections;

public class DialogueHandler : MonoBehaviour
{
	
	
	
		public bool isLeo, isJace;
		public TextMesh textMesh;
		public GameObject  DialogueBox;
		public Transform[] DialogueBoxPosition;
		public float TextBounds;
		private MaleGameManager gameManager;
		private string l_dialogue, j_dialogue, dialogue;
		private int dialogueIndex;
		public int numDialogueLines, numDialogueLinesShown;
		private GameState gameState;
		private int dialogue_position;
		
	
		//leo_dialogueBox, jace_dialogueBox,leo_textMesh, jace_textMesh;
	
		void Start ()
		{

				
				gameManager = FindObjectOfType<MaleGameManager> ();
				gameState = gameManager.State ();
				dialogueIndex = 0;
				dialogue = "";
				int dialogue_position = 0;
				numDialogueLinesShown = 0;
				numDialogueLines = 0;
				
				LoadTxtFile ();
				FindNumLines ();
				DialoguePopUp ();
		
		}
	
		void Update ()
		{

				if (gameManager.isGameState (GameState.Intro) && numDialogueLinesShown == numDialogueLines)
						//StartCoroutine(gameManager.StartGame (3));
						gameManager.StartGame ();
				//Debug.Log ("Inside if statement");

				if (gameManager.State () != gameState) {
						gameState = gameManager.State ();
						//DialogueBox.GetComponent<TextMesh> ().text= " ";
						numDialogueLines = 0;
						numDialogueLinesShown = 0;
						dialogueIndex = 0;
						LoadTxtFile ();
						FindNumLines ();
						DialoguePopUp ();

					
				}

			
					
			
			
				
		}
		
		void OnMouseDown ()
		{
			
			
				if (numDialogueLinesShown < numDialogueLines) {
						DialoguePopUp ();

				}
				
				


		}

		private	void FindNumLines ()
		{
				for (int i = 0; i < dialogue.Length; i++) {
						if (dialogue [i] == '*')
								numDialogueLines++;
				}
		}

		private void DialoguePopUp ()
		{


//		if(dialogue_position == 2)
//			dialogue_position = 0;
				
				string displayText = "";
				for (int i = dialogueIndex; i < dialogue.Length; i++) {
						if (dialogue [i] != '*')
								displayText += dialogue [i];
						else {
							dialogueIndex  = i + 1;
							//DialogueBox.transform.position = DialogueBoxPosition [1].position;

				if (dialogue [dialogueIndex] == '|') {
//										DialogueBox.transform.position = DialogueBoxPosition [1].position;
										dialogueIndex++;
								}
				else if (dialogue [dialogueIndex] == '\\') {
										DialogueBox.transform.position = DialogueBoxPosition [0].position;
										dialogueIndex++;
								} 
										numDialogueLinesShown++;
										StringFormatter (displayText);


								
					
									//	Debug.Log (dialogue_position);
							//


										break;
								}
						



				}
				//dialogue_position++;
		}

		private void StringFormatter (string lineContent)
		{
				string currentWord = "";
				bool isFirstWord = true;
				Renderer currentRenderer;
		
				currentRenderer = DialogueBox.GetComponent<Renderer> ();
				DialogueBox.GetComponent<TextMesh> ().text = currentWord;
		
				for (int i = 0; i < lineContent.Length; i++) {
						//As long as the char isn't a ' ' then it will be added to currentWord
						if (lineContent [i] != ' ') {
								currentWord = currentWord + lineContent [i];
						} else {
								//If currentWord is the first word it is added to the to the dialog box
								if (isFirstWord) {
										DialogueBox.GetComponent<TextMesh> ().text = DialogueBox.GetComponent<TextMesh> ().text + currentWord;
					
										isFirstWord = false;
								} else { // Otherwise it adds the current word with a space before the word.
										DialogueBox.GetComponent<TextMesh> ().text = DialogueBox.GetComponent<TextMesh> ().text + " " + currentWord;
								}
				
								//If after adding the word the line extends past the TextBounds then the word will be added with a line break
								if (currentRenderer.bounds.extents.x > TextBounds) {
										DialogueBox.GetComponent<TextMesh> ().text = DialogueBox.GetComponent<TextMesh> ().text.Remove (DialogueBox.GetComponent<TextMesh> ().text.Length - (currentWord.Length + 1));
										DialogueBox.GetComponent<TextMesh> ().text = DialogueBox.GetComponent<TextMesh> ().text + "\n" + currentWord;
								}
				
								//Resets the current word each time
								currentWord = "";
						}
				}
		}

		public void LoadTxtFile ()
		{
				string objName = "";
				
				//dialogueIndex = 0;
				if (isLeo || isJace) {
			
						objName = this.name;
						
			
						
				}


				TextAsset scenarioText = Resources.Load ("MaleScenarioText/" + objName + "_" + gameState.ToString ()) as TextAsset;
				dialogue = scenarioText.text;
					

				Debug.Log (dialogue);

//	}
	


		}
}

