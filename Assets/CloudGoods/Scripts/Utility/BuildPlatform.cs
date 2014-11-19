using UnityEngine;
using System.Collections;
using System;

public class BuildPlatform : MonoBehaviour
{

    public static event Action<BuildPlatformType> OnBuildPlatformFound;

    public BuildPlatformType SelectedType;

    private static BuildPlatformType selected;

    public enum BuildPlatformType
    {
        Automatic = 0,
        Facebook = 1,
        Kongergate = 2,
        Android = 3,
        IOS = 4,
        CloudGoodsStandAlone = 6,
        Unknown = 7,
        EditorTestPurchasing = 8
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
                if (Application.isEditor)
                {
                    selected = BuildPlatformType.CloudGoodsStandAlone;
                }
#if UNITY_IPHONE
                selected = BuildPlatformType.IOS;
#elif UNITY_ANDROID
                selected = BuildPlatformType.Android;
#endif
#if UNITY_EDITOR
                selected = BuildPlatformType.EditorTestPurchasing;
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
                Debug.Log("Setting build platform to " + selected.ToString());
                OnBuildPlatformFound(selected);
            }
        }
    }
}

