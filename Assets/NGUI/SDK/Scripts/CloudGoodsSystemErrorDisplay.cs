using UnityEngine;
using System.Collections;

public class CloudGoodsSystemErrorDisplay : MonoBehaviour {

    public GameObject ErrorMessagePopup;
    public GameObject ConfirmButton;

    public UILabel ErrorMessage;

    void Awake()
    {
        CloudGoods.onErrorEvent += OnWebserviceErrorReceived;
    }

    void OnWebserviceErrorReceived(string errorMsg)
    {
        ErrorMessagePopup.SetActive(true);
        ErrorMessage.text = "Socialplay System Error: \n" + errorMsg;
    }

    public void CloseErrorPopup()
    {
        ErrorMessagePopup.SetActive(false);
    }
}
