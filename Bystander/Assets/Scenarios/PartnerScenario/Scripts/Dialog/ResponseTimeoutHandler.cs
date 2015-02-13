using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class ResponseTimeoutHandler : MonoBehaviour
{
	private void OnConversationTimeout()
	{
		//This is where custom methods located in the character can be called
		//DialogueManager.CurrentConversant.gameObject.GetComponent<NPCScript>();
	}
}
