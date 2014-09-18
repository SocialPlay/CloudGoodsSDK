using UnityEngine;
using UnityEngine.UI;
using System;

public class UnityUIPremiumBundle : PremiumBundle
{
    public override string Amount
    {
        get { return amount.text; }
        set { amount.text = value; }
    }

    public override Texture CurrencyIcon
    {
        get { return currencyIcon.texture; }
        set { currencyIcon.texture = value; }
    }

    public override string CurrencyName
    {
        get { return currenyName.text; }
        set { currenyName.text = value; }
    }

    public override string Cost
    {
        get { return cost.text; }
        set { cost.text = costPrefix + value; }
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

}
