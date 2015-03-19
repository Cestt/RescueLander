using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {

	public int life = 0;
	public int damageThreshold = 0;
	private ShipAstronautPickUp shipastronautpickup;
	public GameObject lifeBar;
	private float relation;
	[HideInInspector]
	public int totalDamage = 0;
	private tk2dSlicedSprite slicedsprite;
	private LifeBar lifebarScript;
	private IEnumerator coroutine;

	// Use this for initialization
	void Awake () {
	
		shipastronautpickup = this.GetComponent<ShipAstronautPickUp>();
		slicedsprite = lifeBar.GetComponent<tk2dSlicedSprite>();
		lifebarScript = lifeBar.GetComponent<LifeBar>();
		coroutine = lifebarScript.LifeBarReduction(relation);
		relation = slicedsprite.dimensions.x/life;

	}

	void Start() {


	}
	
	// Update is called once per frame
	void Update () {

		if(life < 0){

			if(shipastronautpickup.Astronaut != null){

				shipastronautpickup.Astronaut = null;

			}

			StopCoroutine(coroutine);
			StartCoroutine(lifebarScript.LifeBarReduction(relation));
			Destroy(gameObject);

		}

	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Floor"){

			if(coll.relativeVelocity.magnitude > damageThreshold){
				if(life > 0){
						life -= (int)coll.relativeVelocity.magnitude - damageThreshold;
						totalDamage += (int)coll.relativeVelocity.magnitude - damageThreshold;
						Debug.Log("Total Damage: " + totalDamage);
						StopCoroutine(coroutine);
						StartCoroutine(lifebarScript.LifeBarReduction(relation));
						Debug.Log("Hull Impact damage");
				}
			}
		}
	}
	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Floor"){
			
			if(coll.relativeVelocity.magnitude > damageThreshold){
				if(life > 0){
					life -= (int)coll.relativeVelocity.magnitude - damageThreshold;
					totalDamage += (int)coll.relativeVelocity.magnitude - damageThreshold;
					Debug.Log("Total Damage: " + totalDamage);
					StopCoroutine(coroutine);
					StartCoroutine(lifebarScript.LifeBarReduction(relation));
					Debug.Log("Hull friction damage");
				}
			}
		}
	}


	
}
