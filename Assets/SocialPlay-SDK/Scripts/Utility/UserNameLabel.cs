using UnityEngine;
using System.Collections;

public class UserNameLabel : MonoBehaviour
{
    public UILabel label;

    void OnEnable()
    {
        GameAuthentication.OnUserAuthEvent += GameAuthentication_OnUserAuthEvent;
    }
    void OnDisable()
    {
        GameAuthentication.OnUserAuthEvent -= GameAuthentication_OnUserAuthEvent;
    }

    void GameAuthentication_OnUserAuthEvent(string obj)
    {
        label.text = ItemSystemGameData.userName;
    }
}
