using UnityEngine;
using System.Collections;

public class LifeBar : MonoBehaviour {

	private tk2dSlicedSprite slicedsprite;
	private int damageAcumulated;

	void Awake () {
		slicedsprite = GetComponent<tk2dSlicedSprite>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Starter(int totalDamage,float relation){
		StopCoroutine ("LifeBarReduction");
		StartCoroutine(LifeBarReduction(totalDamage,relation));
	}
	public IEnumerator LifeBarReduction(int totalDamage,float relation){
		int damage = totalDamage - damageAcumulated;
		for(int i = damage; i >= 0; i--){
			if(slicedsprite.dimensions.x > 0){
				slicedsprite.dimensions = new Vector2( slicedsprite.dimensions.x - relation,slicedsprite.dimensions.y);
				Debug.Log("LifeBar reduction");
				yield return new WaitForSeconds(0.5f/damage);
				totalDamage--;
				damageAcumulated++;
			}
			
		}
		
		
	}
}
