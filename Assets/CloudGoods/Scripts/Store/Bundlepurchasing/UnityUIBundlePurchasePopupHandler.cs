using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnityUIBundlePurchasePopupHandler : MonoBehaviour {

    public PremiumCurrencyBundleStore bundleStore;
    public GameObject PurchaseWindow;

    public Text purchaseMessage;

    bool platformPurchaserSet = false;


	// Use this for initialization
	void Update () {
        if (bundleStore.platformPurchasor != null && platformPurchaserSet == false)
        {
			Debug.Log("platform purchase set");
            bundleStore.platformPurchasor.OnPurchaseErrorEvent += platformPurchasor_RecievedPurchaseResponse;
            bundleStore.platformPurchasor.RecievedPurchaseResponse += platformPurchasor_RecievedPurchaseResponse;

            platformPurchaserSet = true;
        }
	}

    void platformPurchasor_RecievedPurchaseResponse(string obj)
    {
        Debug.Log("Purchase popup event called");

        PurchaseWindow.SetActive(true);
        purchaseMessage.text = obj;
    }

    public void CloseWindow()
    {
        PurchaseWindow.SetActive(false);
    }
}
