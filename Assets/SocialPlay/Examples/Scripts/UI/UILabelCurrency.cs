using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;

[RequireComponent(typeof(UILabel))]
public class UILabelCurrency : MonoBehaviour
{
    public enum CurrencyType
    {
        Paid,
        Free
    }

    public CurrencyType type;

    UILabel mLabel;

    void Awake()
    {
        mLabel = GetComponent<UILabel>();
    }

    void OnEnable()
    {
        mLabel.text = (type == CurrencyType.Free ? CurrencyBalance.freeCurrency : CurrencyBalance.paidCurrency).ToString();
    }

    void OnFreeCurrency(int value)
    {
        if (type == CurrencyType.Free) mLabel.text = value.ToString();
    }

    void OnPaidCurrency(int value)
    {
        if (type == CurrencyType.Paid) mLabel.text = value.ToString();
    }

}
