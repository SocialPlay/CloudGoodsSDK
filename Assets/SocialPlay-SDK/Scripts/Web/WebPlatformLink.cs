using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json.Linq;
using SocialPlay.ItemSystems;
using CloudGoodsSDK.Models;
using CloudGoods;

public class WebPlatformLink
{
    public static Action OnGuestModeActivated;
    public static Action<WebserviceCalls.UserInfo> OnRecievedUser;

    public void Initiate()
    {
        CallBackBrowserHook.CreateExternalCall(LinkCallback, "LinkCallback", "Initiate");
    }

    public void LinkCallback(string platformUserInfo)
    {
        var jobject = JObject.Parse(platformUserInfo);

        var isGuest = (bool)jobject["isGuest"];

        if (isGuest)
        {
            if (OnGuestModeActivated != null)
                OnGuestModeActivated();

            return;
        }

        var userinfo = new PlatformUser();

        try
        {
            userinfo.platformUserID = jobject["userID"].ToString();
            userinfo.userName = jobject["userName"].ToString();
            userinfo.platformID = int.Parse(jobject["platformID"].ToString());
            userinfo.appID = Systems.AppId;
        }
        catch (Exception)
        {
            throw new Exception("Platform web script has passed in incorrect data");
        }

        Systems.UserGetter.GetSocialPlayUser(userinfo, (user) =>
        {
            if (OnRecievedUser != null)
                OnRecievedUser(user);
        });
    }
}
