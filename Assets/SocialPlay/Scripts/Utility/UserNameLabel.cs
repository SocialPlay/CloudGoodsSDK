using UnityEngine;
using System.Collections;

public class UserNameLabel : MonoBehaviour
{
    public UILabel label;

    void OnEnable()
    {
        SP.OnRegisteredUserToSession += OnRegisteredUserToSession;
    }
    void OnDisable()
    {
        SP.OnRegisteredUserToSession -= OnRegisteredUserToSession;
    }

    void OnRegisteredUserToSession(string obj)
    {
        label.text = SP.user.userName;
    }
}
