using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CloudGoodsLogout : MonoBehaviour {

    public GameObject SPLoginSystem;

    void Awake()
    {
        CloudGoods.OnRegisteredUserToSession += OnUserLogin;
    }

    void OnUserLogin(string userGuid)
    {
        GetComponent<Button>().interactable = true;
    }

    public void LogoutUser()
    {
        Debug.Log("Logout user");
        CloudGoods.Logout();
        GetComponent<Button>().interactable = false;      
    }
	
}
