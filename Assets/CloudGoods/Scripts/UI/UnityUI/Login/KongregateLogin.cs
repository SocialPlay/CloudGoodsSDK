using UnityEngine;
using System.Collections;

public class KongregateLogin : MonoBehaviour
{

    void Start()
    {
        Debug.Log("Logging into Kongragate");
        this.gameObject.name = "KongragateLogin";
        Application.ExternalEval(
            "if(typeof(kongregateUnitySupport) != 'undefined'){" +
            " kongregateUnitySupport.initAPI('KongragateLogin', 'OnKongregateAPILoaded');" +
            "};"
            );
    }

    public void OnKongregateAPILoaded(string userInfoString)
    {
        string[] parts = userInfoString.Split('|');
        CloudGoods.LoginWithPlatformUser(CloudGoodsPlatform.Kongregate, parts[0], parts[1]);
    }

}
