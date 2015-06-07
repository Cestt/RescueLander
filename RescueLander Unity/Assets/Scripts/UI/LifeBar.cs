using UnityEngine;
using System.Collections;

public class LifeBar : MonoBehaviour {

	private tk2dSlicedSprite slicedsprite;
	private int damageAcumulated;
	private int totalDamage;
	private float relation;
	private int damage;
	public float reductionSpeed;
	void Awake () {
		slicedsprite = GetComponent<tk2dSlicedSprite>();
	}
	
	// Update is called once per frame
	void Update () {

		if(damage <= 0){
			CancelInvoke ("LifeBarReduction");
		}
	
	}
	public void Starter(int totalDamagetemp,float relationtemp){

		CancelInvoke ("LifeBarReduction");
		if(this.gameObject.activeInHierarchy){
			totalDamage = totalDamagetemp;
			relation = relationtemp;
			 damage = totalDamage - damageAcumulated;

			InvokeRepeating("LifeBarReduction",0,reductionSpeed / damage);
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
