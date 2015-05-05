﻿using UnityEngine;
using System.Collections;


[System.Serializable]
public class Movement2 : MonoBehaviour {
	
	RuntimePlatform platform = Application.platform;
	public Touch_Manager touchmanager;
	public GameObject fuelBar;
	public GameObject Thruster_r;
	public GameObject Thruster_l;
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
	public float fuel = 0;
	public float fuelConsumption = 0;
	public float massMotor = 0f;
	public float massAngular = 0f;
	public float angularDrag = 0f;
	public float linearDrag = 0f;
	private float originlSize;
	private float originalFuel;
	private int currentFrame = 0;
	private tk2dSlicedSprite slicedsprite;
	private tk2dSpriteAnimator animator;
	
	
	
	
	private Rigidbody2D rigid;
	
	// Use this for initialization
	void Awake() {
		
		rigid = GetComponent<Rigidbody2D>();
		slicedsprite = fuelBar.GetComponent<tk2dSlicedSprite>();
		originlSize = slicedsprite.dimensions.x;
		originalFuel = fuel;
		animator =  Fire.GetComponent<tk2dSpriteAnimator>();
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if(animator != null & animator.IsPlaying("Fire_Start")||
		   animator != null & animator.IsPlaying("Fire_End")||
		   animator != null & animator.IsPlaying("Fire_Loop")){
			currentFrame = animator.CurrentFrame;
		}
		
		if(platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer || platform == RuntimePlatform.WindowsEditor){
			if(Input.touchCount > 0) {
				
				Touch touch;
				if(!touchmanager.paused){
					if (Input.touchCount == 1){
						
						touch = Input.GetTouch(0);
						
						
						if(touch.phase != TouchPhase.Began || touch.phase != TouchPhase.Canceled || touch.phase != TouchPhase.Ended){
							
							Vector3 tempVect = Camera.main.ScreenToWorldPoint(touch.position);
							
							if(tempVect.x > Camera.main.transform.position.x){
								Vector3 dir = Quaternion.AngleAxis(gameObject.transform.eulerAngles.magnitude + 45, Vector3.forward) * Vector3.right;
								rigid.AddForce(dir * (motorForce + (motorForce * speedUpgrade)),ForceMode2D.Force);
								
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
								
								rigid.AddTorque(angularForce + (angularForce * angularSpeedUpgrade));
								
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
							
						}
						
						if(touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended || Input.touchCount == 2){
							animator.Stop();
							Thruster_l.SetActive(false);
							Thruster_r.SetActive(false);
						}
						
						motor  = false;
					}
					
					
					
					if (Input.touchCount ==2){	
						touch = Input.GetTouch(1);
						Debug.Log("Touch");
						if(touch.phase != TouchPhase.Began || touch.phase != TouchPhase.Canceled || touch.phase != TouchPhase.Ended
						   & fuel >0){
							
							Vector3 dir = Quaternion.AngleAxis(gameObject.transform.eulerAngles.magnitude + 90, Vector3.forward) * Vector3.right;
							rigid.AddForce(dir * (motorForce + (motorForce * speedUpgrade)),ForceMode2D.Force);
							animator = Fire.GetComponent<tk2dSpriteAnimator>();
							if(!Fire.activeInHierarchy)
								Fire.SetActive(true);
							ConsumeFuel();
							/*Javi*/motor = true;
							
						}
						if(touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended){
							motor  = false;
							if(animator.IsPlaying("Fire_Start")){
								animator.Play("Fire_End");
								animator.PlayFromFrame(7-currentFrame);
							}else{
								animator.Play("Fire_End");
							}
						}
						
						
					}	
				}
			}
			
			
		}
		if(platform == RuntimePlatform.WindowsEditor){
			
			if(!touchmanager.paused){
				if(Input.GetKey(KeyCode.Space) & fuel > 0 & rigid.velocity.magnitude < maxSpeed){
					
					Vector3 dir = Quaternion.AngleAxis(gameObject.transform.eulerAngles.magnitude + 90, Vector3.forward) * Vector3.right;
					rigid.AddForce(dir * (motorForce + (motorForce * speedUpgrade)),ForceMode2D.Force);
					animator = Fire.GetComponent<tk2dSpriteAnimator>();
					if(!Fire.activeInHierarchy)
						Fire.SetActive(true);					
					ConsumeFuel();
					/*Javi*/motor = true;
					
				}
				if(Input.GetKeyUp(KeyCode.Space)){
					motor = false;
					if(animator.IsPlaying("Fire_Start")){
						animator.Play("Fire_End");
						animator.PlayFromFrame(7-currentFrame);
					}else{
						animator.Play("Fire_End");
					}
					
					
				}
				
				
				
				if(Input.GetMouseButton(0) & rigid.angularVelocity < maxAngularSpeed){
					
					
					
					Vector3 tempVect = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					
					if(tempVect.x > Camera.main.transform.position.x){
						Vector3 dir = Quaternion.AngleAxis(gameObject.transform.eulerAngles.magnitude + 70, Vector3.forward) * Vector3.right;
						rigid.AddForce(dir * (motorForce + (motorForce * speedUpgrade)),ForceMode2D.Force);
						
						
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
						
						Vector3 dir = Quaternion.AngleAxis(gameObject.transform.eulerAngles.magnitude + 110, Vector3.forward) * Vector3.right;
						rigid.AddForce(dir * (motorForce + (motorForce * speedUpgrade)),ForceMode2D.Force);
						
						if(!Thruster_r.activeInHierarchy){
							Thruster_r.SetActive(true);
						}
						animator = Thruster_r.GetComponent<tk2dSpriteAnimator>();
						if(!animator.IsPlaying("Thruster_Loop")){
							animator.Play("Thruster_Start");
						}
						animator.AnimationCompleted = ThrusterLoop;
					}
					motor  = false;
					
				}
				if(Input.GetMouseButtonUp(0)){
					animator.Stop();
					Thruster_l.SetActive(false);
					Thruster_r.SetActive(false);
				}
				
			}
		}
		
		
	}
	
	void ConsumeFuel () {
		
		fuel -= fuelConsumption;
		slicedsprite.dimensions = new Vector2(originlSize * (fuel/originalFuel),slicedsprite.dimensions.y);
		motor = true;
		if(!animator.IsPlaying("Fire_Loop")){
			if(animator.IsPlaying("Fire_End")){
				animator.Play("Fire_Start");
				animator.PlayFromFrame(7-currentFrame);
			}else{
				animator.Play("Fire_Start");
			}
			
		}
		
		animator.AnimationCompleted = FireLoop;
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
		if(clip.name == "Fire_End")
			Fire.SetActive(false);
		if(!animator.IsPlaying("Fire_Loop") & clip.name != "Fire_End" )
			animator.Play("Fire_Loop");
	}
	
	
	
	
}