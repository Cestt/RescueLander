using UnityEngine;
using System.Collections;


public class UIHide : MonoBehaviour {

	private Animation animation;
	private GameObject ColumnLeft;
	private GameObject ColumnRight;
	private GameObject ship;
	private tk2dCamera camera;
	private bool first = true;
	private Vector2 tempVect;
	private Vector2 originVect;
	private tk2dCamera cam;
	private GameObject UILeft;
	private GameObject UIRight;


	void Awake(){
		cam = GetComponent<tk2dCamera>();
		UILeft = gameObject.transform.FindChild("Left").gameObject;
		UIRight = gameObject.transform.FindChild("Right").gameObject;
		ship = GameObject.Find(dataManger.manager.actualShip + "(Clone)");
		GameObject uicamera = GameObject.Find("UI_Camera");
		ColumnLeft = uicamera.transform.FindChild("Anchor (UpperLeft)/UIBase_Left").gameObject;
		ColumnRight = uicamera.transform.FindChild("Anchor (UpperRight)/UIBase_Right").gameObject;
		Physics2D.IgnoreLayerCollision(5, 8, true);
		camera = this.GetComponentInParent<tk2dCamera>();
		originVect = this.transform.localPosition;
		
	}
	void Update(){
		if(cam.ZoomFactor != 1){
			Vector3 TempVect = UILeft.transform.localScale; 
			TempVect.x /= cam.ZoomFactor;
			TempVect.y /= cam.ZoomFactor;
			UILeft.transform.localScale = TempVect;
			TempVect = UIRight.transform.localScale;
			TempVect.x /= cam.ZoomFactor;
			TempVect.y /= cam.ZoomFactor;
			UIRight.transform.localScale = TempVect;

			TempVect = UILeft.transform.localPosition; 
			TempVect.x /= cam.ZoomFactor;
			TempVect.y /= cam.ZoomFactor;
			UILeft.transform.localPosition = TempVect;
			TempVect = UIRight.transform.localPosition; 
			TempVect.x /= cam.ZoomFactor;
			TempVect.y /= cam.ZoomFactor;
			UIRight.transform.localPosition = TempVect;
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {

		if(coll.name == ship.name){
			Debug.Log("Enter UI");
			if(coll.transform.position.x < Camera.main.transform.position.x){
				animation = ColumnLeft.GetComponent<Animation>();
				animation["UIBaseLeft_Hide"].speed = 1;
				animation.Play("UIBaseLeft_Hide");
			}
			if(coll.transform.position.x > Camera.main.transform.position.x){
				animation = ColumnRight.GetComponent<Animation>();
				animation["UIBaseRight_Hide"].speed = 1;
				animation.Play("UIBaseRight_Hide");
			}

		}
	}
	void OnTriggerExit2D(Collider2D coll) {


		if(coll.name == ship.name){
			Debug.Log("Exit UI");
			if(coll.transform.position.x < Camera.main.transform.position.x){
				animation = ColumnLeft.GetComponent<Animation>();
				animation["UIBaseLeft_Hide"].speed = -1;
				animation.Play("UIBaseLeft_Hide");
				animation["UIBaseLeft_Hide"].time = animation["UIBaseLeft_Hide"].length;
			}
			if(coll.transform.position.x > Camera.main.transform.position.x){
				animation = ColumnRight.GetComponent<Animation>();
				animation["UIBaseRight_Hide"].speed = -1;
				animation.Play("UIBaseRight_Hide");
				animation["UIBaseRight_Hide"].time = animation["UIBaseRight_Hide"].length;
			}
			
		}
	}
}
