using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using Soomla.Store;
using System;


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
	private Social_Manager socialManager;
	private GameObject PUilustration;
	private GameObject pauseText;
	private Ads ads;
	private GameObject check;
	private GameObject options;
	private bool levelEnable = true;
	private tk2dUIToggleButton[] buttonsPaint = new tk2dUIToggleButton[2];
	private tk2dUIToggleButton[] buttonsGarage = new tk2dUIToggleButton[4];
	private tk2dUIToggleButton[] buttonsPowerUps = new tk2dUIToggleButton[3];
	private tk2dUIToggleButton[] buttonsShips = new tk2dUIToggleButton[10];
	private List<tk2dSpriteAnimator> animators = new List<tk2dSpriteAnimator>();
	private GameObject coinCount;
	private bool backCoins = false;
	[HideInInspector]
	public GameObject actualPrompt;
	public int adLimit = 0;
	private GameObject prevStats;
	private List<GameObject> garajeDesactivar; 

	public GoogleAnalyticsV3 googleAnalytics;

	void Awake(){
		uicameraGameobject = GameObject.Find("UI_Camera");
		adLimit = 0;
		garajeDesactivar = new List<GameObject> ();
		powerManager = GetComponent<PowerUp_Manager>();
		if(Application.loadedLevelName != "Menu"){

			uiColumnExtended = uicameraGameobject.transform.FindChild("Anchor (UpperRight)/UIBase_Right/UIBase_RightCol/UIBase_RightCol_Extended").gameObject;
			if(Application.loadedLevelName.Contains("Tuto")){
				if(Application.loadedLevelName == "Tuto_3"){
					animators.Add(GameObject.Find("Astronaut_01").GetComponent<tk2dSpriteAnimator>());
				}
				ship = GameObject.Find("101(Clone)");
			}else{
				ship = GameObject.Find(dataManger.manager.actualShip + "(Clone)");
				pauseText = uicameraGameobject.transform.FindChild("Anchor (LowerCenter)/Paused").gameObject;
				animators.Add(GameObject.Find("Astronaut_01").GetComponent<tk2dSpriteAnimator>());
				animators.Add(GameObject.Find("Astronaut_02").GetComponent<tk2dSpriteAnimator>());
				animators.Add(GameObject.Find("Astronaut_03").GetComponent<tk2dSpriteAnimator>());

			}

			rigid = ship.GetComponent<Rigidbody2D>();
			animation = uiColumnExtended.GetComponent<Animation>();
			sounds = uiColumnExtended.transform.FindChild("Sound_Button").gameObject;
			music = uiColumnExtended.transform.FindChild("Music_Button").gameObject;
			Win = uicameraGameobject.transform.FindChild ("WinLayout").gameObject;
			Lose = uicameraGameobject.transform.FindChild ("LoseLayout").gameObject;
			coinCount = uicameraGameobject.transform.FindChild("Anchor (UpperRight)/UIBase_Right/CoinCount_Ico").gameObject;
			
			animators.Add(GameObject.Find("Landing Platform").transform.FindChild("LandingPlatform_Lights").GetComponent<tk2dSpriteAnimator>());


			//if(!Application.loadedLevelName.Contains("Tuto"))
//			foreach (tk2dSpriteAnimator animat in GameObject.Find("Coins").GetComponentsInChildren<tk2dSpriteAnimator>())
//				animators.Add(animat);

			//if(!Application.loadedLevelName.Contains("Tuto"))
			//foreach (tk2dSpriteAnimator animat in GameObject.Find("Coins").GetComponentsInChildren<tk2dSpriteAnimator>())
				//animators.Add(animat);


		}else{
		
			options = uicameraGameobject.transform.FindChild("Options_Menu").gameObject;
			sounds = options.transform.FindChild("Shop_Bg_01/Sound_Button").gameObject;
			music = options.transform.FindChild("Shop_Bg_01/Music_Button").gameObject;
			check = options.transform.FindChild("Shop_Bg_01/Invert Rotation/Check").gameObject;
			if(dataManger.manager.inverted){
				check.SetActive(true);
			}else{
				check.SetActive(false);
			}

		}
		if(!Application.loadedLevelName.Contains("Tuto")){
			prevStats = GameObject.Find("UI_Camera").transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_Ship01/Stats").gameObject;
			if(dataManger.manager.nextPowerUp){
				dataManger.manager.nextPowerUp = false;
				dataManger.manager.Save(false);
				powerManager.PowerUp("Shield");
			}

		}
			

		
		coin_manager =GameObject.Find("ScoreCoin_Manager").GetComponent<Coin_Manager>();
		ads = GameObject.Find("Data Manager").GetComponent<Ads>();
		garaje = uicameraGameobject.transform.FindChild ("Garage_Menu").gameObject;
		PUilustration = garaje.transform.FindChild("Canvas/Shop_Bg_01/PowerUps_Menu/Illustration_Shield").gameObject;
		garage_manager = GetComponent<Garaje_Manager>();
		socialManager = GetComponent<Social_Manager>();
		colorSet = GetComponent<Color_Set>();
		ShipGaraje = uicameraGameobject.transform.FindChild ("Garage_Menu/Canvas/Shop_Bg_01/Paint_Menu/TV/Ship01_Garage").gameObject;
		buttonsPaint[0] = uicameraGameobject.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Paint_Menu/A_Button").GetComponent<tk2dUIToggleButton>();
		buttonsPaint[1] = uicameraGameobject.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Paint_Menu/B_Button").GetComponent<tk2dUIToggleButton>();
		buttonsGarage[0] = uicameraGameobject.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Header/ShipsButton").GetComponent<tk2dUIToggleButton>();
		buttonsGarage[1] = uicameraGameobject.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Header/PowerUps_Button").GetComponent<tk2dUIToggleButton>();
		buttonsGarage[2] = uicameraGameobject.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Header/Coins_Button").GetComponent<tk2dUIToggleButton>();
		buttonsGarage[3] = uicameraGameobject.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Header/PaintButton").GetComponent<tk2dUIToggleButton>();
		buttonsPowerUps[0] = uicameraGameobject.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/PowerUps_Menu/Button_Shield").GetComponent<tk2dUIToggleButton>();
		buttonsPowerUps[1] = uicameraGameobject.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/PowerUps_Menu/Button_Fuel").GetComponent<tk2dUIToggleButton>();
		buttonsPowerUps[2] = uicameraGameobject.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/PowerUps_Menu/Button_Magnet").GetComponent<tk2dUIToggleButton>();
		buttonsShips[0] = uicameraGameobject.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_Ship01").GetComponent<tk2dUIToggleButton>();
		buttonsShips[1] = uicameraGameobject.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_Taboo").GetComponent<tk2dUIToggleButton>();
		buttonsShips[2] = uicameraGameobject.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_369").GetComponent<tk2dUIToggleButton>();
		buttonsShips[3] = uicameraGameobject.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_Box").GetComponent<tk2dUIToggleButton>();
		buttonsShips[4] = uicameraGameobject.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_UFLO").GetComponent<tk2dUIToggleButton>();
		buttonsShips[5] = uicameraGameobject.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_Mush").GetComponent<tk2dUIToggleButton>();
		buttonsShips[6] = uicameraGameobject.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_Bow").GetComponent<tk2dUIToggleButton>();
		buttonsShips[7] = uicameraGameobject.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_Big").GetComponent<tk2dUIToggleButton>();
		buttonsShips[8] = uicameraGameobject.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_Jupitar").GetComponent<tk2dUIToggleButton>();
		buttonsShips[9] = uicameraGameobject.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_Evolve").GetComponent<tk2dUIToggleButton>();
		colorChange = ShipGaraje.GetComponent<SpriteColorFX.SpriteColorMasks3>();
		uicamera = uicameraGameobject.GetComponent<Camera>();
		share = GetComponent<Share>();
		faceBook = GetComponent<FacebookSocial>();
		if (GetComponent<GoogleAnalyticsV3> ()) {
			googleAnalytics = GetComponent<GoogleAnalyticsV3> ();
		}

	}

	void OnLevelWasLoaded(int level) {
		if (dataManger.manager.nextPowerUp){
			powerManager.PowerUp(dataManger.manager.nextPowerUpName);
			dataManger.manager.nextPowerUp = false;
		}
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

						if (googleAnalytics != null){
							googleAnalytics.LogEvent ("Press", hit.collider.name , "Pulsado", (long)1);
						}
						switch(hit.collider.name ){
						case "Play" :

							forward = true;
							if(levelEnable){

								if(dataManger.manager.tutorial >= 4){
									if(IsInvoking("MoveCamera")){
										CancelInvoke("MoveCamera");
										InvokeRepeating("MoveCamera",0.01f,Time.fixedDeltaTime);
									}else{
										InvokeRepeating("MoveCamera",0.01f,Time.fixedDeltaTime);
									}
									if(PlayerPrefs.GetInt("Garaje") == 0){
										actualPrompt = GameObject.Find("UI_Camera").transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Garage").gameObject;
										actualPrompt.SetActive(true);
										GameObject.Find("UI_Camera").transform.FindChild("Prompt_Menu").gameObject.SetActive(true);
										PlayerPrefs.SetInt("Garaje",1);
									}

								}else{
									googleAnalytics.LogScreen("Tuto_"+dataManger.manager.tutorial);
									Application.LoadLevel("Tuto_"+dataManger.manager.tutorial);
								}
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
							Pause(hit.transform.gameObject,false);
							if (!dataManger.manager.Music)
								music.GetComponentInChildren<tk2dSprite>().SetSprite("MusciIco_Off");
							if (!dataManger.manager.Sounds)
								sounds.GetComponentInChildren<tk2dSprite>().SetSprite("VolumeIco_Off");
							break;
						case "Retry_Button" :
							dataManger.manager.Save(false);
							dataManger.manager.partidas++;
							if(dataManger.manager.partidas >= 3){
								dataManger.manager.partidas = 0;
								ads.LaunchInterstital();
							}
							googleAnalytics.LogScreen(Application.loadedLevel.ToString());
							Application.LoadLevel (Application.loadedLevel);
							break;	
						case "Levels_Button" :
							googleAnalytics.LogScreen("Menu");
							Application.LoadLevel ("Menu");
							if(dataManger.manager.tutorial >= 3){
								dataManger.manager.Camposition = "Forward";
							}
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
								dataManger.manager.GetComponent<AudioSource>().Pause();
								music.GetComponentInChildren<tk2dSprite>().SetSprite("MusciIco_Off");
								Debug.Log("Music off");
							}else{
								dataManger.manager.Music = true;
								dataManger.manager.GetComponent<AudioSource>().Play();
								music.GetComponentInChildren<tk2dSprite>().SetSprite("MusciIco_On");
								Debug.Log("Music on");
							}
							break;
						
						case "Next_Button" :
							int Level = dataManger.manager.actualLevel;
							dataManger.manager.actualLevel ++;
							dataManger.manager.Save(false);
							googleAnalytics.LogScreen("Level_"+(Level + 1)+"_"+dataManger.manager.actualWorld);
							Application.LoadLevel("Level_"+(Level + 1)+"_"+dataManger.manager.actualWorld);
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
							if(Application.loadedLevelName == "Menu"){
								options.SetActive(false);
							}

								uicameraGameobject.transform.FindChild("Prompt_Menu").gameObject.SetActive(false);
							if(actualPrompt != null)
								actualPrompt.SetActive(false);
							if(actualPrompt.name == "Prompt_Ads_Shield")
								ship.GetComponent<Damage>().Finish();

							
							levelEnable = true;
							break;
						case "A_Button" :
							selectedZone = "A";
							if (buttonsPaint[0].IsOn)
								buttonsPaint[0].IsOn = false;
							buttonsPaint[1].IsOn = false;
							Debug.Log("Zone A selected");
							break;
						case "B_Button" :
							selectedZone = "B";
							buttonsPaint[0].IsOn = false;
							if (buttonsPaint[1].IsOn){
								buttonsPaint[1].IsOn = false;
							}
							Debug.Log("Zone B selected");
						break;
						case "PowerUp_Shield" :
							if (!paused){
								if(dataManger.manager.shieldPowerUps >= 1 & Input.touchCount == 1){

									/*if(adLimit < 1 & dataManger.manager.shieldPowerUps == 1){
										uicameraGameobject.transform.FindChild("Prompt_Menu").gameObject.SetActive(true);
										actualPrompt = uicameraGameobject.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Ads_Shield").gameObject;
										actualPrompt.SetActive(true);
										Pause(null,false);
									}*/
									powerManager.PowerUp("Shield");
									//ACHIEVEMENT
									Social.ReportProgress("CgkIuv-YgIkeEAIQBg", 100.0f, (bool success) => {
										socialManager.Check("Achievement","CgkIuv-YgIkeEAIQBg",success);
									});
								}/*else if(adLimit <= 0 & Input.touchCount == 1){
									uicameraGameobject.transform.FindChild("Prompt_Menu").gameObject.SetActive(true);
									actualPrompt = uicameraGameobject.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Ads_Shield").gameObject;
									actualPrompt.SetActive(true);
									Pause(null,false);
								}*/
							}
							break;
						case "PowerUp_Magnet" :

							if (!paused){
								if(dataManger.manager.magnetPowerUps >= 1 & Input.touchCount == 1){

									/*if(adLimit < 1 & dataManger.manager.magnetPowerUps == 1){
										uicameraGameobject.transform.FindChild("Prompt_Menu").gameObject.SetActive(true);
										actualPrompt = uicameraGameobject.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Ads_Magnet").gameObject;
										actualPrompt.SetActive(true);
										Pause(null,false);
									}*/
									powerManager.PowerUp("Magnet");
									//ACHIEVEMENT
									Social.ReportProgress("CgkIuv-YgIkeEAIQBg", 100.0f, (bool success) => {
										socialManager.Check("Achievement","CgkIuv-YgIkeEAIQBg",success);
									});
								}/*else if(adLimit <= 0 & Input.touchCount == 1){
									uicameraGameobject.transform.FindChild("Prompt_Menu").gameObject.SetActive(true);
									actualPrompt = uicameraGameobject.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Ads_Magnet").gameObject;
									actualPrompt.SetActive(true);
									Pause(null,false);
								}*/
							}
							break;
						case "PowerUp_Fuel" :
							if (!paused){
								if(dataManger.manager.fuelPowerUps >= 1 & Input.touchCount == 1){

									/*if(adLimit < 1 & dataManger.manager.fuelPowerUps == 1){
										uicameraGameobject.transform.FindChild("Prompt_Menu").gameObject.SetActive(true);
										actualPrompt = uicameraGameobject.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Ads_Fuel").gameObject;
										actualPrompt.SetActive(true);
										Pause(null,false);
									}*/
									powerManager.PowerUp("Fuel");
									//ACHIEVEMENT
									Social.ReportProgress("CgkIuv-YgIkeEAIQBg", 100.0f, (bool success) => {
										socialManager.Check("Achievement","CgkIuv-YgIkeEAIQBg",success);
									});
								}/*else if(adLimit <= 0 & Input.touchCount == 1){
									uicameraGameobject.transform.FindChild("Prompt_Menu").gameObject.SetActive(true);
									actualPrompt = uicameraGameobject.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Ads_Fuel").gameObject;
									actualPrompt.SetActive(true);
									Pause(null,false);		
								}*/
							}
							break;
						case "Button":
							adLimit++;
							switch(hit.transform.parent.name.Substring(11)){
							case "Shield":
								uicameraGameobject.transform.FindChild("Prompt_Menu").gameObject.SetActive(false);
								uicameraGameobject.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Ads_Shield").gameObject.SetActive(false);
								ads.Launch("Shield","Rewarded");
								break;
							case "Magnet":
								uicameraGameobject.transform.FindChild("Prompt_Menu").gameObject.SetActive(false);
								uicameraGameobject.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Ads_Magnet").gameObject.SetActive(false);
								ads.Launch("Magnet","Rewarded");
								break;
							case "Fuel":
								uicameraGameobject.transform.FindChild("Prompt_Menu").gameObject.SetActive(false);
								uicameraGameobject.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Ads_Fuel").gameObject.SetActive(false);
								ads.Launch("Fuel","Rewarded");
								break;
								
							}
							break;
						case "PaintButton" :
							garage_manager.LayoutChanger("Paint");
							buttonsGarage[0].IsOn = false;
							buttonsGarage[1].IsOn = false;
							buttonsGarage[2].IsOn = false;
							if (buttonsGarage[3].IsOn)
								buttonsGarage[3].IsOn = false;
							break;
						case "ShipsButton" :
							garage_manager.LayoutChanger("Ships");
							if (buttonsGarage[0].IsOn)
								buttonsGarage[0].IsOn = false;
							buttonsGarage[1].IsOn = false;
							buttonsGarage[2].IsOn = false;
							buttonsGarage[3].IsOn = false;
							break;
						case "PowerUps_Button" :
							garage_manager.LayoutChanger("PowerUps");
							buttonsGarage[0].IsOn = false;
							if (buttonsGarage[1].IsOn)
								buttonsGarage[1].IsOn = false;
							buttonsGarage[2].IsOn = false;
							buttonsGarage[3].IsOn = false;
							break;
						case "Coins_Button" :
							garage_manager.LayoutChanger("Coins");
							buttonsGarage[0].IsOn = false;
							buttonsGarage[1].IsOn = false;
							if (buttonsGarage[2].IsOn)
								buttonsGarage[2].IsOn = false;
							buttonsGarage[3].IsOn = false;
							break;
						case "Button_Ship01" :
							if(dataManger.manager.shipUnlocks.Contains("Ship01")){
								dataManger.manager.actualShip = "Ship01";
								dataManger.manager.Save(false);
								colorSet.SpriteSet(true,"Ship01");
								prevStats.SetActive(false);
								hit.transform.FindChild("Stats").gameObject.SetActive(true);
								prevStats = hit.transform.FindChild("Stats").gameObject;
							}else{
								colorSet.SpriteSet(false,"Ship01");
								prevStats.SetActive(false);
								hit.transform.FindChild("Stats").gameObject.SetActive(true);
								prevStats = hit.transform.FindChild("Stats").gameObject;
							}
							for (int i=0; i < 10; i++){
								if (i == 0 && buttonsShips[i].IsOn)
									buttonsShips[i].IsOn = false;
								else
									buttonsShips[i].IsOn = false;
							}
							break;
						case "Button_369" :
							if(dataManger.manager.shipUnlocks.Contains("369")){
								dataManger.manager.actualShip = "369";
								dataManger.manager.Save(false);
								colorSet.SpriteSet(true,"369");
								prevStats.SetActive(false);
								hit.transform.FindChild("Stats").gameObject.SetActive(true);
								prevStats = hit.transform.FindChild("Stats").gameObject;
							}else{
								colorSet.SpriteSet(false,"369");
								prevStats.SetActive(false);
								hit.transform.FindChild("Stats").gameObject.SetActive(true);
								prevStats = hit.transform.FindChild("Stats").gameObject;
							}
							for (int i=0; i < 10; i++){
								if (i == 2 && buttonsShips[i].IsOn)
									buttonsShips[i].IsOn = false;
								else
									buttonsShips[i].IsOn = false;
							}
							break;
						case "Button_Taboo" :
							if(dataManger.manager.shipUnlocks.Contains("Taboo")){
								dataManger.manager.actualShip = "Taboo";
								dataManger.manager.Save(false);
								colorSet.SpriteSet(true,"Taboo");
								prevStats.SetActive(false);
								hit.transform.FindChild("Stats").gameObject.SetActive(true);
								prevStats = hit.transform.FindChild("Stats").gameObject;
							}else{
								colorSet.SpriteSet(false,"Taboo");
								prevStats.SetActive(false);
								hit.transform.FindChild("Stats").gameObject.SetActive(true);
								prevStats = hit.transform.FindChild("Stats").gameObject;
							}
							for (int i=0; i < 10; i++){
								if (i == 1 && buttonsShips[i].IsOn)
									buttonsShips[i].IsOn = false;
								else
									buttonsShips[i].IsOn = false;
							}
							break;
						case "Button_UFLO" :
							if(dataManger.manager.shipUnlocks.Contains("UFLO")){
								dataManger.manager.actualShip = "UFLO";
								dataManger.manager.Save(false);
								colorSet.SpriteSet(true,"UFLO");
								prevStats.SetActive(false);
								hit.transform.FindChild("Stats").gameObject.SetActive(true);
								prevStats = hit.transform.FindChild("Stats").gameObject;
							}else{
								colorSet.SpriteSet(false,"UFLO");
								prevStats.SetActive(false);
								hit.transform.FindChild("Stats").gameObject.SetActive(true);
								prevStats = hit.transform.FindChild("Stats").gameObject;
							}
							for (int i=0; i < 10; i++){
								if (i == 4 && buttonsShips[i].IsOn)
									buttonsShips[i].IsOn = false;
								else
									buttonsShips[i].IsOn = false;
							}
							break;
						case "Button_Box" :
							if(dataManger.manager.shipUnlocks.Contains("Box")){
								dataManger.manager.actualShip = "Box";
								dataManger.manager.Save(false);
								colorSet.SpriteSet(true,"Box");
								prevStats.SetActive(false);
								hit.transform.FindChild("Stats").gameObject.SetActive(true);
								prevStats = hit.transform.FindChild("Stats").gameObject;
							}else{
								colorSet.SpriteSet(false,"Box");
								prevStats.SetActive(false);
								hit.transform.FindChild("Stats").gameObject.SetActive(true);
								prevStats = hit.transform.FindChild("Stats").gameObject;
							}
							for (int i=0; i < 10; i++){
								if (i == 3 && buttonsShips[i].IsOn)
									buttonsShips[i].IsOn = false;
								else
									buttonsShips[i].IsOn = false;
							}
							break;
						case "Button_Mush" :
							if(dataManger.manager.shipUnlocks.Contains("Mush")){
								dataManger.manager.actualShip = "Mush";
								dataManger.manager.Save(false);
								colorSet.SpriteSet(true,"Mush");
								prevStats.SetActive(false);
								hit.transform.FindChild("Stats").gameObject.SetActive(true);
								prevStats = hit.transform.FindChild("Stats").gameObject;
							}else{
								colorSet.SpriteSet(false,"Mush");
								prevStats.SetActive(false);
								hit.transform.FindChild("Stats").gameObject.SetActive(true);
								prevStats = hit.transform.FindChild("Stats").gameObject;
							}
							for (int i=0; i < 10; i++){
								if (i == 5 && buttonsShips[i].IsOn)
									buttonsShips[i].IsOn = false;
								else
									buttonsShips[i].IsOn = false;
							}
							break;
						case "Button_Bow" :
							if(dataManger.manager.shipUnlocks.Contains("Bow")){
								dataManger.manager.actualShip = "Bow";
								dataManger.manager.Save(false);
								colorSet.SpriteSet(true,"Bow");
								prevStats.SetActive(false);
								hit.transform.FindChild("Stats").gameObject.SetActive(true);
								prevStats = hit.transform.FindChild("Stats").gameObject;
							}else{
								colorSet.SpriteSet(false,"Bow");
								prevStats.SetActive(false);
								hit.transform.FindChild("Stats").gameObject.SetActive(true);
								prevStats = hit.transform.FindChild("Stats").gameObject;
							}
							for (int i=0; i < 10; i++){
								if (i == 6 && buttonsShips[i].IsOn)
									buttonsShips[i].IsOn = false;
								else
									buttonsShips[i].IsOn = false;
							}
							break;
						case "Button_Big" :
							if(dataManger.manager.shipUnlocks.Contains("Big")){
								dataManger.manager.actualShip = "Big";
								dataManger.manager.Save(false);
								colorSet.SpriteSet(true,"Big");
								prevStats.SetActive(false);
								hit.transform.FindChild("Stats").gameObject.SetActive(true);
								prevStats = hit.transform.FindChild("Stats").gameObject;
							}else{
								colorSet.SpriteSet(false,"Big");
								prevStats.SetActive(false);
								hit.transform.FindChild("Stats").gameObject.SetActive(true);
								prevStats = hit.transform.FindChild("Stats").gameObject;
							}
							for (int i=0; i < 10; i++){
								if (i == 7 && buttonsShips[i].IsOn)
									buttonsShips[i].IsOn = false;
								else
									buttonsShips[i].IsOn = false;
							}
							break;
						case "Button_Jupitar" :
							if(dataManger.manager.shipUnlocks.Contains("Jupitar")){
								dataManger.manager.actualShip = "Jupitar";
								dataManger.manager.Save(false);
								colorSet.SpriteSet(true,"Jupitar");
								prevStats.SetActive(false);
								hit.transform.FindChild("Stats").gameObject.SetActive(true);
								prevStats = hit.transform.FindChild("Stats").gameObject;
							}else{
								colorSet.SpriteSet(false,"Jupitar");
								prevStats.SetActive(false);
								hit.transform.FindChild("Stats").gameObject.SetActive(true);
								prevStats = hit.transform.FindChild("Stats").gameObject;
							}
							for (int i=0; i < 10; i++){
								if (i == 8 && buttonsShips[i].IsOn)
									buttonsShips[i].IsOn = false;
								else
									buttonsShips[i].IsOn = false;
							}
							break;
						case "Button_Evolve" :
							if(dataManger.manager.shipUnlocks.Contains("Evolve")){
								dataManger.manager.actualShip = "Evolve";
								dataManger.manager.Save(false);
								colorSet.SpriteSet(true,"Evolve");
								prevStats.SetActive(false);
								hit.transform.FindChild("Stats").gameObject.SetActive(true);
								prevStats = hit.transform.FindChild("Stats").gameObject;
							}else{
								colorSet.SpriteSet(false,"Evolve");
								prevStats.SetActive(false);
								hit.transform.FindChild("Stats").gameObject.SetActive(true);
								prevStats = hit.transform.FindChild("Stats").gameObject;
							}
							for (int i=0; i < 10; i++){
								if (i == 9 && buttonsShips[i].IsOn)
									buttonsShips[i].IsOn = false;
								else
									buttonsShips[i].IsOn = false;
							}
							break;
						case "Button_Buy" :
							if(hit.collider.transform.FindChild("Button_Buy_Down").gameObject.activeInHierarchy){
								Value value = hit.collider.gameObject.GetComponent<Value>();
								if(value == null & hit.transform.parent.name.Contains("IAP")){
									if(coin_manager.Compra(0,"IAP",hit.transform.parent.name)){

									}
								}
								if(coin_manager.Compra(value.Cost,value._Type,hit.transform.parent.name.Substring(7))){
									if(value._Type == "Ship" || value._Type == "World"){
										Destroy(hit.collider.gameObject);
									}
									Debug.Log("Comprado " + hit.transform.parent.name.Substring(7));
									//ACHIEVEMENT: 
									Social.ReportProgress("CgkIuv-YgIkeEAIQBA", 100.0f, (bool success) => {
										socialManager.Check("Achievement","CgkIuv-YgIkeEAIQBA",success);
									});
								}else{
									Debug.Log("Algo falla joder");
								}
							}
							break;
						case "Cheat" :
							dataManger.manager.coins += 50000;
							dataManger.manager.Save(false);
							break;
						case "Button_Shield" :
							if(PUilustration != null){
								PUilustration.SetActive(false);
							}
							PUilustration = garaje.transform.FindChild("Canvas/Shop_Bg_01/PowerUps_Menu/Illustration_Shield").gameObject;
							PUilustration.SetActive(true);
							if (buttonsPowerUps[0].IsOn)
								buttonsPowerUps[0].IsOn = false;
							buttonsPowerUps[1].IsOn = false;
							buttonsPowerUps[2].IsOn = false;
							break;
							
						case "Button_Magnet" :
							if(PUilustration != null){
								PUilustration.SetActive(false);
							}
							PUilustration = garaje.transform.FindChild("Canvas/Shop_Bg_01/PowerUps_Menu/Illustration_Magnet").gameObject;
							PUilustration.SetActive(true);
							buttonsPowerUps[0].IsOn = false;
							buttonsPowerUps[1].IsOn = false;
							if (buttonsPowerUps[2].IsOn)
								buttonsPowerUps[2].IsOn = false;
							break;
							
						case "Button_Fuel" :
							if(PUilustration != null){
								PUilustration.SetActive(false);
							}
							PUilustration = garaje.transform.FindChild("Canvas/Shop_Bg_01/PowerUps_Menu/Illustration_Fuel").gameObject;
							PUilustration.SetActive(true);
							buttonsPowerUps[0].IsOn = false;
							if (buttonsPowerUps[1].IsOn)
								buttonsPowerUps[1].IsOn = false;
							buttonsPowerUps[2].IsOn = false;
							break;
						
						case "Check_Frame" :
							dataManger.manager.inverted = !dataManger.manager.inverted;
							if(dataManger.manager.inverted){
								check.SetActive(true);
							}else{
								check.SetActive(false);
							}
							dataManger.manager.Save(false);
							break;	
						case "Options_Button" :
							options.SetActive(true);
							if (!dataManger.manager.Music)
								music.GetComponentInChildren<tk2dSprite>().SetSprite("MusciIco_Off");
							if (!dataManger.manager.Sounds)
								sounds.GetComponentInChildren<tk2dSprite>().SetSprite("VolumeIco_Off");
							levelEnable = false;
							break;
						case "GooglePlayButton":
							// authenticate user:
							Social.localUser.Authenticate((bool success) => {
								socialManager.Check("Login",success);
							});
							break;
						case "LeaderboardButton":
							// show leaderboard UI
							Social.ShowLeaderboardUI();
							break;
						case "AchievmentsButton":
							// show achievements UI
							Social.ShowAchievementsUI();
							break;	
						case "FeedbackButton":
							string email = "info@evolvegames.es";
							string subject = MyEscapeURL("FeedBack Rescue Lander");
							string body = MyEscapeURL("");
							
							Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
						
							break;
						case "RateUsButton":
							if (platform == RuntimePlatform.Android){
								Application.OpenURL("market://details?id=com.EvolveGames.RescueLander");
							}
							else if (platform == RuntimePlatform.IPhonePlayer){
								Application.OpenURL("itms-apps://itunes.apple.com/app/idYOUR_ID");
							}
							PlayerPrefs.SetInt("Rate",100);
							PlayerPrefs.Save();
							break;
						case "MorePowerUps_Button":
							Garaje(true);
							garage_manager.LayoutChanger("PowerUps");
							break;	
							
						case "SkipButton":
							uicameraGameobject.transform.FindChild("Prompt_Menu").gameObject.SetActive(true);

							uicameraGameobject.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_TutoSkip").gameObject.SetActive(true);
							break;
						case "Button_Later":
							uicameraGameobject.transform.FindChild("Prompt_Menu").gameObject.SetActive(false);
							//uicameraGameobject.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_TutoSkip").gameObject.SetActive(false);
							break;
						case "Button_Never":
							PlayerPrefs.SetInt("Rate",100);
							PlayerPrefs.Save();
							//dataManger.manager.tutorial = 4;
							//dataManger.manager.Save(false);
							//googleAnalytics.LogScreen ("Menu");
							uicameraGameobject.transform.FindChild("Prompt_Menu").gameObject.SetActive(false);
							Application.LoadLevel("Menu");
							break;
						case "MoreCoins_Button":
							Garaje(true);
							garage_manager.LayoutChanger("Coins");
							buttonsGarage[0].IsOn = false;
							buttonsGarage[1].IsOn = false;
							//if (buttonsGarage[2].IsOn)
								buttonsGarage[2].IsOn = true;
							buttonsGarage[3].IsOn = false;
							uicameraGameobject.transform.FindChild("Prompt_Menu").gameObject.SetActive(false);
							actualPrompt.SetActive(false);
							break;
						case "HelpButton":
							actualPrompt = uicameraGameobject.transform.FindChild("Prompt_Help").gameObject;
							actualPrompt.SetActive(true);
							break;	
						case "FacebookButton":
							Application.OpenURL("https://www.facebook.com/EvolveGames.dev");
							break;
						case "TwitterButton":
							Application.OpenURL("https://twitter.com/EvolveGames_");
							break;
						case "Reset_Button" :
							googleAnalytics.LogScreen (Application.loadedLevel.ToString());
							Application.LoadLevel (Application.loadedLevel);
							break;
						case "Button_World_Ice" :
							if (dataManger.manager.worldUnlocks.Contains("Ice")){
								Debug.Log("World: " + hit.transform.name.Substring(13));
								hit.transform.GetComponent<World_Change>().ChangeLevelName(hit.transform.name.Substring(13));
							}
							break;
						case "Button_World_Mars" :
							Debug.Log("World: " + hit.transform.name.Substring(13));
							hit.transform.GetComponent<World_Change>().ChangeLevelName(hit.transform.name.Substring(13));
							break;
						case "Tuto_Button" : 
							dataManger.manager.tutorial = 1;
							dataManger.manager.Save(false);
							Application.LoadLevel("Tuto_1");
							break;
						default :

							break;
							
						}
						if(hit.collider.tag == "Level_Ico" & levelEnable){
							ParseLevelName(hit.collider.name);

						}
						if(hit.collider.name.Contains("Color")){
							if(hit.collider.gameObject.GetComponent<Color_Enabled>().enabled == true){
								Color colorApply = hit.collider.gameObject.GetComponentInChildren<tk2dSprite>().color;
								bool changeColor = false;
								if(selectedZone == "A"){
									colorChange.colorMaskRed = colorApply;
									colorSet.ColorSet(colorApply,"A");
									changeColor = true;
								}
								if(selectedZone == "B"){
									colorChange.colorMaskGreen = colorApply;
									colorSet.ColorSet(colorApply,"B");
									changeColor = true;
								}
								dataManger.manager.Save(false);
								//ACHIEVEMENT
								if (changeColor){
									Social.ReportProgress("CgkIuv-YgIkeEAIQBQ", 100.0f, (bool success) => {
										socialManager.Check("Achievement","CgkIuv-YgIkeEAIQBQ",success);
									});
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
		

					switch(hit.collider.name ){
					case "Play" :
						forward = true;
						if(levelEnable){

							if(dataManger.manager.tutorial >= 4){

									if(PlayerPrefs.GetInt("Garaje") == 0){
										actualPrompt = GameObject.Find("UI_Camera").transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Garage").gameObject;
										actualPrompt.SetActive(true);
										GameObject.Find("UI_Camera").transform.FindChild("Prompt_Menu").gameObject.SetActive(true);
										PlayerPrefs.SetInt("Garaje",1);
										PlayerPrefs.Save();
									}else{
										if(IsInvoking("MoveCamera")){
											CancelInvoke("MoveCamera");
											InvokeRepeating("MoveCamera",0.01f,Time.fixedDeltaTime);
										}else{
											InvokeRepeating("MoveCamera",0.01f,Time.fixedDeltaTime);
										}
									}

							}else{
								googleAnalytics.LogScreen("Tuto_"+dataManger.manager.tutorial);
								Application.LoadLevel("Tuto_"+dataManger.manager.tutorial);
							}
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
						Pause(hit.transform.gameObject,false);
						if (!dataManger.manager.Music)
							music.GetComponentInChildren<tk2dSprite>().SetSprite("MusciIco_Off");
						if (!dataManger.manager.Sounds)
							sounds.GetComponentInChildren<tk2dSprite>().SetSprite("VolumeIco_Off");
						break;
					case "Retry_Button" :
						dataManger.manager.Save(false);
						dataManger.manager.partidas++;
						if(dataManger.manager.partidas >= 3){
							dataManger.manager.partidas = 0;
							ads.LaunchInterstital();
						}
						googleAnalytics.LogScreen(Application.loadedLevel.ToString());
						Application.LoadLevel (Application.loadedLevel);
						break;	
					case "Levels_Button" :
						googleAnalytics.LogScreen("Menu");
						Application.LoadLevel ("Menu");
						if(dataManger.manager.tutorial >= 3){
							dataManger.manager.Camposition = "Forward";
						}
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
							dataManger.manager.GetComponent<AudioSource>().Pause();
							music.GetComponentInChildren<tk2dSprite>().SetSprite("MusciIco_Off");
							Debug.Log("Music off");
						}else{
							dataManger.manager.Music = true;
							dataManger.manager.GetComponent<AudioSource>().Play();
							music.GetComponentInChildren<tk2dSprite>().SetSprite("MusciIco_On");
							Debug.Log("Music on");
						}
						break;
					case "Next_Button" :
						int Level = dataManger.manager.actualLevel;
						dataManger.manager.actualLevel ++;
						dataManger.manager.Save(false);
						googleAnalytics.LogScreen("Level_"+(Level + 1)+"_"+dataManger.manager.actualWorld);
						Application.LoadLevel("Level_"+(Level + 1)+"_"+dataManger.manager.actualWorld);
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
						if(Application.loadedLevelName == "Menu"){
							options.SetActive(false);
						}
						uicameraGameobject.transform.FindChild("Prompt_Menu").gameObject.SetActive(false);
						if(actualPrompt != null)
							actualPrompt.SetActive(false);
						
						levelEnable = true;
						break;
					case "A_Button" :
						selectedZone = "A";
						if (buttonsPaint[0].IsOn){
							buttonsPaint[0].IsOn = false;
						}
						buttonsPaint[1].IsOn = false;
						Debug.Log("Zone A selected");
						break;
					case "B_Button" :
						selectedZone = "B";
						buttonsPaint[0].IsOn = false;
						if (buttonsPaint[1].IsOn){
							buttonsPaint[1].IsOn = false;
						}
						Debug.Log("Zone B selected");
						break;
					case "PowerUp_Shield" :
						if (!paused){
							if(dataManger.manager.shieldPowerUps >= 1){
								powerManager.PowerUp("Shield");

							}/*else{
								uicameraGameobject.transform.FindChild("Prompt_Menu").gameObject.SetActive(true);
								actualPrompt = uicameraGameobject.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Ads_Shield").gameObject;
								actualPrompt.SetActive(true);
								Pause(null,false);
							}*/
						}
						break;
					case "PowerUp_Magnet" :
						if (!paused){
							if(dataManger.manager.magnetPowerUps >= 1){

								powerManager.PowerUp("Magnet");

							}/*else{
								uicameraGameobject.transform.FindChild("Prompt_Menu").gameObject.SetActive(true);
								actualPrompt = uicameraGameobject.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Ads_Magnet").gameObject;
								actualPrompt.SetActive(true);
								Pause(null,false);
							}*/
						}
						break;
					case "PowerUp_Fuel" :
						if (!paused){
							if(dataManger.manager.fuelPowerUps >= 1 ){
								powerManager.PowerUp("Fuel");

							}/*else{
								Debug.Log("PU_Global");
								uicameraGameobject.transform.FindChild("Prompt_Menu").gameObject.SetActive(true);
								actualPrompt = uicameraGameobject.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Ads_Fuel").gameObject;
								actualPrompt.SetActive(true);
								Pause(null,false);
							}*/
						}
						break;
					case "Button":
						
						switch(hit.transform.parent.name.Substring(11)){

						case "Shield":
							//uicameraGameobject.transform.FindChild("Prompt_Menu").gameObject.SetActive(false);
							//uicameraGameobject.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Ads_Shield").gameObject.SetActive(false);
							ads.Launch("Shield","Rewarded");
								break;
						case "Magnet":
							uicameraGameobject.transform.FindChild("Prompt_Menu").gameObject.SetActive(false);
							uicameraGameobject.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Ads_Magnet").gameObject.SetActive(false);
							ads.Launch("Magnet","Rewarded");
							break;
						case "Fuel":

							uicameraGameobject.transform.FindChild("Prompt_Menu").gameObject.SetActive(false);
							uicameraGameobject.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Ads_Fuel").gameObject.SetActive(false);
							ads.Launch("Fuel","Rewarded");
							break;

						}
						break;
					case "PaintButton" :
						garage_manager.LayoutChanger("Paint");
						buttonsGarage[0].IsOn = false;
						buttonsGarage[1].IsOn = false;
						buttonsGarage[2].IsOn = false;
						if (buttonsGarage[3].IsOn)
							buttonsGarage[3].IsOn = false;
						break;
					case "ShipsButton" :
						garage_manager.LayoutChanger("Ships");
						if (buttonsGarage[0].IsOn)
							buttonsGarage[0].IsOn = false;
						buttonsGarage[1].IsOn = false;
						buttonsGarage[2].IsOn = false;
						buttonsGarage[3].IsOn = false;
						break;
					case "PowerUps_Button" :
						garage_manager.LayoutChanger("PowerUps");
						buttonsGarage[0].IsOn = false;
						if (buttonsGarage[1].IsOn)
							buttonsGarage[1].IsOn = false;
						buttonsGarage[2].IsOn = false;
						buttonsGarage[3].IsOn = false;
						break;
					case "Coins_Button" :
						garage_manager.LayoutChanger("Coins");
						buttonsGarage[0].IsOn = false;
						buttonsGarage[1].IsOn = false;
						if (buttonsGarage[2].IsOn)
							buttonsGarage[2].IsOn = false;
						buttonsGarage[3].IsOn = false;
						break;
					case "Button_Ship01" :
						if(dataManger.manager.shipUnlocks.Contains("Ship01")){
							dataManger.manager.actualShip = "Ship01";
							dataManger.manager.Save(false);
							colorSet.SpriteSet(true,"Ship01");
							prevStats.SetActive(false);
							hit.transform.FindChild("Stats").gameObject.SetActive(true);
							prevStats = hit.transform.FindChild("Stats").gameObject;
						}else{
							colorSet.SpriteSet(false,"Ship01");
							prevStats.SetActive(false);
							hit.transform.FindChild("Stats").gameObject.SetActive(true);
							prevStats = hit.transform.FindChild("Stats").gameObject;
						}
						for (int i=0; i < 10; i++){
							if (i == 0 && buttonsShips[i].IsOn)
								buttonsShips[i].IsOn = false;
							else
								buttonsShips[i].IsOn = false;
						}
						break;
					case "Button_369" :
						if(dataManger.manager.shipUnlocks.Contains("369")){
							dataManger.manager.actualShip = "369";
							dataManger.manager.Save(false);
							colorSet.SpriteSet(true,"369");
							prevStats.SetActive(false);
							hit.transform.FindChild("Stats").gameObject.SetActive(true);
							prevStats = hit.transform.FindChild("Stats").gameObject;
						}else{
							colorSet.SpriteSet(false,"369");
							prevStats.SetActive(false);
							hit.transform.FindChild("Stats").gameObject.SetActive(true);
							prevStats = hit.transform.FindChild("Stats").gameObject;
						}
						for (int i=0; i < 10; i++){
							if (i == 2 && buttonsShips[i].IsOn)
								buttonsShips[i].IsOn = false;
							else
								buttonsShips[i].IsOn = false;
						}
						break;
					case "Button_Taboo" :
						if(dataManger.manager.shipUnlocks.Contains("Taboo")){
							dataManger.manager.actualShip = "Taboo";
							dataManger.manager.Save(false);
							colorSet.SpriteSet(true,"Taboo");
							prevStats.SetActive(false);
							hit.transform.FindChild("Stats").gameObject.SetActive(true);
							prevStats = hit.transform.FindChild("Stats").gameObject;
						}else{
							colorSet.SpriteSet(false,"Taboo");
							prevStats.SetActive(false);
							hit.transform.FindChild("Stats").gameObject.SetActive(true);
							prevStats = hit.transform.FindChild("Stats").gameObject;
						}
						for (int i=0; i < 10; i++){
							if (i == 1 && buttonsShips[i].IsOn)
								buttonsShips[i].IsOn = false;
							else
								buttonsShips[i].IsOn = false;
						}
						break;
					case "Button_UFLO" :
						if(dataManger.manager.shipUnlocks.Contains("UFLO")){
							dataManger.manager.actualShip = "UFLO";
							dataManger.manager.Save(false);
							colorSet.SpriteSet(true,"UFLO");
							prevStats.SetActive(false);
							hit.transform.FindChild("Stats").gameObject.SetActive(true);
							prevStats = hit.transform.FindChild("Stats").gameObject;
						}else{
							colorSet.SpriteSet(false,"UFLO");
							prevStats.SetActive(false);
							hit.transform.FindChild("Stats").gameObject.SetActive(true);
							prevStats = hit.transform.FindChild("Stats").gameObject;
						}
						for (int i=0; i < 10; i++){
							if (i == 4 && buttonsShips[i].IsOn)
								buttonsShips[i].IsOn = false;
							else
								buttonsShips[i].IsOn = false;
						}
						break;
					case "Button_Box" :
						if(dataManger.manager.shipUnlocks.Contains("Box")){
							dataManger.manager.actualShip = "Box";
							dataManger.manager.Save(false);
							colorSet.SpriteSet(true,"Box");
							prevStats.SetActive(false);
							hit.transform.FindChild("Stats").gameObject.SetActive(true);
							prevStats = hit.transform.FindChild("Stats").gameObject;
						}else{
							colorSet.SpriteSet(false,"Box");
							prevStats.SetActive(false);
							hit.transform.FindChild("Stats").gameObject.SetActive(true);
							prevStats = hit.transform.FindChild("Stats").gameObject;
						}
						for (int i=0; i < 10; i++){
							if (i == 3 && buttonsShips[i].IsOn)
								buttonsShips[i].IsOn = false;
							else
								buttonsShips[i].IsOn = false;
						}
						break;
					case "Button_Mush" :
						if(dataManger.manager.shipUnlocks.Contains("Mush")){
							dataManger.manager.actualShip = "Mush";
							dataManger.manager.Save(false);
							colorSet.SpriteSet(true,"Mush");
							prevStats.SetActive(false);
							hit.transform.FindChild("Stats").gameObject.SetActive(true);
							prevStats = hit.transform.FindChild("Stats").gameObject;
						}else{
							colorSet.SpriteSet(false,"Mush");
							prevStats.SetActive(false);
							hit.transform.FindChild("Stats").gameObject.SetActive(true);
							prevStats = hit.transform.FindChild("Stats").gameObject;
						}
						for (int i=0; i < 10; i++){
							if (i == 5 && buttonsShips[i].IsOn)
								buttonsShips[i].IsOn = false;
							else
								buttonsShips[i].IsOn = false;
						}
						break;
					case "Button_Bow" :
						if(dataManger.manager.shipUnlocks.Contains("Bow")){
							dataManger.manager.actualShip = "Bow";
							dataManger.manager.Save(false);
							colorSet.SpriteSet(true,"Bow");
							prevStats.SetActive(false);
							hit.transform.FindChild("Stats").gameObject.SetActive(true);
							prevStats = hit.transform.FindChild("Stats").gameObject;
						}else{
							colorSet.SpriteSet(false,"Bow");
							prevStats.SetActive(false);
							hit.transform.FindChild("Stats").gameObject.SetActive(true);
							prevStats = hit.transform.FindChild("Stats").gameObject;
						}
						for (int i=0; i < 10; i++){
							if (i == 6 && buttonsShips[i].IsOn)
								buttonsShips[i].IsOn = false;
							else
								buttonsShips[i].IsOn = false;
						}
						break;
					case "Button_Big" :
						if(dataManger.manager.shipUnlocks.Contains("Big")){
							dataManger.manager.actualShip = "Big";
							dataManger.manager.Save(false);
							colorSet.SpriteSet(true,"Big");
							prevStats.SetActive(false);
							hit.transform.FindChild("Stats").gameObject.SetActive(true);
							prevStats = hit.transform.FindChild("Stats").gameObject;
						}else{
							colorSet.SpriteSet(false,"Big");
							prevStats.SetActive(false);
							hit.transform.FindChild("Stats").gameObject.SetActive(true);
							prevStats = hit.transform.FindChild("Stats").gameObject;
						}
						for (int i=0; i < 10; i++){
							if (i == 7 && buttonsShips[i].IsOn)
								buttonsShips[i].IsOn = false;
							else
								buttonsShips[i].IsOn = false;
						}
						break;
					case "Button_Jupitar" :
						if(dataManger.manager.shipUnlocks.Contains("Jupitar")){
							dataManger.manager.actualShip = "Jupitar";
							dataManger.manager.Save(false);
							colorSet.SpriteSet(true,"Jupitar");
							prevStats.SetActive(false);
							hit.transform.FindChild("Stats").gameObject.SetActive(true);
							prevStats = hit.transform.FindChild("Stats").gameObject;
						}else{
							colorSet.SpriteSet(false,"Jupitar");
							prevStats.SetActive(false);
							hit.transform.FindChild("Stats").gameObject.SetActive(true);
							prevStats = hit.transform.FindChild("Stats").gameObject;
						}
						for (int i=0; i < 10; i++){
							if (i == 8 && buttonsShips[i].IsOn)
								buttonsShips[i].IsOn = false;
							else
								buttonsShips[i].IsOn = false;
						}
						break;
					case "Button_Evolve" :
						if(dataManger.manager.shipUnlocks.Contains("Evolve")){
							dataManger.manager.actualShip = "Evolve";
							dataManger.manager.Save(false);
							colorSet.SpriteSet(true,"Evolve");
							prevStats.SetActive(false);
							hit.transform.FindChild("Stats").gameObject.SetActive(true);
							prevStats = hit.transform.FindChild("Stats").gameObject;
						}else{
							colorSet.SpriteSet(false,"Evolve");
							prevStats.SetActive(false);
							hit.transform.FindChild("Stats").gameObject.SetActive(true);
							prevStats = hit.transform.FindChild("Stats").gameObject;
						}
						for (int i=0; i < 10; i++){
							if (i == 9 && buttonsShips[i].IsOn)
								buttonsShips[i].IsOn = false;
							else
								buttonsShips[i].IsOn = false;
						}
						break;
					case "Button_Buy" :
						if(hit.collider.transform.FindChild("Button_Buy_Down").gameObject.activeInHierarchy){
							Value value = hit.collider.gameObject.GetComponent<Value>();
							if(value == null & hit.transform.parent.name.Contains("IAP")){
								if(coin_manager.Compra(0,"IAP",hit.transform.parent.name)){
									
								}else{
									Debug.Log("Algo falla");
								}
							}
							if(value != null)
								if(coin_manager.Compra(value.Cost,value._Type,hit.transform.parent.name.Substring(7))){
									if(value._Type == "Ship" || value._Type == "World"){
										Destroy(hit.collider.gameObject);
										if(value._Type == "Ship"){
										hit.transform.parent.FindChild("Owned_Label").gameObject.SetActive(true);
										}
									}
									Debug.Log("Comprado " + hit.transform.parent.name.Substring(7));
								}else{
									Debug.Log("Algo falla");
								}
						}
						break;
					case "Cheat" :
						dataManger.manager.coins += 50000;
						dataManger.manager.Save(false);
						break;
					case "Button_Shield" :
						if(PUilustration != null){
							PUilustration.SetActive(false);
						}
						PUilustration = garaje.transform.FindChild("Canvas/Shop_Bg_01/PowerUps_Menu/Illustration_Shield").gameObject;
						PUilustration.SetActive(true);
						if (buttonsPowerUps[0].IsOn)
							buttonsPowerUps[0].IsOn = false;
						buttonsPowerUps[1].IsOn = false;
						buttonsPowerUps[2].IsOn = false;
						break;
						
					case "Button_Magnet" :
						if(PUilustration != null){
							PUilustration.SetActive(false);
						}
						PUilustration = garaje.transform.FindChild("Canvas/Shop_Bg_01/PowerUps_Menu/Illustration_Magnet").gameObject;
						PUilustration.SetActive(true);
						buttonsPowerUps[0].IsOn = false;
						buttonsPowerUps[1].IsOn = false;
						if (buttonsPowerUps[2].IsOn)
							buttonsPowerUps[2].IsOn = false;
						break;
						
					case "Button_Fuel" :
						if(PUilustration != null){
							PUilustration.SetActive(false);
						}
						PUilustration = garaje.transform.FindChild("Canvas/Shop_Bg_01/PowerUps_Menu/Illustration_Fuel").gameObject;
						PUilustration.SetActive(true);
						buttonsPowerUps[0].IsOn = false;
						if (buttonsPowerUps[1].IsOn)
							buttonsPowerUps[1].IsOn = false;
						buttonsPowerUps[2].IsOn = false;
						break;
					case "Check_Frame" :
						dataManger.manager.inverted = !dataManger.manager.inverted;
						if(dataManger.manager.inverted){
							check.SetActive(true);
						}else{
							check.SetActive(false);
						}
						dataManger.manager.Save(false);
						break;

					case "Options_Button" :
						options.SetActive(true);
						if (!dataManger.manager.Music)
							music.GetComponentInChildren<tk2dSprite>().SetSprite("MusciIco_Off");
						if (!dataManger.manager.Sounds)
							sounds.GetComponentInChildren<tk2dSprite>().SetSprite("VolumeIco_Off");
						levelEnable = false;
						break;

					case "GooglePlayButton":
						// authenticate user:
						Social.localUser.Authenticate((bool success) => {
							socialManager.Check("Login",success);
						});
						break;
					case "LeaderboardButton":
						// show leaderboard UI
						Social.ShowLeaderboardUI();
						break;
					case "AchievmentsButton":
						// show achievements UI
						Social.ShowAchievementsUI();
						break;	
					case "FeedbackButton":
						string email = "info@evolvegames.es";
						string subject = MyEscapeURL("FeedBack Rescue Lander");
						string body = MyEscapeURL("");
						
						Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
						
						break;
					case "MorePowerUps_Button":
						Garaje(true);
						garage_manager.LayoutChanger("PowerUps");
						break;	
					case "SkipButton":
						uicameraGameobject.transform.FindChild("Prompt_Menu").gameObject.SetActive(true);
						uicameraGameobject.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_TutoSkip").gameObject.SetActive(true);
						break;
					case "Button_Later":
						uicameraGameobject.transform.FindChild("Prompt_Menu").gameObject.SetActive(false);
						//uicameraGameobject.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_TutoSkip").gameObject.SetActive(false);
						break;
					case "Button_Never":
						PlayerPrefs.SetInt("Rate",100);
						PlayerPrefs.Save();
						/*
						dataManger.manager.tutorial = 4;
						dataManger.manager.Save(false);
						googleAnalytics.LogScreen ("Menu");
						Application.LoadLevel("Menu");*/
						uicameraGameobject.transform.FindChild("Prompt_Menu").gameObject.SetActive(false);
						break;
					case "HelpButton":
						actualPrompt = uicameraGameobject.transform.FindChild("Prompt_Help").gameObject;
						actualPrompt.SetActive(true);
						break;	
					case "MoreCoins_Button":
						Garaje(true);
						garage_manager.LayoutChanger("Coins");
						buttonsGarage[0].IsOn = false;
						buttonsGarage[1].IsOn = false;
						//if (buttonsGarage[2].IsOn)
							buttonsGarage[2].IsOn = true;
						buttonsGarage[3].IsOn = false;
						uicameraGameobject.transform.FindChild("Prompt_Menu").gameObject.SetActive(false);
						if(actualPrompt != null)
							actualPrompt.SetActive(false);
						break;
					case "FacebookButton":
						Application.OpenURL("https://www.facebook.com/EvolveGames.dev");
						break;
					case "TwitterButton":
						Application.OpenURL("https://twitter.com/EvolveGames_");
						break;
						
					case "Reset_Button" :
						googleAnalytics.LogScreen (Application.loadedLevel.ToString());
						Application.LoadLevel (Application.loadedLevel);
						break;
					case "Button_World_Ice" :
						if (dataManger.manager.worldUnlocks.Contains("Ice")){
							Debug.Log("World: " + hit.transform.name.Substring(13));
							hit.transform.GetComponent<World_Change>().ChangeLevelName(hit.transform.name.Substring(13));
						}
						break;
					case "Button_World_Mars" :
						Debug.Log("World: " + hit.transform.name.Substring(13));
						hit.transform.GetComponent<World_Change>().ChangeLevelName(hit.transform.name.Substring(13));
						break;
					case "Tuto_Button" : 
						dataManger.manager.tutorial = 1;
						dataManger.manager.Save(false);
						Application.LoadLevel("Tuto_1");
						break;
					default :
						
						break;
						
					}
					if(hit.collider.tag == "Level_Ico" & levelEnable){
						ParseLevelName(hit.collider.name);
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
					Pause(null,false);
				}else{
					Pause(null,false);
				}
			}

		}
	
		if (backCoins && !animation.IsPlaying("UIBase_RightCol_extended_UpDown")) {
			coinCount.transform.localPosition = new Vector3 (-90, coinCount.transform.localPosition.y);
		}
	}


	public bool Pause(GameObject temp, bool Ads){


		if(!paused){
			paused = true;
			if(!Ads){
				rigid.isKinematic = true;
				animation["UIBase_RightCol_extended_UpDown"].speed = 1;
				animation.Play("UIBase_RightCol_extended_UpDown");
				if(!Application.loadedLevelName.Contains("Tuto"))
				pauseText.SetActive(true);
				backCoins = false;
				coinCount.transform.localPosition = new Vector3(-210,coinCount.transform.localPosition.y);
				for (int i=0; i< animators.Count; i++){
					if (animators[i] != null)
						animators[i].Pause();
				}
			}




			return true;
		}else{

			paused = false;
			if(!Ads){
				rigid.isKinematic = false;
				rigid.velocity = ship.GetComponent<Damage>().saveSpeed;
				animation["UIBase_RightCol_extended_UpDown"].speed = -1;
				animation.Play("UIBase_RightCol_extended_UpDown");
				animation["UIBase_RightCol_extended_UpDown"].time = animation["UIBase_RightCol_extended_UpDown"].length;
				backCoins = true;
		
				if(!Application.loadedLevelName.Contains("Tuto"))
					pauseText.SetActive(false);
				
				if(Application.loadedLevelName.Contains("Level")){
					uicameraGameobject.transform.FindChild("Prompt_Menu").gameObject.SetActive(false);
					/*uicameraGameobject.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Ads_Magnet").gameObject.SetActive(false);
					uicameraGameobject.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Ads_Shield").gameObject.SetActive(false);
					uicameraGameobject.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Ads_Fuel").gameObject.SetActive(false);*/
				}
				for (int i=0; i< animators.Count; i++){
					if (animators[i] != null)
						animators[i].Resume();
				}
			}

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
			}
		}else{
			tempPos.x = Camera.main.GetComponent<tk2dCamera>().nativeResolutionWidth/2;
			if(Vector2.Distance(tempPos,Camera.main.transform.position)<1){
				CancelInvoke("MoveCamera");
			}else{
				Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position,tempPos,MenuSpeed*Time.fixedDeltaTime);

			}
		}

	}

	void Garaje(bool activate){
		if(activate){
			garajeDesactivar.Clear ();
			if(Application.loadedLevelName != "Menu"){
				garajeDesactivar.Add (GameObject.Find("UI_Camera").transform.FindChild("Anchor (LowerLeft)").gameObject);
				garajeDesactivar[garajeDesactivar.Count-1].SetActive(false);
				garajeDesactivar.Add (GameObject.Find("UI_Camera").transform.FindChild("Anchor (LowerRight)").gameObject);
				garajeDesactivar[garajeDesactivar.Count-1].SetActive(false);
				if(Win.activeInHierarchy){
					Result = "Win";
					Win.SetActive(false);
				}
				if(Lose.activeInHierarchy){
					Result = "Lose";
					Lose.SetActive(false);
				}
			}else{
				GameObject.Find("UI_Camera").transform.FindChild("Options_Menu").gameObject.SetActive(false);
				garajeDesactivar.Add (GameObject.Find("UI_Camera").transform.FindChild("Anchor (UpperRight)").gameObject);
				garajeDesactivar[garajeDesactivar.Count-1].SetActive(false);
				garajeDesactivar.Add (GameObject.Find("Back_Button"));
				garajeDesactivar[garajeDesactivar.Count-1].SetActive(false);
				garajeDesactivar.Add (GameObject.Find ("Button_Garage"));
				garajeDesactivar[garajeDesactivar.Count-1].SetActive(false);
				garajeDesactivar.Add (GameObject.Find ("World_Buttons"));
				garajeDesactivar[garajeDesactivar.Count-1].SetActive(false);
			}
			if(actualPrompt != null){
				actualPrompt.SetActive(false);
				GameObject.Find("UI_Camera").transform.FindChild("Prompt_Menu").gameObject.SetActive(false);
			}
			garaje.SetActive(true);
			levelEnable = false;
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
			foreach(GameObject garajeActivar in garajeDesactivar)
				garajeActivar.SetActive(true);
			garaje.SetActive(false);
			levelEnable = true;
			Result = null;
		}


	}

	void ParseLevelName(string name){
		string tempString;
		string tempString2;
		if(name.Substring(7,1) == "_"){
			tempString2 = name.Substring(6,1);
			tempString = name.Substring(8);
		}else{
			tempString2 = name.Substring(6,2);
			tempString = name.Substring(9);
		}

		int tempInt = int.Parse(tempString2);

		if(tempString == "Mars"){
			if(dataManger.manager.unlocksMars >= tempInt){
				uicameraGameobject.transform.FindChild("Loading").gameObject.SetActive(true);
				dataManger.manager.actualLevel = tempInt;
				dataManger.manager.actualWorld = tempString;
				Debug.Log("World : "+tempString+" Level : "+tempInt);
				StartCoroutine(LoadLevelAsync(tempInt,tempString));
				levelEnable = false;
			}
		}
		if(tempString == "Ice"){
			if(dataManger.manager.unlocksIce >= tempInt){
				uicameraGameobject.transform.FindChild("Loading").gameObject.SetActive(true);
				dataManger.manager.actualLevel = tempInt;
				dataManger.manager.actualWorld = tempString;
				Debug.Log("World : "+tempString+" Level : "+tempInt);
				StartCoroutine(LoadLevelAsync(tempInt,tempString));
				levelEnable = false;
			}
		}
	}

	IEnumerator LoadLevelAsync(int Level,string World){
		if (googleAnalytics != null) {
			googleAnalytics.LogScreen ("Level_" + Level + "_" + World);
		}
		AsyncOperation async = Application.LoadLevelAsync("Level_" + Level + "_" + World);
		yield return async;
		Debug.Log("Loading complete");
	}


	string MyEscapeURL (string url)
	{
		return WWW.EscapeURL(url).Replace("+","%20");
	}


}
