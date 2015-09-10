using UnityEngine;
using System.Collections;
using SmartLocalization;

public class Localization_Bridge : MonoBehaviour {

	 
	public static Localization_Bridge loc;
	public static LanguageManager manager;


	void Awake () {
		 manager = LanguageManager.Instance;
		if(loc == null){
			
			loc = this;
			DontDestroyOnLoad(gameObject);
			manager.OnChangeLanguage += OnChangeLanguaje;
			SmartCultureInfo systemLanguaje = manager.GetSupportedSystemLanguage();
			
			if(systemLanguaje!= null){
				Debug.Log("Languaje supported");
				manager.ChangeLanguage(systemLanguaje);
			}else{
				Debug.Log("Languaje not supported");
				manager.ChangeLanguage("en");

			}
			
		}else if(loc != this){
			
			Destroy(gameObject);
			
		}


	}
	

	void Update () {
	
	}

	void OnDestroy(){

			if(LanguageManager.HasInstance){
				LanguageManager.Instance.OnChangeLanguage -= OnChangeLanguaje;
			}
	}
	void OnChangeLanguaje(LanguageManager langManager){

	}
	void OnLevelWasLoaded(int level) {
		GameObject UICamera = GameObject.Find("UI_Camera");
		if(Application.loadedLevelName == "Menu"){
			GameObject.Find("Button_Garage").transform.FindChild("GarageButton_Graphic/TextGarage").GetComponent<ResizeText>().ChangeText
				(Localization_Bridge.manager.GetTextValue("RescueLander.garage"));
			GameObject.Find("World_Buttons").transform.FindChild("Button_World_Mars/World1_Text").GetComponent<ResizeText>().ChangeText
				(Localization_Bridge.manager.GetTextValue("RescueLander.mars"));
			UICamera.transform.FindChild("Options_Menu/Shop_Bg_01/Invert Rotation").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.invert"));
			UICamera.transform.FindChild("Options_Menu/Shop_Bg_01/Texts/RateUs_Text").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.rateus"));
			UICamera.transform.FindChild("Options_Menu/Shop_Bg_01/Texts/Achievments_Text").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.achievements"));
			UICamera.transform.FindChild("Options_Menu/Shop_Bg_01/Texts/Leaderboard_Text").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.leaderboard"));
			UICamera.transform.FindChild("Options_Menu/Shop_Bg_01/Texts/GooglePlay_Text").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.login"));
			UICamera.transform.FindChild("Options_Menu/Shop_Bg_01/Texts/Feedback_Text").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.welove"));

		}
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Header/ShipsButton/ShipsButton_Text").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.ships"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Header/PowerUps_Button/PowerUps_Text").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.powerups"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Header/Coins_Button/Coins_Text").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.coins"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Header/PaintButton/PaintButton_Text").GetComponent<ResizeText>().ChangeText 
			(Localization_Bridge.manager.GetTextValue("RescueLander.paint"));

		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/ShipsButton_Text").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.ships"));
	}

}
