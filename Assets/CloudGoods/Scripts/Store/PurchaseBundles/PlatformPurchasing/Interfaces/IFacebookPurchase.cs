using UnityEngine;
using System.Collections;
using System;

public  interface IFacebookPurchase {

     void Purchase(PremiumBundle bundleItem, int amount, Action<string> callback);
    
}
