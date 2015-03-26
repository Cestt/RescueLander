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

	// Use this for initialization
	void Start () {

		rigid = GetComponent<Rigidbody2D>();


	
	}
	
	// Update is called once per frame
	void Update () {

		if(Pickable = true & rigid.velocity.magnitude == 0 & Astronaut != null){

			animator = Astronaut.GetComponent<tk2dSpriteAnimator>();
			Pickable = false;
			animator.Play("Halo");
			animator.AnimationCompleted = DestroyAstro;
			Astronaut = null;
			astronautPicked++;
			Debug.Log("Astronaut Picked");

		}


	
	}
	void DestroyAstro(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip){
		Destroy(Astronaut);
		animator.AnimationCompleted = null;
	}
}
