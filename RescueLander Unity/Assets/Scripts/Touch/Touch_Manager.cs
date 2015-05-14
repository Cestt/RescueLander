using UnityEngine;
using System.Collections;

public class Touch_Manager : MonoBehaviour {
	RuntimePlatform platform = Application.platform;
	[HideInInspector]
	public GameObject uicameraGameobject;
	public Camera uicamera;
	[HideInInspector]
	public bool paused;
	private GameObject ship;
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
	private SpriteColorFX.SpriteColorTintMask3 colorChange;
	public GameObject ShipGaraje;




	void Awake(){
		if(uiColumnExtended != null){
			ship = GameObject.Find(dataManger.manager.actualShip + "(Clone)");
			rigid = ship.GetComponent<Rigidbody2D>();
			animation = uiColumnExtended.GetComponent<Animation>();
		}
		colorChange = ShipGaraje.GetComponent<SpriteColorFX.SpriteColorTintMask3>();
		colorSet = GetComponent<Color_Set>();
		uicameraGameobject = GameObject.Find("UI_Camera");
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
							break;
						case "Sound_Button" :
							if(dataManger.manager.Sounds){
								dataManger.manager.Sounds = false;
								sounds.GetComponent<tk2dSprite>().SetSprite("VolumeIco_Off");
								Debug.Log("Sound off");
							}else{
								dataManger.manager.Sounds = true;
								sounds.GetComponent<tk2dSprite>().SetSprite("VolumeIco_On");
								Debug.Log("Sound on");
							}
							break;
						case "Music_Button" :
							if(dataManger.manager.Music){
								dataManger.manager.Music = false;
								music.GetComponent<tk2dSprite>().SetSprite("MusciIco_Off");
								Debug.Log("Music off");
							}else{
								dataManger.manager.Music = true;
								music.GetComponent<tk2dSprite>().SetSprite("MusciIco_On");
								Debug.Log("Music on");
							}
							break;
							
						case "NextLvl_Button" :
							Application.LoadLevel("Menu");
							dataManger.manager.Camposition = "Forward";
							break;
						case "Share_Button" :
							share.ShareScreenshot();
							break;
						case "Social Media Buttons" :
							faceBook.FBLogin();
							break;
						case "Button_Garaje" :
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
						default :
							
							break;
							
						}
						if(hit.collider.tag == "Level_Ico"){
							string tempString = hit.collider.name.Substring(6);
							int tempInt = int.Parse(tempString);
							
							if(dataManger.manager.unlocks >= tempInt){
								dataManger.manager.actualLevel = tempInt;
								Debug.Log("Loading Level: " + tempInt);
								Application.LoadLevel("Level_"+tempInt);
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
						break;
					case "Sound_Button" :
						if(dataManger.manager.Sounds){
							dataManger.manager.Sounds = false;
							sounds.GetComponent<tk2dSprite>().SetSprite("VolumeIco_Off");
							Debug.Log("Sound off");
						}else{
							dataManger.manager.Sounds = true;
							sounds.GetComponent<tk2dSprite>().SetSprite("VolumeIco_On");
							Debug.Log("Sound on");
						}
						break;
					case "Music_Button" :
						if(dataManger.manager.Music){
							dataManger.manager.Music = false;
							music.GetComponent<tk2dSprite>().SetSprite("MusciIco_Off");
							Debug.Log("Music off");
						}else{
							dataManger.manager.Music = true;
							music.GetComponent<tk2dSprite>().SetSprite("MusciIco_On");
							Debug.Log("Music on");
						}
						break;
					case "NextLvl_Button" :
						Application.LoadLevel("Menu");
						dataManger.manager.Camposition = "Forward";
						break;
					case "Share_Button" :
						share.ShareScreenshot();
						Debug.Log("Launch Share");
						break;
					case "Social Media Buttons" :
						faceBook.FBLogin();
						break;
					case "Button_Garaje" :
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
					default :
						
						break;
						
					}
					if(hit.collider.tag == "Level_Ico"){
						string tempString = hit.collider.name.Substring(6);
						int tempInt = int.Parse(tempString);
						
						if(dataManger.manager.unlocks >= tempInt){
							dataManger.manager.actualLevel = tempInt;
							Debug.Log("Loading Level: " + tempInt);
							Application.LoadLevel("Level_"+ tempInt);
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


	void Pause(GameObject temp){

		if(!paused){
			paused = true;
			rigid.isKinematic = true;
			animation["UIBase_RightCol_extended_UpDown"].speed = 1;
			animation.Play("UIBase_RightCol_extended_UpDown");
 			Debug.Log("Pause");
		}else{

			paused = false;
			rigid.isKinematic = false;
			animation["UIBase_RightCol_extended_UpDown"].speed = -1;
			animation.Play("UIBase_RightCol_extended_UpDown");
			animation["UIBase_RightCol_extended_UpDown"].time = animation["UIBase_RightCol_extended_UpDown"].length;
			Debug.Log("UnPause");
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


}