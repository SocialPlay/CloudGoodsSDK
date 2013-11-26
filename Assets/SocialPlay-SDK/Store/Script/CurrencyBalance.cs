using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;

public class CurrencyBalance : MonoBehaviour {

    public UILabel paidCurrencyLabel;
    public UILabel freeCurrencyLabel;

    public static int freeCurrency = 0;
    public static int paidCurrency = 0;

	// Use this for initialization
	void Start () {

        GameAuthentication.OnUserAuthEvent += GetCurrencyBalance;
	}

    public void GetCurrencyBalance(string userAuth)
    {
         WebserviceCalls.webservice.GetFreeCurrencyBalance(ItemSystemGameData.UserID.ToString(), ItemSystemGameData.AppID.ToString(), OnReceivedFreeCurrency);
         WebserviceCalls.webservice.GetPaidCurrencyBalance(ItemSystemGameData.UserID.ToString(), ItemSystemGameData.AppID.ToString(), OnReceievedPaidCurrency);
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
        paidCurrencyLabel.text = paidToken.ToString();
        paidCurrency = int.Parse(paidToken.ToString());
    }
	
}
