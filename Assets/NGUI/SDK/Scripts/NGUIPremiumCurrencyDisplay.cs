using UnityEngine;
using System.Collections;

public class NGUIPremiumCurrencyDisplay: MonoBehaviour {

    public UILabel PaidCurrencyLabel;
	
	// Update is called once per frame
	void Update () {
        if (PaidCurrencyLabel != null)
            PaidCurrencyLabel.text = CloudGoods.premiumCurrency.ToString();
	}
}
