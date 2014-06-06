using UnityEngine;
using System.Collections;

public class NGUICreditDisplay: MonoBehaviour {

    public UILabel creditLabel;
	
	// Update is called once per frame
	void Update () {
        if (creditLabel != null)
            creditLabel.text = SP.paidCurrency.ToString();
	}
}
