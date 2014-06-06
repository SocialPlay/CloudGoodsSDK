using UnityEngine;
using System.Collections;
using System;


public class CloudGoodsSetup : MonoBehaviour
{
    public static Action<bool> CloudGoodsInitialized;

    void Start()
    {
       SP.OnUserAuthorized += OnReceivedSocialPlayUser;

        WebPlatformLink webplatformLink = new WebPlatformLink();
        webplatformLink.Initiate();
    }

    void OnReceivedSocialPlayUser(SocialPlayUser socialplayMsg)
    {
        if(CloudGoodsInitialized != null)
            CloudGoodsInitialized(true);
    }
}
