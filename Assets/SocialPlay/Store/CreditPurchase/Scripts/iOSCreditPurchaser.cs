using UnityEngine;
using System.Collections;
using System;

public class iOSCreditPurchaser : MonoBehaviour, IPlatformPurchaser {

	public event Action<string> RecievedPurchaseResponse;
	public int currentBundleID = 0;

	void Start()
	{
		iOSConnect.onReceivedMessage += OnReceivedPurchaseResponse;
	}
	
	public void Purchase(string id, int amount, string userID)
	{
		Debug.Log ("Purchase clicked for : " + id);
		iOSConnect.RequestInAppPurchase (id);
	}
	
	public void OnReceivedPurchaseResponse(string data)
	{
		Debug.Log (data);
	}
	
	void OnPurchaseCreditsCallback(string data)
	{
		//SP.GetPaidCurrencyBalance(null);
	}
}
