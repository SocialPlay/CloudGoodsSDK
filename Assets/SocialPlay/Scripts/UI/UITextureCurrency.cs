using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UITexture))]
public class UITextureCurrency : MonoBehaviour
{
    public CurrencyType type = CurrencyType.Coins;
    UITexture mTexture;

    void Awake()
    {
        mTexture = GetComponent<UITexture>();
        if (type == CurrencyType.Coins)
        {
            SP.OnFreeCurrencyTexture += OnFreeCurrency;
            mTexture.mainTexture = SP.freeCurrencyTexture;            
        }
        else if (type == CurrencyType.Credits)
        {
            SP.OnPaidCurrencyTexture += OnPaidCurrency;
            mTexture.mainTexture = SP.paidCurrencyTexture;
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