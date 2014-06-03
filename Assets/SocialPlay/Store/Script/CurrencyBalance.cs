using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;

public class CurrencyBalance : MonoBehaviour
{

    public int AccessLocation = 0;
    public UILabel paidCurrencyLabel;
    public UILabel freeCurrencyLabel;

    public static int freeCurrency = 0;
    public static int paidCurrency = 0;

    // Use this for initialization
    void Start()
    {
        SP.OnRegisteredUserToSession += GetCurrencyBalance;
    }

    public void GetCurrencyBalance(string userAuth)
    {
        if(freeCurrencyLabel != null)
            SP.GetFreeCurrencyBalance(ItemSystemGameData.UserID.ToString(), AccessLocation, OnReceivedFreeCurrency);

        if(paidCurrencyLabel != null)
            SP.GetPaidCurrencyBalance(ItemSystemGameData.UserID.ToString(), OnReceievedPaidCurrency);
    }

    void OnReceivedFreeCurrency(string freeCurrencyBalance)
    {
        JToken freeToken = JToken.Parse(freeCurrencyBalance);
        freeCurrencyLabel.text = freeToken.ToString();
        freeCurrency = int.Parse(freeToken.ToString());
    }

    void OnReceievedPaidCurrency(string paidCurrencyBalance)
    {
        JToken paidToken = JToken.Parse(paidCurrencyBalance);
        SetItemPaidCurrency(paidToken.ToString());
    }

    public void SetItemPaidCurrency(string currency)
    {
        paidCurrencyLabel.text = currency;
        paidCurrencyLabel.UpdateNGUIText();
        paidCurrency = int.Parse(currency);
    }

}
