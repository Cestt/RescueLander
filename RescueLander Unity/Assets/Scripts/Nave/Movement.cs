using UnityEngine;
using System.Collections;


[System.Serializable]
public class Movement : MonoBehaviour {

	RuntimePlatform platform = Application.platform;
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



	private Rigidbody2D rigid;

	// Use this for initialization
	void Awake() {

		rigid = GetComponent<Rigidbody2D>();

	}

	// Update is called once per frame
	void FixedUpdate () {


			if(platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer){
				if(Input.touchCount > 0) {

				Touch touch;
						
						if (Input.touchCount == 1){

								touch = Input.GetTouch(0);
							
							if(touch.phase != TouchPhase.Began || touch.phase != TouchPhase.Canceled || touch.phase != TouchPhase.Ended){

								if(touch.position.x > Camera.main.transform.position.x + Camera.main.pixelWidth/2){
									rigid.AddTorque(-angularForce - (angularForce * angularSpeedUpgrade));
									Debug.Log("Right");
							}else if(touch.position.x < Camera.main.transform.position.x + Camera.main.pixelWidth/2){
									
									rigid.AddTorque(angularForce + (angularForce * angularSpeedUpgrade));
									Debug.Log("Left");
								}

							}
					
							
						}
									
											

						if (Input.touchCount >1){	
								touch = Input.GetTouch(1);

							if(touch.phase != TouchPhase.Began || touch.phase != TouchPhase.Canceled || touch.phase != TouchPhase.Ended
					   				& fuel >0){
										
										Vector3 dir = Quaternion.AngleAxis(-90, Vector3.forward) * Vector3.right;
										rigid.AddForce(dir * (motorForce + (gameObject.transform.eulerAngles.magnitude + 90 * speedUpgrade)),ForceMode2D.Force);
										ConsumeFuel();		
									
							}	
						}	
				}

			}else if(platform == RuntimePlatform.WindowsEditor){

							if(Input.GetKey(KeyCode.Space) & fuel > 0 & rigid.velocity.magnitude < maxSpeed){
									
									Vector3 dir = Quaternion.AngleAxis(gameObject.transform.eulerAngles.magnitude + 90, Vector3.forward) * Vector3.right;
									rigid.AddForce(dir * (motorForce + (motorForce * speedUpgrade)),ForceMode2D.Force);
									ConsumeFuel();
									
							}

							
				
									if(Input.GetMouseButton(0) & rigid.angularVelocity < maxAngularSpeed){
								
									RaycastHit _hit;
									if(Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward,out _hit)){
											
										
														
												if(_hit.point.x > Camera.main.transform.position.x + Camera.main.pixelWidth/2){
															rigid.AddTorque(-angularForce - (angularForce * angularSpeedUpgrade));
															Debug.Log("Right");
															
												}else if(_hit.point.x < Camera.main.transform.position.x + Camera.main.pixelWidth/2){
															
															rigid.AddTorque(angularForce + (angularForce * angularSpeedUpgrade));
															Debug.Log("Left");
												}

											
												
										
										
									}
										
							}
								
				}

		}

	void ConsumeFuel () {
		fuel -= fuelConsumption;
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
