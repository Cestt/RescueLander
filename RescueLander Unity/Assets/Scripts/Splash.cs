using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("Next",1.3f);
	}
	
	// Update is called once per frame
	void Next () {
		Application.LoadLevel("Menu");
	}
}
