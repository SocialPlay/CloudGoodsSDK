using UnityEngine;
using UnityEngine.UI;
using System;

public class UICreditBundle : MonoBehaviour
{
    public string Amount
    {
        get { return amount.text; }
        set { amount.text = value; }
    }

    public RawImage CurrencyIcon
    {
        get { return currencyIcon; }
        set { currencyIcon = value; }
    }

    public string CurrencyName
    {
        get { return currenyName.text; }
        set { currenyName.text = value; }
    }

    public string Cost
    {
        get { return cost.text; }
        set { cost.text = costPrefix + value; }
    }

    public string Description
    {
        get { return ""; }// return description == null ? "" : description.text; }
        set { } //if (description != null) description.text = value; }
    }

    public string ProductID { get; set; }
    public string BundleID { get; set; }

    public Text amount;
    public Text currenyName;
    public RawImage currencyIcon;
    public string costPrefix = "$ ";
    public Text cost;
    // public Text description;
    public Action<UICreditBundle> OnPurchaseRequest;

    Action<GameObject> purchaseRequestCallback;


    void Start()
    {
        if (string.IsNullOrEmpty(SP.PremiumCurrencyName))
        {
            SP.OnPremiumCurrencyName += OnPremiumCurrencyName;
        }
        else
        {
            CurrencyName = SP.PremiumCurrencyName;
        }
    }

    void OnPremiumCurrencyName(string premiumName)
    {
        CurrencyName = premiumName;
    }

    public void MakePurchaseRequest()
    {
        if (OnPurchaseRequest != null) OnPurchaseRequest(this);
    }

    public void SetIcon(Texture2D texture)
    {
        if (currencyIcon != null)
        {
            currencyIcon.texture = texture;
        }
    }

}
