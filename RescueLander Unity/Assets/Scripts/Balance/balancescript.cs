using UnityEngine;
using System.Collections;

public class balancescript : MonoBehaviour {

	private Movement movement;
	private Rigidbody2D rigid;
	public float massAngular = 0f;
	public float massMotor = 0f;
	public float angularDrag = 0f;
	public float linearDrag = 0f;



	public void BalanceCalc(){
		movement = GetComponent<Movement>();
		rigid = GetComponent<Rigidbody2D>();

		movement.angularForce = rigid.mass * massAngular;
		movement.motorForce = rigid.mass * massMotor;
		rigid.angularDrag = movement.angularForce * angularDrag;
		rigid.drag = movement.motorForce * linearDrag;

	}

}
