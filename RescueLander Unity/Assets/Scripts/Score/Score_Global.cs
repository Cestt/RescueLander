using UnityEngine;
using System.Collections;

public class Score_Global : MonoBehaviour {

	tk2dTextMesh text;

	void Awake(){
		text = GetComponent<tk2dTextMesh>();
	}

	void Update () {
		text.text = Localization_Bridge.manager.GetTextValue("RescueLander.LevelsScore") + dataManger.manager.scoresMars["Level_"+dataManger.manager.actualLevel];
	}
}
