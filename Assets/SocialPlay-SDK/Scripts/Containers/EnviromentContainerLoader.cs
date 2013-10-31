using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;
using System;

public class EnviromentContainerLoader : MonoBehaviour
{

    private bool isLoadCalled = false;

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
        if (!isLoadCalled && ItemSystemGameData.SessionID != Guid.Empty)
        {
            LoadAllContainerItems();
        }
    }

    public void LoadAllContainerItems()
    {
        isLoadCalled = true;
        foreach (LoadItemsForContainer loader in GameObject.FindObjectsOfType(typeof(LoadItemsForContainer)))
        {
            loader.LoadItems();
        }

    }
}
