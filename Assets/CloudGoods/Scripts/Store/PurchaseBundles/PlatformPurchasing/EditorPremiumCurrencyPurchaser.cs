using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class EditorPremiumCurrencyPurchaser : MonoBehaviour, IPlatformPurchaser {

    public event Action<string> RecievedPurchaseResponse;
    public event Action<string> OnPurchaseErrorEvent;

    public void Purchase(PremiumBundle bundleItem, int amount, string userID)
    {
        OnReceivedPurchaseResponse("Successful Purchase");
    }

    public void OnReceivedPurchaseResponse(string data)
    {
        if(RecievedPurchaseResponse != null)
            RecievedPurchaseResponse(data);
    }

}
