using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json.Linq;
using CloudGoods;

public class GameAuthentication : MonoBehaviour
{

    private static GameAuthentication instance = null;

    public static event Action<string> OnUserAuthEvent;

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

    public static void OnUserAuthorized(WebserviceCalls.UserGuid socialplayMsg)
    {

        new ItemSystemGameData(GetAppID(), socialplayMsg.userGuid, 1, Guid.NewGuid().ToString());

        Debug.Log("Logged in as user " + socialplayMsg.userName + " : " + socialplayMsg.userGuid);

        GetGameSession(ItemSystemGameData.UserID, 1, OnRegisteredSession);

    }

    private static void GetGameSession(Guid UserID, int instanceID, Action<Guid> callback)
    {
        WebserviceCalls webserviceCalls = GameObject.FindObjectOfType(typeof(WebserviceCalls)) as WebserviceCalls;
        //todo remove email
        webserviceCalls.RegisterGameSession(UserID, 1, (x) =>
        {
            JToken token = JToken.Parse(x);
            Guid sessionGuid = new Guid(token.ToString());
            callback(sessionGuid);
        });
    }

    static void OnRegisteredSession(Guid sessionID)
    {
        ItemSystemGameData.SessionID = sessionID;

        if (OnUserAuthEvent != null)
            OnUserAuthEvent(ItemSystemGameData.UserID.ToString());
    }
}
