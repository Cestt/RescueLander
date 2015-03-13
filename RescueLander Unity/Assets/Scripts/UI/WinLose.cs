using UnityEngine;
using System.Collections;

public class WinLose : MonoBehaviour {

	public GameObject WinSprite;
	Halo_Anim haloanim;
	// Use this for initialization
	void Awake () {
	
		haloanim = WinSprite.GetComponent<Halo_Anim>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void End(string result){

		if(result == "Win"){
			StartCoroutine("Win");
		}

		if(result == "Lose"){


		}

		TakeScreenShot(1);

	}
	void TakeScreenShot(int superSize){


		Application.CaptureScreenshot(Application.persistentDataPath + "Screenshots/screenShot",superSize);
	
	}

	IEnumerator Win(){
		
		haloanim.win =true;
		WinSprite.transform.position = new Vector2(Camera.main.pixelWidth/2,Camera.main.pixelHeight/2);
		yield return new WaitForSeconds(0f);
	}
}
