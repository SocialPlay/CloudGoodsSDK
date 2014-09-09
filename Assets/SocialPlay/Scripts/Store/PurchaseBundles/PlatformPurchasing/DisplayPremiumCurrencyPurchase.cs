using UnityEngine;
using System.Collections;

public class DisplayPremiumCurrencyPurchase : MonoBehaviour {

    public GameObject CreditsPurchasePanel;

	// Use this for initialization
	void Start () {
        UIEventListener.Get(gameObject).onClick += DisplayCreditsPurchasePanel;
	}


    void DisplayCreditsPurchasePanel(GameObject buttonPressed)
    {
        CreditsPurchasePanel.SetActive(true);

        if (CreditsPurchasePanel.GetComponent<PremiumCurrencyBundleStore>().isInitialized == false)
        {
            CreditsPurchasePanel.GetComponent<PremiumCurrencyBundleStore>().Initialize();
        }
    }
}
