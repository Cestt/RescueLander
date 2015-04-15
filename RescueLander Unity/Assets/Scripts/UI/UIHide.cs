using UnityEngine;
using System.Collections;

public class UIHide : MonoBehaviour {

	private Animation animation;
	public GameObject ColumnLeft;
	public GameObject ColumnRight;
	public GameObject ship;
	private tk2dCamera camera;
	private bool first = true;


	void Awake(){

		Physics2D.IgnoreLayerCollision(5, 8, true);
		camera = this.GetComponent<tk2dCamera>();
	
	}

	void Update(){
		if(ship.transform.position.x > Camera.main.transform.position.x - (camera.nativeResolutionHeight/2 + 545) & 
		   ship.transform.position.y < Camera.main.transform.position.y + (camera.nativeResolutionWidth/2 - 128)){
			if(first){
				animation = ColumnLeft.GetComponent<Animation>();
				animation["UIBaseLeft_Hide"].speed = 1;
				animation.Play("UIBaseLeft_Hide");
				first = false;
			}

		}else{
			if(!first){
				animation = ColumnLeft.GetComponent<Animation>();
				animation["UIBaseLeft_Hide"].speed = -1;
				animation.Play("UIBaseLeft_Hide");
				animation["UIBaseLeft_Hide"].time = animation["UIBaseLeft_Hide"].length;
				first = true;
			}

		}
	}
	void OnTriggerEnter2D(Collider2D coll) {
		Debug.Log("Enter UI");
		if(coll.name == "Ship"){
			if(this.name == "Anim_Collider_L"){
				animation = ColumnLeft.GetComponent<Animation>();
				animation["UIBaseLeft_Hide"].speed = 1;
				animation.Play("UIBaseLeft_Hide");
			}
			if(this.name == "Anim_Collider_R"){
				animation = ColumnRight.GetComponent<Animation>();
				animation["UIBaseRight_Hide"].speed = 1;
				animation.Play("UIBaseRight_Hide");
			}

		}
	}
	void OnTriggerExit2D(Collider2D coll) {
		Debug.Log("Exit UI");

		if(coll.name == "Ship"){
			if(this.name == "Anim_Collider_L"){
				animation = ColumnLeft.GetComponent<Animation>();
				animation["UIBaseLeft_Hide"].speed = -1;
				animation.Play("UIBaseLeft_Hide");
				animation["UIBaseLeft_Hide"].time = animation["UIBaseLeft_Hide"].length;
			}
			if(this.name == "Anim_Collider_R"){
				animation = ColumnRight.GetComponent<Animation>();
				animation["UIBaseRight_Hide"].speed = -1;
				animation.Play("UIBaseRight_Hide");
				animation["UIBaseRight_Hide"].time = animation["UIBaseRight_Hide"].length;
			}
			
		}
	}
}
