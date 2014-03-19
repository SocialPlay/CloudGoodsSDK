using UnityEngine;
using System.Collections;

public class WebPlatformLogin : MonoBehaviour
{
    void Start()
    {
#if UNITY_WEBPLAYER
        WebPlatformLink.OnRecievedUser += GameAuthentication.OnUserAuthorized;
        webPlatformLink = new WebPlatformLink();
        webPlatformLink.Initiate();
#endif
    }

}
