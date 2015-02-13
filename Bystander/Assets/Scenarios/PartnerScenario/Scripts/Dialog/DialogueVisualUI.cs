using UnityEngine;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.UnityGUI;

public class DialogueVisualUI : UnityDialogueUI {

	// This is a reference to the NPC subtitle fade effect. If the subtitle panel is already
	// visible, we want to manually disable the fade effect so the panel stays visible.
	public FadeEffect npcBorderFadeEffect;

	// We use this bool to remember whether the subtitle panel is already visible:
	private bool isSubtitleVisible = false;

	// Make sure npcBorderFadeEffect is assigned:
	public override void Awake () {
		base.Awake();
		if (npcBorderFadeEffect == null) Debug.LogError(string.Format("{0}: NPC Border Fade Effect is unassigned!", DialogueDebug.Prefix));
	}

	// When the UI has just been opened, the subtitle panel is not visible:
	public override void Open()	{
		base.Open();
		isSubtitleVisible = false;
	}

	// If the subtitle panel is already visible, disable the fade effect:
	public override void ShowSubtitle(Subtitle subtitle) {
		if (npcBorderFadeEffect != null) npcBorderFadeEffect.enabled = !isSubtitleVisible;
		isSubtitleVisible = true;
		base.ShowSubtitle(subtitle);
	}

}
