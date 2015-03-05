using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.UnityGUI;

public class DoubleDialogueUI : UnityDialogueUI {

	public UnityDialogueControls uiSet2;

	private void Update () {
		if (Input.GetKeyDown(KeyCode.X)) {
			Close(); // Close the current UI elements.
			dialogue = uiSet2; // Set the current UI elements to set 2.
			Open(); // Open set 2.
		}
	}
}
