using UnityEngine;
using System.Collections;
using System;

public class SPLogout : MonoBehaviour {

    public static event Action SPUserLogout;

    public GameObject logoutButton;

    // Use this for initialization
    void Start()
    {
        if (logoutButton != null)
        {
            UIEventListener.Get(logoutButton).onClick += LogoutUser;
        }
    }

    public void LogoutUser(GameObject logoutButton)
    {
        SP.Logout();

        if (SPUserLogout != null) SPUserLogout();
    }
	
}
