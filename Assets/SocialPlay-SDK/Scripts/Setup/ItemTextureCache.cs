using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ItemTextureCache : MonoBehaviour {

    public static ItemTextureCache instance;

    void Start()
    {
        instance = this;
    }

    public Texture2D defaultTexture;

    public Dictionary<string, Texture2D> ItemTextures = new Dictionary<string,Texture2D>() ;

    public void GetItemTexture(string URL, Action<ImageStatus, Texture2D> callback)
    {
        try
        {
            if (ItemTextures.ContainsKey(URL))
            {
                callback(ImageStatus.Cache, ItemTextures[URL]);
            }
            else
                GetItemTextureFromWeb(URL, callback);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            callback(ImageStatus.Error, null);
        }
    }

    private void GetItemTextureFromWeb(string URL, Action<ImageStatus, Texture2D> callback)
    {
        WWW www = new WWW(URL);

        StartCoroutine(OnReceivedItemTexture(www, callback, URL));
    }

    IEnumerator OnReceivedItemTexture(WWW www, Action<ImageStatus, Texture2D> callback, string imageURL)
    {
        yield return www;

        if(www.error == null)
        {
            if (ItemTextures.ContainsKey(imageURL))
            {
                callback(ImageStatus.Cache, ItemTextures[imageURL]);
            }
            else
            {
                ItemTextures.Add(imageURL, www.texture);
                callback(ImageStatus.Web, www.texture);
            }
        }
        else 
        {
            if (defaultTexture != null)
                callback(ImageStatus.Cache, defaultTexture);
            else
                callback(ImageStatus.Error, null);
        }
    }

    public enum ImageStatus
    {
        Web,
        Cache,
        Error
    }
}
