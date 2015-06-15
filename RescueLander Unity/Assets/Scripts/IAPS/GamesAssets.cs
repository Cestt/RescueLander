using UnityEngine;
using System.Collections;
using Soomla.Store;



public class GameAssets:IStoreAssets {
	//Update the 0 if you add more avaialbe items later, or else you will get errors.
	
	public int GetVersion() {
		return 1;
	}
	
	public VirtualCurrency[] GetCurrencies ()
	{
		return new VirtualCurrency[]{COINS};
	}
	public VirtualGood[] GetGoods ()
	{
		return new VirtualGood[] {};
	}
	public VirtualCurrencyPack[] GetCurrencyPacks ()
	{
		return new VirtualCurrencyPack[] {
			COINS_5000,
			COINS_11000,
			COINS_30000,
			COINS_80000,
			COINS_250000,
			COINS_575000
		};
	}
	public VirtualCategory[] GetCategories ()
	{
		return new VirtualCategory[] {};
	}

	
	
	#if UNITY_ANDROID
	public const string NO_ADDS_PRODUCT_ID = "se.dixum.tut.soomla.android.removeads";
	#elif UNITY_IPHONE
	public const string NO_ADDS_PRODUCT_ID = "se.dixum.tut.soomla.ios.removeads";
	#endif
	
	public const string COINS_5000_ID = "Test";
	public const string COINS_11000_ID = "Test1";
	public const string COINS_30000_ID = "Test2";
	public const string COINS_80000_ID = "Test3";
	public const string COINS_250000_ID = "Test4";
	public const string COINS_575000_ID = "Test5";
	public const string COINS_ID = "COINS";

	public static VirtualCurrency COINS = new VirtualCurrency(
		"Coins",
		"In game currency",
		COINS_ID);
	

	public static VirtualCurrencyPack COINS_5000 = new VirtualCurrencyPack(
		"5000 coins",
		"Buy 5000 coins",
		COINS_5000_ID,
		5000,
		COINS_ID,
		new PurchaseWithMarket(COINS_5000_ID,0.5));
	public static VirtualCurrencyPack COINS_11000 = new VirtualCurrencyPack(
		"11000 coins",
		"Buy 11000 coins",
		COINS_11000_ID,
		11000,
		COINS_ID,
		new PurchaseWithMarket(COINS_11000_ID,0.5));
	public static VirtualCurrencyPack COINS_30000 = new VirtualCurrencyPack(
		"30000 coins",
		"Buy 30000 coins",
		COINS_5000_ID,
		30000,
		COINS_ID,
		new PurchaseWithMarket(COINS_30000_ID,0.5));
	public static VirtualCurrencyPack COINS_80000 = new VirtualCurrencyPack(
		"80000 coins",
		"Buy 80000 coins",
		COINS_80000_ID,
		80000,
		COINS_ID,
		new PurchaseWithMarket(COINS_80000_ID,0.5));
	public static VirtualCurrencyPack COINS_250000 = new VirtualCurrencyPack(
		"250000 coins",
		"Buy 250000 coins",
		COINS_5000_ID,
		250000,
		COINS_ID,
		new PurchaseWithMarket(COINS_250000_ID,0.5));
	public static VirtualCurrencyPack COINS_575000 = new VirtualCurrencyPack(
		"575000 coins",
		"Buy 575000 coins",
		COINS_5000_ID,
		575000,
		COINS_ID,
		new PurchaseWithMarket(COINS_575000_ID,0.5));


	
	
	
	
	
	
	
}