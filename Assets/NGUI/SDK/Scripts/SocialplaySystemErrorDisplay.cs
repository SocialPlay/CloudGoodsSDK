using UnityEngine;
using System.Collections;

public class SocialplaySystemErrorDisplay : MonoBehaviour {

    public GameObject ErrorMessagePopup;
    public GameObject ConfirmButton;

    public UILabel ErrorMessage;

    void Awake()
    {
        SP.onErrorEvent += OnWebserviceErrorReceived;
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
