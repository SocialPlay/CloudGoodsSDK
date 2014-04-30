using UnityEngine;
using System.Collections;
using System;
using CloudGoods;
using SocialPlay.ItemSystems;

public class CloudGoodsSetup : MonoBehaviour
{
    public string AccessToken;

    public static Action<bool> CloudGoodsInitialized;

    void Start()
    {
        if (string.IsNullOrEmpty(AccessToken))
            throw new Exception("You must supply an AppId to communicate with Cloud Goods");


        Systems.AppId = new Guid(AccessToken);
        WebPlatformLink.OnRecievedUser += OnReceivedSocialPlayUser;

        WebPlatformLink webplatformLink = new WebPlatformLink();
        webplatformLink.Initiate();
    }

    void OnReceivedSocialPlayUser(WebserviceCalls.UserInfo socialplayMsg)
    {
        new ItemSystemGameData(AccessToken, socialplayMsg.userGuid, 1, Guid.NewGuid().ToString(), socialplayMsg.userName);

        if(CloudGoodsInitialized != null)
            CloudGoodsInitialized(true);
    }
}
