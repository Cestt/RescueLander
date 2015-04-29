using UnityEngine;
using System.Collections;

public class FacebookSocial : MonoBehaviour {

	private bool init = false;

	void Awake () {
	
		FB.Init(InitCheck,onHideUnity);

	}

	private void InitCheck(){
		 
		init = true;
		Debug.Log("Facebook initiated");
	}

	private void onHideUnity(bool isGameShown){

		if(isGameShown){
			Time.timeScale = 1;
		}else{
			Time.timeScale = 0;
		}
	}

	public void FBLogin(){
		if(FB.IsLoggedIn){

			Debug.Log("Already logged");
			InviteFB();

		}else{
			
			FB.Login("",AuthCallBack);
		}


	}

	private void AuthCallBack(FBResult result){

		if(FB.IsLoggedIn){
			Debug.Log("Loged in: ");
		}else{
			Debug.Log("Error on login");
		}
	}

	private void ShareFB(){

		FB.Feed(
			linkCaption: "To fresh desde evolve",
			picture: "http://www.evolvegames.es/images/manu_03.jpg",
			linkName: "Can you rescue my Lander ;)?",
			link: "http://apps.facebook.com/" + FB.AppId + "?/challenge_brag" + (FB.IsLoggedIn ? FB.UserId : "guest")



			);
	}

	private void InviteFB(){

		FB.AppRequest(
			message: "Check this out",
			title: "Come join Evolve Games"


			);
	}

}
