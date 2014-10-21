using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UILabel))]
public class UILabelCurrency : MonoBehaviour 
{
	public CurrencyType type = CurrencyType.Standard;
	UILabel mLabel;

	void Awake () 
	{
		mLabel = GetComponent<UILabel>();
		if (type == CurrencyType.Standard) SP.OnStandardCurrency += OnFreeCurrency;
		else if(type == CurrencyType.Premium) SP.OnPremiumCurrency += OnPaidCurrency;
	}

	void OnFreeCurrency(int currency)
	{
		mLabel.text = currency.ToString();
	}

	void OnPaidCurrency(int currency)
	{
		mLabel.text = currency.ToString();
	}
}
