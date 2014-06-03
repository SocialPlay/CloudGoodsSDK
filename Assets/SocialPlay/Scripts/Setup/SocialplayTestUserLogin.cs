using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using CloudGoodsSDK.Models;
using CloudGoods;


public class SocialplayTestUserLogin : MonoBehaviour
{
    WebPlatformLink webPlatformLink;

    // Use this for initialization
    void Start()
    {
        Debug.Log("In Editor, Logging in as Test User with access token: " + SP.AppID);
        var socialPlayUserObj = new PlatformUser();
        socialPlayUserObj.appID = SP.GetAppID();
        socialPlayUserObj.platformID = 1;
        socialPlayUserObj.platformUserID = "2";
        socialPlayUserObj.userName = "Editor Test User";
        Systems.UserGetter.GetSocialPlayUser(socialPlayUserObj, SP.OnUserAuthorized);

    }
}
