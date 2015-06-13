using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla.Store;

	public class GameAssets : IStoreAssets {

		
		
	

		public int GetVersion() {
			return 0;
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
		return new VirtualCurrencyPack[] {COINS_PACK_5000,COINS_PACK_11000,COINS_PACK_30000,COINS_PACK_80000,COINS_PACK_250000,COINS_PACK_575000};
		}
		public VirtualCategory[] GetCategories ()
		{
			return new VirtualCategory[] {};
		}
		
		
		public const string COINS_ID  = "coin_currency";
		#if UNITY_ANDROID
			public const string COINS_PACK_5000_ID = "android.test.purchased";
			public const string COINS_PACK_11000_ID = "android.test.purchased1";
			public const string COINS_PACK_30000_ID = "android.test.purchased2";
			public const string COINS_PACK_80000_ID = "android.test.purchased3";
			public const string COINS_PACK_250000_ID = "android.test.purchased4";
			public const string COINS_PACK_575000_ID = "android.test.purchased5";
		#elif UNITY_IPHONE
		
		#endif
		
		public static VirtualCurrency COINS = new VirtualCurrency(
		"Coins",                                  // name
		"Coins currency",                          // description
		COINS_ID                     // item ID
		);
		
		

		public static VirtualCurrencyPack COINS_PACK_5000 = new VirtualCurrencyPack(
			"producto.coins_5000", 
			"Pack 5000 Coins",
			COINS_PACK_5000_ID,
			5000,
			COINS_ID,
			new PurchaseWithMarket(COINS_PACK_5000_ID,0.5)
			);

		public static VirtualCurrencyPack COINS_PACK_11000 = new VirtualCurrencyPack(
			"producto.coins_11000", 
			"Pack 11000 Coins",
			COINS_PACK_11000_ID,
			11000,
			COINS_ID,
			new PurchaseWithMarket(COINS_PACK_11000_ID,0.5)
			);
		public static VirtualCurrencyPack COINS_PACK_30000 = new VirtualCurrencyPack(
			"producto.coins_30000", 
			"Pack 30000 Coins",
			COINS_PACK_30000_ID,
			30000,
			COINS_ID,
			new PurchaseWithMarket(COINS_PACK_30000_ID,0.5)
			);
		public static VirtualCurrencyPack COINS_PACK_80000 = new VirtualCurrencyPack(
			"producto.coins_80000", 
			"Pack 80000 Coins",
			COINS_PACK_80000_ID,
			80000,
			COINS_ID,
			new PurchaseWithMarket(COINS_PACK_80000_ID,0.5)
			);
		public static VirtualCurrencyPack COINS_PACK_250000 = new VirtualCurrencyPack(
			"producto.coins_250000", 
			"Pack 250000 Coins",
			COINS_PACK_250000_ID,
			250000,
			COINS_ID,
			new PurchaseWithMarket(COINS_PACK_250000_ID,0.5)
			);
		public static VirtualCurrencyPack COINS_PACK_575000 = new VirtualCurrencyPack(
			"producto.coins_575000", 
			"Pack 575000 Coins",
			COINS_PACK_575000_ID,
			575000,
			COINS_ID,
			new PurchaseWithMarket(COINS_PACK_575000_ID,0.5)
			);
		
	}

	
