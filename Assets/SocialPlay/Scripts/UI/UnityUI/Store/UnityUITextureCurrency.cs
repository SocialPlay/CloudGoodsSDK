using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnityUITextureCurrency : MonoBehaviour {

    public CurrencyType type = CurrencyType.Coins;
    RawImage mTexture;

    void Awake()
    {
        mTexture = GetComponent<RawImage>();
        if (type == CurrencyType.Coins)
        {
            SP.OnFreeCurrencyTexture += OnFreeCurrency;
            mTexture.texture = SP.standardCurrencyTexture;
        }
        else if (type == CurrencyType.Credits)
        {
            SP.OnPaidCurrencyTexture += OnPaidCurrency;
            mTexture.texture = SP.premiumCurrencyTexture;
        }
    }

    void OnFreeCurrency(Texture2D currencyTexture)
    {
        mTexture.texture = currencyTexture;
    }

    void OnPaidCurrency(Texture2D currencyTexture)
    {
        mTexture.texture = currencyTexture;
    }
}
