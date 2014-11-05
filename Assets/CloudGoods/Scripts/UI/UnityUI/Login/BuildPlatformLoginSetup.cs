using UnityEngine;
using System.Collections;

public class BuildPlatformLoginSetup : MonoBehaviour
{

    void Awake()
    {
        if (BuildPlatform.GetPlatform() == BuildPlatform.BuildPlatformType.Facebook)
        {
            this.gameObject.AddComponent<CloudGoodsFacebookLogin>();
        }
        else if (BuildPlatform.GetPlatform() == BuildPlatform.BuildPlatformType.Kongergate)
        {
            this.gameObject.AddComponent<KongregateLogin>();
        }
        else if (BuildPlatform.GetPlatform() == BuildPlatform.BuildPlatformType.Automatic)
        {   
            BuildPlatform.OnBuildPlatformFound += CloudGoodsSettings_onBuildPlatformFound;
        }
    }

    void CloudGoodsSettings_onBuildPlatformFound(BuildPlatform.BuildPlatformType platform)
    {
        Debug.Log("Recived Platform Type setting login to match");
        if (BuildPlatform.GetPlatform() == BuildPlatform.BuildPlatformType.Facebook)
        {
            this.gameObject.AddComponent<CloudGoodsFacebookLogin>();
        }
        if (BuildPlatform.GetPlatform() == BuildPlatform.BuildPlatformType.Kongergate)
        {
            this.gameObject.AddComponent<KongregateLogin>();
        }

    }
}
