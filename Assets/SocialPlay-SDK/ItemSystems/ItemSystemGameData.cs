using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ItemSystemGameData
{
    public static  Guid AppID;
    public static Guid UserID;
    public static int InstanceID;
    public static Guid SessionID;

    public  ItemSystemGameData(string AppID, string UserID, int InstanceID, string SessionID)
    {
        ItemSystemGameData.AppID = new Guid(AppID);
        ItemSystemGameData.UserID = new Guid(UserID);
        ItemSystemGameData.InstanceID = InstanceID;
        ItemSystemGameData.SessionID = new Guid(SessionID);
    }
}
