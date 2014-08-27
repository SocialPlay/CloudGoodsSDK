using UnityEngine;
using System;

public class UICreditBundle : MonoBehaviour
{
    public string Amount
    {
        get { return amount.text; }
        set { amount.text = value; }
    }

    public UITexture CurrencyIcon
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
        set { cost.text = value; }
    }

    public string Description
    {
        get { return description == null ? "" : description.text; }
        set { if (description != null) description.text = value; }
    }

    public string ProductID { get; set; }
    public string BundleID { get; set; }

    public UILabel amount;
    public UILabel currenyName;
    public UITexture currencyIcon;
    public UILabel cost;
    public UILabel description;
    public UIButton PurchaseButton;
    public Action<UICreditBundle> OnPurchaseRequest;

    Action<GameObject> purchaseRequestCallback;

    void Awake()
    {
        EventDelegate.Add(PurchaseButton.onClick, OnPurchase);
    }

    void OnPurchase()
    {
        if (OnPurchaseRequest != null) OnPurchaseRequest(this);
    }

    public void SetIcon(Texture2D texture)
    {
        if (currencyIcon != null)
        {
            currencyIcon.mainTexture = texture;
            TweenAlpha.Begin(currencyIcon.cachedGameObject, 0.3f, 1).from = 0;
        }
    }

}
