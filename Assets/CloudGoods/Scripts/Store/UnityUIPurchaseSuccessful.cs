using UnityEngine;
using System.Collections;

public class UnityUIPurchaseSuccessful : MonoBehaviour {

    public GameObject purchaseSuccessPopup;

	// Use this for initialization
	void Awake () {
        UnityUIItemPurchase.OnPurchasedItem += UnityUIItemPurchase_OnPurchasedItem;
        UnityUIBundlePurchasing.OnPurchaseSuccessful += UnityUIItemPurchase_OnPurchasedItem;
	}

    void UnityUIItemPurchase_OnPurchasedItem(string obj)
    {
        purchaseSuccessPopup.SetActive(true);
    }

    public void ClosePopup()
    {
        purchaseSuccessPopup.SetActive(false);
    }
}
