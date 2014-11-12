using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum SEXHAS_STATE {GUIDANCE_1, PANEL, HALL, BATHRM, BLACKTOP, CLASS, CAF, GUIDANCE_2}


public class GrandMaster_SexHasManager : MonoBehaviour {

	/*The primary job of this script is to keep track of how many sections of the 
	 * app have been completed altogether.*/

	//And to update the gameState and make sure user is in right part of the app at the right time. 

	public List<GameObject> situationList = new List<GameObject>();
	private GameObject uiRoot;
	public bool bInstantiated;
	private bool bUiRoot = true;
	private CameraScript camScript;
	private SEXHAS_STATE sexHasState;
	public SEXHAS_STATE SexHasState {
		get{
			return sexHasState;
		}

		set{
			sexHasState = value;
		}
	}
		
	void Start(){
		sexHasState = SEXHAS_STATE.GUIDANCE_1;
		uiRoot = GameObject.FindGameObjectWithTag ("Guidance_UI_Root");
		camScript = Camera.main.GetComponent ("CameraScript") as CameraScript;
	}

	void Update(){
		switch (sexHasState) {
		case SEXHAS_STATE.GUIDANCE_1:
			Debug.Log (" Inside guidance1 state." + sexHasState);
			uiRoot.SetActive (true);
			bUiRoot = true;
			camScript.moveBackToGuidance ();
			break;
		case SEXHAS_STATE.PANEL:
			Debug.Log (" Inside guidance2 state.");
			if (bUiRoot) {
				uiRoot.SetActive (false);
				bUiRoot = false;
			}
			if (!bInstantiated) {
				//foreach(GameObject situation in situationList){
				GameObject.Destroy (situationList [0]);
				//}
				situationList [0] = (GameObject) Instantiate (situationList [0], new Vector3 (0, -12, 0), Quaternion.identity);
				camScript.moveToPanel ();
				bInstantiated = true;
			}
			break;
		case SEXHAS_STATE.HALL:
			Debug.Log (" Inside Hall state.");
			camScript.moveToSituHall ();
			break;
		case SEXHAS_STATE.BLACKTOP:
			Debug.Log (" Inside Blacktop state.");
			camScript.moveToSituBlackTop();
			break;
		case SEXHAS_STATE.CLASS:
			Debug.Log (" Inside class state.");
			camScript.moveToSituClass ();
			break;
		case SEXHAS_STATE.BATHRM:
			Debug.Log (" Inside bathrm state.");
			camScript.moveToSituBthrm ();
			break;
		case SEXHAS_STATE.CAF:
			Debug.Log (" Inside caf state.");
			camScript.moveToSituCaf();
			break;
		default:
			break;
		}
	}




}
