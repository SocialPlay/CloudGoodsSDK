using UnityEngine;
using System.Collections;

public class UserNameLabel : MonoBehaviour
{
    public UILabel label;

    void OnEnable()
    {
        GameAuthentication.OnRegisteredUserToSession += GameAuthentication_OnUserAuthEvent;
    }
    void OnDisable()
    {
        GameAuthentication.OnRegisteredUserToSession -= GameAuthentication_OnUserAuthEvent;
    }

    void GameAuthentication_OnUserAuthEvent(string obj)
    {
        label.text = ItemSystemGameData.userName;
    }
}
