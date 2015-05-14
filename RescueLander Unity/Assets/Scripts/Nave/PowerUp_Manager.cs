using UnityEngine;
using System.Collections;

public class PowerUp_Manager : MonoBehaviour {

	private GameObject ship;
	private GameObject lifeBar;
	private GameObject fuelBar;
	private float lifeBarOriginalSize;
	private float fuelBarOriginalSize;
	public int Magnet_Duration;
	public int Shield_Duration;
	public int Life_Recover;
	public int Fuel_Recover;
	public float Shield_DmgReduction;
	private float actualTime;
	private int timerTime;
	private bool running;
	private GameObject actualPowerUp;
	private Damage damage;

	void Awake () {
	
		ship = GameObject.Find(dataManger.manager.actualShip + "(Clone)");
		GameObject temp = GameObject.Find("UI_Camera");
		lifeBar = temp.transform.FindChild("Anchor (UpperLeft)/UIBase_Left/BarraFondo_Vida/BarraVida").gameObject;
		fuelBar = temp.transform.FindChild("Anchor (UpperLeft)/UIBase_Left/BarraFondo_Fuel/BarraFuel").gameObject;
		lifeBarOriginalSize = lifeBar.GetComponent<tk2dSlicedSprite>().dimensions.x;
		fuelBarOriginalSize = fuelBar.GetComponent<tk2dSlicedSprite>().dimensions.x;

	}

	void Update(){
		if(running & Time.time > actualTime + timerTime){
			Timer("Stop",0,actualPowerUp);
			damage = ship.GetComponent<Damage>();
		}
	}
	
	public void PowerUp(string Power){

		switch(Power){
		case "Life" :
			damage.life += (damage.maxLife  * Life_Recover)/100;
			break;
		case "Fuel" :
			Movement movement = ship.GetComponent<Movement>();
			movement.fuel += (movement.originalFuel  * Fuel_Recover)/100;
			break;
		case "Shield" :
			Timer("Start",Shield_Duration,ship.transform.FindChild("PU_Shield").gameObject);
			break;
		case "Magnet" :
			Timer("Start",Shield_Duration,ship.transform.FindChild("PU_Magnet").gameObject);
			break;

		}
	}

	private void Timer(string Switch,int _timerTime,GameObject _actualPowerUp){

		if(Switch == "Start"){
			actualTime = Time.time;
			timerTime = _timerTime;
			actualPowerUp = _actualPowerUp;
			if(_actualPowerUp.name == "PU_Shield"){
				damage.DamageVariant = Shield_DmgReduction;
			}
			running = true;
			actualPowerUp.SetActive(true);
		}else{
			actualTime = null;
			timerTime = null;
			if(actualPowerUp == "PU_Shield"){
				damage.DamageVariant = 0;
			}
			actualPowerUp.SetActive(false);
			actualPowerUp = null;
			running = false;
		}
	}
}
