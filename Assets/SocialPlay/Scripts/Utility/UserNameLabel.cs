using UnityEngine;
using System.Collections;

public class UserNameLabel : MonoBehaviour
{
    public UILabel label;

    void OnEnable()
    {
        SP.OnRegisteredUserToSession += GameAuthentication_OnUserAuthEvent;
    }
    void OnDisable()
    {
        SP.OnRegisteredUserToSession -= GameAuthentication_OnUserAuthEvent;
    }

    void GameAuthentication_OnUserAuthEvent(string obj)
    {
        label.text = ItemSystemGameData.userName;
    }
}
