using UnityEngine;
using System.Collections;

public class MoveOut_UICamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, -1500);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
