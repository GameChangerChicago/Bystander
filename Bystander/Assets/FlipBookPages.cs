using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class FlipBookPages : MonoBehaviour {

	public List<GameObject> bookPages = new List<GameObject>();
	public int pageIndex, test;


	// Use this for initialization
	void Start () {

		pageIndex = 0;
		test = 0;
		InvokeRepeating ("BookAnimation", 5f, 10f);
		
	}
	
	// Update is called once per frame
	void Update () {
	

	}

	IEnumerator FlipPage(){


		for (float t = 0.0f; t <1.0f; t+=Time.deltaTime/3f) {
			Vector3 newVector = Vector3.Lerp (new Vector3 (0, 180, 0), new Vector3 (0, 360, 0), t);
			bookPages [pageIndex].GetComponent<Transform> ().eulerAngles = newVector;

			yield return null;
		
		}
		Debug.Log (test++);

	}

	 public void BookAnimation(){

		StartCoroutine ("FlipPage");

		if (pageIndex < bookPages.Count - 1) {				
						pageIndex++;

//			if(pageIndex + 1 < bookPages.Count - 1){
//						bookPages [pageIndex+1].SetActive (true);
//			}
//			else {
//				bookPages[0].SetActive(true);
//			}
//
//			if(pageIndex - 1 < 0)
//				bookPages [0].SetActive(false);
//			else
//				bookPages[pageIndex -1].SetActive(false);
		}

				else {
						pageIndex = 0;
						//bookPages[pageIndex].SetActive(true);
				}



	}


		
}
