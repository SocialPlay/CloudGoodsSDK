﻿using UnityEngine;
using System.Collections;

public class NGUIPaidCurrencyDisplay: MonoBehaviour {

    public UILabel PaidCurrencyLabel;
	
	// Update is called once per frame
	void Update () {
        if (PaidCurrencyLabel != null)
            PaidCurrencyLabel.text = SP.paidCurrency.ToString();
	}
}
