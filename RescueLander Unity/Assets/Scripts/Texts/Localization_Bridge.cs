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
		if(Application.loadedLevelName == "Menu"){
			GameObject.Find("Button_Garage").transform.FindChild("GarageButton_Graphic/TextGarage").GetComponent<tk2dTextMesh>().text = 
				Localization_Bridge.manager.GetTextValue("RescueLander.garage");
		}
	}
}
