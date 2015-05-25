﻿using UnityEngine;
using System.Collections;

public class Touch_Manager : MonoBehaviour {
	RuntimePlatform platform = Application.platform;
	[HideInInspector]
	public GameObject uicameraGameobject;
	public Camera uicamera;
	[HideInInspector]
	public bool paused;
	public GameObject ship;
	private Rigidbody2D rigid;
	public GameObject uiColumnExtended;
	private Animation animation;
	public GameObject music;
	public GameObject sounds;
	public float MenuSpeed;
	private bool forward;
	public GameObject Win;
	public GameObject Lose;
	public GameObject garaje;
	private string Result;
	private Share share;
	private FacebookSocial faceBook;
	private string selectedZone = "A";
	private Color_Set colorSet;
	private SpriteColorFX.SpriteColorMasks3 colorChange;
	private GameObject ShipGaraje;
	private PowerUp_Manager powerManager;
	private Garaje_Manager garage_manager;
	private Coin_Manager coin_manager;
	private GameObject PUilustration;
	private GameObject pauseText;
	private Ads ads;




	void Awake(){
		uicameraGameobject = GameObject.Find("UI_Camera");
		if(Application.loadedLevelName != "Menu"){
			uiColumnExtended = uicameraGameobject.transform.FindChild("Anchor (UpperRight)/UIBase_Right/UIBase_RightCol/UIBase_RightCol_Extended").gameObject;
			ship = GameObject.Find(dataManger.manager.actualShip + "(Clone)");
			rigid = ship.GetComponent<Rigidbody2D>();
			animation = uiColumnExtended.GetComponent<Animation>();
			sounds = uiColumnExtended.transform.FindChild("Sound_Button").gameObject;
			music = uiColumnExtended.transform.FindChild("Music_Button").gameObject;
			Win = uicameraGameobject.transform.FindChild ("WinLayout").gameObject;
			Lose = uicameraGameobject.transform.FindChild ("LoseLayout").gameObject;
			pauseText = uicameraGameobject.transform.FindChild("Anchor (LowerCenter)/Paused").gameObject;
		}
		ads = GetComponent<Ads>();
		garaje = uicameraGameobject.transform.FindChild ("Garage_Menu").gameObject;
		garage_manager = GetComponent<Garaje_Manager>();
		powerManager = GetComponent<PowerUp_Manager>();
		colorSet = GetComponent<Color_Set>();
		ShipGaraje = uicameraGameobject.transform.FindChild ("Garage_Menu/Shop_Bg_01/Paint_Menu/TV/Ship01_Garage").gameObject;
		colorChange = ShipGaraje.GetComponent<SpriteColorFX.SpriteColorMasks3>();
		uicamera = uicameraGameobject.GetComponent<Camera>();
		share = GetComponent<Share>();
		faceBook = GetComponent<FacebookSocial>();
		coin_manager =GameObject.Find("ScoreCoin_Manager").GetComponent<Coin_Manager>();

	}



	void Update () {
		if(platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer){
			if(Input.touchCount > 0) {
				if(Input.GetTouch(0).phase == TouchPhase.Began){
					
					Ray ray;
					Ray ray2;
					RaycastHit hit;
					
					ray = uicamera.ScreenPointToRay(Input.mousePosition);
					ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
					
					if (Physics.Raycast(ray.origin,ray.direction * 100, out hit) || Physics.Raycast(ray2.origin,ray.direction * 100, out hit)) {
						
						Debug.Log("Hit");
						
						switch(hit.collider.name ){
						case "Play" :
							forward = true;
							if(IsInvoking("MoveCamera")){
								CancelInvoke("MoveCamera");
								InvokeRepeating("MoveCamera",0.01f,Time.fixedDeltaTime);
							}else{
								InvokeRepeating("MoveCamera",0.01f,Time.fixedDeltaTime);
							}
							
							break;	
						case "Back_Button" :
							forward = false;
							if(IsInvoking("MoveCamera")){
								CancelInvoke("MoveCamera");
								InvokeRepeating("MoveCamera",0.01f,Time.fixedDeltaTime);
							}else{
								InvokeRepeating("MoveCamera",0.01f,Time.fixedDeltaTime);
							}
							
							break;	
						case "Pause_Button" :
							Pause(hit.transform.gameObject);
							break;
						case "Retry_Button" :
							dataManger.manager.Save(false);
							Application.LoadLevel (Application.loadedLevel);
							break;	
						case "Levels_Button" :
							Application.LoadLevel ("Menu");
							dataManger.manager.Camposition = "Forward";
							dataManger.manager.Save(false);
							break;
						case "Sound_Button" :
							if(dataManger.manager.Sounds){
								dataManger.manager.Sounds = false;
								sounds.GetComponentInChildren<tk2dSprite>().SetSprite("VolumeIco_Off");
								Debug.Log("Sound off");
							}else{
								dataManger.manager.Sounds = true;
								sounds.GetComponentInChildren<tk2dSprite>().SetSprite("VolumeIco_On");
								Debug.Log("Sound on");
							}
							break;
						case "Music_Button" :
							if(dataManger.manager.Music){
								dataManger.manager.Music = false;
								music.GetComponentInChildren<tk2dSprite>().SetSprite("MusciIco_Off");
								Debug.Log("Music off");
							}else{
								dataManger.manager.Music = true;
								music.GetComponentInChildren<tk2dSprite>().SetSprite("MusciIco_On");
								Debug.Log("Music on");
							}
							break;
							
						case "Next_Button" :
							int Level = dataManger.manager.actualLevel;
							dataManger.manager.actualLevel ++;
							dataManger.manager.Save(false);
							Application.LoadLevel("Level_"+(Level + 1));
							break;
						case "Share_Button" :
							share.ShareScreenshot();
							break;
						case "Social Media Buttons" :
							faceBook.FBLogin();
							break;
						case "Button_Garage" :
							Garaje(true);
							break;
						case "Exit_Button" :
							dataManger.manager.Save(false);
							Garaje(false);
							break;
						case "A_Button" :
							selectedZone = "A";
							Debug.Log("Zone A selected");
							break;
						case "B_Button" :
							selectedZone = "B";
							Debug.Log("Zone B selected");
						break;
						case "PowerUp_Shield" :
							if(dataManger.manager.shieldPowerUps >= 1){
								powerManager.PowerUp("Shield");
							}else{
								ads.Launch("Shield");
							}
							break;
						case "PowerUp_Magnet" :
							if(dataManger.manager.magnetPowerUps >= 1){
								powerManager.PowerUp("Magnet");
							}else{
								ads.Launch("Magnet");
							}
							break;
						case "PowerUp_Fuel" :
							if(dataManger.manager.fuelPowerUps >= 1){
								powerManager.PowerUp("Fuel");
							}else{
								ads.Launch("Fuel");
							}
							break;
						case "GarageHeader_Button" :
							garage_manager.LayoutChanger("Paint");
							break;
						case "ShipsButton" :
							garage_manager.LayoutChanger("Ships");
							break;
						case "PowerUps_Button" :
							garage_manager.LayoutChanger("PowerUps");
							break;
						case "Coins_Button" :
							garage_manager.LayoutChanger("Coins");
							break;
						case "ButtonShip_Ship01" :
							if(dataManger.manager.shipUnlocks.Contains("Ship01")){
								dataManger.manager.actualShip = "Ship01";
								dataManger.manager.Save(false);
								colorSet.SpriteSet(true,"Ship01");
							}else{
								colorSet.SpriteSet(false,"Ship01");
							}

							break;
						case "ButtonShip_369" :
							if(dataManger.manager.shipUnlocks.Contains("369")){
								dataManger.manager.actualShip = "369";
								dataManger.manager.Save(false);
								colorSet.SpriteSet(true,"369");
							}else{
								colorSet.SpriteSet(false,"369");
							}
							break;
						case "ButtonShip_Taboo" :
							if(dataManger.manager.shipUnlocks.Contains("Taboo")){
								dataManger.manager.actualShip = "Taboo";
								dataManger.manager.Save(false);
								colorSet.SpriteSet(true,"Taboo");
							}else{
								colorSet.SpriteSet(false,"Taboo");
							}
							break;
						case "ButtonShip_UFLO" :
							if(dataManger.manager.shipUnlocks.Contains("UFLO")){
								dataManger.manager.actualShip = "UFLO";
								dataManger.manager.Save(false);
								colorSet.SpriteSet(true,"UFLO");
							}else{
								colorSet.SpriteSet(false,"UFLO");
							}
							break;
						case "ButtonShip_Box" :
							if(dataManger.manager.shipUnlocks.Contains("Box")){
								dataManger.manager.actualShip = "Box";
								dataManger.manager.Save(false);
								colorSet.SpriteSet(true,"Box");
							}else{
								colorSet.SpriteSet(false,"Box");
							}
							break;
						case "ButtonShip_Mush" :
							if(dataManger.manager.shipUnlocks.Contains("Mush")){
								dataManger.manager.actualShip = "Mush";
								dataManger.manager.Save(false);
								colorSet.SpriteSet(true,"Mush");
							}else{
								colorSet.SpriteSet(false,"Mush");
							}
							break;
						case "ButtonShip_Bow" :
							if(dataManger.manager.shipUnlocks.Contains("Bow")){
								dataManger.manager.actualShip = "Bow";
								dataManger.manager.Save(false);
								colorSet.SpriteSet(true,"Bow");
							}else{
								colorSet.SpriteSet(false,"Bow");
							}
							break;
						case "Button_Buy" :
							if(hit.collider.transform.FindChild("Button_Buy_Down").gameObject.activeInHierarchy){
								Value value = hit.collider.gameObject.GetComponent<Value>();
								if(coin_manager.Compra(value.Cost,value._Type,hit.transform.parent.name.Substring(10))){
									Destroy(hit.collider.gameObject);
									Debug.Log("Comprado");
								}else{
									Debug.Log("Algo falla joder");
								}
							}
							break;
						case "Cheat" :
							dataManger.manager.coins += 50000;
							break;
						case "Button_PowerUp_Shield" :
							if(PUilustration != null){
								PUilustration.SetActive(false);
							}
							PUilustration = hit.transform.FindChild("Illustration_Shield").gameObject;
							PUilustration.SetActive(true);
							break;
							
						case "Button_PowerUp_Magnet" :
							if(PUilustration != null){
								PUilustration.SetActive(false);
							}
							PUilustration = hit.transform.FindChild("Illustration_Magnet").gameObject;
							PUilustration.SetActive(true);
							break;
							
						case "Button_PowerUp_Fuel" :
							if(PUilustration != null){
								PUilustration.SetActive(false);
							}
							PUilustration = hit.transform.FindChild("Illustration_Fuel").gameObject;
							PUilustration.SetActive(true);
							break;
						default :
							
							break;
							
						}
						if(hit.collider.tag == "Level_Ico"){
							string tempString = hit.collider.name.Substring(6);
							int tempInt = int.Parse(tempString);
							Debug.Log("Clicked Level: "+tempInt);
							if(dataManger.manager.unlocks >= tempInt){
								dataManger.manager.actualLevel = tempInt;
								StartCoroutine(LoadLevelAsync(tempInt));
							}
						}
						if(hit.collider.name.Contains("Color")){
							if(hit.collider.gameObject.GetComponent<Color_Enabled>().enabled == true){
								Color colorApply = hit.collider.gameObject.GetComponentInChildren<tk2dSprite>().color;
								if(selectedZone == "A"){
									colorChange.colorMaskRed = colorApply;
									colorSet.ColorSet(colorApply,"A");
								}
								if(selectedZone == "B"){
									colorChange.colorMaskGreen = colorApply;
									colorSet.ColorSet(colorApply,"B");
								}
							}
							
						}

						
					}
								
				}
			}
		}else if(platform == RuntimePlatform.WindowsEditor){
			
			if(Input.GetMouseButtonUp(0)){
				
				Ray ray;
				Ray ray2;
				RaycastHit hit;

				ray = uicamera.ScreenPointToRay(Input.mousePosition);
				ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);

				if (Physics.Raycast(ray.origin,ray.direction * 100, out hit) 
				    || Physics.Raycast(ray2.origin,ray.direction * 100, out hit)) {
		
					Debug.Log("Hit");

					switch(hit.collider.name ){
					case "Play" :
						forward = true;
						if(IsInvoking("MoveCamera")){
							CancelInvoke("MoveCamera");
							InvokeRepeating("MoveCamera",0.01f,Time.fixedDeltaTime);
						}else{
							InvokeRepeating("MoveCamera",0.01f,Time.fixedDeltaTime);
						}

						break;	
					case "Back_Button" :
						forward = false;
						if(IsInvoking("MoveCamera")){
							CancelInvoke("MoveCamera");
							InvokeRepeating("MoveCamera",0.01f,Time.fixedDeltaTime);
						}else{
							InvokeRepeating("MoveCamera",0.01f,Time.fixedDeltaTime);
						}
						
						break;	
					case "Pause_Button" :
						Pause(hit.transform.gameObject);
						break;
					case "Retry_Button" :
						dataManger.manager.Save(false);
						Application.LoadLevel (Application.loadedLevel);
						break;	
					case "Levels_Button" :
						Application.LoadLevel ("Menu");
						dataManger.manager.Camposition = "Forward";
						dataManger.manager.Save(false);
						break;
					case "Sound_Button" :
						if(dataManger.manager.Sounds){
							dataManger.manager.Sounds = false;
							sounds.GetComponentInChildren<tk2dSprite>().SetSprite("VolumeIco_Off");
							Debug.Log("Sound off");
						}else{
							dataManger.manager.Sounds = true;
							sounds.GetComponentInChildren<tk2dSprite>().SetSprite("VolumeIco_On");
							Debug.Log("Sound on");
						}
						break;
					case "Music_Button" :
						if(dataManger.manager.Music){
							dataManger.manager.Music = false;
							music.GetComponentInChildren<tk2dSprite>().SetSprite("MusciIco_Off");
							Debug.Log("Music off");
						}else{
							dataManger.manager.Music = true;
							music.GetComponentInChildren<tk2dSprite>().SetSprite("MusciIco_On");
							Debug.Log("Music on");
						}
						break;
					case "Next_Button" :
						int Level = dataManger.manager.actualLevel;
						dataManger.manager.actualLevel ++;
						dataManger.manager.Save(false);
						Application.LoadLevel("Level_"+(Level + 1));
						break;
					case "Share_Button" :
						share.ShareScreenshot();
						Debug.Log("Launch Share");
						break;
					case "Social Media Buttons" :
						faceBook.FBLogin();
						break;
					case "Button_Garage" :
						Garaje(true);
						break;
					case "Exit_Button" :
						Garaje(false);
						dataManger.manager.Save(false);
						break;
					case "A_Button" :
						selectedZone = "A";
						Debug.Log("Zone A selected");
						break;
					case "B_Button" :
						selectedZone = "B";
						Debug.Log("Zone B selected");
						break;
					case "PowerUp_Shield" :
						if(dataManger.manager.shieldPowerUps >= 1){
							powerManager.PowerUp("Shield");
						}else{
							ads.Launch("Shield");
						}
						break;
					case "PowerUp_Magnet" :
						if(dataManger.manager.magnetPowerUps >= 1){
							powerManager.PowerUp("Magnet");
						}else{
							ads.Launch("Magnet");
						}
						break;
					case "PowerUp_Fuel" :
						if(dataManger.manager.fuelPowerUps >= 1){
							powerManager.PowerUp("Fuel");
						}else{
							ads.Launch("Fuel");
						}
						break;
					case "GarageHeader_Button" :
						garage_manager.LayoutChanger("Paint");
						break;
					case "ShipsButton" :
						garage_manager.LayoutChanger("Ships");
						break;
					case "PowerUps_Button" :
						garage_manager.LayoutChanger("PowerUps");
						break;
					case "Coins_Button" :
						garage_manager.LayoutChanger("Coins");
						break;
					case "ButtonShip_Ship01" :
						if(dataManger.manager.shipUnlocks.Contains("Ship01")){
							dataManger.manager.actualShip = "Ship01";
							dataManger.manager.Save(false);
							colorSet.SpriteSet(true,"Ship01");
						}else{
							colorSet.SpriteSet(false,"Ship01");
						}
						
						break;
					case "ButtonShip_369" :
						if(dataManger.manager.shipUnlocks.Contains("369")){
							dataManger.manager.actualShip = "369";
							dataManger.manager.Save(false);
							colorSet.SpriteSet(true,"369");
						}else{
							colorSet.SpriteSet(false,"369");
						}
						break;
					case "ButtonShip_Taboo" :
						if(dataManger.manager.shipUnlocks.Contains("Taboo")){
							dataManger.manager.actualShip = "Taboo";
							dataManger.manager.Save(false);
							colorSet.SpriteSet(true,"Taboo");
						}else{
							colorSet.SpriteSet(false,"Taboo");
						}
						break;
					case "ButtonShip_UFLO" :
						if(dataManger.manager.shipUnlocks.Contains("UFLO")){
							dataManger.manager.actualShip = "UFLO";
							dataManger.manager.Save(false);
							colorSet.SpriteSet(true,"UFLO");
						}else{
							colorSet.SpriteSet(false,"UFLO");
						}
						break;
					case "ButtonShip_Box" :
						if(dataManger.manager.shipUnlocks.Contains("Box")){
							dataManger.manager.actualShip = "Box";
							dataManger.manager.Save(false);
							colorSet.SpriteSet(true,"Box");
						}else{
							colorSet.SpriteSet(false,"Box");
						}
						break;
					case "ButtonShip_Mush" :
						if(dataManger.manager.shipUnlocks.Contains("Mush")){
							dataManger.manager.actualShip = "Mush";
							dataManger.manager.Save(false);
							colorSet.SpriteSet(true,"Mush");
						}else{
							colorSet.SpriteSet(false,"Mush");
						}
						break;
					case "ButtonShip_Bow" :
						if(dataManger.manager.shipUnlocks.Contains("Bow")){
							dataManger.manager.actualShip = "Bow";
							dataManger.manager.Save(false);
							colorSet.SpriteSet(true,"Bow");
						}else{
							colorSet.SpriteSet(false,"Bow");
						}
						break;
					case "Button_Buy" :
						if(hit.collider.transform.FindChild("Button_Buy_Down").gameObject.activeInHierarchy){
							Value value = hit.collider.gameObject.GetComponent<Value>();
							if(coin_manager.Compra(value.Cost,value._Type,hit.transform.parent.name.Substring(11))){
								if(value._Type == "Ship" || value._Type == "World"){
									Destroy(hit.collider.gameObject);
								}
								Debug.Log("Comprado");
							}else{
								Debug.Log("Algo falla joder");
							}
						}
						break;
					case "Cheat" :
						dataManger.manager.coins += 50000;
						break;
					case "Button_PowerUp_Shield" :
						if(PUilustration != null){
							PUilustration.SetActive(false);
						}
						PUilustration = hit.transform.FindChild("Illustration_Shield").gameObject;
						PUilustration.SetActive(true);
						break;
						
					case "Button_PowerUp_Magnet" :
						if(PUilustration != null){
							PUilustration.SetActive(false);
						}
						PUilustration = hit.transform.FindChild("Illustration_Magnet").gameObject;
						PUilustration.SetActive(true);
						break;
						
					case "Button_PowerUp_Fuel" :
						if(PUilustration != null){
							PUilustration.SetActive(false);
						}
						PUilustration = hit.transform.FindChild("Illustration_Fuel").gameObject;
						PUilustration.SetActive(true);
						break;
					default :
						
						break;
						
					}
					if(hit.collider.tag == "Level_Ico"){
						string tempString = hit.collider.name.Substring(6);
						int tempInt = int.Parse(tempString);
						Debug.Log("Clicked Level: "+tempInt);
						if(dataManger.manager.unlocks >= tempInt){
							dataManger.manager.actualLevel = tempInt;
							StartCoroutine(LoadLevelAsync(tempInt));
						}
					}
					if(hit.collider.name== "Color_01"){
						if(hit.collider.gameObject.GetComponent<Color_Enabled>().enabled == true){
							Color colorApply = hit.collider.gameObject.GetComponentInChildren<tk2dSprite>().color;
							if(selectedZone == "A"){
								colorChange.colorMaskRed = colorApply;
								colorSet.ColorSet(colorApply,"A");
							}
							if(selectedZone == "B"){
								colorChange.colorMaskGreen = colorApply;
								colorSet.ColorSet(colorApply,"B");
							}
						}

					}
					
				}
				
			}
		}

		if(Input.GetKeyDown(KeyCode.Escape)){

			if(Application.loadedLevelName == "Menu"){
				if(dataManger.manager.Camposition == "Forward"){
					forward = false;
					CancelInvoke("MoveCamera");
					InvokeRepeating("MoveCamera",0.01f,Time.fixedDeltaTime);
					dataManger.manager.Camposition = "";
				}else{
					dataManger.manager.Save(true);
					Application.Quit();
				}

			}else{
				if(paused){
					Pause(null);
				}else{
					Application.LoadLevel("Menu");
				}
			}

		}

	}


	public bool Pause(GameObject temp){


		if(!paused){
			paused = true;
			rigid.isKinematic = true;
			animation["UIBase_RightCol_extended_UpDown"].speed = 1;
			animation.Play("UIBase_RightCol_extended_UpDown");
			pauseText.SetActive(true);
 			Debug.Log("Pause");
			return true;
		}else{

			paused = false;
			rigid.isKinematic = false;
			animation["UIBase_RightCol_extended_UpDown"].speed = -1;
			animation.Play("UIBase_RightCol_extended_UpDown");
			animation["UIBase_RightCol_extended_UpDown"].time = animation["UIBase_RightCol_extended_UpDown"].length;
			Debug.Log("UnPause");
			pauseText.SetActive(false);
			return false;
		}
	}

	void MoveCamera(){

		Vector3 tempPos = Camera.main.transform.position;
		if(forward){
			tempPos.x = 3840 - Camera.main.GetComponent<tk2dCamera>().nativeResolutionWidth/2;
			if(Vector2.Distance(tempPos,Camera.main.transform.position)<1){
				CancelInvoke("MoveCamera");
			}else{
				Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position,tempPos,MenuSpeed*Time.fixedDeltaTime);
				Debug.Log("MoveCamera");
			}
		}else{
			tempPos.x = Camera.main.GetComponent<tk2dCamera>().nativeResolutionWidth/2;
			if(Vector2.Distance(tempPos,Camera.main.transform.position)<1){
				CancelInvoke("MoveCamera");
			}else{
				Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position,tempPos,MenuSpeed*Time.fixedDeltaTime);
				Debug.Log("MoveCamera");
			}
		}

	}

	void Garaje(bool activate){
		if(activate){
			if(Application.loadedLevelName != "Menu"){
				if(Win.activeInHierarchy){
					Result = "Win";
					Win.SetActive(false);
				}
				if(Lose.activeInHierarchy){
					Result = "Lose";
					Lose.SetActive(false);
				}
			}
			garaje.SetActive(true);
		}
		if(!activate){
			if(Application.loadedLevelName != "Menu"){
				if(Result == "Win"){
					Win.SetActive(true);
				}
				if(Result == "Lose"){
					Lose.SetActive(true);
				}
			}
			garaje.SetActive(false);
			Result = null;
		}


	}

	IEnumerator LoadLevelAsync(int Level){
		AsyncOperation async = Application.LoadLevelAsync("Level_" + Level);
		yield return async;
		Debug.Log("Loading complete");
	}


}
