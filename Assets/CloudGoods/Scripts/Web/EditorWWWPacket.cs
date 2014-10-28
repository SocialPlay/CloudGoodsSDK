using UnityEngine;
using System.Collections;
using System;


/// <summary>
/// Used to call a simple WWW that returns a string to a callBack 
/// Self-Managed.
/// Runs in editor Mode.
/// </summary>
 

public class EditorWWWPacket : MonoBehaviour
{

    public string url;
    public Action<string> callBack;
    public bool isReady = false;

    public static void Creat(string URL, Action<string> callBack)
    {
        GameObject obj = new GameObject("Editor WWW Packet");
        EditorWWWPacket packet = obj.AddComponent<EditorWWWPacket>();
        packet.url = URL;
        packet.callBack = callBack;
        packet.isReady = true;        
    }

    void LateUpdate()
    {
        if (isReady)
        {
            StartWWW(url, callBack);
            isReady = false;
        }
    }


    void StartWWW(string URL, Action<string> callBack)
    {
        url = URL;
        StartCoroutine(StartWWWCoroutine(URL, callBack));
    }

    IEnumerator StartWWWCoroutine(string URL, Action<string> callback)
    {
        WWW www = new WWW(URL);

        yield return www;

        if (www.error != null)
        {
            Debug.LogError(string.Format("WWW error laoding {1}: {0}", www.error, URL));
            DestroyImmediate(this.gameObject);
        }
        else
        {
            Responce(www.text, callback);
        }

    }

    public void Responce(string data, Action<string> callback)
    {
        callback(data);
        DestroyImmediate(this.gameObject);
    }
}
