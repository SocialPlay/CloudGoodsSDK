using UnityEngine;
using System.Collections;

public class PurchaseButtonDisplay : MonoBehaviour
{
    public CurrencyType currencyType;
    public UIButton ActiveButton;
    public UILabel InsufficientFundsLabel;
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
        else if (currencyType == CurrencyType.Coins)
        {
            if (itemCost <= SP.standardCurrency)
                SetActive();
            else
                SetInactive();
        }
        else if (currencyType == CurrencyType.Credits) 
        {
            if (itemCost <= SP.premiumCurrency)
                SetActive();
            else
                SetInactive();
        }
    }
}
