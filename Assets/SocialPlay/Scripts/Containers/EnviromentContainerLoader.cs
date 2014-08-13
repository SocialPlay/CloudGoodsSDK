using UnityEngine;
using System.Collections;

using System;

public class EnviromentContainerLoader : MonoBehaviour
{

    private static bool isLoadCalled = false;

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
        LoadAllContainerItems();
    }

    public void Start()
    {
        if (SP.isLogged)
        {
            LoadAllContainerItems();
        }
    }

    public void LoadAllContainerItems()
    {
        if (isLoadCalled) return;
        isLoadCalled = true;
        foreach (ContainerItemLoader loader in GameObject.FindObjectsOfType(typeof(ContainerItemLoader)))
        {
            loader.LoadItems();
        }

    }
}
