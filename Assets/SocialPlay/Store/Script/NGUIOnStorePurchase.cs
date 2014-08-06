using UnityEngine;
using System.Collections;

public class NGUIOnStorePurchase : MonoBehaviour {

    public GameObject purchaseConfirmPanel;

	// Use this for initialization
	void Start () {
        ItemPurchase.OnPurchasedItem += ItemPurchase_OnPurchasedItem;
	}

    void ItemPurchase_OnPurchasedItem(string obj)
    {
        purchaseConfirmPanel.SetActive(true);
    }

    public void CloseWindow()
    {
        purchaseConfirmPanel.SetActive(false);
    }

	
}
