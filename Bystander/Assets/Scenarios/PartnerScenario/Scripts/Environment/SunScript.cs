using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class SunScript : MonoBehaviour {

    int _hollyTolerance, _hollyCurrentTolerance, _hollyUpdatedTolerance;

    bool updating;
   // public GameObject _notTheSun;

   

	// Use this for initialization
	void Start () {
        _hollyTolerance = 0;
       
        updating = false;
        
	
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(_hollyCurrentTolerance);
        _hollyCurrentTolerance = DialogueLua.GetVariable("Wrong").AsInt + DialogueLua.GetVariable("Affect").AsInt;

        if ((_hollyTolerance != _hollyCurrentTolerance) && !updating)
        {
            
            updating = true;
            UpdateTolerance();
        }

        
	
	}

    void UpdateTolerance()
    {
        Debug.Log("asdfas");
        if (_hollyCurrentTolerance > _hollyTolerance)
        {
            iTween.ScaleTo(this.gameObject, new Vector3(1, 1, 1), .5f);
            _hollyTolerance += 1;
            Debug.Log("Get bigger");
            updating = false;
         
        }
        if (_hollyTolerance > _hollyCurrentTolerance)
        {
            
            iTween.ScaleTo(this.gameObject, new Vector3(.5f, .5f, .5f), .5f);
            _hollyTolerance -= 1;
            Debug.Log("Get smaller");
            updating = false;
        }

        

        

    }
}
