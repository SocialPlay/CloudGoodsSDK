using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;
using System;

public class EnviromentContainerLoader : MonoBehaviour
{

    private static bool isLoadCalled = false;

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
        LoadAllContainerItems();
    }

    void Awake()
    {
        ItemConversion.converter = new GameObjectItemDataConverter();
    }

    public void Start()
    {
        if (ItemSystemGameData.SessionID != Guid.Empty)
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
