using UnityEngine;
using System.Collections;

public class BackToBase : MonoBehaviour {

	private GameObject ship;
	private ShipAstronautPickUp astroPickUp;
	private bool once = false;


	void Awake () {
		if(Application.loadedLevelName.Contains("Tuto")){
			
			ship = GameObject.Find("101(Clone)");
		}else{
			ship = GameObject.Find(dataManger.manager.actualShip + "(Clone)");
		}
		astroPickUp = ship.GetComponent<ShipAstronautPickUp>();
	}
	
	// Update is called once per frame
	void Update () {
	
		if(astroPickUp.astronautPicked >=3 & dataManger.manager.actualLevel < 4& dataManger.manager.actualWorld == "Mars" & !once){
			transform.FindChild("BackToPlatform_Text").gameObject.SetActive(true);
		}
	}
}
