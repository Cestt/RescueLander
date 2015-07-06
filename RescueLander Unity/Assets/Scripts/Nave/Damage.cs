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
	public int damageThresholdFrictionIce = 0;
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
	private tk2dSpriteAnimator animator2;
	private float realDamage;
	WinLose winLose;
	Movement movement;
	bool first = true;
	public float thrusterExtra;
	private float prevSpeed;
	private Rigidbody2D rigid;
	[HideInInspector]
	public int dmgReduction = 0;
	[HideInInspector]
	public Vector2 saveSpeed;
	private Touch_Manager touch;
	private Sound_Manager soundManager;
	private bool activateAlarm; 
	private bool activateExplosion;
	private float actualTime;
	private bool once = false;
	// Use this for initialization
	void Awake () {
		rigid = GetComponent<Rigidbody2D>();
		movement = GetComponent<Movement> ();
		maxLife =life;
		Transform findChild = transform.FindChild("Explosion");
		explosion = findChild.gameObject;
		sparks = GameObject.Find("Sparks");
		GameObject temp = GameObject.Find("UI_Camera");
		lifeBar = temp.transform.FindChild("Anchor (UpperLeft)/UIBase_Left/BarraFondo_Vida/BarraVida").gameObject;
		GameManager = GameObject.Find("Game Manager");
		touch = GameManager.GetComponent<Touch_Manager>();
		shipastronautpickup = this.GetComponent<ShipAstronautPickUp>();
		slicedsprite = lifeBar.GetComponent<tk2dSlicedSprite>();
		lifebarScript = lifeBar.GetComponent<LifeBar>();
		relation = slicedsprite.dimensions.x/life;
		winLose = GameManager.GetComponent<WinLose> ();
		soundManager = GameManager.GetComponent<Sound_Manager>();
		activateAlarm = true;
		activateExplosion = true;
		animator = sparks.GetComponent<tk2dSpriteAnimator>();
		animator2 = explosion.GetComponent<tk2dSpriteAnimator>();
	}


	
	// Update is called once per frame
	void Update () {
		if((gameObject.transform.eulerAngles.magnitude < 50 || gameObject.transform.eulerAngles.magnitude > 320) & first == false ){
			movement.angularSpeedUpgrade = 0;
			movement.Thruster_l.GetComponent<tk2dSprite>().color = new Color32(255,255,255,255);
			movement.Thruster_r.GetComponent<tk2dSprite>().color = new Color32(255,255,255,255);
			movement.Thruster_l.GetComponent<tk2dSprite>().scale = new Vector3(0.55f,0.55f,1);
			movement.Thruster_r.GetComponent<tk2dSprite>().scale = new Vector3(0.55f,0.55f,1);
			first = true;
		}
		if(!touch.paused){
			prevSpeed = rigid.velocity.magnitude;
			saveSpeed = rigid.velocity;
		}
		if(transform.position.y < 0){
			life = -1;
		}

		if(life <= (maxLife*0.6f) & life >= (maxLife*0.3f)){
			slicedsprite.SetSprite("BarraVida_Naranja");
		}
		if(life < (maxLife*0.3f)){
			slicedsprite.SetSprite("BarraVida_Roja");
			if (activateAlarm){
				soundManager.PlaySound("Alarm");
				activateAlarm = false;
			}
		}

		if(life < 0 & activateExplosion){

			if(shipastronautpickup.Astronaut != null){

				shipastronautpickup.Astronaut = null;

			}

			lifebarScript.Starter((int)slicedsprite.dimensions.x,relation);
			explosion.SetActive(true);
			animator2.Play("Explosion");
			gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
			if(Application.loadedLevelName.Contains("Tuto")){
				GetComponent<tk2dSprite>().enabled = false;
			}else{
				GetComponent<SpriteRenderer>().sprite = null;
			}
			if(transform.FindChild("Ship_Window") != null){
				GameObject temp = transform.FindChild("Ship_Window").gameObject;
				if(temp != null)
					temp.SetActive(false);
			}
			if (transform.FindChild("Fire") != null){
				GameObject temp = transform.FindChild("Fire").gameObject;
				if(temp != null)
					temp.SetActive(false);
			}
			if (transform.FindChild("Fire_Smoke") != null){
				GameObject temp = transform.FindChild("Fire_Smoke").gameObject;
				if(temp != null)
					temp.SetActive(false);
			}
			if (transform.FindChild("Thruster_R") != null){
				GameObject temp = transform.FindChild("Thruster_R").gameObject;
				if(temp != null)
					temp.SetActive(false);
			} 
			if (transform.FindChild("Thruster_L") != null){
				GameObject temp = transform.FindChild("Thruster_L").gameObject;
				if(temp != null)
					temp.SetActive(false);
			} 
			activateExplosion = false;


			Debug.Log("Last instruction");
			animator2.AnimationCompleted = DestroyShip;


		}

	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Floor"){

			if(prevSpeed > damageThreshold){
				ContactPoint2D contactpoint = coll.contacts[0];
				sparks.SetActive(true);
				sparks.transform.position = contactpoint.point;
				Quaternion rot = Quaternion.FromToRotation(Vector3.up, contactpoint.normal);
				sparks.transform.rotation = rot;

				animator.Play("Sparks");
				animator.AnimationCompleted = ResetSparks;
				

				realDamage = (int)prevSpeed - damageThreshold;
				realDamage = realDamage + ((DamageVariant * realDamage)/100)-((dmgReduction * realDamage)/100);
				life -= (int)realDamage;
				if(totalDamage <= maxLife){
					totalDamage += (int)realDamage;
				}
				lifebarScript.Starter(totalDamage,relation);


			}
			if(gameObject.transform.eulerAngles.magnitude > 50 & gameObject.transform.eulerAngles.magnitude < 320 & first == true ){
				movement.angularSpeedUpgrade = thrusterExtra;
				movement.Thruster_l.GetComponent<tk2dSprite>().color = new Color32(0,144,229,255);
				movement.Thruster_r.GetComponent<tk2dSprite>().color = new Color32(0,144,229,255);
				movement.Thruster_l.GetComponent<tk2dSprite>().scale = new Vector3(1,1,1);
				movement.Thruster_r.GetComponent<tk2dSprite>().scale = new Vector3(1,1,1);
				first = false;
			}
		}
	}
	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Floor"){
			//Caso normal, el limite normal
			int damageThresholdFrictionAct = damageThresholdFriction;
			//Si el material es hielo se asigna el limite de hielo
			if (coll.gameObject.name == "BezierCurve"){
				PhysicsMaterial2D  floorMat = coll.gameObject.GetComponent<EdgeCollider2D>().sharedMaterial;
				if (floorMat != null && floorMat.name == "IceMaterial")
					damageThresholdFrictionAct = damageThresholdFrictionIce;
			}
			if(coll.relativeVelocity.magnitude > damageThresholdFrictionAct){

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
				lifebarScript.Starter(totalDamage,relation);

				
			}

		}
	}


	void DestroyShip(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip){
		Debug.Log("Show Lose");
		winLose.End("Lose");
		Debug.Log("Destroy ship");
		Destroy(gameObject);
	}
	void ResetSparks(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip){
		animator.AnimationCompleted = null;
		Vector2 temp = new Vector2(-100,0);
		sparks.transform.position = temp;
	}


	
}
