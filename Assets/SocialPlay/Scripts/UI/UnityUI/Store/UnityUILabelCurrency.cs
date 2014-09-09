using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnityUILabelCurrency : MonoBehaviour {

    public CurrencyType type = CurrencyType.Coins;
    Text mLabel;

    void Awake()
    {
        mLabel = GetComponent<Text>();
        if (type == CurrencyType.Coins) SP.OnFreeCurrency += OnFreeCurrency;
        else if (type == CurrencyType.Credits) SP.OnPaidCurrency += OnPaidCurrency;
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
