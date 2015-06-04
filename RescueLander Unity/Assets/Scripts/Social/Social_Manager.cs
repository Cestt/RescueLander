using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;


public class Social_Manager : MonoBehaviour{


	void Awake(){
		//SocialPending();
		if(Application.loadedLevelName == "Menu"){
			// recommended for debugging:
			PlayGamesPlatform.DebugLogEnabled = true;
			// Activate the Google Play Games platform
			PlayGamesPlatform.Activate();
		}
	}

	private void SocialPending(){
		List<string> notPending = new List<string>();

		foreach(KeyValuePair <string,string> par in dataManger.manager.socialPending){
			if (par.Key.Contains("Leaderboard")){
				Debug.Log ("Leaderboard pendiente");
				Social.ReportScore(int.Parse(par.Value), "CgkIuv-YgIkeEAIQDQ", (bool success) => {
					if (success)
						notPending.Add(par.Key);
				});
			}else if (par.Key.Contains("Achievement")){
				Debug.Log ("Achievement pendiente :"+par.Value);

			}
		}

		foreach(string key in notPending){
			dataManger.manager.socialPending.Remove(key);
		}
	}

	public void Check(string type, bool success){
		switch(type){
			case "Login" :
				if (success){
					Debug.Log ("Login Correcto");
					//Codigo de login satisfactorio
				}else{
					Debug.Log ("Login Fallido");
					//Codigo de login erroneo
				}
				break;
			case "Leaderboard":
				if (success){
					Debug.Log ("Publicacion en el leaderboard correcto");
					//Codigo leaderboard satisfactorio
				}else{
					Debug.Log ("Publicacion en el leaderboard incorrecta");
					//Codigo leaderboard satisfactorio erroneo
				}
				break;
			case "Achievement":
				if (success){
					Debug.Log ("Logro desloqueado correcto");
					//Codigo logro satisfactorio
				}else{
					Debug.Log ("Logro desbloqueado incorrecto");
					//Codigo de logro erroneo
				}
				break;
		}
	}

	public void Check(string type,string name, bool success){
		switch(type){
		case "Login" :
			if (success){
				Debug.Log ("Login Correcto");
				//Codigo de login satisfactorio
			}else{
				Debug.Log ("Login Fallido");
				//Codigo de login erroneo
			}
			break;
		case "Leaderboard":
			if (success){
				Debug.Log ("Publicacion en el leaderboard correcto: "+name);
				//Codigo leaderboard satisfactorio
			}else{
				Debug.Log ("Publicacion en el leaderboard incorrecta: "+name);
				//Codigo leaderboard satisfactorio erroneo
				dataManger.manager.socialPending.Add(type +""+ dataManger.manager.socialPending.Count,name);
			}
			break;
		case "Achievement":
			if (success){
				Debug.Log ("Logro desloqueado correcto: "+name);
				//Codigo logro satisfactorio
			}else{
				Debug.Log ("Logro desbloqueado incorrecto: "+name);
				//Codigo de logro erroneo
				dataManger.manager.socialPending.Add(type +""+ dataManger.manager.socialPending.Count,name);
			}
			break;
		}
	}
}
