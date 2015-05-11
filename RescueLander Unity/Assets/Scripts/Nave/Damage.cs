using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {

	public GameObject explosion;
	public GameObject sparks;
	public int life = 0;
	private int maxLife;
	public int damageThreshold = 0;
	public int damageThresholdFriction = 0;
	private ShipAstronautPickUp shipastronautpickup;
	public GameObject lifeBar;
	public GameObject GameManager;
	private float relation;
	private int totalDamage;
	private tk2dSlicedSprite slicedsprite;
	private LifeBar lifebarScript;
	private IEnumerator coroutine;
	private tk2dSpriteAnimator animator;
	WinLose winLose;

	// Use this for initialization
	void Awake () {
	
		shipastronautpickup = this.GetComponent<ShipAstronautPickUp>();
		slicedsprite = lifeBar.GetComponent<tk2dSlicedSprite>();
		lifebarScript = lifeBar.GetComponent<LifeBar>();
		coroutine = lifebarScript.LifeBarReduction(totalDamage,relation);
		relation = slicedsprite.dimensions.x/life;
		winLose = GameManager.GetComponent<WinLose> ();
		maxLife =life;


	}


	
	// Update is called once per frame
	void Update () {

		if(life < 0){

			if(shipastronautpickup.Astronaut != null){

				shipastronautpickup.Astronaut = null;

			}
			animator = explosion.GetComponent<tk2dSpriteAnimator>();
			lifebarScript.Starter((int)slicedsprite.dimensions.x,relation);
			explosion.SetActive(true);
			animator.Play("Explosion");
			winLose.End("Lose");
			animator.AnimationCompleted = DestroyShip;

		}

	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Floor"){

			if(coll.relativeVelocity.magnitude > damageThreshold){
				ContactPoint2D contactpoint = coll.contacts[0];
				sparks.transform.position = contactpoint.point;
				animator = sparks.GetComponent<tk2dSpriteAnimator>();
				sparks.SetActive(true);
				animator.Play("Sparks");
				animator.AnimationCompleted = ResetSparks;
				

				life -= (int)coll.relativeVelocity.magnitude - damageThreshold;
				if(totalDamage <= maxLife){
					totalDamage += (int)coll.relativeVelocity.magnitude - damageThreshold;
				}
				Debug.Log("Total Damage: " + totalDamage);
				lifebarScript.Starter(totalDamage,relation);
				Debug.Log("Hull Impact damage");

			}
		}
	}
	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Floor"){
			
			if(coll.relativeVelocity.magnitude > damageThresholdFriction){

				ContactPoint2D contactpoint = coll.contacts[0];
				sparks.transform.position = contactpoint.point;
				animator = sparks.GetComponent<tk2dSpriteAnimator>();
				sparks.SetActive(true);
				animator.Play("Sparks");
				animator.AnimationCompleted = ResetSparks;
				
				life -= (int)coll.relativeVelocity.magnitude - damageThreshold;
					if(totalDamage <= maxLife){
						totalDamage += (int)coll.relativeVelocity.magnitude - damageThreshold;
					}
				Debug.Log("Total Damage: " + totalDamage);
				lifebarScript.Starter(totalDamage,relation);
				Debug.Log("Hull friction damage");
				
			}
		}
	}

	void DestroyShip(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip){
		animator.AnimationCompleted = null;
		Destroy(gameObject);
	}
	void ResetSparks(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip){
		animator.AnimationCompleted = null;
		Vector2 temp = new Vector2(-100,0);
		sparks.transform.position = temp;
	}


	
}
