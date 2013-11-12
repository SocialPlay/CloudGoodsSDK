using UnityEngine;
using System.Collections;
using System;

public interface IBundlePurchaser {

    event Action<string> RecievedPurchaseResponse;

    void Purchase(string id, int amount, string userID);
    void OnRecievedPurchaseResponse(string data);
}
