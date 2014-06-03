using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using CloudGoodsSDK.Models;
using System.Collections.Generic;

public interface ISocialPlayerUserGetter
{
    void GetSocialPlayUser(PlatformUser socialPlayUser, Action<SP.UserInfo> callback);
}

public class SocialPlayUserWebServiceGetter : ISocialPlayerUserGetter
{
    public void GetSocialPlayUser(PlatformUser socialPlayUser, Action<SP.UserInfo> callback)
    {
        //todo remove email
        SP.GetUserFromWorld(socialPlayUser.platformID, socialPlayUser.platformUserID, socialPlayUser.userName, null, (SP.UserInfo x) =>
        {
            SP.UserInfo userGuid = x;
            callback(userGuid);
        });
    }
}
