using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class Purchaser : MonoBehaviour, IStoreListener
{
	private static IStoreController m_StoreController;          // The Unity Purchasing system.
	private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

	public static string product_50_godlies = "godlies50";
	public static string product_500_godlies = "godlies500"; 
	public static string product_decara = "decara";
	public static string product_bobman = "bobman";
	public static string product_hoaxer = "hoaxer";
	public static string product_garelio = "garelio";
	public static string product_zeta = "zeta";
	public static string product_xpeed = "xpeed";

	void Start(){
		// If we haven't set up the Unity Purchasing reference
		if (m_StoreController == null){
			// Begin to configure our connection to Purchasing
			InitializePurchasing();
		}
	}

	public void InitializePurchasing() {
		// If we have already connected to Purchasing ...
		if (IsInitialized()){
			return;
		}

		// Create a builder, first passing in a suite of Unity provided stores.
		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

		builder.AddProduct(product_50_godlies, ProductType.Consumable);
		builder.AddProduct(product_500_godlies, ProductType.Consumable);
		builder.AddProduct(product_decara, ProductType.NonConsumable);
		builder.AddProduct(product_bobman, ProductType.NonConsumable);
		builder.AddProduct(product_hoaxer, ProductType.NonConsumable);
		builder.AddProduct(product_garelio, ProductType.NonConsumable);
		builder.AddProduct(product_zeta, ProductType.NonConsumable);
		builder.AddProduct(product_xpeed, ProductType.NonConsumable);

		UnityPurchasing.Initialize(this, builder);
	}


	private bool IsInitialized(){
		// Only say we are initialized if both the Purchasing references are set.
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}


	public void Buy50Godlies(){
		BuyProductID(product_50_godlies);
	}
	public void Buy500Godlies(){
		BuyProductID(product_500_godlies);
	}
	public void BuyLyner(){
			PlayerPrefs.SetString ("car", "Lyner");
			GameObject.Find ("ButtonManager").GetComponent<ButtonManager> ().PrepareShopItems ();
	}
	public void BuyDeCara(){
		if (PlayerPrefs.GetInt ("DeCara") == 0)
			BuyProductID (product_decara);
		else {
			PlayerPrefs.SetString ("car", "DeCara");
			GameObject.Find ("ButtonManager").GetComponent<ButtonManager> ().PrepareShopItems ();
		}
	}
	public void BuyBobman(){
		if(PlayerPrefs.GetInt("Bobman") == 0)
			BuyProductID(product_bobman);
		else {
			PlayerPrefs.SetString ("car", "Bobman");
			GameObject.Find ("ButtonManager").GetComponent<ButtonManager> ().PrepareShopItems ();
		}
	}
	public void BuyHoaxer(){
		if(PlayerPrefs.GetInt("Hoaxer") == 0)
			BuyProductID(product_hoaxer);
		else {
			PlayerPrefs.SetString ("car", "Hoaxer");
			GameObject.Find ("ButtonManager").GetComponent<ButtonManager> ().PrepareShopItems ();
		}
	}
	public void BuyGarelio(){
		if(PlayerPrefs.GetInt("GarelioST") == 0)
			BuyProductID(product_garelio);
		else {
			PlayerPrefs.SetString ("car", "GarelioST");
			GameObject.Find ("ButtonManager").GetComponent<ButtonManager> ().PrepareShopItems ();
		}
	}
	public void BuyZeta(){
		if(PlayerPrefs.GetInt("ZetaX") == 0)
			BuyProductID(product_zeta);
		else {
			PlayerPrefs.SetString ("car", "ZetaX");
			GameObject.Find ("ButtonManager").GetComponent<ButtonManager> ().PrepareShopItems ();
		}
	}
	public void BuyXpeed(){
		if(PlayerPrefs.GetInt("XpeedRX") == 0)
			BuyProductID(product_xpeed);
		else {
			PlayerPrefs.SetString ("car", "XpeedRX");
			GameObject.Find ("ButtonManager").GetComponent<ButtonManager> ().PrepareShopItems ();
		}
	}

	void BuyProductID(string productId){
		// If Purchasing has been initialized ...
		if (IsInitialized()){
			// ... look up the Product reference with the general product identifier and the Purchasing 
			// system's products collection.
			Product product = m_StoreController.products.WithID(productId);

			// If the look up found a product for this device's store and that product is ready to be sold ... 
			if (product != null && product.availableToPurchase){
				Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
				// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
				// asynchronously.
				m_StoreController.InitiatePurchase(product);
			}
			else{
				// ... report the product look-up failure situation  
				Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
			}
		}
		else{
			// ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
			// retrying initiailization.
			Debug.Log("BuyProductID FAIL. Not initialized.");
		}
	}

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions){
		// Purchasing has succeeded initializing. Collect our Purchasing references.
		Debug.Log("OnInitialized: PASS");

		// Overall Purchasing system, configured with products for this application.
		m_StoreController = controller;
		// Store specific subsystem, for accessing device-specific store features.
		m_StoreExtensionProvider = extensions;
	}
		
	public void OnInitializeFailed(InitializationFailureReason error){
		// Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
		Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}
		
	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) {
		// A consumable product has been purchased by this user.
		if (String.Equals(args.purchasedProduct.definition.id, product_50_godlies, StringComparison.Ordinal)){
			PlayerPrefs.SetInt ("Godlies", PlayerPrefs.GetInt("Godlies") + 50);
			Debug.Log("Purchased 50 Godlies");
		}
		else if (String.Equals(args.purchasedProduct.definition.id, product_500_godlies, StringComparison.Ordinal)){
			PlayerPrefs.SetInt ("Godlies", PlayerPrefs.GetInt("Godlies") + 500);
			Debug.Log("Purchased 500 Godlies");
		}
		else if (String.Equals(args.purchasedProduct.definition.id, product_decara, StringComparison.Ordinal)){
			PlayerPrefs.SetInt ("DeCara", 1);
			PlayerPrefs.SetString ("car", "DeCara");
			GameObject.Find ("ButtonManager").GetComponent<ButtonManager> ().PrepareShopItems ();
			Debug.Log("Purchased DeCara");
		}
		else if (String.Equals(args.purchasedProduct.definition.id, product_bobman, StringComparison.Ordinal)){
			PlayerPrefs.SetInt ("Bobman", 1);
			PlayerPrefs.SetString ("car", "Bobman");
			GameObject.Find ("ButtonManager").GetComponent<ButtonManager> ().PrepareShopItems ();
			Debug.Log("Purchased Bobman");
		}
		else if (String.Equals(args.purchasedProduct.definition.id, product_hoaxer, StringComparison.Ordinal)){
			PlayerPrefs.SetInt ("Hoaxer", 1);
			PlayerPrefs.SetString ("car", "Hoaxer");
			GameObject.Find ("ButtonManager").GetComponent<ButtonManager> ().PrepareShopItems ();
			Debug.Log("Purchased Hoaxer");
		}
		else if (String.Equals(args.purchasedProduct.definition.id, product_garelio, StringComparison.Ordinal)){
			PlayerPrefs.SetInt ("GarelioST", 1);
			PlayerPrefs.SetString ("car", "GarelioST");
			GameObject.Find ("ButtonManager").GetComponent<ButtonManager> ().PrepareShopItems ();
			Debug.Log("Purchased GarelioST");
		}
		else if (String.Equals(args.purchasedProduct.definition.id, product_zeta, StringComparison.Ordinal)){
			PlayerPrefs.SetInt ("ZetaX", 1);
			PlayerPrefs.SetString ("car", "ZetaX");
			GameObject.Find ("ButtonManager").GetComponent<ButtonManager> ().PrepareShopItems ();
			Debug.Log("Purchased ZetaX");
		}
		else if (String.Equals(args.purchasedProduct.definition.id, product_xpeed, StringComparison.Ordinal)){
			PlayerPrefs.SetInt ("XpeedRX", 1);
			PlayerPrefs.SetString ("car", "XpeedRX");
			GameObject.Find ("ButtonManager").GetComponent<ButtonManager> ().PrepareShopItems ();
			Debug.Log("Purchased XpeedRX");
		}
		else {
			Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
		}
		return PurchaseProcessingResult.Complete;
	}

	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason){
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}
}