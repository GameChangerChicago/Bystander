using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public enum HB_GameState
{
	PrePartner,
	PreParty,
	PreHarassment,
	PreMale,
}
;

public class HubWorldManager : MonoBehaviour {



	private string nextLevel;
	public static HB_GameState gameState;
	private int i;
	private string[] level_Names;
	public List<GameObject> LevelDescriptions;


	void Awake(){

		DontDestroyOnLoad (this);
		}

	// Use this for initialization
	void Start () {
	
		i = 0;
		level_Names = Enum.GetNames (typeof(HB_GameState)); 
		nextLevel = level_Names [i];
		LevelDescriptions [i].SetActive (true);

	}
	
	// Update is called once per frame
	void Update () {

	
	}

	public void prepNextLevel ()
	{
		LevelDescriptions [i].SetActive (false);
		i++;
		nextLevel = level_Names [i];
		LevelDescriptions [i].SetActive (true);

	}


	public string NextLevel ()
	{
		return nextLevel;
	}
}
