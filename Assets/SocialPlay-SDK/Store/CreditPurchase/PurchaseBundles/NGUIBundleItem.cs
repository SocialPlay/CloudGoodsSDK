using UnityEngine;
using System;

public class NGUIBundleItem : MonoBehaviour
{
    public UILabel amount;
    
    Action<GameObject> purchaseRequestCallback;

    public string Amount
    {
        get { return amount.text; }
        set { amount.text = value; }
    }

    public UILabel currenyName;

    public string CurrencyName
    {
        get { return currenyName.text; }
        set { currenyName.text = value; }
    }

    public UISprite currencyIcon;

    public UISprite CurrencyIcon
    {
        get { return currencyIcon; }
        set { currencyIcon = value; }
    }

    public UILabel cost;

    public string Cost
    {
        get { return cost.text; }
        set { cost.text = value; }
    }

    public string Id { get; set; }

    public UIButton PurchaseButton;

    Action<GameObject> purchaseButtonClicked;

    public Action<GameObject> PurhcaseButtonClicked
    {
        set { UIEventListener.Get(PurchaseButton.gameObject).onClick += obj => value(obj); }
    }

}
