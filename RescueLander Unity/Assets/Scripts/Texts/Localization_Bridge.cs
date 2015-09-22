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
		/*GameObject.Find("Error").transform.FindChild("TextMesh").GetComponent<ResizeText>().ChangeText(Localization_Bridge.manager.GetTextValue("RescueLander.SplashError1"));
		GameObject.Find("Error").transform.FindChild("TextMesh 1").GetComponent<ResizeText>().ChangeText(Localization_Bridge.manager.GetTextValue("RescueLander.SplashError2"));
		GameObject.Find("Error").transform.FindChild("Exit_Button/Exit_Text").GetComponent<ResizeText>().ChangeText(Localization_Bridge.manager.GetTextValue("RescueLander.SplashErrorExit"));*/


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
				(Localization_Bridge.manager.GetTextValue("RescueLander.Garage"));
			UICamera.transform.FindChild("Prompt_Help/Shop_Bg_01/Tuto_Text").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.HelpMenuText"));
			UICamera.transform.FindChild("Loading").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.Loading"));
			UICamera.transform.FindChild("Prompt_Help/Shop_Bg_01/Tuto_Button/Tuto_Text").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.HelpMenuTutoButton"));
			GameObject.Find("World_Buttons").transform.FindChild("Button_World_Mars/World1_Text").GetComponent<ResizeText>().ChangeText
				(Localization_Bridge.manager.GetTextValue("RescueLander.Mars"));
			UICamera.transform.FindChild("Options_Menu/Shop_Bg_01/Header/OptionsHeader/OptionsHeader_Text").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.Options"));
			UICamera.transform.FindChild("Options_Menu/Shop_Bg_01/Invert Rotation").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.InvertRotation"));
			UICamera.transform.FindChild("Options_Menu/Shop_Bg_01/Texts/RateUs_Text").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.OptionsMenuRateUs"));
			UICamera.transform.FindChild("Options_Menu/Shop_Bg_01/Texts/Achievments_Text").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.OptionsMenuRateUs"));
			UICamera.transform.FindChild("Options_Menu/Shop_Bg_01/Texts/Leaderboard_Text").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.OptionsMenuLeaderboard"));
			UICamera.transform.FindChild("Options_Menu/Shop_Bg_01/Texts/GooglePlay_Text").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.OptionsMenuLogin"));
			UICamera.transform.FindChild("Options_Menu/Shop_Bg_01/Texts/Feedback_Text").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.OptionsMenuFeedback"));

		

		}

		if(Application.loadedLevelName == "Tuto_1"){
			UICamera.transform.FindChild("UI_Camera/Tutorial/Step1/ChatText_1").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.Tuto1Step1"));
			UICamera.transform.FindChild("UI_Camera/Tutorial/Step1/TapToContinue").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.TutoTapContinue"));
			UICamera.transform.FindChild("UI_Camera/Prompt_Menu/Shop_Bg_01/Prompt_TutoReward_1/Text_Reward2").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.PromptTutoReward1"));
		}

		if(Application.loadedLevelName == "Tuto_2"){
			UICamera.transform.FindChild("UI_Camera/Tutorial/Step1/Prompt_Menu_Turn/Shop_Bg_01/ChatText_2").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.Tuto2Step1Text1"));
			
			UICamera.transform.FindChild("UI_Camera/Tutorial/Step1/Prompt_Menu_Turn/Shop_Bg_01/ChatText_3").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.Tuto2Step1Text2"));
			
			UICamera.transform.FindChild("UI_Camera/Tutorial/Step1/Prompt_Menu_Turn/Shop_Bg_01/Ok_Button/Ok_Text").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.Ok"));
			
			UICamera.transform.FindChild("UI_Camera/Tutorial/Step2/ChatText_1").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.Tuto2Step1Text1"));
			
			UICamera.transform.FindChild("UI_Camera/Tutorial/ChatText_4").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.Tuto2Step2"));
			
			UICamera.transform.FindChild("UI_Camera/Prompt_Menu/Shop_Bg_01/Prompt_TutoReward_2/Text_Reward3").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.PromptTutoReward2"));

			UICamera.transform.FindChild("UI_Camera/Tutorial/Step1/TapToContinue").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.TutoTapContinue"));
		}
		if(!Application.loadedLevelName.Contains("Tuto") & !Application.loadedLevelName.Contains("Menu")){
			UICamera.transform.FindChild("LoseLayout/More_PowerUps/More_PowerUps_Text").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.LoseMorePU"));
			UICamera.transform.FindChild("LoseLayout/More_PowerUps/MorePowerUps_Button/MorePowerUps_Text").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.LoseGetSome"));
			UICamera.transform.FindChild("Anchor (LowerCenter)/Paused").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.IngamePaused"));
			UICamera.transform.FindChild("Anchor (LowerCenter)/BackToPlatform_Text").GetComponent<ResizeText>().ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.IngameReachPlatform"));
			GameObject.Find("Button_Garage").transform.FindChild("Anchor (LowerRight)/GarageButton_Graphic/TextGarage").GetComponent<ResizeText>().ChangeText
				(Localization_Bridge.manager.GetTextValue("RescueLander.Garage"));
		}

		#region Prompts Menu
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Ads_WinCoins/Text_Ads").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.PromptWinCoins"));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Ads_WinShield/Text_Ads").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.PromptWinShield"));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Ads_WinFuel/Text_Ads").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.PromptWinFuel"));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Ads_Fuel/Text_Ads").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.PromptAdsFuel"));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Controls/Text_Controls").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.PromptControlsText1"));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Controls/Text_Controls 1").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.PromptControlsText2"));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Garage/Garage_Text").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.PromptGarageText1"));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_Garage/Garage_Text 1").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.PromptGarageText2"));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_MoreMoney/Text_MoreCoins").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.PromptGarageText2"));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_MoreMoney/MoreCoins_Button/Text_Coins").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MoreCoins"));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_RateUs/Text_RateUs 1").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.PromptRateusText1"));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_RateUs/Text_RateUs 2").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.PromptRateusText2"));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_RateUs/Text_RateUs 3").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.PromptRateusText3"));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_RateUs/Button/Text_Sure").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.PromptRateusSure"));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_RateUs/Button_Later/Button_Graphic/Text_Later").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.PromptRateusLater"));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_RateUs/Button_Never/Button_Graphic/Text_Never").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.PromptRateusLater"));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_ErrorIAP/Text_Error1").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.PromptErrorIAP1"));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_ErrorIAP/Text_Error2").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.PromptErrorIAP2"));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_MarsFinished/Text_MarsFinished").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.PromptMarsFinishedText1"));
		UICamera.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_MarsFinished/Text_MarsFinished 1").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.PromptMarsFinishedText2"));
		#endregion
		
		#region Garage Menu
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Header/ShipsButton/ShipsButton_Text").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuShips"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Header/PowerUps_Button/PowerUps_Text").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuPUs"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Header/Coins_Button/Coins_Text").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuCoins"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Header/PaintButton/PaintButton_Text").GetComponent<ResizeText>().ChangeText 
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuPaint"));
		
		#region IAPS Menu
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Coins_Menu/IAP_Ico_01/Button_Buy/Button_Buy_Down/ButtonShip_Price").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuCoinsGetIt"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Coins_Menu/IAP_Ico_02/Button_Buy/Button_Buy_Down/ButtonShip_Price").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuCoinsGetIt"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Coins_Menu/IAP_Ico_03/Button_Buy/Button_Buy_Down/ButtonShip_Price").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuCoinsGetIt"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Coins_Menu/IAP_Ico_04/Button_Buy/Button_Buy_Down/ButtonShip_Price").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuCoinsGetIt"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Coins_Menu/IAP_Ico_05/Button_Buy/Button_Buy_Down/ButtonShip_Price").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuCoinsGetIt"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Coins_Menu/IAP_Ico_06/Button_Buy/Button_Buy_Down/ButtonShip_Price").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuCoinsGetIt"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Coins_Menu/NoAds_Panel/NoAds_Text").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuCoinsAds"));
		#endregion
		
		#region PU Menu
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/PowerUps_Menu/Button_Shield/ButtonPowerUp_Text").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuPUShieldTitle"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/PowerUps_Menu/Button_Shield/ButtonPowerUp_Have").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuPUYouHave"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/PowerUps_Menu/Illustration_Shield/Illustration_Text").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuPUShieldDesc"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/PowerUps_Menu/Button_Fuel/ButtonPowerUp_Text").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuPUFuelTitle"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/PowerUps_Menu/Button_Fuel/ButtonPowerUp_Have").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuPUYouHave"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/PowerUps_Menu/Illustration_Shield/Illustration_Text").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuPUShieldDesc"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/PowerUps_Menu/Button_Magnet/ButtonPowerUp_Text").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuPUMagnetTitle"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/PowerUps_Menu/Button_Magne/ButtonPowerUp_Have").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuPUYouHave"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/PowerUps_Menu/Illustration_Fuel/Illustration_Text").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuPUFuelDesc"));
		#endregion
		
		
		#region Ships Menu
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_Ship01/ButtonShip_Description").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuShipsShip01Desc"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_Box/ButtonShip_Description").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuShipsBoxDesc"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_369/ButtonShip_Description").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuShips369Desc"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_Big/ButtonShip_Description").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuShipsBigDesc"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_Taboo/ButtonShip_Description").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuShipsTabooDesc"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_Jupitar/ButtonShip_Description").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuShipsJupitarDesc"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_Mush/ButtonShip_Description").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuShipsMushDesc"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_UFLO/ButtonShip_Description").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuShipsUFLODesc"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_Evolve/ButtonShip_Description").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuShipsEvolveDesc"));
		UICamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/Ship_Buttons_Scroll/Ship_Buttons/Button_Bow/ButtonShip_Description").GetComponent<ResizeText>().ChangeText  
			(Localization_Bridge.manager.GetTextValue("RescueLander.MenuShipsBowDesc"));
		#endregion
		#endregion


	}

}
