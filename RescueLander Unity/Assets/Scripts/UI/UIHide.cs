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
	private Vector2 originVect2;
	private Vector2 originVect3;
	private Vector2 originVect4;
	private tk2dCamera cam;
	private GameObject UILeft;
	private GameObject UIRight;


	void Awake(){
		ship = GameObject.Find(dataManger.manager.actualShip + "(Clone)");
		cam = GetComponentInParent<tk2dCamera>();
		UILeft = Camera.main.transform.FindChild("Left").gameObject;
		UIRight = Camera.main.transform.FindChild("Right").gameObject;
		Vector3 TempVect;
		TempVect = new Vector3(UILeft.transform.position.x,UILeft.transform.position.y,ship.transform.position.z);
		UILeft.transform.position = TempVect;
		TempVect = new Vector3(UIRight.transform.position.x,UIRight.transform.position.y,ship.transform.position.z);
		UIRight.transform.position = TempVect;
		originVect = new Vector2(1,1);

		originVect3 = UILeft.transform.localPosition; 
		originVect2 = UIRight.transform.localScale;
		originVect4 = UIRight.transform.localPosition; 

		GameObject uicamera = GameObject.Find("UI_Camera");
		ColumnLeft = uicamera.transform.FindChild("Anchor (UpperLeft)/UIBase_Left").gameObject;
		ColumnRight = uicamera.transform.FindChild("Anchor (UpperRight)/UIBase_Right").gameObject;
		Physics2D.IgnoreLayerCollision(5, 8, true);
		camera = this.GetComponentInParent<tk2dCamera>();
		
	}
	void Update(){
		if(cam.ZoomFactor > 1){ 
			tempVect = originVect;
			tempVect.x /= cam.ZoomFactor;
			tempVect.y /= cam.ZoomFactor;
			UILeft.transform.localScale = tempVect;
			 
			tempVect = originVect2;
			tempVect.x /= cam.ZoomFactor;
			tempVect.y /= cam.ZoomFactor;
			UIRight.transform.localScale = tempVect;


			tempVect = originVect3;
			tempVect.x /= cam.ZoomFactor;
			tempVect.y /= cam.ZoomFactor;
			UILeft.transform.localPosition = tempVect;

			tempVect = originVect4;
			tempVect.x /= cam.ZoomFactor;
			tempVect.y /= cam.ZoomFactor;
			UIRight.transform.localPosition = tempVect;
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {

		if(coll.name == ship.name){
			Debug.Log("Enter UI");
			if(this.name == "Left"){
				animation = ColumnLeft.GetComponent<Animation>();
				animation["UIBaseLeft_Hide"].speed = 1;
				animation.Play("UIBaseLeft_Hide");
			}
			if(this.name == "Right"){
				animation = ColumnRight.GetComponent<Animation>();
				animation["UIBaseRight_Hide"].speed = 1;
				animation.Play("UIBaseRight_Hide");
			}

		}
	}
	void OnTriggerExit2D(Collider2D coll) {


		if(coll.name == ship.name){
			Debug.Log("Exit UI");
			if(this.name == "Left"){
				animation = ColumnLeft.GetComponent<Animation>();
				animation["UIBaseLeft_Hide"].speed = -1;
				animation.Play("UIBaseLeft_Hide");
				animation["UIBaseLeft_Hide"].time = animation["UIBaseLeft_Hide"].length;
			}
			if(this.name == "Right"){
				animation = ColumnRight.GetComponent<Animation>();
				animation["UIBaseRight_Hide"].speed = -1;
				animation.Play("UIBaseRight_Hide");
				animation["UIBaseRight_Hide"].time = animation["UIBaseRight_Hide"].length;
			}
			
		}
	}
}
