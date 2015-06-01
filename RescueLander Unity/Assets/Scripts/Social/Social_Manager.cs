using UnityEngine;
using System.Collections;

public class Social_Manager : MonoBehaviour{

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
			}
			break;
		case "Achievement":
			if (success){
				Debug.Log ("Logro desloqueado correcto: "+name);
				//Codigo logro satisfactorio
			}else{
				Debug.Log ("Logro desbloqueado incorrecto: "+name);
				//Codigo de logro erroneo
			}
			break;
		}
	}
}
