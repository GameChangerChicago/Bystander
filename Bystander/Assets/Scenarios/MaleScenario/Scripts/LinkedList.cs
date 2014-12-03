using UnityEngine;
using System.Collections;

public class LinkedList : MonoBehaviour {

	private Node head;
	public Node tail; 

	// Use this for initialization
	void Start () {
		
		head = new Node(this);

	}
	
	void OnMouseDown(){
//		head.setNext (tail);

		Instantiate(tail); 


		Debug.Log("the new Texture should have appeared");

		}

}