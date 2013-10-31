using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BundleSystemInitilizer : MonoBehaviour
{
    void Awake()
    {
        SocialPlay.Bundles.BundleSystem.URL = "http://socialplay.blob.core.windows.net/unityassetbundles/";
        SocialPlay.Bundles.BundleSystem.InitializeTracker();
    }
}
