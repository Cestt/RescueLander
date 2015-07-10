using UnityEngine;
using System.Collections;
using System;
using Soomla.Store;

public class Coin_Manager : MonoBehaviour {

	private int totalCoins;
	public int OneStarCoin, TwoStarCoin, ThreeStarCoin;
	[HideInInspector]
	public int levelCoins;
	private Social_Manager socialManager;
	Touch_Manager touch;

	void Awake(){
		socialManager = GameObject.Find ("Game Manager").GetComponent<Social_Manager>();
		touch = GameObject.Find("Game Manager").GetComponent<Touch_Manager>();
	}
	public bool Compra(int Coins,string Type,string Item){
		try{
			if(Coins > dataManger.manager.coins){
				GameObject uicameraGameobject = GameObject.Find("UI_Camera");
				uicameraGameobject.transform.FindChild("Prompt_Menu").gameObject.SetActive(true);
				touch.actualPrompt = uicameraGameobject.transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_MoreMoney").gameObject;
				touch.actualPrompt.SetActive(true);
				return false;
			}else{
				dataManger.manager.coins -= Coins;
				dataManger.manager.coinsSpend += Coins;
				//ACHIEVEMENT
				if (dataManger.manager.coinsSpend >= 100000){
					Social.ReportProgress("CgkIuv-YgIkeEAIQEg", 100.0f, (bool success) => {
						socialManager.Check("Achievement","CgkIuv-YgIkeEAIQEg",success);
					});
				}
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
				if(Type == "IAP"){
					if(Item.Contains("1"))
					   StoreInventory.BuyItem(GameAssets.COINS_5000.ItemId);
					else if(Item.Contains("2"))
						StoreInventory.BuyItem(GameAssets.COINS_11000.ItemId);
					else if(Item.Contains("3"))
						StoreInventory.BuyItem(GameAssets.COINS_30000.ItemId);
					else if(Item.Contains("4"))
						StoreInventory.BuyItem(GameAssets.COINS_80000.ItemId);
					else if(Item.Contains("5"))
						StoreInventory.BuyItem(GameAssets.COINS_250000.ItemId);
					else if(Item.Contains("6"))
						StoreInventory.BuyItem(GameAssets.COINS_575000.ItemId);

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
		dataManger.manager.coins += coin;
		dataManger.manager.coinsAcumulated += coin;
		//ACHIEVEMENT
		if (dataManger.manager.coinsAcumulated >= 100000){
			Social.ReportProgress("CgkIuv-YgIkeEAIQEQ", 100.0f, (bool success) => {
				socialManager.Check("Achievement","CgkIuv-YgIkeEAIQEQ",success);
			});
		}
	}
	public void LevelCoin(float coin){
		levelCoins += (int)coin;

	}
}
