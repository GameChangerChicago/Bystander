using UnityEngine;
using System.Collections;

public class SlideShow : MonoBehaviour {
	#region Support class
	[System.Serializable]
	public class Logo{
		public Texture2D image;
		public Color background;
		public float duration = 2.0f;
		public bool skippable = true;
	}
	[System.Serializable]
	public class KeySet{
		public bool useTouches = true;
		public int numberOfTouchesNeeded = 1;
		public KeyCode[] keysToUse;
	}
	#endregion
	#region Public variables
	public Logo[] logos;
	public KeySet exitWith;
	public string levelToLoad;
	public GUITexture logosScreen;
	public Animation fader;
	#endregion
	#region Private variables
	private int currentLogo = -1;
	private float startTime = 0.0f;
	private float logoMaxWidth, logoMaxHeight,
				actualWidth, actualHeight, texAspect;
	private bool transition = false;
	#endregion
	#region Built-in methods
	void Start(){
		this.logoMaxWidth = Screen.width * 0.8f;
		this.logoMaxHeight = Screen.height * 0.8f;
		StartCoroutine("SwitchLogo");
	}
	void Update(){
		if(!this.transition && (
								Time.time - this.startTime > this.logos[this.currentLogo].duration ||
									(
									this.logos[this.currentLogo].skippable &&
									GetSkipButtonPessed()
									)
								)
			)
			StartCoroutine("SwitchLogo");
	}
	#endregion
	#region Private methods
	private IEnumerator SwitchLogo(){
		this.transition = true;
		this.fader.Play("FadeOut");
		yield return new WaitForSeconds(0.5f);
		if(this.currentLogo + 1 < this.logos.Length){
			this.currentLogo++;
			this.texAspect = (float)((float)this.logos[this.currentLogo].image.width / (float)this.logos[this.currentLogo].image.height);
			this.actualWidth = this.logoMaxWidth;
			this.actualHeight = this.actualWidth / this.texAspect;
			if(this.actualHeight > this.logoMaxHeight){
				this.actualHeight = this.logoMaxHeight;
				this.actualWidth = this.actualHeight * this.texAspect;
			}
			this.logosScreen.pixelInset = new Rect(
					-(this.actualWidth * 0.5f),
					-(this.actualHeight * 0.5f),
					this.actualWidth,
					this.actualHeight
				);
			this.logosScreen.texture = this.logos[this.currentLogo].image;
			try{
				Camera.main.backgroundColor = this.logos[this.currentLogo].background;
			}catch{}
			this.fader.Play("FadeIn");
			yield return new WaitForSeconds(0.5f);
			this.startTime = Time.time;
			this.transition = false;
		}else
			Application.LoadLevel(this.levelToLoad);
	}
	private bool GetSkipButtonPessed(){
		if(this.exitWith.useTouches && Input.touches.Length == this.exitWith.numberOfTouchesNeeded)
			return true;
		foreach(KeyCode k in this.exitWith.keysToUse)
			if(Input.GetKeyDown(k))	return true;
		return false;
	}
	#endregion
}
