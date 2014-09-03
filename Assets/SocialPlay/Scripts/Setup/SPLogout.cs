using UnityEngine;
using System.Collections;
using System;

public class SPLogout : MonoBehaviour {

    public GameObject SPLoginSystem;

    public static event Action SPUserLogout;

    public void LogoutUser()
    {
        SP.Logout();

        if (SPUserLogout != null) SPUserLogout();
    }
	
}
