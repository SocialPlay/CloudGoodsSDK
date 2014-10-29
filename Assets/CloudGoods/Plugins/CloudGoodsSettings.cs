using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CloudGoodsSettings : ScriptableObject
{
    [System.Serializable]
    public class DropPrefab
    {
        public GameObject prefab;
        public List<ItemFilterSystem> itemFilters = new List<ItemFilterSystem>();
    }

    public enum ScreenType
    {
        Settings,
        About,
        _LastDoNotUse,
    }

    static public string VERSION = "1.0";

    static public string mainPath = "Assets/CloudGoods/";

    public string appID;
    public string appSecret;
    public ScreenType screen;
    public string url = "http://webservice.socialplay.com/cloudgoods/cloudgoodsservice.svc/";
    public string bundlesUrl = "https://socialplay.blob.core.windows.net/unityassetbundles/";
    public string androidKey = "";
    public Texture2D defaultTexture;
    public GameObject defaultItemDrop;
    public GameObject defaultUIItem;
    //public List<DropPrefab> dropPrefabs = new List<DropPrefab>();

    static CloudGoodsSettings mInst;

    static public CloudGoodsSettings instance
    {
        get
        {
            if (mInst == null) mInst = (CloudGoodsSettings)Resources.Load("CloudGoodsSettings", typeof(CloudGoodsSettings));
            return mInst;
        }
    }

    //static public List<DropPrefab> DropPrefabs
    //{
    //    get
    //    {
    //        return instance.dropPrefabs;
    //    }
    //}

    static public GameObject DefaultItemDrop
    {
        get
        {
            return instance.defaultItemDrop;
        }
    }

    static public GameObject DefaultUIItem
    {
        get
        {
            return instance.defaultUIItem;
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

    static public Texture2D DefaultTexture
    {
        get
        {
            return instance.defaultTexture;
        }
    }

    static public string BundlesUrl
    {
        get
        {
            return instance.bundlesUrl;
        }
    }

    static public string AndroidKey
    {
        get
        {
            return instance.androidKey;
        }
    }
}
