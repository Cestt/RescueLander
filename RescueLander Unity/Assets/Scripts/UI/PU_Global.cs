using UnityEngine;
using System.Collections;

public class PU_Global : MonoBehaviour {

	private string PU;
	private tk2dTextMesh text;
	void Awake () {
	
		text = GetComponent<tk2dTextMesh>();
		if(transform.parent.name.Contains("Shield")){
			PU = "Shield";
		}
		if(transform.parent.name.Contains("Magnet")){
			PU = "Magnet";
		}
		if(transform.parent.name.Contains("Fuel")){
			PU = "Fuel";
		}
	}

	IEnumerator Start () {
		while (true) {
			switch(PU){
			case "Shield":
				text.text = dataManger.manager.shieldPowerUps.ToString();
				break;
			case "Magnet":
				text.text = dataManger.manager.magnetPowerUps.ToString();
				break;
			case "Fuel":
				text.text = dataManger.manager.fuelPowerUps.ToString();
				break;
			}
			yield return new WaitForSeconds(1f);
		}
	}

}
