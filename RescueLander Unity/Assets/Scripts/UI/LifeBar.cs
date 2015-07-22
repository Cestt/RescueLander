using UnityEngine;
using System.Collections;

public class LifeBar : MonoBehaviour {

	private tk2dSlicedSprite slicedsprite;
	private int damageAcumulated;
	private int totalDamage;
	private float relation;
	private int damage;
	public float reductionSpeed;
	public int speed = 20;

	void Awake () {
		slicedsprite = GetComponent<tk2dSlicedSprite>();
	}
	
	// Update is called once per frame
	void Update () {

		/*if(damage <= 0){
			CancelInvoke ("LifeBarReduction");
		}*/
		if (damage > 0) {

			if (damage - speed >= 0){
				slicedsprite.dimensions = new Vector2( slicedsprite.dimensions.x - (speed*relation),slicedsprite.dimensions.y);
				totalDamage -= speed;
				damageAcumulated +=speed;
				damage -= speed;
			}else{
				slicedsprite.dimensions = new Vector2( slicedsprite.dimensions.x - (damage*relation),slicedsprite.dimensions.y);
				totalDamage -= damage;
				damageAcumulated += damage;
				damage -= damage;
			}

		}
	}

	public void Starter(int totalDamagetemp,float relationtemp){
		//CancelInvoke ("LifeBarReduction");
		if(this.gameObject.activeInHierarchy){
			totalDamage = totalDamagetemp;
			relation = relationtemp;
			damage = totalDamage - damageAcumulated;
			//InvokeRepeating("LifeBarReduction",0,reductionSpeed);
		}
	}

	private void LifeBarReduction(){
			if(slicedsprite.dimensions.x > 5){
				slicedsprite.dimensions = new Vector2( slicedsprite.dimensions.x - relation,slicedsprite.dimensions.y);
				Debug.Log("LifeBar reduction");
				totalDamage--;
				damageAcumulated++;
			damage --;
			}
	}
}
