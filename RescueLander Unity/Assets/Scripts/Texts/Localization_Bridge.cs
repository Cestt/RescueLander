using UnityEngine;
using System.Collections;
using SmartLocalization;

public class Localization_Bridge : MonoBehaviour {

	 
	public static Localization_Bridge loc;
	[HideInInspector]
	public string Score;

	void Awake () {
		LanguageManager manager = LanguageManager.Instance;
		if(loc == null){
			
			loc = this;
			DontDestroyOnLoad(gameObject);
			manager.OnChangeLanguage += OnChangeLanguaje;
			SmartCultureInfo systemLanguaje = manager.GetSupportedSystemLanguage();
			
			if(manager.IsLanguageSupported(systemLanguaje)){
				manager.ChangeLanguage(systemLanguaje);
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
		Score = langManager.GetTextValue("RescueLander.Score");
	}
}
