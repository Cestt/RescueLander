using UnityEngine;
using System.Collections;

public class PowerUp_Manager : MonoBehaviour {

	private GameObject ship;
	private GameObject fuelBar;
	private float fuelBarOriginalSize;
	public int Magnet_Duration;
	public int Shield_Duration;
	public int Fuel_Recover;
	public float Shield_DmgReduction;
	private float actualTime;
	private int timerTime;
	private bool running;
	private GameObject actualPowerUp;
	private Damage damage;
	private Touch_Manager touch;
	public bool On;

	void Awake () {
		if (Application.loadedLevelName != "Menu") {
			if(Application.loadedLevelName.Contains("Tuto")){
				ship = GameObject.Find("101(Clone)");
			}else{
				ship = GameObject.Find(dataManger.manager.actualShip + "(Clone)");
				
			}
			GameObject temp = GameObject.Find("UI_Camera");
			fuelBar = temp.transform.FindChild("Anchor (UpperLeft)/UIBase_Left/BarraFondo_Fuel/BarraFuel").gameObject;
			fuelBarOriginalSize = fuelBar.GetComponent<tk2dSlicedSprite>().dimensions.x;		
			damage = ship.GetComponent<Damage>();
			touch = gameObject.GetComponent<Touch_Manager>();
		}


	}

	void Update(){
		if(running & Time.time > actualTime + timerTime){
			Timer("Stop",0,actualPowerUp);

		}
	}
	
	public void PowerUp(string Power){
		if(!touch.paused){
			switch(Power){
				
			case "Fuel" :
				Debug.Log("Fuel PU");
				Movement movement = ship.GetComponent<Movement>();
				movement.fuel += (movement.originalFuel  * Fuel_Recover)/100;
				tk2dSlicedSprite sliced = fuelBar.GetComponent<tk2dSlicedSprite>();
				sliced.dimensions = new Vector2( 
				                                sliced.dimensions.x  + ((fuelBarOriginalSize * Fuel_Recover)/100),sliced.dimensions.y);
				dataManger.manager.fuelPowerUps --;
				if(sliced.dimensions.x > fuelBarOriginalSize || movement.fuel > movement.originalFuel){
					sliced.dimensions = new Vector2( 
					                                fuelBarOriginalSize,sliced.dimensions.y);
					movement.fuel = movement.originalFuel;
				}
				dataManger.manager.Save(false);
				break;
			case "Shield" :
				Debug.Log("Shield PU");
				Timer("Start",Shield_Duration,ship.transform.FindChild("PU_Shield").gameObject);
				dataManger.manager.shieldPowerUps --;
				dataManger.manager.Save(false);
				break;
			case "Magnet" :
				Debug.Log("Magnet PU");
				Timer("Start",Shield_Duration,ship.transform.FindChild("PU_Magnet").gameObject);
				dataManger.manager.magnetPowerUps --;
				dataManger.manager.Save(false);
				break;
				
			}
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
			if(_actualPowerUp.name == "PU_Magnet"){
				On = true;
			}
			running = true;
			actualPowerUp.SetActive(true);
		}else{
			actualTime = 0;
			timerTime = 0;
			if(actualPowerUp.name == "PU_Shield"){
				damage.DamageVariant = 0;
			}
			if(_actualPowerUp.name == "PU_Magnet"){
				On = false;
			}
			actualPowerUp.SetActive(false);
			actualPowerUp = null;
			running = false;
		}
	}
}
