using UnityEngine;
using UnityEngine.UI;
using System;

public class UnityUIPremiumBundle : PremiumBundle
{
    public override string Amount
    {
        get { return amount.text; }
        set { if(amount != null) amount.text = value; }
    }

    public override Texture CurrencyIcon
    {
        get { return currencyIcon.texture; }
        set { currencyIcon.texture = value; }
    }

    public override string PremiumCurrencyName
    {
        get { return currenyName.text; }
        set { if (currenyName != null) currenyName.text = value; }
    }

    public override string PremiumBundleName
    {
        get { return BundleName.text; }
        set { BundleName.text = value; }
    }

    public override string Cost
    {
        get { return cost.text; }
        set { if(cost != null)cost.text = costPrefix + value; }
    }

    public override string Description
    {
        get { return ""; }// return description == null ? "" : description.text; }
        set { } //if (description != null) description.text = value; }
    }

    public string ProductID { get; set; }
    public string BundleID { get; set; }

    public Text amount;
    public Text currenyName;
    public Text BundleName; 
    public RawImage currencyIcon;
    public string costPrefix = "$ ";
    public Text cost;
    // public Text description;

    Action<GameObject> purchaseRequestCallback;

    public override void SetIcon(Texture2D texture)
    {
        if (currencyIcon != null)
        {
            currencyIcon.texture = texture;
        }
    }


    public override void SetBundleName(string name)
    {
        if (BundleName != null)
        {
            BundleName.text = name;
        }
    }
}
