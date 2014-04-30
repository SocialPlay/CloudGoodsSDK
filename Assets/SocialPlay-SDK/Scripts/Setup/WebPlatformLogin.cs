using UnityEngine;
using System.Collections;
using CloudGoodsSDK.Models;
using System;

public class WebPlatformLogin : MonoBehaviour
{

    public void LoginWithPlatformUser(Guid AppID, int platformID, string platformUserID, string userName)
    {
        PlatformUser platformUser = new PlatformUser();

        platformUser.appID = AppID;
        platformUser.platformID = platformID;
        platformUser.platformUserID = platformUserID;
        platformUser.userName = userName;

        SocialPlayUserWebServiceGetter userGetter = new SocialPlayUserWebServiceGetter();
        userGetter.GetSocialPlayUser(platformUser, OnReceivedUserInfo);
    }


    void OnReceivedUserInfo(WebserviceCalls.UserInfo userInfo)
    {
        GameAuthentication.OnUserAuthorized(userInfo);
    }
}
