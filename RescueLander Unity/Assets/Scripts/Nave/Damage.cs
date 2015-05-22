using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {
	[HideInInspector]
	public GameObject explosion;
	[HideInInspector]
	public GameObject sparks;
	public int life = 0;
	[HideInInspector]
	public int maxLife;
	public int damageThreshold = 0;
	public int damageThresholdFriction = 0;
	public float DamageVariant;
	private ShipAstronautPickUp shipastronautpickup;
	private GameObject lifeBar;
	[HideInInspector]
	public GameObject GameManager;
	private float relation;
	private int totalDamage;
	private tk2dSlicedSprite slicedsprite;
	private LifeBar lifebarScript;
	private tk2dSpriteAnimator animator;
	private float realDamage;
	WinLose winLose;
	Movement movement;
	bool first = true;
	public float thrusterExtra;

	// Use this for initialization
	void Awake () {
		movement = GetComponent<Movement> ();
		maxLife =life;
		Transform findChild = transform.FindChild("Explosion");
		explosion = findChild.gameObject;
		sparks = GameObject.Find("Sparks");
		GameObject temp = GameObject.Find("UI_Camera");
		lifeBar = temp.transform.FindChild("Anchor (UpperLeft)/UIBase_Left/BarraFondo_Vida/BarraVida").gameObject;
		Debug.Log (lifeBar.name);
		GameManager = GameObject.Find("Game Manager");
		shipastronautpickup = this.GetComponent<ShipAstronautPickUp>();
		slicedsprite = lifeBar.GetComponent<tk2dSlicedSprite>();
		lifebarScript = lifeBar.GetComponent<LifeBar>();
		relation = slicedsprite.dimensions.x/life;
		winLose = GameManager.GetComponent<WinLose> ();



	}


	
	// Update is called once per frame
	void Update () {
		if(life <= (maxLife*3)/4 & life >= (maxLife*2)/4){
			slicedsprite.SetSprite("BarraVida_Naranja");
		}
		if(life < (maxLife*2)/4){
			slicedsprite.SetSprite("BarraVida_Roja");
		}

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
				sparks.SetActive(true);
				sparks.transform.position = contactpoint.point;
				Quaternion rot = Quaternion.FromToRotation(Vector3.up, contactpoint.normal);
				sparks.transform.rotation = rot;
				animator = sparks.GetComponent<tk2dSpriteAnimator>();
				animator.Play("Sparks");
				animator.AnimationCompleted = ResetSparks;
				

				realDamage = (int)coll.relativeVelocity.magnitude - damageThreshold;
				realDamage = realDamage + ((DamageVariant * realDamage)/100);
				life -= (int)realDamage;
				if(totalDamage <= maxLife){
					totalDamage += (int)realDamage;
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
				Quaternion rot = Quaternion.FromToRotation(Vector3.up, contactpoint.normal);
				sparks.transform.rotation = rot;
				animator = sparks.GetComponent<tk2dSpriteAnimator>();
				sparks.SetActive(true);
				animator.Play("Sparks");
				animator.AnimationCompleted = ResetSparks;
				
				realDamage = (int)coll.relativeVelocity.magnitude - damageThreshold;
				realDamage = realDamage + ((DamageVariant * realDamage)/100);
				life -= (int)realDamage;
				if(totalDamage <= maxLife){
					totalDamage += (int)realDamage;
				}
				Debug.Log("Total Damage: " + totalDamage);
				lifebarScript.Starter(totalDamage,relation);
				Debug.Log("Hull friction damage");
				
			}
			if(gameObject.transform.eulerAngles.magnitude > 90 & gameObject.transform.eulerAngles.magnitude < 270 & first == true ){
				movement.angularSpeedUpgrade = thrusterExtra;
				movement.Thruster_l.GetComponent<tk2dSprite>().color = new Color(0,144,229);
				movement.Thruster_r.GetComponent<tk2dSprite>().color = new Color(0,144,229);
				movement.Thruster_l.GetComponent<tk2dSprite>().scale = new Vector3(1,1,1);
				movement.Thruster_r.GetComponent<tk2dSprite>().scale = new Vector3(1,1,1);
				first = false;
			}
		}
	}
	void OnCollisionExit2D(Collision2D coll) {

		movement.angularSpeedUpgrade = 0;
		movement.Thruster_l.GetComponent<tk2dSprite>().color = new Color(255,255,255);
		movement.Thruster_r.GetComponent<tk2dSprite>().color = new Color(255,255,255);
		movement.Thruster_l.GetComponent<tk2dSprite>().scale = new Vector3(0.55f,0.55f,1);
		movement.Thruster_r.GetComponent<tk2dSprite>().scale = new Vector3(0.55f,0.55f,1);
		first = true;

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
