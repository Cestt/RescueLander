using UnityEngine;
using System.Collections;

public class Stars_Global : MonoBehaviour {

	tk2dTextMesh text;

	void Awake () {
	text = GetComponent<tk2dTextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = dataManger.manager.totalStars.ToString();
	}
}
