using UnityEngine;
using System.Collections;
using System;
using CloudGoods;


public class CloudGoodsSetup : MonoBehaviour
{
    public static Action<bool> CloudGoodsInitialized;

    void Start()
    {
        WebPlatformLink.OnRecievedUser += OnReceivedSocialPlayUser;

        WebPlatformLink webplatformLink = new WebPlatformLink();
        webplatformLink.Initiate();
    }

    void OnReceivedSocialPlayUser(SP.UserInfo socialplayMsg)
    {
        new ItemSystemGameData(SP.AppID, socialplayMsg.userGuid, 1, Guid.NewGuid().ToString(), socialplayMsg.userName, socialplayMsg.userEmail);

        if(CloudGoodsInitialized != null)
            CloudGoodsInitialized(true);
    }
}
