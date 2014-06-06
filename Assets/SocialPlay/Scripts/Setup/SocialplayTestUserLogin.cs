using UnityEngine;

public class SocialplayTestUserLogin : MonoBehaviour
{
    void Start()
    {
        Debug.Log("In Editor, Logging in as Test User with access token: " + SP.AppID);
		SP.LoginWithPlatformUser(SocialPlayPlatform.SocialPlay, "2", "Editor Test User");
    }
}
