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
        CreditsPurchasePanel.SetActiveRecursively(true);

        if (CreditsPurchasePanel.GetComponent<CreditBundleStore>().isInitialized == false)
        {
            CreditsPurchasePanel.GetComponent<CreditBundleStore>().UserID = "342B3080-9077-472A-B2F0-F913BE69F135";
            CreditsPurchasePanel.GetComponent<CreditBundleStore>().Initialize();
        }
    }
}
