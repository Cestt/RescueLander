using UnityEngine;
using System.Collections;


[System.Serializable]
public class Movement : MonoBehaviour {

	RuntimePlatform platform = Application.platform;
	public GameObject fuelBar;
	[HideInInspector]
	public float motorForce = 0f;
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
	private float relation;
	private tk2dSlicedSprite slicedsprite;



	private Rigidbody2D rigid;

	// Use this for initialization
	void Awake() {

		rigid = GetComponent<Rigidbody2D>();
		slicedsprite = fuelBar.GetComponent<tk2dSlicedSprite>();
		relation = slicedsprite.dimensions.x/fuel;
		
	}

	// Update is called once per frame
	void FixedUpdate () {


			if(platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer || platform == RuntimePlatform.WindowsEditor){
				if(Input.touchCount > 0) {

				Touch touch;
						
						if (Input.touchCount == 1){

								touch = Input.GetTouch(0);
								
							
							if(touch.phase != TouchPhase.Began || touch.phase != TouchPhase.Canceled || touch.phase != TouchPhase.Ended){

								Vector3 tempVect = Camera.main.ScreenToWorldPoint(touch.position);

								if(tempVect.x > Camera.main.transform.position.x){
									rigid.AddTorque(-angularForce - (angularForce * angularSpeedUpgrade));
									Debug.Log("Right");
								} 
								if(tempVect.x < Camera.main.transform.position.x){
									
									rigid.AddTorque(angularForce + (angularForce * angularSpeedUpgrade));
									Debug.Log("Left");
								}

							}
					
							
						}
									
											

						if (Input.touchCount ==2){	
								touch = Input.GetTouch(1);
								Debug.Log("Touch");
							if(touch.phase != TouchPhase.Began || touch.phase != TouchPhase.Canceled || touch.phase != TouchPhase.Ended
					   				& fuel >0){
										
										Vector3 dir = Quaternion.AngleAxis(gameObject.transform.eulerAngles.magnitude + 90, Vector3.forward) * Vector3.right;
										rigid.AddForce(dir * (motorForce + (motorForce * speedUpgrade)),ForceMode2D.Force);
										ConsumeFuel();		
									
							}	
						}	
				}

			}
			if(platform == RuntimePlatform.WindowsEditor){

							if(Input.GetKey(KeyCode.Space) & fuel > 0 & rigid.velocity.magnitude < maxSpeed){
									
									Vector3 dir = Quaternion.AngleAxis(gameObject.transform.eulerAngles.magnitude + 90, Vector3.forward) * Vector3.right;
									rigid.AddForce(dir * (motorForce + (motorForce * speedUpgrade)),ForceMode2D.Force);
									ConsumeFuel();
									
							}

							
				
									if(Input.GetMouseButton(0) & rigid.angularVelocity < maxAngularSpeed){
								
									
											
											Vector3 tempVect = Camera.main.ScreenToWorldPoint(Input.mousePosition);
														
											if(tempVect.x > Camera.main.transform.position.x){
															rigid.AddTorque(-angularForce - (angularForce * angularSpeedUpgrade));
															Debug.Log("Right");
															
												}
											if(tempVect.x  < Camera.main.transform.position.x){
															
															rigid.AddTorque(angularForce + (angularForce * angularSpeedUpgrade));
															Debug.Log("Left");
												}

											
												
										
										
									
										
							}
								
				}

		}

	void ConsumeFuel () {
		fuel -= fuelConsumption;
		slicedsprite.dimensions = new Vector2(slicedsprite.dimensions.x - (relation*fuelConsumption),slicedsprite.dimensions.y);
		Debug.Log("Fuel consumption");
	}

	public void BalanceCalc(){

		rigid = GetComponent<Rigidbody2D>();

		angularForce = rigid.mass * massAngular;
		motorForce = rigid.mass * massMotor;
		rigid.angularDrag = angularForce * angularDrag;
		rigid.drag = motorForce * linearDrag;
		
	}



	}
