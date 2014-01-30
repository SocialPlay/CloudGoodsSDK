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
        PlayerPrefs.DeleteKey("SocialPlay_UserGuid");
        PlayerPrefs.DeleteKey("SocialPlay_UserName");
        PlayerPrefs.DeleteKey("SocialPlay_UserEmail");

        SPUserLogout(true);
    }
	
}
