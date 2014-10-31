using UnityEngine;

public class CloudGoodsTestUserLogin : MonoBehaviour
{
    void Start()
    {
        Debug.Log("In Editor, Logging in as Test User with access token: " + CloudGoods.AppID);
		CloudGoods.LoginWithPlatformUser(CloudGoodsPlatform.SocialPlay, "2", "Editor Test User");
    }
}
