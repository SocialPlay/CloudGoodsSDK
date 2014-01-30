using UnityEngine;
using System.Collections;
using System;

public class SPLogout : MonoBehaviour {

    public Action<bool> SPUserLogout;

    public GameObject logoutButton;

    // Use this for initialization
    void Start()
    {
        UIEventListener.Get(logoutButton).onClick += LogoutUser;
    }

    void LogoutUser(GameObject logoutButton)
    {
        SPUserLogout(true);
    }
	
}
