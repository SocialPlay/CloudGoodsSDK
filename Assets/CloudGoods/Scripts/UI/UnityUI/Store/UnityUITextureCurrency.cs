using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(RawImage))]
public class UnityUITextureCurrency : MonoBehaviour {

    public CurrencyType type = CurrencyType.Standard;
    RawImage mTexture;

    void Awake()
    {
        mTexture = GetComponent<RawImage>();
        if (type == CurrencyType.Standard)
        {
            CloudGoods.OnStandardCurrencyTexture += OnFreeCurrency;
            mTexture.texture = CloudGoods.standardCurrencyTexture;
        }
        else if (type == CurrencyType.Premium)
        {
            CloudGoods.OnPremiumCurrencyTexture += OnPaidCurrency;
            mTexture.texture = CloudGoods.premiumCurrencyTexture;
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
