using UnityEngine;
using System.Collections;

public class Stats_Fix : MonoBehaviour {

	private Vector3 originalPosition;

	void Start(){
		originalPosition = transform.position;
	}
	// Update is called once per frame
	void Update () {
		transform.position = originalPosition;
	}
}
