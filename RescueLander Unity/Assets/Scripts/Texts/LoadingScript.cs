﻿using UnityEngine;
using System.Collections;

public class LoadingScript : MonoBehaviour {

	private int numText = 0;
	private float timeUpdate;
	private tk2dTextMesh text;
	// Use this for initialization
	void Awake () {
		text = GetComponent<tk2dTextMesh>();
		text.text = "Loading";
	}
	
	// Update is called once per frame
	void Update () {
		timeUpdate += Time.deltaTime;
		if (timeUpdate >= 0.5f){
			if (numText == 0)
				text.text = "Loading";
			else if (numText == 1)
				text.text = "Loading .";
			else if (numText == 2)
				text.text = "Loading . .";
			else if (numText == 3)
				text.text = "Loading . . .";
			numText++;
			if (numText == 4)
				numText = 0;
			timeUpdate = 0;
		}
	}
}