using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CloudGoodsLogout : MonoBehaviour {

    public GameObject SPLoginSystem;

    public static event Action SPUserLogout;

    public void LogoutUser()
    {
        CloudGoods.Logout();
        if (SPUserLogout != null) SPUserLogout();
    }
	
}
