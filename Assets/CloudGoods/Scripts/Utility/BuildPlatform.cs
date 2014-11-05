using UnityEngine;
using System.Collections;
using System;

public class BuildPlatform : MonoBehaviour
{

    public static event Action<BuildPlatformType> OnBuildPlatformFound;

    public BuildPlatformType SelectedType;

    private static bool MadeRequestForDomain = false;
    private static string domainURL = string.Empty;
    private static BuildPlatformType selected;

    public enum BuildPlatformType
    {
        Automatic = 0,
        Facebook = 1,
        Kongergate = 2,
        Android = 3,
        IOS = 4,
        CloudGoodsStandAlone = 6,
        Unknown = 7
    }

    private static BuildPlatform instance;

    void Awake()
    {
        instance = this;
    }

    public static BuildPlatformType GetPlatform()
    {
        if (instance == null)
        {
            return BuildPlatformType.Unknown;
        }
        if (instance.SelectedType == BuildPlatformType.Automatic)
        {
#if UNITY_WEBPLAYER
            if (!MadeRequestForDomain)
            {
                MadeRequestForDomain = true;
                selected = BuildPlatformType.Automatic;
                DomainCheck dc = instance.gameObject.AddComponent<DomainCheck>() as DomainCheck;
                Debug.Log(dc.name);

                dc.onRecivedDomain += (url) =>
                {
                    Debug.Log("Recived Domain URL");
                    domainURL = url;
                    Debug.Log(domainURL);
                    if (domainURL.Contains("kongregate"))
                    {
                        Debug.Log("Setting Platform to Kongregate");
                        selected = BuildPlatformType.Kongergate;
                    }
                    else if (domainURL.Contains("facebook") || domainURL.Contains("fbsbx"))
                    {
                        Debug.Log("Setting platform to Facebook");
                        selected = BuildPlatformType.Facebook;
                    }
                    else
                    {
                        Debug.Log("Setting platform to Unknown");
                        selected = BuildPlatformType.Unknown;
                    }

                    if (OnBuildPlatformFound != null)
                    {
                        OnBuildPlatformFound(selected);
                    }
                };
            }

#elif UNITY_IPHONE
                selected = BuildPlatformType.IOS;
#elif UNITY_ANDROID
                selected = BuildPlatformType.Android;
#endif
        }
        else
        {
            selected = instance.SelectedType;
        }
        return selected;
    }
}

