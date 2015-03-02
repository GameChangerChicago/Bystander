using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class GeenaCharacterScript : MonoBehaviour {

    private string _Geena;
    private bool _theShakes, _theTuck;
    public GameObject Holly;
    float VectorXXX, VectorWHY, VectorDBZ;
	// Use this for initialization
	void Start () {
		_theShakes = false;
		_theTuck = true;
        VectorXXX = this.gameObject.transform.position.x;
        VectorWHY = this.gameObject.transform.position.y;
		VectorDBZ = this.gameObject.transform.position.z;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.name == "Holly" && !_theShakes)
			{
				Debug.Log("JUH?");
				StartCoroutine(Erre(Random.Range(4, 8)));
				_theShakes = true;
			}
		else if (this.gameObject.name == "Geena" && _theTuck)
			{
				Debug.Log("SUP dayyyyyvis");
				StartCoroutine(Comf(Random.Range(8, 11)));
				_theTuck = false;
			}

	}
	
	//This method changes the displayed name of the Dialog UI if there are multiple NPCs
    private void ChangeNames()
    {
        for (int i = 0; i < FindObjectOfType<DialogUINameHandler>().DisplayNames.Length; i++)
        {
            FindObjectOfType<DialogUINameHandler>().DisplayNames[i].text = _Geena;
        }
    }

    IEnumerator Erre(float waitTime) {
    
        yield return new WaitForSeconds(waitTime);
        	iTween.ShakePosition(this.gameObject, new Vector3(.0222f, .0222f, .0222f), 2.22f);
        yield return new WaitForSeconds(waitTime/1.222f);
        	_theShakes = false;

    }

     IEnumerator Comf(float waitTime) {
    
        yield return new WaitForSeconds(waitTime);
        	iTween.MoveFrom(this.gameObject, new Vector3(VectorXXX, VectorWHY, VectorDBZ), .222f);
        yield return new WaitForSeconds(.0888f);
        	iTween.MoveTo(this.gameObject, new Vector3(VectorXXX, 4.666f, VectorDBZ), .222f);
        	_theTuck = true;
        yield return new WaitForSeconds(.0888f);
        	iTween.MoveFrom(this.gameObject, new Vector3(VectorXXX, 4.666f, VectorDBZ), .222f);
        yield return new WaitForSeconds(.0888f);
        	iTween.MoveTo(this.gameObject, new Vector3(VectorXXX, VectorWHY, VectorDBZ), .222f);
        	_theTuck = true;

    }

}
