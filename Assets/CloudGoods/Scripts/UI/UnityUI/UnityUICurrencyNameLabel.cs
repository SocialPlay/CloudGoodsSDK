using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UnityUICurrencyNameLabel : MonoBehaviour
{
    public string prefix;
    public string suffix;
    public CurrencyType type = CurrencyType.Standard;
    Text mLabel;

    void Start()
    {
        mLabel = GetComponent<Text>();

        if (type == CurrencyType.Standard) CloudGoods.OnStandardCurrencyName += OnCurrencyNameChange;
        else if (type == CurrencyType.Premium) CloudGoods.OnPremiumCurrencyName += OnCurrencyNameChange;
    }

    void OnCurrencyNameChange(string currency)
    {
        mLabel.text = string.Format("{0}{1}{2}", prefix, currency, suffix);
    }
}
