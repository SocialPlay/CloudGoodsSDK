using UnityEngine;
using System.Collections;

[System.Serializable]
public class SocialPlaySettings : ScriptableObject
{
    public enum ScreenType
    {
        Settings,
        About,
        _LastDoNotUse,
    }

    static public string VERSION = "1.0";

    static public string mainPath = "Assets/SocialPlay/";

    public string appID;
    public string appSecret;
    public ScreenType screen;
    public string url = "https://SocialPlayWebService.azurewebsites.net/cloudgoods/cloudgoodsservice.svc/";
    public string bundlesUrl = "https://socialplay.blob.core.windows.net/unityassetbundles/";

    static SocialPlaySettings mInst;

    static public SocialPlaySettings instance
    {
        get
        {
            if (mInst == null) mInst = (SocialPlaySettings)Resources.Load("SocialPlaySettings", typeof(SocialPlaySettings));
            return mInst;
        }
    }

    static public string AppSecret
    {
        get
        {
            return instance.appSecret;
        }
    }

    static public string AppID
    {
        get
        {
            return instance.appID;
        }
    }

    static public string Url
    {
        get
        {
            return instance.url;
        }
    }

    static public string BundlesUrl
    {
        get
        {
            return instance.bundlesUrl;
        }
    }
}
