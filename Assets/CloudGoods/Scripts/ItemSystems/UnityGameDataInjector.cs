using System;
using UnityEngine;


public class UnityGameDataInjector : MonoBehaviour
{
    public string AppID;
    public string UserID;
    public int InstanceID;
    public string SessionID;

    public void InjectGameData()
    {
        string msg = string.Empty;

        try { new Guid(AppID); }
        catch { msg += "\nCan not parse App ID!"; }


        try { new Guid(UserID); }
        catch { msg += "\nCan not parse User ID!"; }


        SessionID = Guid.NewGuid().ToString();

        if (msg == string.Empty)
        {
            //new ItemSystemGameData(AppID, UserID, InstanceID, SessionID, "User name", "");
        }
        else
        {
            Debug.LogWarning(msg);
        }
    }
}

