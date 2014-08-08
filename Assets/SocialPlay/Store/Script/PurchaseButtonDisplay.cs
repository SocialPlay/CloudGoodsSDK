﻿using UnityEngine;
using System.Collections;

public class PurchaseButtonDisplay : MonoBehaviour
{
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
        if (!string.IsNullOrEmpty(InsufficientFundsTextOverride) && InsufficientFundsTextOverride != InsufficientFundsLabel.text)
        {
            InsufficientFundsLabel.text = InsufficientFundsTextOverride;
        }
        ActiveButton.gameObject.SetActive(false);
        InsufficientFundsLabel.text = "N/A";
        InsufficientFundsLabel.gameObject.SetActive(true);
    }

    public void SetState(int itemCost)
    {
        if (itemCost < 0)
        {
            SetNotApplicable();
        }
        else if (itemCost <= SP.freeCurrency)
        {
            SetActive();
        }
        else if (itemCost > SP.freeCurrency) 
        {
            SetInactive();
        }
    }
}
