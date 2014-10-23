using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnityUIWebserviceErrorPopup : MonoBehaviour {

    public Text ErrorMessageText;

    public GameObject popupWindow;

    private const bool allowCarrierDataNetwork = false;
    private const string pingAddress = "8.8.8.8"; // Google Public DNS server
    private const float waitingTime = 2.0f;

    private Ping ping;
    private float pingStartTime;
    float pingNextStartTime = 0.0f;
    private float pingNextMaxTime = 2.0f;

    bool hasShownPopup = false;


    void Start()
    {
        CloudGoods.OnRegisteredUserToSession += OnUserRegister;
        CloudGoods.onErrorEvent += OnWebserviceError;

        bool internetPossiblyAvailable;
        switch (Application.internetReachability)
        {
            case NetworkReachability.ReachableViaLocalAreaNetwork:
                internetPossiblyAvailable = true;
                break;
            case NetworkReachability.ReachableViaCarrierDataNetwork:
                internetPossiblyAvailable = allowCarrierDataNetwork;
                break;
            default:
                internetPossiblyAvailable = false;
                break;
        }
        if (!internetPossiblyAvailable)
        {
            InternetIsNotAvailable();
            return;
        }
        ping = new Ping(pingAddress);
        pingStartTime = Time.time;
    }

    void OnUserRegister(string data)
    {
        //SP.GetOwnerItemById(0, 48254, OnReceivedOwnerItem);

        //SP.ConsumePremiumCurrency(1, OnConsumeCurrency);

        CloudGoods.ConsumeItemById(111544, 1, 0, OnConsumeItemByID);
    }

    void OnConsumeItemByID(ConsumeResponse consumeItemResponse)
    {
        Debug.Log("Consume item Response: " + consumeItemResponse.Result);

    }

    void OnConsumeCurrency(ConsumeResponse currencyresult)
    {
        Debug.Log(currencyresult);
    }

    void OnReceivedOwnerItem(List<ItemData> item)
    {
        if (item.Count > 0)
            Debug.Log("Item found: " + item[0].itemName + " with amount of : " + item[0].stackSize);
        else
            Debug.Log("NO items found");
    }

    public void Update()
    {
        pingNextStartTime += Time.deltaTime;

        if (ping == null)
        {
            if (pingNextStartTime >= pingNextMaxTime)
            {
                ping = new Ping(pingAddress);
                pingStartTime = Time.time;
                pingNextStartTime = 0.0f;
            }
        }

        if (ping != null)
        {
            bool stopCheck = true;
            if (ping.isDone)
                InternetAvailable();
            else if (Time.time - pingStartTime < waitingTime)
                stopCheck = false;
            else
                InternetIsNotAvailable();
            if (stopCheck)
                ping = null;
        }
    }

    private void InternetIsNotAvailable()
    {
        if (!hasShownPopup)
        {
            ErrorMessageText.text = "Please connect to the internet to continue";
            popupWindow.SetActive(true);
            hasShownPopup = true;
        }
    }

    private void InternetAvailable()
    {
        hasShownPopup = false;
    }


    void OnWebserviceError(string error)
    {
        ErrorMessageText.text = error;
        popupWindow.SetActive(true);
    }

    public void ClosePopupWindow()
    {
        popupWindow.SetActive(false);
    }

}
