using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using CloudGoodsSDK.Models;
using System.Collections.Generic;

public interface ISocialPlayerUserGetter
{
    void GetSocialPlayUser(PlatformUser socialPlayUser, Action<WebserviceCalls.UserGuid> callback);
}

public class SocialPlayUserWebServiceGetter : ISocialPlayerUserGetter
{
    public void GetSocialPlayUser(PlatformUser socialPlayUser, Action<WebserviceCalls.UserGuid> callback)
    {
        WebserviceCalls webserviceCalls = GameObject.FindObjectOfType(typeof(WebserviceCalls)) as WebserviceCalls;
        //todo remove email
        webserviceCalls.GetUserFromWorld(socialPlayUser.appID, socialPlayUser.platformID, socialPlayUser.platformUserID, socialPlayUser.userName, null, (x) =>
        {
            Debug.Log(x);
            JToken token = JToken.Parse(x);
            WebserviceCalls.UserGuid userGuid = JsonConvert.DeserializeObject<WebserviceCalls.UserGuid>(token.ToString());

            callback(userGuid);
        });
    }
}
