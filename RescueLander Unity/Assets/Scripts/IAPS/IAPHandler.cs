﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla.Store;

public class IAPHandler : MonoBehaviour {
	
	private static IAPHandler iapHandler;

	



	
	void Start() {
		
		StoreEvents.OnMarketPurchase += onMarketPurchase;
		StoreEvents.OnMarketRefund += onMarketRefund;
		StoreEvents.OnItemPurchased += onItemPurchased;
		StoreEvents.OnGoodEquipped += onGoodEquipped;
		StoreEvents.OnGoodUnEquipped += onGoodUnequipped;
		StoreEvents.OnGoodUpgrade += onGoodUpgrade;
		StoreEvents.OnBillingSupported += onBillingSupported;
		StoreEvents.OnBillingNotSupported += onBillingNotSupported;
		StoreEvents.OnMarketPurchaseStarted += onMarketPurchaseStarted;
		StoreEvents.OnItemPurchaseStarted += onItemPurchaseStarted;
		StoreEvents.OnUnexpectedErrorInStore += onUnexpectedErrorInStore;
		StoreEvents.OnCurrencyBalanceChanged += onCurrencyBalanceChanged;
		StoreEvents.OnGoodBalanceChanged += onGoodBalanceChanged;
		StoreEvents.OnMarketPurchaseCancelled += onMarketPurchaseCancelled;
		StoreEvents.OnRestoreTransactionsStarted += onRestoreTransactionsStarted;
		StoreEvents.OnRestoreTransactionsFinished += onRestoreTransactionsFinished;
		
		#if UNITY_ANDROID && !UNITY_EDITOR
		StoreEvents.OnIabServiceStarted += onIabServiceStarted;
		StoreEvents.OnIabServiceStopped += onIabServiceStopped;
		#endif
		SoomlaStore.Initialize(new GameAssets());
		
		
		
	}
	
	/// <summary>
	/// Handles a market purchase event.
	/// </summary>
	/// <param name="pvi">Purchasable virtual item.</param>
	/// <param name="purchaseToken">Purchase token.</param>
	public void onMarketPurchase(PurchasableVirtualItem pvi, string purchaseToken,Dictionary<string,string> dic) {

	}
	
	/// <summary>
	/// Handles a market refund event.
	/// </summary>
	/// <param name="pvi">Purchasable virtual item.</param>
	public void onMarketRefund(PurchasableVirtualItem pvi) {
		
	}
	
	/// <summary>
	/// Handles an item purchase event. 
	/// </summary>
	/// <param name="pvi">Purchasable virtual item.</param>
	public void onItemPurchased(PurchasableVirtualItem pvi,string Item) {

		Debug.Log("Item Purchased");
		
	}
	
	/// <summary>
	/// Handles a good equipped event.
	/// </summary>
	/// <param name="good">Equippable virtual good.</param>
	public void onGoodEquipped(EquippableVG good) {
		
	}
	
	/// <summary>
	/// Handles a good unequipped event.
	/// </summary>
	/// <param name="good">Equippable virtual good.</param>
	public void onGoodUnequipped(EquippableVG good) {
		
	}
	
	/// <summary>
	/// Handles a good upgraded event. 
	/// </summary>
	/// <param name="good">Virtual good that is being upgraded.</param>
	/// <param name="currentUpgrade">The current upgrade that the given virtual 
	/// good is being upgraded to.</param>
	public void onGoodUpgrade(VirtualGood good, UpgradeVG currentUpgrade) {
		
	}
	
	/// <summary>
	/// Handles a billing supported event.
	/// </summary>
	public void onBillingSupported() {
		
	}
	
	/// <summary>
	/// Handles a billing NOT supported event.
	/// </summary>
	public void onBillingNotSupported() {
		
	}
	
	/// <summary>
	/// Handles a market purchase started event. 
	/// </summary>
	/// <param name="pvi">Purchasable virtual item.</param>
	public void onMarketPurchaseStarted(PurchasableVirtualItem pvi) {
		Debug.Log ("Market Purchase Started");
	}
	
	/// <summary>
	/// Handles an item purchase started event. 
	/// </summary>
	/// <param name="pvi">Purchasable virtual item.</param>
	public void onItemPurchaseStarted(PurchasableVirtualItem pvi) {
		
	}
	
	/// <summary>
	/// Handles an item purchase cancelled event. 
	/// </summary>
	/// <param name="pvi">Purchasable virtual item.</param>
	public void onMarketPurchaseCancelled(PurchasableVirtualItem pvi) {


	}
	
	/// <summary>
	/// Handles an unexpected error in store event.
	/// </summary>
	/// <param name="message">Error message.</param>
	public void onUnexpectedErrorInStore(string message) {
		

		
		
	}
	
	/// <summary>
	/// Handles a currency balance changed event.
	/// </summary>
	/// <param name="virtualCurrency">Virtual currency whose balance has changed.</param>
	/// <param name="balance">Balance of the given virtual currency.</param>
	/// <param name="amountAdded">Amount added to the balance.</param>
	public void onCurrencyBalanceChanged(VirtualCurrency virtualCurrency, int balance, int amountAdded) {
		Debug.Log("Bought Coins  Added"+amountAdded);
		dataManger.manager.coins += amountAdded;
		dataManger.manager.Save(false);
	}
	
	/// <summary>
	/// Handles a good balance changed event.
	/// </summary>
	/// <param name="good">Virtual good whose balance has changed.</param>
	/// <param name="balance">Balance.</param>
	/// <param name="amountAdded">Amount added.</param>
	public void onGoodBalanceChanged(VirtualGood good, int balance, int amountAdded) {
		//ExampleLocalStoreInfo.UpdateBalances();
	}
	
	/// <summary>
	/// Handles a restore Transactions process started event.
	/// </summary>
	public void onRestoreTransactionsStarted() {
		
	}
	
	/// <summary>
	/// Handles a restore transactions process finished event. 
	/// </summary>
	/// <param name="success">If set to <c>true</c> success.</param>
	public void onRestoreTransactionsFinished(bool success) {

	}
	
	
	
	#if UNITY_ANDROID && !UNITY_EDITOR
	public void onIabServiceStarted() {
		
	}
	public void onIabServiceStopped() {
		
	}
	#endif
	
	
	
	
}