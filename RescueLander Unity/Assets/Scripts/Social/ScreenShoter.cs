using UnityEngine;
using System.Collections;
using System.IO;

public class ScreenShoter : MonoBehaviour {

	public GameObject UICamera;
	public GameObject Ship;


	public void LaunchScreenshot(int width, int heigth, bool hide){

		StartCoroutine(Screenshot(width, heigth, hide));

	}
	private IEnumerator Screenshot(int width, int heigth, bool hide){


		if(hide){
			UICamera.SetActive(false);
		}

		yield return new WaitForEndOfFrame();
		//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
		// create the texture
		Texture2D screenTexture = new Texture2D(width, heigth,TextureFormat.RGB24,true);

		
		// put buffer into texture
		if(hide){
			screenTexture.ReadPixels(new Rect(Camera.main.WorldToScreenPoint(Ship.transform.position).x - width/2,
			                                  Camera.main.WorldToScreenPoint(Ship.transform.position).y - heigth/2, width, heigth)
			                         ,0,0);


		}else{
			screenTexture.ReadPixels(new Rect(0, 0 , width, heigth)
			                         ,0,0);
		}

		
		// apply
		screenTexture.Apply();
		//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
		
		byte[] dataToSave = screenTexture.EncodeToPNG();
		
		string destination = Path.Combine(Application.persistentDataPath,"Screenshot.png");
		
		File.WriteAllBytes(destination, dataToSave);

		if(!UICamera.activeInHierarchy){
			UICamera.SetActive(true);
		}
	}
}
