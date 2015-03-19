using UnityEngine;
using System.Collections;

public class LifeBar : MonoBehaviour {

	private tk2dSlicedSprite slicedsprite;
	public GameObject ship;
	private Damage damageScript;

	void Awake () {
		slicedsprite = GetComponent<tk2dSlicedSprite>();
		damageScript = ship.GetComponent<Damage> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public  IEnumerator LifeBarReduction(float relation){

		for(int damage = damageScript.totalDamage; damage >= 0; damage--){
			if(slicedsprite.dimensions.x > 0){
				slicedsprite.dimensions = new Vector2( slicedsprite.dimensions.x - relation,slicedsprite.dimensions.y);
				Debug.Log("LifeBar reduction");
				yield return new WaitForSeconds(1f/damage);
				damageScript.totalDamage--;

				
			}
			
		}
		
		
	}
}
