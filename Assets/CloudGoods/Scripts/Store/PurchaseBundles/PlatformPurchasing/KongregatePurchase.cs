// ----------------------------------------------------------------------
// <copyright file="KongregatePurchase.cs" company="SocialPlay">
//     Copyright statement. All right reserved
// </copyright>
// Owner: Alex Zanfir
// ------------------------------------------------------------------------
using System;

using UnityEngine;


public class KongregatePurchase : MonoBehaviour, IPlatformPurchaser
{
    public event Action<string> RecievedPurchaseResponse;
    public event Action<string> OnPurchaseErrorEvent;

    void Start()
    {
        Application.ExternalEval("function KongregateOnPurchaseResult(result) {" +
            "GetUnity().SendMessage('PremiumCurrencyBundleStore', 'OnReceivedPurchaseResponse', result.success.toString());" +
            "}"
            );
    }

    public void Purchase(PremiumBundle bundleItem, int amount, string appID)
    {
        string data = "'{\"id\":\"" + bundleItem.BundleID + "\",\"amount\":\"" + amount + "\",\"type\":\"Premium\",\"appID\":\"" + CloudGoods.AppID + "\"}'";
        Debug.Log(data);
        string final = "kongregate.mtx.purchaseItemsRemote(" +
           data +
           ", KongregateOnPurchaseResult);";
        Application.ExternalEval(final);
        Debug.Log(final);
    }

    public void OnReceivedPurchaseResponse(string data)
    {
        Debug.Log("OnRecievedPurchaseResponse " + data);

        if (RecievedPurchaseResponse != null)
            RecievedPurchaseResponse(data);
    }



}

