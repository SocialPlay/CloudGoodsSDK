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

    public UITexture currencyIcon;

    public UITexture CurrencyIcon
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

	public UILabel description;

	public string Description
	{
		get { return description == null ? "" : description.text; }
		set { if(description != null) description.text = value; }
	}

    public string ProductID { get; set; }

    public string BundleID { get; set; }

    public UIButton PurchaseButton;
	public Action< NGUIBundleItem> OnPurchaseRequest;

	void Awake()
	{
		EventDelegate.Add(PurchaseButton.onClick, OnClick);
	}

	void OnClick()
    {
		Debug.Log ("Buy button clicked");
        if(OnPurchaseRequest != null) OnPurchaseRequest(this);
    }

    public void SetCredtiBundleIcon(Texture2D texture)
    {
        if (currencyIcon != null)
            currencyIcon.mainTexture = texture;
    }

}
