using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {
	
		private Node next;
		private Object data;
		
		public Node(this Object data){
			next = null;
			data = data;
		}
		
		public Node(this Object data, Node nextValue){
			next = nextValue;
			data = data;
			
		}
		
		
		
		public Object getNode() {
			return data; 
		}
		
		public void setNode(Object data){
			data = data;
		}
		
		public Node getNext(){
			return next;
		}
		
		public void setNext(Node nextValue){
			next = nextValue;
		}
	}

