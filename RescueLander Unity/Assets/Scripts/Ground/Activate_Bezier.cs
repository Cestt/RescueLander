using UnityEngine;
using System.Collections;

public class Activate_Bezier : MonoBehaviour {

	// Use this for initialization
	void Start () {
		EdgeCollider2D collider = GetComponent<EdgeCollider2D> ();
		collider.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
