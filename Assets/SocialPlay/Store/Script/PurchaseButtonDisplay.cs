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
        if (!string.IsNullOrEmpty(InsufficientFundsTextOverride) && InsufficientFundsTextOverride != InsufficientFundsLabel.text)
        {
            InsufficientFundsLabel.text = InsufficientFundsTextOverride;
        }
        ActiveButton.gameObject.SetActive(true);
        InsufficientFundsLabel.gameObject.SetActive(false);
    }

    public void SetNotApplicable()
    {
        ActiveButton.gameObject.SetActive(false);
        InsufficientFundsLabel.gameObject.SetActive(false);
    }

    public void SetState(int itemCost)
    {
        if (itemCost < 0)
        {
            SetNotApplicable();
        }
        else if (currencyType == CurrencyType.Credits)
        {
            Debug.Log("paid");
            if (itemCost > SP.paidCurrency)
                SetInactive();
            else
                SetActive();
        }
        else if (currencyType == CurrencyType.Coins) 
        {
            if (itemCost > SP.freeCurrency)
                SetInactive();
            else
                SetActive();
        }
    }
}
