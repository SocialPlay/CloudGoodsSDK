using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UILabel))]
public class UILabelCurrency : MonoBehaviour 
{
	public CurrencyType type = CurrencyType.Coins;
	UILabel mLabel;

	void Awake () 
	{
		mLabel = GetComponent<UILabel>();
		if (type == CurrencyType.Coins) SP.OnFreeCurrency += OnFreeCurrency;
		else if(type == CurrencyType.Credits) SP.OnPaidCurrency += OnPaidCurrency;
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
