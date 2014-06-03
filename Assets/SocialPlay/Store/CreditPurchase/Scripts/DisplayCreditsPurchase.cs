using UnityEngine;
using System.Collections;

public class DisplayCreditsPurchase : MonoBehaviour {

    public GameObject CreditsPurchasePanel;

	// Use this for initialization
	void Start () {
        UIEventListener.Get(gameObject).onClick += DisplayCreditsPurchasePanel;
	}


    void DisplayCreditsPurchasePanel(GameObject buttonPressed)
    {
        CreditsPurchasePanel.SetActive(true);

        if (CreditsPurchasePanel.GetComponent<CreditBundleStore>().isInitialized == false)
        {
            CreditsPurchasePanel.GetComponent<CreditBundleStore>().Initialize();
        }
    }
}
