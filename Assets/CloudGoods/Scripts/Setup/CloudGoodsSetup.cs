using UnityEngine;
using System.Collections;
using System;


public class CloudGoodsSetup : MonoBehaviour
{
    public static Action<bool> CloudGoodsInitialized;

    void Start()
    {
       CloudGoods.OnUserAuthorized += OnReceivedSocialPlayUser;

        WebPlatformLink webplatformLink = new WebPlatformLink();
        webplatformLink.Initiate();
    }

    void OnReceivedSocialPlayUser(CloudGoodsUser socialplayMsg)
    {
        if(CloudGoodsInitialized != null)
            CloudGoodsInitialized(true);
    }
}
