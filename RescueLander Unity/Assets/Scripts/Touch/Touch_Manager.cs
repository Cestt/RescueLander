using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

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
	private tk2dUIToggleButton[] buttonsGarage = new tk2dUIToggleButton[3];
	private List<tk2dSpriteAnimator> animators = new List<tk2dSpriteAnimator>();

	void Awake(){
		uicameraGameobject = GameObject.Find("UI_Camera");
		if(Application.loadedLevelName != "Menu"){
			uiColumnExtended = uicameraGameobject.transform.FindChild("Anchor (UpperRight)/UIBase_Right/UIBase_RightCol/UIBase_RightCol_Extended").gameObject;
			if(Application.loadedLevelName.Contains("Tuto")){
				ship = GameObject.Find("101(Clone)");
			}else{
				ship = GameObject.Find(dataManger.manager.actualShip + "(Clone)");
				pauseText = uicameraGameobject.transform.FindChild("Anchor (LowerCenter)/Paused").gameObject;

			}

			rigid = ship.GetComponent<Rigidbody2D>();
			animation = uiColumnExtended.GetComponent<Animation>();
			sounds = uiColumnExtended.transform.FindChild("Sound_Button").gameObject;
			music = uiColumnExtended.transform.FindChild("Music_Button").gameObject;
			Win = uicameraGameobject.transform.FindChild ("WinLayout").gameObject;
			Lose = uicameraGameobject.transform.FindChild ("LoseLayout").gameObject;

			animators.Add(GameObject.Find("Landing Platform").transform.FindChild("LandingPlatform_Lights").GetComponent<tk2dSpriteAnimator>());
			animators.Add(GameObject.Find("Astronaut_01").GetComponent<tk2dSpriteAnimator>());
			animators.Add(GameObject.Find("Astronaut_02").GetComponent<tk2dSpriteAnimator>());
			animators.Add(GameObject.Find("Astronaut_03").GetComponent<tk2dSpriteAnimator>());
			foreach (tk2dSpriteAnimator animat in GameObject.Find("Coins").GetComponentsInChildren<tk2dSpriteAnimator>())
				animators.Add(animat);
		}else{
		
			options = uicameraGameobject.transform.FindChild("Options_Menu").gameObject;
			check = options.transform.FindChild("Shop_Bg_01/Invert Rotation/Check").gameObject;
			if(dataManger.manager.inverted){
				check.SetActive(true);
			}else{
				check.SetActive(false);
			}

		}
		coin_manager =GameObject.Find("ScoreCoin_Manager").GetComponent<Coin_Manager>();
		ads = GetComponent<Ads>();
		garaje = uicameraGameobject.transform.FindChild ("Garage_Menu").gameObject;
		PUilustration = garaje.transform.FindChild("Canvas/Shop_Bg_01/PowerUps_Menu/Illustration_Shield").gameObject;
		garage_manager = GetComponent<Garaje_Manager>();
		powerManager = GetComponent<PowerUp_Manager>();
		socialManager = GetComponent<Social_Manager>();
		colorSet = GetComponent<Color_Set>();
		ShipGaraje = uicameraGameobject.transform.FindChild ("Garage_Menu/Canvas/Shop_Bg_01/Paint_Menu/TV/Ship01_Garage").gameObject;
		buttonsPaint[0] = uicameraGameobject.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Paint_Menu/A_Button").GetComponent<tk2dUIToggleButton>();
		buttonsPaint[1] = uicameraGameobject.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Paint_Menu/B_Button").GetComponent<tk2dUIToggleButton>();
		buttonsGarage[0] = uicameraGameobject.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Header/ShipsButton").GetComponent<tk2dUIToggleButton>();
		buttonsGarage[1] = uicameraGameobject.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Header/PowerUps_Button").GetComponent<tk2dUIToggleButton>();
		buttonsGarage[2] = uicameraGameobject.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Header/Coins_Button").GetComponent<tk2dUIToggleButton>();
		colorChange = ShipGaraje.GetComponent<SpriteColorFX.SpriteColorMasks3>();
		uicamera = uicameraGameobject.GetComponent<Camera>();
		share = GetComponent<Share>();
		faceBook = GetComponent<FacebookSocial>();


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
							if(dataManger.manager.tutorial >= 0){
								if(IsInvoking("MoveCamera")){
									CancelInvoke("MoveCamera");
									InvokeRepeating("MoveCamera",0.01f,Time.fixedDeltaTime);
								}else{
									InvokeRepeating("MoveCamera",0.01f,Time.fixedDeltaTime);
								}
							}else{
								Application.LoadLevel("Tuto_"+dataManger.manager.tutorial);
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
							if(Application.loadedLevelName == "Menu"){
								options.SetActive(false);
							}

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
								if(dataManger.manager.shieldPowerUps >= 1){
									powerManager.PowerUp("Shield");
									//ACHIEVEMENT
									Social.ReportProgress("CgkIuv-YgIkeEAIQBg", 100.0f, (bool success) => {
										socialManager.Check("Achievement","CgkIuv-YgIkeEAIQBg",success);
									});
								}else{
									ads.Launch("Shield");
								}
							}
							break;
						case "PowerUp_Magnet" :
							if (!paused){
								if(dataManger.manager.magnetPowerUps >= 1){
									powerManager.PowerUp("Magnet");
									//ACHIEVEMENT
									Social.ReportProgress("CgkIuv-YgIkeEAIQBg", 100.0f, (bool success) => {
										socialManager.Check("Achievement","CgkIuv-YgIkeEAIQBg",success);
									});
								}else{
									ads.Launch("Magnet");
								}
							}
							break;
						case "PowerUp_Fuel" :
							if (!paused){
								if(dataManger.manager.fuelPowerUps >= 1 ){
									powerManager.PowerUp("Fuel");
									//ACHIEVEMENT
									Social.ReportProgress("CgkIuv-YgIkeEAIQBg", 100.0f, (bool success) => {
										socialManager.Check("Achievement","CgkIuv-YgIkeEAIQBg",success);
									});
								}else{
									ads.Launch("Fuel");
								}
							}
							break;
						case "GarageHeader_Button" :
							garage_manager.LayoutChanger("Paint");
							buttonsGarage[0].IsOn = false;
							buttonsGarage[1].IsOn = false;
							buttonsGarage[2].IsOn = false;
							break;
						case "ShipsButton" :
							garage_manager.LayoutChanger("Ships");
							if (buttonsGarage[0].IsOn)
								buttonsGarage[0].IsOn = false;
							buttonsGarage[1].IsOn = false;
							buttonsGarage[2].IsOn = false;
							break;
						case "PowerUps_Button" :
							garage_manager.LayoutChanger("PowerUps");
							buttonsGarage[0].IsOn = false;
							if (buttonsGarage[1].IsOn)
								buttonsGarage[1].IsOn = false;
							buttonsGarage[2].IsOn = false;
							break;
						case "Coins_Button" :
							garage_manager.LayoutChanger("Coins");
							buttonsGarage[0].IsOn = false;
							buttonsGarage[1].IsOn = false;
							if (buttonsGarage[2].IsOn)
								buttonsGarage[2].IsOn = false;
							break;
						case "Button_Ship01" :
							if(dataManger.manager.shipUnlocks.Contains("Ship01")){
								dataManger.manager.actualShip = "Ship01";
								dataManger.manager.Save(false);
								colorSet.SpriteSet(true,"Ship01");
							}else{
								colorSet.SpriteSet(false,"Ship01");
							}
							
							break;
						case "Button_369" :
							if(dataManger.manager.shipUnlocks.Contains("369")){
								dataManger.manager.actualShip = "369";
								dataManger.manager.Save(false);
								colorSet.SpriteSet(true,"369");
							}else{
								colorSet.SpriteSet(false,"369");
							}
							break;
						case "Button_Taboo" :
							if(dataManger.manager.shipUnlocks.Contains("Taboo")){
								dataManger.manager.actualShip = "Taboo";
								dataManger.manager.Save(false);
								colorSet.SpriteSet(true,"Taboo");
							}else{
								colorSet.SpriteSet(false,"Taboo");
							}
							break;
						case "Button_UFLO" :
							if(dataManger.manager.shipUnlocks.Contains("UFLO")){
								dataManger.manager.actualShip = "UFLO";
								dataManger.manager.Save(false);
								colorSet.SpriteSet(true,"UFLO");
							}else{
								colorSet.SpriteSet(false,"UFLO");
							}
							break;
						case "Button_Box" :
							if(dataManger.manager.shipUnlocks.Contains("Box")){
								dataManger.manager.actualShip = "Box";
								dataManger.manager.Save(false);
								colorSet.SpriteSet(true,"Box");
							}else{
								colorSet.SpriteSet(false,"Box");
							}
							break;
						case "Button_Mush" :
							if(dataManger.manager.shipUnlocks.Contains("Mush")){
								dataManger.manager.actualShip = "Mush";
								dataManger.manager.Save(false);
								colorSet.SpriteSet(true,"Mush");
							}else{
								colorSet.SpriteSet(false,"Mush");
							}
							break;
						case "Button_Bow" :
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
							break;
							
						case "Button_Magnet" :
							if(PUilustration != null){
								PUilustration.SetActive(false);
							}
							PUilustration = garaje.transform.FindChild("Canvas/Shop_Bg_01/PowerUps_Menu/Illustration_Magnet").gameObject;
							PUilustration.SetActive(true);
							break;
							
						case "Button_Fuel" :
							if(PUilustration != null){
								PUilustration.SetActive(false);
							}
							PUilustration = garaje.transform.FindChild("Canvas/Shop_Bg_01/PowerUps_Menu/Illustration_Fuel").gameObject;
							PUilustration.SetActive(true);
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
								Debug.Log ("Rate Android");
								Application.OpenURL("market://details?id=YOUR_ID");
							}
							else if (platform == RuntimePlatform.IPhonePlayer){
								Debug.Log ("Rade Iphone");
								Application.OpenURL("itms-apps://itunes.apple.com/app/idYOUR_ID");
							}
							break;
						default :
							
							break;
							
						}
						if(hit.collider.tag == "Level_Ico" & levelEnable){
							string tempString = hit.collider.name.Substring(6);
							int tempInt = int.Parse(tempString);
							Debug.Log("Clicked Level: "+tempInt);
							if(dataManger.manager.unlocks >= tempInt){
								dataManger.manager.actualLevel = tempInt;
								StartCoroutine(LoadLevelAsync(tempInt));
								levelEnable = false;
							}
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
		
					Debug.Log("Hit");

					switch(hit.collider.name ){
					case "Play" :
						forward = true;
						if(dataManger.manager.tutorial >= 0){
							if(IsInvoking("MoveCamera")){
								CancelInvoke("MoveCamera");
								InvokeRepeating("MoveCamera",0.01f,Time.fixedDeltaTime);
							}else{
								InvokeRepeating("MoveCamera",0.01f,Time.fixedDeltaTime);
							}
						}else{
							Application.LoadLevel("Tuto_"+dataManger.manager.tutorial);
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
						if(Application.loadedLevelName == "Menu"){
							options.SetActive(false);
						}

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
							}else{
								ads.Launch("Shield");
							}
						}
						break;
					case "PowerUp_Magnet" :
						if (!paused){
							if(dataManger.manager.magnetPowerUps >= 1){
								powerManager.PowerUp("Magnet");
							}else{
								ads.Launch("Magnet");
							}
						}
						break;
					case "PowerUp_Fuel" :
						if (!paused){
							if(dataManger.manager.fuelPowerUps >= 1){
								powerManager.PowerUp("Fuel");
							}else{
								ads.Launch("Fuel");
							}
						}
						break;
					case "GarageHeader_Button" :
						garage_manager.LayoutChanger("Paint");
						buttonsGarage[0].IsOn = false;
						buttonsGarage[1].IsOn = false;
						buttonsGarage[2].IsOn = false;
						break;
					case "ShipsButton" :
						garage_manager.LayoutChanger("Ships");
						if (buttonsGarage[0].IsOn)
							buttonsGarage[0].IsOn = false;
						buttonsGarage[1].IsOn = false;
						buttonsGarage[2].IsOn = false;
						break;
					case "PowerUps_Button" :
						garage_manager.LayoutChanger("PowerUps");
						buttonsGarage[0].IsOn = false;
						if (buttonsGarage[1].IsOn)
							buttonsGarage[1].IsOn = false;
						buttonsGarage[2].IsOn = false;
						break;
					case "Coins_Button" :
						garage_manager.LayoutChanger("Coins");
						buttonsGarage[0].IsOn = false;
						buttonsGarage[1].IsOn = false;
						if (buttonsGarage[2].IsOn)
							buttonsGarage[2].IsOn = false;
						break;
					case "Button_Ship01" :
						if(dataManger.manager.shipUnlocks.Contains("Ship01")){
							dataManger.manager.actualShip = "Ship01";
							dataManger.manager.Save(false);
							colorSet.SpriteSet(true,"Ship01");
						}else{
							colorSet.SpriteSet(false,"Ship01");
						}
						
						break;
					case "Button_369" :
						if(dataManger.manager.shipUnlocks.Contains("369")){
							dataManger.manager.actualShip = "369";
							dataManger.manager.Save(false);
							colorSet.SpriteSet(true,"369");
						}else{
							colorSet.SpriteSet(false,"369");
						}
						break;
					case "Button_Taboo" :
						if(dataManger.manager.shipUnlocks.Contains("Taboo")){
							dataManger.manager.actualShip = "Taboo";
							dataManger.manager.Save(false);
							colorSet.SpriteSet(true,"Taboo");
						}else{
							colorSet.SpriteSet(false,"Taboo");
						}
						break;
					case "Button_UFLO" :
						if(dataManger.manager.shipUnlocks.Contains("UFLO")){
							dataManger.manager.actualShip = "UFLO";
							dataManger.manager.Save(false);
							colorSet.SpriteSet(true,"UFLO");
						}else{
							colorSet.SpriteSet(false,"UFLO");
						}
						break;
					case "Button_Box" :
						if(dataManger.manager.shipUnlocks.Contains("Box")){
							dataManger.manager.actualShip = "Box";
							dataManger.manager.Save(false);
							colorSet.SpriteSet(true,"Box");
						}else{
							colorSet.SpriteSet(false,"Box");
						}
						break;
					case "Button_Mush" :
						if(dataManger.manager.shipUnlocks.Contains("Mush")){
							dataManger.manager.actualShip = "Mush";
							dataManger.manager.Save(false);
							colorSet.SpriteSet(true,"Mush");
						}else{
							colorSet.SpriteSet(false,"Mush");
						}
						break;
					case "Button_Bow" :
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
							Debug.Log(hit.transform.parent.name.Substring(7));
							if(coin_manager.Compra(value.Cost,value._Type,hit.transform.parent.name.Substring(7))){
								if(value._Type == "Ship" || value._Type == "World"){
									Destroy(hit.collider.gameObject);
								}
								Debug.Log("Comprado " + hit.transform.parent.name.Substring(7));
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
						PUilustration = garaje.transform.Find("Canvas/Shop_Bg_01/PowerUps_Menu/Illustration_Shield").gameObject;
						PUilustration.SetActive(true);
						break;
						
					case "Button_Magnet" :
						if(PUilustration != null){
							PUilustration.SetActive(false);
						}
						PUilustration = garaje.transform.FindChild("Canvas/Shop_Bg_01/PowerUps_Menu/Illustration_Magnet").gameObject;
						PUilustration.SetActive(true);
						break;
						
					case "Button_Fuel" :
						if(PUilustration != null){
							PUilustration.SetActive(false);
						}
						PUilustration = garaje.transform.FindChild("Canvas/Shop_Bg_01/PowerUps_Menu/Illustration_Fuel").gameObject;
						PUilustration.SetActive(true);
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
					default :
						
						break;
						
					}
					if(hit.collider.tag == "Level_Ico" & levelEnable){
						string tempString = hit.collider.name.Substring(6);
						int tempInt = int.Parse(tempString);
						Debug.Log("Clicked Level: "+tempInt);
						if(dataManger.manager.unlocks >= tempInt){
							dataManger.manager.actualLevel = tempInt;
							StartCoroutine(LoadLevelAsync(tempInt));
							levelEnable = false;
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
			if(!Application.loadedLevelName.Contains("Tuto"))
			pauseText.SetActive(true);
			for (int i=0; i< animators.Count; i++){
				if (animators[i] != null)
					animators[i].Pause();
			}
 			Debug.Log("Pause");
			return true;
		}else{

			paused = false;
			rigid.isKinematic = false;
			rigid.velocity = ship.GetComponent<Damage>().saveSpeed;
			animation["UIBase_RightCol_extended_UpDown"].speed = -1;
			animation.Play("UIBase_RightCol_extended_UpDown");
			animation["UIBase_RightCol_extended_UpDown"].time = animation["UIBase_RightCol_extended_UpDown"].length;
			Debug.Log("UnPause");
			pauseText.SetActive(false);
			for (int i=0; i< animators.Count; i++){
				if (animators[i] != null)
					animators[i].Resume();
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
			garaje.SetActive(false);
			levelEnable = true;
			Result = null;
		}


	}

	IEnumerator LoadLevelAsync(int Level){
		AsyncOperation async = Application.LoadLevelAsync("Level_" + Level);
		yield return async;
		Debug.Log("Loading complete");
	}

	string MyEscapeURL (string url)
	{
		return WWW.EscapeURL(url).Replace("+","%20");
	}


}
