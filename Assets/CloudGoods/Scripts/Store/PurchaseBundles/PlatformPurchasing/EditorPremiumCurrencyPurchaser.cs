using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class EditorPremiumCurrencyPurchaser : MonoBehaviour, IPlatformPurchaser {

    public event Action<string> RecievedPurchaseResponse;
    public event Action<string> OnPurchaseErrorEvent;

    public enum EditorPurchaseResultType
    {
        Pass,
        Fail,
        Error
    }

    public EditorPurchaseResultType resultType = EditorPurchaseResultType.Pass;

    public void Purchase(PremiumBundle bundleItem, int amount, string userID)
    {
        switch (resultType)
        {
            case EditorPurchaseResultType.Pass:
                OnReceivedPurchaseResponse("Successful Purchase");
                break;
            case EditorPurchaseResultType.Fail:
                OnReceivedPurchaseResponse("Failed Purchase");
                break;
            case EditorPurchaseResultType.Error:
                OnReceivedPurchaseResponse("Error Occured when purchasing");
                break;
        }
    }

    public void OnReceivedPurchaseResponse(string data)
    {
        switch (data)
        {
            case "Successful Purchase":
                if (RecievedPurchaseResponse != null)
                    RecievedPurchaseResponse(data);
                break;
            case "Failed Purchase":
                if (OnPurchaseErrorEvent != null)
                    OnPurchaseErrorEvent(data);
                break;
            case "Error Occured when purchasing":
                if (OnPurchaseErrorEvent != null)
                    OnPurchaseErrorEvent(data);
                break;
        }

    }

}
