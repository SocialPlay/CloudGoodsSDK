using UnityEngine;
using System.Collections;

public class UserNameLabel : MonoBehaviour
{
    public UILabel label;

    void OnEnable()
    {
		SP.OnUserAuthorized += OnUserAuthorized;
    }
    void OnDisable()
    {
		SP.OnUserAuthorized -= OnUserAuthorized;
    }

	void OnUserAuthorized(SocialPlayUser user)
    {
        label.text = SP.user.userName;
    }
}
