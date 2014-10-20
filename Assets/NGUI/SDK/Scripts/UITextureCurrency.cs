using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UITexture))]
public class UITextureCurrency : MonoBehaviour
{
    public CurrencyType type = CurrencyType.Standard;
    UITexture mTexture;

    void Awake()
    {
        mTexture = GetComponent<UITexture>();
        if (type == CurrencyType.Standard)
        {
            SP.OnStandardCurrencyTexture += OnFreeCurrency;
            mTexture.mainTexture = SP.standardCurrencyTexture;            
        }
        else if (type == CurrencyType.Premium)
        {
            SP.OnPremiumCurrencyTexture += OnPaidCurrency;
            mTexture.mainTexture = SP.premiumCurrencyTexture;
        }

        if(mTexture.mainTexture != null) TweenAlpha.Begin(mTexture.cachedGameObject, 0.3f, 1).from = 0;
    }

    void OnFreeCurrency(Texture2D currencyTexture)
    {
        mTexture.mainTexture = currencyTexture;
        if (mTexture.mainTexture != null) TweenAlpha.Begin(mTexture.cachedGameObject, 0.3f, 1).from = 0;
    }

    void OnPaidCurrency(Texture2D currencyTexture)
    {
        mTexture.mainTexture = currencyTexture;
        if (mTexture.mainTexture != null) TweenAlpha.Begin(mTexture.cachedGameObject, 0.3f, 1).from = 0;
    }
}