using UnityEngine;
using System.Collections;
using System;

public class Coin_Manager : MonoBehaviour {

	private int totalCoins;
	public int OneStarCoin, TwoStarCoin, ThreeStarCoin;
	[HideInInspector]
	public int levelCoins;

	public bool Compra(int Coins,string Type,string Item){
		try{
			if(Coins > dataManger.manager.coins){
				return false;
			}else{
				dataManger.manager.coins -= Coins;
				if(Type == "PowerUp"){
					switch(Item){
					case "Shield":
						dataManger.manager.shieldPowerUps ++;
						dataManger.manager.Save(false);
						break;
					case "Magnet":
						dataManger.manager.magnetPowerUps ++;
						dataManger.manager.Save(false);
						break;
					case "Fuel":
						dataManger.manager.fuelPowerUps ++;
						dataManger.manager.Save(false);
						break;
						
					}
				}
				if(Type == "Ship"){
					dataManger.manager.shipUnlocks.Add(Item);
					dataManger.manager.Save(false);
				}

				
				return true;
			}

		}
		catch(Exception e){
			Debug.Log("Excepcion: " + e);
			return false;
		}
	}
	public void LevelCoin(int coin){
		levelCoins += coin;
		dataManger.manager.coins += coin;
	}
}
