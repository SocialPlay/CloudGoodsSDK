using UnityEngine;
using System.Collections;


public class WebItemServiceInjector : MonoBehaviour
{
    string remoteURL = "https://SocialPlayWebService.azurewebsites.net/cloudgoods/cloudgoodsservice.svc/";

    void Awake()
    {
        if (this.GetComponent<WebServiceUrlSwitcher>())
        {
            WebServiceUrlSwitcher switcher = this.GetComponent<WebServiceUrlSwitcher>();
            if (switcher.isUsed)
            {
                remoteURL = switcher.preferredURL;
            }
        }

        //WebItemService. = new WebItemService(remoteURL);
        SocialPlay.ServiceClient.Open.Url = remoteURL;
    }
}
