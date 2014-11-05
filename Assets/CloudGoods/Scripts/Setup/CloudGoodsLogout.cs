using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CloudGoodsLogout : MonoBehaviour {

    public GameObject SPLoginSystem;

    public static event Action CloudGoodsUserLogout;

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
        if (CloudGoodsLogout.CloudGoodsUserLogout != null)
        {
            Debug.Log("logout hooked on to, callign logout event");
            CloudGoodsLogout.CloudGoodsUserLogout();
        }
    }
	
}
