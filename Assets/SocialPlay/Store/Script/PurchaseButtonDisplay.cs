using UnityEngine;
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

    public void SetState(bool state)
    {
        if (state)
        {
            SetActive();
        }
        else
        {
            SetInactive();
        }
    }
}
