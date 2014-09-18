using UnityEngine;
using System;

public class NGUIPremiumBundle : PremiumBundle
{
    public override string Amount
    {
        get { return amount.text; }
        set { amount.text = value; }
    }

    public override Texture CurrencyIcon
    {
        get { return currencyIcon.mainTexture; }
        set { currencyIcon.mainTexture = value; }
    }

    public override string CurrencyName
    {
        get { return currenyName.text; }
        set { currenyName.text = value; }
    }

    public override string Cost
    {
        get { return cost.text; }
        set { cost.text = value; }
    }

    public override string Description
    {
        get { return description == null ? "" : description.text; }
        set { if (description != null) description.text = value; }
    }


    public UILabel amount;
    public UILabel currenyName;
    public UITexture currencyIcon;
    public UILabel cost;
    public UILabel description;
    public UIButton PurchaseButton;

    void Awake()
    {
        EventDelegate.Add(PurchaseButton.onClick, OnPurchase);
    }

    void OnPurchase()
    {
        if (OnPurchaseRequest != null) OnPurchaseRequest(this);
    }

    public override void SetIcon(Texture2D texture)
    {
        if (currencyIcon != null)
        {
            currencyIcon.mainTexture = texture;
            TweenAlpha.Begin(currencyIcon.cachedGameObject, 0.3f, 1).from = 0;
        }
    }

}
