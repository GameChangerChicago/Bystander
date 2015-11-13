using UnityEngine;
using System.Collections;

public class DialogueHandler: MonoBehaviour
{
	
	
	
		public bool isLeo, isJace;
		public TextMesh textMesh;
		public GameObject  dialogueBox;
		public Transform[] dialogueBoxPosition, dialogueBoxPositionBack;
		private AnimationManager animationManager;
		private AniState aniState;
		public float TextBounds, y_TextBounds;
		private MaleGameManager gameManager;
		private string dialogue;
		private int dialogueIndex;
		public int numDialogueLines, numDialogueLinesShown;
		private GameState gameState;
		public float TextBoxBounds;
		private int dialogue_position;
		public SpriteRenderer questionSprite, dialogueSprite, virgilDialogueSprite;
		
	
	
		//leo_dialogueBox, jace_dialogueBox,leo_textMesh, jace_textMesh;
	
		void Start ()
		{
		
		
				gameManager = FindObjectOfType<MaleGameManager> ();
				animationManager = FindObjectOfType<AnimationManager> ();
				aniState = animationManager.State ();
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
						StartCoroutine (gameManager.StartGame ());
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
		
				if (!animationManager.isAniState (aniState))
						aniState = animationManager.State ();

		
		
				if (gameManager.isGameState (GameState.Outro) && numDialogueLinesShown == numDialogueLines)
						StartCoroutine (gameManager.EndGame ());

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
											
										if (animationManager.State () == AniState.Back) {
												dialogueBox.transform.position = dialogueBoxPosition [4].position;
										} else if (animationManager.State () == AniState.Profile) {
												dialogueBox.transform.position = dialogueBoxPosition [0].position;
										} else if (animationManager.State () == AniState.CloseUp) {
												dialogueBox.transform.position = dialogueBoxPosition [6].position;
										}
						
										
										dialogueSprite.enabled = true;
										questionSprite.enabled = false;
										virgilDialogueSprite.enabled = false;
										dialogueBox.GetComponent<TextMesh> ().color = Color.black;
										//dialogueBox.GetComponent<TextMesh> ().fontSize = 40;
										//TextBounds = 5;
					
										i++;
								} else if (dialogue [i] == '\\') {

										if (animationManager.State () == AniState.Back) {
												dialogueBox.transform.position = dialogueBoxPosition [5].position;
										} else if (animationManager.State () == AniState.Profile || animationManager.State () == AniState.CloseUp) {
												dialogueBox.transform.position = dialogueBoxPosition [1].position;
										}

										
										dialogueSprite.enabled = true;
										questionSprite.enabled = false;
										virgilDialogueSprite.enabled = false;
										dialogueBox.GetComponent<TextMesh> ().color = Color.black;
										//dialogueBox.GetComponent<TextMesh> ().fontSize = 40;
										//TextBounds = 5;

										i++;
								} else  if (dialogue [i] == '@') {
										dialogueBox.transform.position = dialogueBoxPosition [3].position;
										//	dialogueBox.GetComponent<TextMesh> ().fontSize = 40;
										TextBounds = 7;
										dialogueSprite.enabled = false;
										questionSprite.enabled = true;

										i++;
								} else if (dialogue [i] == '$') {
										dialogueBox.transform.position = dialogueBoxPosition [2].position;
										dialogueSprite.enabled = false;
										virgilDialogueSprite.enabled = true;
										dialogueBox.GetComponent<TextMesh> ().color = Color.white;
										//dialogueBox.GetComponent<TextMesh> ().fontSize = 40;
										//TextBounds = 5;
										i++;

								}

								//dialogueIndex = i + 1;
								displayText += dialogue [i];


								
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
								
								dialogueSprite.GetComponent<Transform> ().localScale = new Vector3 (currentRenderer.bounds.extents.x + 1f, currentRenderer.bounds.extents.y + TextBoxBounds, currentRenderer.bounds.extents.z);
								virgilDialogueSprite.GetComponent<Transform> ().localScale = new Vector3 (currentRenderer.bounds.extents.x + 1f, currentRenderer.bounds.extents.y + TextBoxBounds, currentRenderer.bounds.extents.z);

								//dialogueSprite.GetComponent<Transform>().localPosition = //dialogueBox.transform.bounds.center; //new Vector3 (currentRenderer.bounds.center.x, currentRenderer.bounds.center.y, currentRenderer.bounds.center.z);
								
								//dialogueSprite.GetComponenet<Transform>().
								

						
				
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
				
				
				
				//	}
		
		
		
		}

		void DefaultText ()
		{
				dialogueBox.GetComponent<TextMesh> ().fontSize = 30;
				dialogueSprite.enabled = true;
				questionSprite.enabled = false;
				dialogueBox.GetComponent<TextMesh> ().color = Color.black;



		}

}


