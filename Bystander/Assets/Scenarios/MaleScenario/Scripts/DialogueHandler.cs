using UnityEngine;
using System.Collections;

public class DialogueHandler : MonoBehaviour
{
	
	
	
	public bool isLeo, isJace;
	public TextMesh textMesh;
	public GameObject  DialogueBox;
	public float TextBounds;
	private MaleGameManager gameManager;
	private string l_dialogue, j_dialogue, dialogue;
	
	//leo_dialogueBox, jace_dialogueBox,leo_textMesh, jace_textMesh;
	
	void Start ()
	{
		
		gameManager = FindObjectOfType<MaleGameManager> ();
		
				
		string objName = "";
		string stateName = "";
		
		if (isLeo || isJace) {
			
			objName = this.name;
			stateName = gameManager.State().ToString();
			
			Debug.Log (objName + "_" +stateName);
		}
		
			TextAsset scenarioText = Resources.Load ("MaleScenarioText/" + objName + "_" + stateName) as TextAsset;
		
			dialogue = scenarioText.text;
		
	}
	
	void Update ()
	{
		
		if(gameManager.isGameState(GameState.Intro)){

			StringFormatter(dialogue);
			DialogueBox.GetComponent<Renderer> ().enabled = true;
			DialogueBox.GetComponentInChildren<SpriteRenderer> ().enabled = true;


			
		}
	}

	private void StringFormatter(string lineContent)
	{
		string currentWord = "";
		bool isFirstWord = true;
		Renderer currentRenderer;
		
		currentRenderer = DialogueBox.GetComponent<Renderer>();
		DialogueBox.GetComponent<TextMesh>().text = currentWord;
		
		for (int i = 0; i < lineContent.Length; i++)
		{
			//As long as the char isn't a ' ' then it will be added to currentWord
			if (lineContent[i] != ' ')
			{
				currentWord = currentWord + lineContent[i];
			}
			else
			{
				//If currentWord is the first word it is added to the to the dialog box
				if (isFirstWord)
				{
					DialogueBox.GetComponent<TextMesh>().text = DialogueBox.GetComponent<TextMesh>().text + currentWord;
					
					isFirstWord = false;
				}
				else // Otherwise it adds the current word with a space before the word.
				{
					DialogueBox.GetComponent<TextMesh>().text = DialogueBox.GetComponent<TextMesh>().text + " " + currentWord;
				}
				
				//If after adding the word the line extends past the TextBounds then the word will be added with a line break
				if (currentRenderer.bounds.extents.x > TextBounds)
				{
					DialogueBox.GetComponent<TextMesh>().text = DialogueBox.GetComponent<TextMesh>().text.Remove(DialogueBox.GetComponent<TextMesh>().text.Length - (currentWord.Length + 1));
					DialogueBox.GetComponent<TextMesh>().text = DialogueBox.GetComponent<TextMesh>().text + "\n" + currentWord;
				}
				
				//Resets the current word each time
				currentWord = "";
			}
		}
	}
	


}

