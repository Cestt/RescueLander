﻿using UnityEngine;
using System.Collections;
using System.IO;

public class Share : MonoBehaviour {

	private bool isProcessing = false;
	private ScreenShoter screenshoter;
	public GameObject screenshotSprite;

	void Awake(){

		screenshoter = GetComponent<ScreenShoter>();
	}

	public void ShareScreenshot(){
		Debug.Log("Share");
		isProcessing = true;

		screenshoter.LaunchScreenshot(Screen.width, Screen.height, false);

		string destination = Path.Combine(Application.persistentDataPath,"Screenshot.png");

		byte[] bytes = File.ReadAllBytes(Application.persistentDataPath + "/Screenshot.png");
		Texture2D texture = new Texture2D(125, 125, TextureFormat.DXT1,false);
		texture.filterMode = FilterMode.Trilinear;
		texture.LoadImage(bytes);


		Sprite sprite = Sprite.Create(texture, new Rect(0,0,125, 125), new Vector2(0.0f,0.0f), 1.0f);
		screenshotSprite.GetComponent<SpriteRenderer> ().sprite = sprite;
		screenshotSprite.transform.localScale = new Vector3(2,2,1);
		
		if(!Application.isEditor)
		{
			// block to open the file and share it ------------START
			AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
			intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
			AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
			AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse","file://" + destination);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
			intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
			
			// option one:
			currentActivity.Call("startActivity", intentObject);
			

			
		}
		isProcessing = false;

	}
}