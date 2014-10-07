using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UnityUIWebserviceErrorPopup : MonoBehaviour {

    public Text ErrorMessageText;

    public GameObject popupWindow;

    private const bool allowCarrierDataNetwork = false;
    private const string pingAddress = "8.8.8.8"; // Google Public DNS server
    private const float waitingTime = 2.0f;

    private Ping ping;
    private float pingStartTime;


    void Start()
    {
        SP.onErrorEvent += OnWebserviceError;

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

    public void Update()
    {
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
        ErrorMessageText.text = "Please connect to the internet to continue";
        popupWindow.SetActive(true);
    }

    private void InternetAvailable()
    {
        Debug.Log("Internet is available! ;)");
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
