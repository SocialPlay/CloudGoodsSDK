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

    public static BuildPlatformType Platform
    {
        get
        {
            if (instance == null)
            {
                return BuildPlatformType.Unknown;
            }
            if (instance.SelectedType == BuildPlatformType.Automatic)
            {
#if UNITY_IPHONE
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
        set
        {
            selected = value;
            if (OnBuildPlatformFound != null)
            {
                OnBuildPlatformFound(selected);
            }
        }
    }
}

