using UnityEngine;
using System.Collections;
using System;

public class FacebookPurchasing : MonoBehaviour, IFacebookPurchase {

    public void Purchase(PremiumBundle bundleItem, int amount, Action<string> callback)
    {
        FB.Canvas.Pay(product: "https://staging.socialplay.com/CreditBundleDataFacebook?BundleID=" + bundleItem.BundleID,
                      quantity: amount,
                      callback: delegate(FBResult response)
                      {
                          callback(response.Text);
                          Debug.Log("Purchase Response: " + response.Text);
                      }
        );
    }
}
