using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {

	public int life = 0;
	public int damageThreshold = 0;
	private ShipAstronautPickUp shipastronautpickup;
	public GameObject lifeBar;
	public GameObject GameManager;
	private float relation;
	private int totalDamage;
	private tk2dSlicedSprite slicedsprite;
	private LifeBar lifebarScript;
	private IEnumerator coroutine;
	WinLose winLose;

	// Use this for initialization
	void Awake () {
	
		shipastronautpickup = this.GetComponent<ShipAstronautPickUp>();
		slicedsprite = lifeBar.GetComponent<tk2dSlicedSprite>();
		lifebarScript = lifeBar.GetComponent<LifeBar>();
		coroutine = lifebarScript.LifeBarReduction(totalDamage,relation);
		relation = slicedsprite.dimensions.x/life;
		winLose = GameManager.GetComponent<WinLose> ();

	}

	void Start() {


	}
	
	// Update is called once per frame
	void Update () {

		if(life < 0){

			if(shipastronautpickup.Astronaut != null){

				shipastronautpickup.Astronaut = null;

			}

			lifebarScript.Starter((int)slicedsprite.dimensions.x,relation);
			winLose.End("Lose");
			Destroy(gameObject);

		}

	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Floor"){

			if(coll.relativeVelocity.magnitude > damageThreshold){

				life -= (int)coll.relativeVelocity.magnitude - damageThreshold;
				totalDamage += (int)coll.relativeVelocity.magnitude - damageThreshold;
				Debug.Log("Total Damage: " + totalDamage);
				lifebarScript.Starter(totalDamage,relation);
				Debug.Log("Hull Impact damage");

			}else{

				Debug.Log("No hull impact damage");

			}
		}
	}
	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Floor"){
			
			if(coll.relativeVelocity.magnitude > damageThreshold){
				
				life -= (int)coll.relativeVelocity.magnitude - damageThreshold;
				totalDamage += (int)coll.relativeVelocity.magnitude - damageThreshold;
				Debug.Log("Total Damage: " + totalDamage);
				lifebarScript.Starter(totalDamage,relation);
				Debug.Log("Hull friction damage");
				
			}else{
				
				Debug.Log("No hull friction damage");
				
			}
		}
	}


	
}
