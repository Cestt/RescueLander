using UnityEngine;
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
		
		string destination = Path.Combine(Application.persistentDataPath,"Screenshot.png");

		/*Texture2D screenTexture = new Texture2D(Screen.width, Screen.height,TextureFormat.RGB24,true);
		screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height),0,0);
		screenTexture.Apply();
	
		byte[] dataToSave = screenTexture.EncodeToPNG();
		File.WriteAllBytes(destination, dataToSave);
		*/

		

		
		

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
