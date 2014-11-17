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
	public bool bHallInstantitated;
	public bool bBlckInstantitated;
	public bool bClassInstantitated;
	public bool bBathInstantitated;
	public bool bCafInstantitated;

	public static readonly Vector3 panelSpot = new Vector3 (0, -12, 0);
	public static readonly Vector3 hallSpot = new Vector3 (0, -24, 0);
	public static readonly Vector3 blckSpot = new Vector3 (0, -36, 0);
	public static readonly Vector3 classSpot = new Vector3 (0, -48, 0);
	public static readonly Vector3 bthrmSpot = new Vector3 (0, -60, 0);
	public static readonly Vector3 cafSpot = new Vector3 (0, -72, 0);

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
			camScript.moveBackToGuidance();
			break;
		case SEXHAS_STATE.PANEL:
			Debug.Log (" Inside guidance2 state.");
			if (bUiRoot) {
				uiRoot.SetActive (false);
				bUiRoot = false;
			}
			if (!bInstantiated) {
				GameObject.Destroy (situationList [0]);
				situationList [0] = (GameObject) Instantiate (situationList [0], panelSpot, Quaternion.identity);
				camScript.moveToPanel ();
				bInstantiated = true;
			}
			break;
		case SEXHAS_STATE.HALL:
			if (!bHallInstantitated) {
				Debug.Log (" Inside Hall state.");
				GameObject.Destroy (situationList [1]);
				situationList [1] = (GameObject) Instantiate (situationList [1], hallSpot, Quaternion.identity);
				camScript.moveToSituHall ();
				bHallInstantitated = true;
			} 
			break;
		case SEXHAS_STATE.BLACKTOP:
			if (!bBlckInstantitated) {
				GameObject.Destroy (situationList [2]);
				situationList [2] = (GameObject) Instantiate (situationList [2], blckSpot, Quaternion.identity);
				Debug.Log (" Inside Blacktop state.");
				camScript.moveToSituBlackTop ();
				bBlckInstantitated = true;
			}
			break;
		case SEXHAS_STATE.CLASS:
			if (!bClassInstantitated) {
				Debug.Log (" Inside class state.");
				camScript.moveToSituClass ();
				GameObject.Destroy (situationList [3]);
				situationList [3] = (GameObject) Instantiate (situationList [3], classSpot, Quaternion.identity);
				bClassInstantitated = true;
			}
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
