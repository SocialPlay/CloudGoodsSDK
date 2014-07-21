using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UITexture))]
public class UITextureCurrency : MonoBehaviour
{
    public CurrencyType type = CurrencyType.Coins;
    UITexture mLabel;

    void Awake()
    {
        mLabel = GetComponent<UITexture>();
        if (type == CurrencyType.Coins) SP.OnFreeCurrencyTexture += OnFreeCurrency;
        else if (type == CurrencyType.Credits) SP.OnPaidCurrencyTexture += OnPaidCurrency;
    }

    void OnFreeCurrency(Texture2D currencyTexture)
    {
        mLabel.mainTexture = currencyTexture;
    }

    void OnPaidCurrency(Texture2D currencyTexture)
    {
        mLabel.mainTexture = currencyTexture;
    }
}