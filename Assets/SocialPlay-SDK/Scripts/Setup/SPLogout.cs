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
        PlayerPrefs.DeleteKey("SocialPlay_UserGuid");
        PlayerPrefs.DeleteKey("SocialPlay_UserName");
        PlayerPrefs.DeleteKey("SocialPlay_UserEmail");

        new ItemSystemGameData(Guid.Empty.ToString(), Guid.Empty.ToString(), 0, Guid.Empty.ToString(), "");

        if (SPUserLogout != null) SPUserLogout();
    }
	
}
