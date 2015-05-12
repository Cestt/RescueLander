using UnityEngine;
using System.Collections;

public class ShipAstronautPickUp : MonoBehaviour {


	[HideInInspector]
	public bool Pickable = false;
	[HideInInspector]
	public GameObject Astronaut;
	[HideInInspector]
	public int astronautPicked = 0;
	private Rigidbody2D rigid;
	private tk2dSpriteAnimator animator;
	private Touch_Manager touchmanager;


	// Use this for initialization
	void Awake () {
		touchmanager = GameObject.Find("Game Manager").GetComponent<Touch_Manager>();
		rigid = GetComponent<Rigidbody2D>();


	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Pickable = true & rigid.velocity.magnitude <= 0.0001 & Astronaut != null & touchmanager.paused ==false){

			animator = Astronaut.GetComponent<tk2dSpriteAnimator>();
			if(!animator.IsPlaying("Halo")){
				animator.Play("Halo");
				astronautPicked++;
				Pickable = false;
				animator.AnimationCompleted = DestroyAstro;
				Astronaut = null;
			}

			Debug.Log("Astronaut Picked: "+astronautPicked);

		}


	
	}
	void DestroyAstro(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip){
		Destroy(Astronaut);
		animator.AnimationCompleted = null;
	}
}
