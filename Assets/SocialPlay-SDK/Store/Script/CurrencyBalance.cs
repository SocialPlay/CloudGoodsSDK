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
        GameAuthentication.OnRegisteredUserToSession += GetCurrencyBalance;
    }

    public void GetCurrencyBalance(string userAuth)
    {
        if(freeCurrencyLabel != null)
            WebserviceCalls.webservice.GetFreeCurrencyBalance(ItemSystemGameData.UserID.ToString(), AccessLocation, ItemSystemGameData.AppID.ToString(), OnReceivedFreeCurrency);

        if(paidCurrencyLabel != null)
            WebserviceCalls.webservice.GetPaidCurrencyBalance(ItemSystemGameData.UserID.ToString(), ItemSystemGameData.AppID.ToString(), OnReceievedPaidCurrency);
    }

    void OnReceivedFreeCurrency(string freeCurrencyBalance)
    {
        Debug.Log(freeCurrencyBalance);
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
