using UnityEngine;
using System.Collections;

public class WinLose : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void End(string result){

		if(result == "Win"){


		}

		if(result == "Lose"){


		}

		TakeScreenShot(1);

	}
	void TakeScreenShot(int superSize){


		Application.CaptureScreenshot(Application.persistentDataPath + "Screenshots/screenShot",superSize);
	
	}
}
