using UnityEngine;
using System.Collections;

public class Open_FlashBack : MonoBehaviour {


	public GameObject myFlashBack;

	private int mouseCounter;

	void Start(){


	}

	void OnMouseDown(){

		mouseCounter++;

		if (mouseCounter%2 ==1) {
			myFlashBack.renderer.enabled = true;
		}
		else
			myFlashBack.renderer.enabled = false;

	}

}
