using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json.Linq;

public class WebPlatformLink
{
    public static Action OnGuestModeActivated;
    //public static Action<UserInfo> OnRecievedUser;

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

        try
        {
			CloudGoods.LoginWithPlatformUser((CloudGoodsPlatform)Enum.Parse(typeof(CloudGoodsPlatform), jobject["platformID"].ToString()), jobject["userID"].ToString(), jobject["userName"].ToString());
        }
        catch (Exception)
        {
            throw new Exception("Platform web script has passed in incorrect data");
        }		

        /*Systems.UserGetter.GetSocialPlayUser(userinfo, (user) =>
        {
            if (OnRecievedUser != null)
                OnRecievedUser(user);
        });*/
    }
}
