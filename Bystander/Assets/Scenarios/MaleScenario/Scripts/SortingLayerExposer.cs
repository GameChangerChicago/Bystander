using UnityEngine;
using System.Collections;

public class SortingLayerExposer : MonoBehaviour
{
	
	public string SortingLayerName = "Default";
	public int SortingOrder = 0;
	
	void Awake ()
	{
		gameObject.GetComponent<Renderer> ().sortingLayerName = SortingLayerName;
		gameObject.GetComponent<Renderer> ().sortingOrder = SortingOrder;
	}
}