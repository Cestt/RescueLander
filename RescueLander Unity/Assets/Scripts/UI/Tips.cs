using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tips : MonoBehaviour {

	List<string> textsTips;

	void Awake () {
		//Carga de todos los textos
		textsTips = new List<string> ();
		textsTips.Add ("TIP 1");
		textsTips.Add ("TIP 2");
		textsTips.Add ("TIP 3");
		textsTips.Add ("TIP 4");
		textsTips.Add ("TIP 5");
		textsTips.Add ("TIP 6");
		GetComponent<tk2dTextMesh> ().text = textsTips [Random.Range (0, textsTips.Count)];
	}

}
