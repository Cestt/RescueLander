using UnityEngine;
using System.Collections;


[System.Serializable]
public class Movement : MonoBehaviour {

	RuntimePlatform platform = Application.platform;
	private Touch_Manager touchmanager;
	[HideInInspector]
	public GameObject fuelBar;
	[HideInInspector]
	public GameObject Thruster_r;
	[HideInInspector]
	public GameObject Thruster_l;
	[HideInInspector]
	public GameObject Fire;
	[HideInInspector]
	public float motorForce = 0f;
	[HideInInspector]
	public bool motor;
	public float speedUpgrade = 0f;
	public int maxSpeed = 0;
	[HideInInspector]
	public float angularForce = 0f;
	public float angularSpeedUpgrade = 0f;
	public int maxAngularSpeed = 0;
	[HideInInspector]
	public float fuel;
	public float fuelConsumption = 0;
	public float massMotor = 0f;
	public float massAngular = 0f;
	public float angularDrag = 0f;
	public float linearDrag = 0f;
	private float originlSize;
	public float originalFuel;
	private int currentFrame = 0;
	private tk2dSlicedSprite slicedsprite;
	private tk2dSpriteAnimator animator;
	private tk2dSpriteAnimator animator2;
	private Rigidbody2D rigid;
	private AudioSource audioSource;
	private AudioClip clipRealenti;
	private AudioClip clipMotor;
	public float [] fuelLevel;
	public float actualTime;
	public float LoseTime;
	private bool running;
	private AudioSource audioThruster;
	// Use this for initialization
	void Awake() {

		if(Application.loadedLevelName.Contains("Tuto")){
			fuel = 2500;
		}else{
			fuel = fuelLevel[dataManger.manager.actualLevel-1];
		}
		touchmanager = GameObject.Find("Game Manager").GetComponent<Touch_Manager>();
		fuelBar = GameObject.Find("BarraFuel");
		Transform findChild = transform.FindChild("Thruster_R");
		Thruster_r = findChild.gameObject;
		findChild = transform.FindChild("Thruster_L");
		Thruster_l = findChild.gameObject;
		findChild = transform.FindChild("Fire");
		Fire = findChild.gameObject;
		animator2 = Fire.GetComponent<tk2dSpriteAnimator>();
		rigid = GetComponent<Rigidbody2D>();
		slicedsprite = fuelBar.GetComponent<tk2dSlicedSprite>();
		originlSize = slicedsprite.dimensions.x;
		originalFuel = fuel;
		animator =  Fire.GetComponent<tk2dSpriteAnimator>();

		audioSource = GetComponent<AudioSource>();
		clipRealenti = Resources.Load ("Sounds/EngineRealenti(Loop)") as AudioClip;
		clipMotor = Resources.Load ("Sounds/Motor1(Loop)") as AudioClip;
		audioThruster = transform.FindChild("Feet").GetComponent<AudioSource>();
	}


	void FixedUpdate () {
		if (!dataManger.manager.Sounds && audioSource.isPlaying){
			audioSource.Stop ();
		}else if (dataManger.manager.Sounds && !audioSource.isPlaying){
			audioSource.Play();
		}
		if(fuel < 0 & !running){
			actualTime = Time.time;
			running = true;

		}else if(fuel > 0){
			running = false;
		}
		if(running & actualTime + LoseTime < Time.time){
			GameObject.Find("Game Manager").GetComponent<WinLose>().End("Lose");
		}
		if(animator != null & animator.IsPlaying("Fire_Start")||
		   animator != null & animator.IsPlaying("Fire_End")||
		   animator != null & animator.IsPlaying("Fire_Loop")){
			currentFrame = animator.CurrentFrame;
		}
			if(platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer){
				if (motor & Input.touchCount < 2){
					motor  = false;
					if(animator2.IsPlaying("Fire_Start")){
						animator2.Play("Fire_End");
						animator2.PlayFromFrame(7-currentFrame);
					}else{
						animator2.Play("Fire_End");
					}
					if (dataManger.manager.Sounds){
						if (audioSource.clip != clipRealenti || ( audioSource.clip == clipRealenti && !audioSource.isPlaying)){
							audioSource.clip = clipRealenti;
							audioSource.Play();
						}
					}
				}
				if(Input.touchCount > 0) {

				Touch touch;
				if(!touchmanager.paused){
					if (Input.touchCount == 1){
						
						touch = Input.GetTouch(0);
						
						
						if(touch.phase != TouchPhase.Began || touch.phase != TouchPhase.Canceled || touch.phase != TouchPhase.Ended){

							Vector3 tempVect = Camera.main.ScreenToWorldPoint(touch.position);
							
							if(tempVect.x > Camera.main.transform.position.x){
								if(dataManger.manager.inverted){
									rigid.AddTorque(angularForce + ((angularForce * angularSpeedUpgrade)/100));
								}else{
									rigid.AddTorque(-angularForce - ((angularForce * angularSpeedUpgrade)/100));
								}
								if(angularSpeedUpgrade != 0){
									ConsumeFuel(false,false);
								}

								
								if(!Thruster_l.activeInHierarchy){
									Thruster_l.SetActive(true);
								}
								if(Thruster_r.activeInHierarchy){
									Thruster_r.SetActive(false);
								}
								animator = Thruster_l.GetComponent<tk2dSpriteAnimator>();
								if(!animator.IsPlaying("Thruster_Loop")){
									animator.Play("Thruster_Start");
								}
								animator.AnimationCompleted = ThrusterLoop;
							} 
							if(tempVect.x < Camera.main.transform.position.x){

								if(dataManger.manager.inverted){
									rigid.AddTorque(-angularForce - ((angularForce * angularSpeedUpgrade)/100));
								}else{
									rigid.AddTorque(angularForce + ((angularForce * angularSpeedUpgrade)/100));
								}
								if(angularSpeedUpgrade != 0){
									ConsumeFuel(false,false);
								}

								
								if(!Thruster_r.activeInHierarchy){
									Thruster_r.SetActive(true);
								}
								if(Thruster_l.activeInHierarchy){
									Thruster_l.SetActive(false);
								}
								animator = Thruster_r.GetComponent<tk2dSpriteAnimator>();
								if(!animator.IsPlaying("Thruster_Loop")){
									animator.Play("Thruster_Start");
								}
								
								animator.AnimationCompleted = ThrusterLoop;
							}

							if (dataManger.manager.Sounds && !audioThruster.isPlaying)
								audioThruster.Play();
						}
						
						if(touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended || Input.touchCount == 2){
							animator.Stop();
							Thruster_l.SetActive(false);
							Thruster_r.SetActive(false);
							if (audioThruster.isPlaying)
								audioThruster.Stop();
						}
						
						motor  = false;
					}
					
					
					
					if (Input.touchCount == 2 & fuel > 0){	
						touch = Input.GetTouch(1);
						animator.Stop();
						Thruster_l.SetActive(false);
						Thruster_r.SetActive(false);
						if (audioThruster.isPlaying)
							audioThruster.Stop();
						if(touch.phase != TouchPhase.Began || touch.phase != TouchPhase.Canceled || touch.phase != TouchPhase.Ended){
							
							Vector3 dir = Quaternion.AngleAxis(gameObject.transform.eulerAngles.magnitude + 90, Vector3.forward) * Vector3.right;
							rigid.AddForce(dir * (motorForce + (motorForce * speedUpgrade)),ForceMode2D.Force);

							if(!Fire.activeInHierarchy)
								Fire.SetActive(true);
							ConsumeFuel(true,false);
							motor = true;

							if (dataManger.manager.Sounds){
								if (audioSource.clip != clipMotor || ( audioSource.clip == clipMotor && !audioSource.isPlaying)){
									audioSource.clip = clipMotor;
									audioSource.Play();
								}
							}
						}
						if(touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended){
							motor  = false;
							if(animator2.IsPlaying("Fire_Start")){
								animator2.Play("Fire_End");
								animator2.PlayFromFrame(7-currentFrame);
							}else{
								animator2.Play("Fire_End");
							}
							if (dataManger.manager.Sounds){
								if (audioSource.clip != clipRealenti || ( audioSource.clip == clipRealenti && !audioSource.isPlaying)){
									audioSource.clip = clipRealenti;
									audioSource.Play();
								}
							}
						}
						
						
					}	
				}
			}
			
			
		}
		if(platform == RuntimePlatform.WindowsEditor){

			if(!touchmanager.paused){

				if(Input.GetKey(KeyCode.Space) & fuel > 0 & rigid.velocity.magnitude < maxSpeed){
					animator.Stop();
					Thruster_l.SetActive(false);
					Thruster_r.SetActive(false);
					if (audioThruster.isPlaying)
						audioThruster.Stop();

					Vector3 dir = Quaternion.AngleAxis(gameObject.transform.eulerAngles.magnitude + 90, Vector3.forward) * Vector3.right;
					rigid.AddForce(dir * (motorForce + (motorForce * speedUpgrade)),ForceMode2D.Force);

					if(!Fire.activeInHierarchy)
						Fire.SetActive(true);					
					ConsumeFuel(true,false);
					motor = true;
					if (dataManger.manager.Sounds){
						if (audioSource.clip != clipMotor || ( audioSource.clip == clipMotor && !audioSource.isPlaying)){
							audioSource.volume = 0.35f;
							audioSource.clip = clipMotor;
							audioSource.Play();
						}
					}
				}
				if(Input.GetKeyUp(KeyCode.Space)){
					motor = false;
					if(animator2.IsPlaying("Fire_Start")){
						animator2.Play("Fire_End");
						animator2.PlayFromFrame(7-currentFrame);
					}else{
						animator2.Play("Fire_End");
					}
					if (dataManger.manager.Sounds){
						if (audioSource.clip != clipRealenti || ( audioSource.clip == clipRealenti && !audioSource.isPlaying)){
							audioSource.volume = 0.1f;
							audioSource.clip = clipRealenti;
							audioSource.Play();
						}
					}
				}
				
				
				
				if(Input.GetMouseButton(0) & rigid.angularVelocity < maxAngularSpeed){
					
					
					
					Vector3 tempVect = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					
					if(tempVect.x > Camera.main.transform.position.x){
						if(dataManger.manager.inverted){
							rigid.AddTorque(angularForce + ((angularForce * angularSpeedUpgrade)/100));
						}else{
							rigid.AddTorque(-angularForce - ((angularForce * angularSpeedUpgrade)/100));
						}
						if(angularSpeedUpgrade != 0){
							ConsumeFuel(false,true);
						}
						
						if(!Thruster_l.activeInHierarchy){
							Thruster_l.SetActive(true);
						}
						animator = Thruster_l.GetComponent<tk2dSpriteAnimator>();
						if(!animator.IsPlaying("Thruster_Loop")){
							animator.Play("Thruster_Start");
						}
						animator.AnimationCompleted = ThrusterLoop;
						
					}
					if(tempVect.x  < Camera.main.transform.position.x){
						
						if(dataManger.manager.inverted){
							rigid.AddTorque(-angularForce - ((angularForce * angularSpeedUpgrade)/100));
						}else{
							rigid.AddTorque(angularForce + ((angularForce * angularSpeedUpgrade)/100));
						}
						if(angularSpeedUpgrade != 0){
							ConsumeFuel(false,true);
						}
						
						if(!Thruster_r.activeInHierarchy){
							Thruster_r.SetActive(true);
						}
						animator = Thruster_r.GetComponent<tk2dSpriteAnimator>();
						if(!animator.IsPlaying("Thruster_Loop")){
							animator.Play("Thruster_Start");
						}
						animator.AnimationCompleted = ThrusterLoop;
					}

					if (dataManger.manager.Sounds && !audioThruster.isPlaying)
						audioThruster.Play();
					motor  = false;
					
				}
				if(Input.GetMouseButtonUp(0)){
					animator.Stop();
					Thruster_l.SetActive(false);
					Thruster_r.SetActive(false);
					if (audioThruster.isPlaying)
						audioThruster.Stop();
				}
				
			}
		}
		
		
	}
	
	void ConsumeFuel (bool Complete,bool increment) {
		if(!increment){
			fuel -= fuelConsumption;
		}else{
			fuel -= fuelConsumption * 1.5f;
		}
		if(fuel > 0)
		slicedsprite.dimensions = new Vector2(originlSize * (fuel/originalFuel),slicedsprite.dimensions.y);
		motor = true;
		if(Complete){
			if(!animator2.IsPlaying("Fire_Loop")){
				if(animator2.IsPlaying("Fire_End")){
					animator2.Play("Fire_Start");
					animator2.PlayFromFrame(7-currentFrame);
				}else{
					animator2.Play("Fire_Start");
				}
				
			}
			
			animator2.AnimationCompleted = FireLoop;
		}

	}

	public void BalanceCalc(){

		rigid = GetComponent<Rigidbody2D>();

		angularForce = rigid.mass * massAngular;
		motorForce = rigid.mass * massMotor;
		rigid.angularDrag = angularForce * angularDrag;
		rigid.drag = motorForce * linearDrag;
		
	}

	void ThrusterLoop(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip){
		if(!animator.IsPlaying("Thruster_Loop"))
		animator.Play("Thruster_Loop");
	}

	void FireLoop(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip){
		if(!animator2.IsPlaying("Fire_Loop") & clip.name != "Fire_End" )
			animator2.Play("Fire_Loop");
	}




	}
