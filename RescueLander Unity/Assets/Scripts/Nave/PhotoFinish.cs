using UnityEngine;
using System.Collections;

public class PhotoFinish : MonoBehaviour {


	public GameObject photo;
	public int resWidth = 400; public int resHeight = 400;
	

	private GameObject ship;
	private Vector3 plataform_pos;
	private ShipAstronautPickUp pickUp;
	private ShipAstronautDrop drop;
	private Camera cam;

	void Awake(){


		photo = GameObject.Find("UI_Camera").transform.FindChild("WinLayout/Resume/Pic_Frame/PicRenderer").gameObject;
		if (Application.loadedLevelName.Contains ("Tuto")){
			ship = GameObject.Find ("101(Clone)");
		}else{
			ship = GameObject.Find (dataManger.manager.actualShip+"(Clone)");
		}
		drop = ship.GetComponent<ShipAstronautDrop>();
		plataform_pos = GameObject.Find ("Landing Platform").transform.position;
		pickUp = ship.GetComponent<ShipAstronautPickUp>();
		cam = GetComponent<Camera>();
		cam.enabled = false;
	}

	void LateUpdate(){
		//Condiciones
		if (ship != null & photo != null){
			Vector3 ship_pos = ship.transform.position;

			Vector2 vect = new Vector2(ship_pos.x - plataform_pos.x, ship_pos.y - plataform_pos.y);
			float dist = Mathf.Sqrt( (vect.x*vect.x) + (vect.y*vect.y));

			//Debug.Log ("DIST: "+dist +"VECTOR "+ vect);
			if (dist < 116f & vect.y < 70f & vect.x < 107 & vect.x > -105 & pickUp.astronautPicked >= drop.totalAstronauts){
				//Movemos en el eje X la camara
				int maxScrollX = GameObject.Find("Camera 2DTK").GetComponent<Cameraposition>().maxScrollX;
				transform.position = new Vector3(ship_pos.x,transform.position.y,0);
				transform.position = new Vector3(Mathf.Clamp(transform.position.x,cam.pixelHeight/2,
				                                             maxScrollX - cam.pixelHeight/2),
				                                 transform.position.y,
				                                 transform.position.z);
				TakePhoto();
			}
		}
	}

	void TakePhoto () {
		//Camera cam = GetComponent<Camera>();
		cam.enabled = true;
		RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
		cam.targetTexture = rt;
		Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
		cam.Render();
		RenderTexture currentRT = RenderTexture.active;
		RenderTexture.active = rt;
		screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
		screenShot.Apply();
		Sprite sprt = Sprite.Create (screenShot,new Rect(0,0,resWidth,resHeight),new Vector2(0.5f,0.5f),1);
		photo.GetComponent<SpriteRenderer>().sprite = sprt;
		RenderTexture.active = currentRT;
		//Save screenshoot
		/*byte[] bytes = screenShot.EncodeToPNG();
		string filename = "C:/Users/Aarón/Documents/Juegos" + "/photo1.png";
		System.IO.File.WriteAllBytes(filename, bytes);*/
		Destroy (gameObject);

	}
}