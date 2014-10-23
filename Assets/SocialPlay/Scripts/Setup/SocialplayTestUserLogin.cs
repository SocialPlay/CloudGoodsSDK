using UnityEngine;

public class SocialplayTestUserLogin : MonoBehaviour
{
    void Start()
    {
        Debug.Log("In Editor, Logging in as Test User with access token: " + CloudGoods.AppID);
		CloudGoods.LoginWithPlatformUser(SocialPlayPlatform.SocialPlay, "2", "Editor Test User");
    }
}
