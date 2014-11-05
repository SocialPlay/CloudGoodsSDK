using UnityEngine;
using System.Collections;

public class KongregateLogin : MonoBehaviour
{

    void Start()
    {
        this.gameObject.name = "KongragateLogin";
        Application.ExternalEval(
            "if(typeof(kongregateUnitySupport) != 'undefined'){" +
            " kongregateUnitySupport.initAPI('KongragateLogin', 'OnKongregateAPILoaded');" +
            "};"
            );
    }

    public void OnKongregateAPILoaded(string userInfoString)
    {
        if (BuildPlatform.Platform == BuildPlatform.BuildPlatformType.Automatic)
        {
            BuildPlatform.Platform = BuildPlatform.BuildPlatformType.Kongergate;
        }
        string[] parts = userInfoString.Split('|');
        CloudGoods.LoginWithPlatformUser(CloudGoodsPlatform.Kongregate, parts[0], parts[1]);
    }

}
