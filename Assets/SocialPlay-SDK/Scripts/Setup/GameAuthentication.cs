using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json.Linq;
using CloudGoods;

public class GameAuthentication : MonoBehaviour
{

    private static GameAuthentication instance = null;

    public static event Action<string> OnRegisteredUserToSession;

    public static event Action<WebserviceCalls.UserInfo> OnUserAuthorizedEvent;

    public string AppID;

    public static string GetAppID()
    {
        return instance.AppID;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Systems.AppId = new Guid(AppID);
    }

    public static void OnUserAuthorized(WebserviceCalls.UserInfo socialplayMsg)
    {
        new ItemSystemGameData(GetAppID(), socialplayMsg.userGuid, -1, Guid.NewGuid().ToString(), socialplayMsg.userName, socialplayMsg.userEmail);

        OnUserAuthorizedEvent(socialplayMsg);

        GetGameSession(ItemSystemGameData.UserID, GetAppID(), 1, OnRegisteredSession);

    }

    private static void GetGameSession(Guid UserID, string AppID, int instanceID, Action<Guid> callback)
    {
        WebserviceCalls webserviceCalls = GameObject.FindObjectOfType(typeof(WebserviceCalls)) as WebserviceCalls;
        //todo remove email
        webserviceCalls.RegisterGameSession(UserID, AppID, instanceID, (x) =>
        {
            JToken token = JToken.Parse(x);
            Guid sessionGuid = new Guid(token.ToString());
            callback(sessionGuid);
        });
    }

    static void OnRegisteredSession(Guid sessionID)
    {
        ItemSystemGameData.SessionID = sessionID;

        if (OnRegisteredUserToSession != null)
            OnRegisteredUserToSession(ItemSystemGameData.UserID.ToString());
    }
}
