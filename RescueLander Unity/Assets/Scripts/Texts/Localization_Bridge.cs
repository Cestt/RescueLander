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
				Debug.Log("Languaje: "+ systemLanguaje.englishName + " supported");
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

		if(Application.loadedLevelName == "Tuto_1"){
			UICamera.transform.FindChild("UI_Camera/Tutorial/Step1/ChatText_1").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue(""));
			
			UICamera.transform.FindChild("UI_Camera/Prompt_Menu/Shop_Bg_01/Prompt_TutoReward_1/Text_Reward2").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue(""));
		}

		if(Application.loadedLevelName == "Tuto_2"){
			UICamera.transform.FindChild("UI_Camera/Tutorial/Step1/Prompt_Menu_Turn/Shop_Bg_01/ChatText_2").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue(""));
			
			UICamera.transform.FindChild("UI_Camera/Tutorial/Step1/Prompt_Menu_Turn/Shop_Bg_01/ChatText_3").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue(""));
			
			UICamera.transform.FindChild("UI_Camera/Tutorial/Step1/Prompt_Menu_Turn/Shop_Bg_01/Ok_Button/Ok_Text").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue(""));
			
			UICamera.transform.FindChild("UI_Camera/Tutorial/Step2/ChatText_1").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue(""));
			
			UICamera.transform.FindChild("UI_Camera/Tutorial/ChatText_4").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue(""));
			
			UICamera.transform.FindChild("UI_Camera/Prompt_Menu/Shop_Bg_01/Prompt_TutoReward_2/Text_Reward3").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue(""));
		}

		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Ads_WinCoins/Text_Ads").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue(""));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Ads_WinShield/Text_Ads").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue(""));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Ads_WinFuel/Text_Ads").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue(""));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Ads_Fuel/Text_Ads").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue(""));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Controls/Text_Controls").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue(""));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Controls/Text_Controls 1").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue(""));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Garage/Garage_Text").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue(""));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Garage/Garage_Text 1").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue(""));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_MoreMoney/Text_MoreCoins").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue(""));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_RateUs/Text_RateUs 1").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue(""));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_RateUs/Text_RateUs 2").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue(""));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_RateUs/Text_RateUs 3").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue(""));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_ErrorIAP/Text_Error1").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue(""));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_ErrorIAP/Text_Error2").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue(""));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_MarsFinished/Text_MarsFinished").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue(""));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_MarsFinished/Text_MarsFinished 1").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue(""));

		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Header/ShipsButton/ShipsButton_Text").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.ships"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Header/PowerUps_Button/PowerUps_Text").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.powerups"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Header/Coins_Button/Coins_Text").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.coins"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Header/PaintButton/PaintButton_Text").GetComponent<ResizeText>().ChangeText 
			(Localization_Bridge.manager.GetTextValue("RescueLander.paint"));
	


		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_Ship01/ButtonShip_Description").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.descoriginal"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_Box/ButtonShip_Description").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.descmighty"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_369/ButtonShip_Description").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.desczoom"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_Big/ButtonShip_Description").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.desczoom"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_Taboo/ButtonShip_Description").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.desctaboo"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_Jupitar/ButtonShip_Description").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.desctaboo"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_Mush/ButtonShip_Description").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.descsomush"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_UFLO/ButtonShip_Description").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.descuflo"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_Evolve/ButtonShip_Description").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.descuflo"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_Bow/ButtonShip_Description").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.descbow"));


	}

}
