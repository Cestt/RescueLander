using UnityEngine;
using System.Collections;

public class IceCube : MonoBehaviour {
	
	tk2dSpriteAnimator animator;
	tk2dSpriteAnimator animatorAstronaut;
	Movement move;
	public float timeCube = 3; 
	GameObject ship;
	
	void Awake () {
		animator = GetComponent<tk2dSpriteAnimator> ();
		animatorAstronaut = transform.parent.GetComponent<tk2dSpriteAnimator> ();
		animator.AnimationCompleted = IceCubeLoop;
		ship = GameObject.Find (dataManger.manager.actualShip + "(Clone)");
		move = ship.GetComponent<Movement> ();
	}
	
	
	private void IceCubeLoop(tk2dSpriteAnimator animatorSprite, tk2dSpriteAnimationClip clip){
		animatorAstronaut.Play ();
		gameObject.SetActive (false);
	}
	
	void OnTriggerStay2D(Collider2D coll) {
		if (coll.gameObject.name == "Fire" & (ship.gameObject.transform.eulerAngles.magnitude < 50 || ship.gameObject.transform.eulerAngles.magnitude > 320)){
			if (move.motor){
				timeCube -= Time.deltaTime;
				Debug.Log ("TIEMPO RESTANTE: "+ timeCube);
				if (timeCube <= 0){
					animatorAstronaut.Play ();
					gameObject.SetActive (false);
				}
				//animator.Play();
			}else if (animator.Playing){
				animator.Pause();
			}
			
		}
	}
	
	void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.name == "Fire" & animator.Playing) {
			//animator.Pause ();
		}
	}
}
