using UnityEngine;
using System.Collections;

public class DialogueHandler: MonoBehaviour
{
	
	
	
		public bool isLeo, isJace;
		public TextMesh textMesh;
		public GameObject  dialogueBox;
		public Transform[] dialogueBoxPosition;
		public float TextBounds;
		private MaleGameManager gameManager;
		private string dialogue;
		private int dialogueIndex;
		public int numDialogueLines, numDialogueLinesShown;
		private GameState gameState;
		private int dialogue_position;
		public SpriteRenderer questionSprite, dialogueSprite, virgilDialogueSprite;
		
	
	
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
				//questionBox = dialogueBox.;
		
		}
	
		void Update ()
		{
		
				if (gameManager.isGameState (GameState.Intro) && numDialogueLinesShown == numDialogueLines)
						StartCoroutine(gameManager.StartGame());
						//gameManager.StartGame ();
						
		
				if (gameManager.State () != gameState) {
						gameState = gameManager.State ();
						numDialogueLines = 0;
						numDialogueLinesShown = 0;
						dialogueIndex = 0;
						LoadTxtFile ();
						FindNumLines ();
						DialoguePopUp ();
			
			
				}
		
				Debug.Log ("Inside Update with Dialogue Length: " + dialogue.Length);
				Debug.Log ("Inside Dialogue Update Function: " + dialogueIndex);
		
		
		
		
		}
	
		void OnMouseDown ()
		{
				//dialogueSprite.enabled = false;

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
		

				string displayText = "";
				for (int i = dialogueIndex; i < dialogue.Length; i++) {
						if (dialogue [i] != '*') {
								
								if (dialogue [i] == '|') {
										dialogueBox.transform.position = dialogueBoxPosition [0].position;
										dialogueSprite.enabled = true;
										questionSprite.enabled = false;
										virgilDialogueSprite.enabled = false;
										dialogueBox.GetComponent<TextMesh> ().color = Color.black;
										dialogueBox.GetComponent<TextMesh> ().fontSize = 40;
					TextBounds = 5;
					
										i++;
								} else if (dialogue [i] == '\\') {
										dialogueBox.transform.position = dialogueBoxPosition [1].position;
										dialogueSprite.enabled = true;
										questionSprite.enabled = false;
										virgilDialogueSprite.enabled = false;
										dialogueBox.GetComponent<TextMesh> ().color = Color.black;
										dialogueBox.GetComponent<TextMesh> ().fontSize = 40;
					TextBounds = 5;

										i++;
								} else  if (dialogue [i] == '@') {
										dialogueBox.transform.position = dialogueBoxPosition [3].position;
										dialogueBox.GetComponent<TextMesh>().fontSize = 45;
										TextBounds = 7;
										dialogueSprite.enabled = false;
										questionSprite.enabled = true;

										i++;
								} else if (dialogue [i] == '$') {
										dialogueBox.transform.position = dialogueBoxPosition [2].position;
										dialogueSprite.enabled = false;
										virgilDialogueSprite.enabled = true;
										dialogueBox.GetComponent<TextMesh> ().color = Color.white;
										dialogueBox.GetComponent<TextMesh> ().fontSize = 40;
					TextBounds = 5;
										i++;

								}

								//dialogueIndex = i + 1;
								displayText += dialogue [i];


								Debug.Log ("Inside Dialogue PopUp: " + dialogueIndex);
						} else {

								dialogueIndex = i + 1;
								numDialogueLinesShown++;
								StringFormatter (displayText);
								break;
				
				
				

						}
			
			
			
			
				}
				
		}
	
		private void StringFormatter (string lineContent)
		{
				string currentWord = "";
				bool isFirstWord = true;
				Renderer currentRenderer;
		
				currentRenderer = dialogueBox.GetComponent<Renderer> ();
				dialogueBox.GetComponent<TextMesh> ().text = currentWord;
		
				for (int i = 0; i < lineContent.Length; i++) {
						//As long as the char isn't a ' ' then it will be added to currentWord
						if (lineContent [i] != ' ') {
								currentWord = currentWord + lineContent [i];
						} else {
								//If currentWord is the first word it is added to the to the dialog box
								if (isFirstWord) {
										dialogueBox.GetComponent<TextMesh> ().text = dialogueBox.GetComponent<TextMesh> ().text + currentWord;
					
										isFirstWord = false;
								} else { // Otherwise it adds the current word with a space before the word.
										dialogueBox.GetComponent<TextMesh> ().text = dialogueBox.GetComponent<TextMesh> ().text + " " + currentWord;
								}
				
								//If after adding the word the line extends past the TextBounds then the word will be added with a line break
								if (currentRenderer.bounds.extents.x > TextBounds) {
										dialogueBox.GetComponent<TextMesh> ().text = dialogueBox.GetComponent<TextMesh> ().text.Remove (dialogueBox.GetComponent<TextMesh> ().text.Length - (currentWord.Length + 1));
										dialogueBox.GetComponent<TextMesh> ().text = dialogueBox.GetComponent<TextMesh> ().text + "\n" + currentWord;
								}
				
								//Resets the current word each time
								currentWord = "";
						}
				}
		}
	
		public void LoadTxtFile ()
		{


				dialogueSprite.enabled = true;
				questionSprite.enabled = false;
				virgilDialogueSprite.enabled = false;
				TextAsset scenarioText = Resources.Load ("MaleScenarioText/" + gameState.ToString ()) as TextAsset;
				
				dialogue = scenarioText.text;
				
				
				Debug.Log (dialogue);
		
				//	}
		
		
		
		}

	void DefaultText()
	{
		dialogueBox.GetComponent<TextMesh>().fontSize = 30;
		dialogueSprite.enabled = true;
		questionSprite.enabled = false;
		dialogueBox.GetComponent<TextMesh> ().color = Color.black;



		}

}


