using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnityUIPurchaseButtonDisplay : MonoBehaviour {

    public CurrencyType currencyType;
    public Button ActiveButton;
    public Text InsufficientFundsLabel;
    public Text CurrencyText;
    public string InsufficientFundsTextOverride = "";


    public void SetInactive()
    {
        ActiveButton.transform.parent.gameObject.SetActive(true);
        if (!string.IsNullOrEmpty(InsufficientFundsTextOverride) && InsufficientFundsTextOverride != InsufficientFundsLabel.text)
        {
            InsufficientFundsLabel.text = InsufficientFundsTextOverride;
        }
        ActiveButton.gameObject.SetActive(false);
        InsufficientFundsLabel.text = "Insufficent Funds";
        InsufficientFundsLabel.gameObject.SetActive(true);
        CurrencyText.gameObject.SetActive(false);
    }

    public void SetActive()
    {
        ActiveButton.transform.parent.gameObject.SetActive(true);
        if (!string.IsNullOrEmpty(InsufficientFundsTextOverride) && InsufficientFundsTextOverride != InsufficientFundsLabel.text)
        {
            InsufficientFundsLabel.text = InsufficientFundsTextOverride;
        }
        ActiveButton.gameObject.SetActive(true);
        InsufficientFundsLabel.gameObject.SetActive(false);
        CurrencyText.gameObject.SetActive(true);
    }

    public void SetNotApplicable()
    {
        ActiveButton.transform.parent.gameObject.SetActive(false);
    }

    public void SetState(int itemCost)
    {
        if (itemCost < 0)
        {
            SetNotApplicable();
        }
        else if (currencyType == CurrencyType.Standard)
        {
            if (itemCost <= CloudGoods.standardCurrency)
                SetActive();
            else
                SetInactive();
        }
        else if (currencyType == CurrencyType.Premium)
        {
            if (itemCost <= CloudGoods.premiumCurrency)
                SetActive();
            else
                SetInactive();
        }
    }
}
