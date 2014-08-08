using UnityEngine;
using System.Collections;

public class DisplayPaidCurrencyPurchase : MonoBehaviour {

    public GameObject CreditsPurchasePanel;

	// Use this for initialization
	void Start () {
        UIEventListener.Get(gameObject).onClick += DisplayCreditsPurchasePanel;
	}


    void DisplayCreditsPurchasePanel(GameObject buttonPressed)
    {
        CreditsPurchasePanel.SetActive(true);

        if (CreditsPurchasePanel.GetComponent<PaidCurrencyBundleStore>().isInitialized == false)
        {
            CreditsPurchasePanel.GetComponent<PaidCurrencyBundleStore>().Initialize();
        }
    }
}
