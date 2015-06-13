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
	private Sound_Manager soundManager;

	// Use this for initialization
	void Awake () {
		touchmanager = GameObject.Find("Game Manager").GetComponent<Touch_Manager>();
		rigid = GetComponent<Rigidbody2D>();
		soundManager = GameObject.Find("Game Manager").GetComponent<Sound_Manager>();

	}

	void Start(){
		soundManager.PlaySound("EngineStart");
	}

	// Update is called once per frame
	void Update () {

		if(Pickable = true & rigid.velocity.magnitude <= 0.01 & Astronaut != null & touchmanager.paused ==false &
		   (gameObject.transform.eulerAngles.magnitude < 50 || gameObject.transform.eulerAngles.magnitude > 320)){

			animator = Astronaut.GetComponent<tk2dSpriteAnimator>();
			if(!animator.IsPlaying("Halo")){
				animator.Play("Halo");
				astronautPicked++;
				soundManager.PlaySound("PickUp");
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
