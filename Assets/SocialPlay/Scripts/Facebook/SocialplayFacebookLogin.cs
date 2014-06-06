﻿using UnityEngine;
using System.Collections;

public class SocialplayFacebookLogin : MonoBehaviour {

    public static bool isFBInitialized = false;

    void Awake()
    {
        FB.Init(SetInit, OnHideUnity);  
    }

    public void PromptFacebookUserLogin()
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("Already logged in");
        }

        FB.Login("email,publish_actions", FBLoginCallback); 
    }

    void FBLoginCallback(FBResult FBResult)
    {
        OnLoggedIn();
    }

    void OnLoggedIn()
    {
        SP.LoginWithPlatformUser(SocialPlayPlatform.Facebook, FB.UserId, "new user");
    }       

    private void SetInit()
    {
        isFBInitialized = true; // "enabled" is a property inherited from MonoBehaviour                  
        if (FB.IsLoggedIn)
        {
            OnLoggedIn();
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // pause the game - we will need to hide                                             
            Time.timeScale = 0;
        }
        else
        {
            // start the game back up - we're getting focus again                                
            Time.timeScale = 1;
        }
    }
}
