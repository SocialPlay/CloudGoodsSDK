using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;

public class NGUIItemSystemSetup : MonoBehaviour
{

    public GameObject itemDataPrefab;

    void Awake()
    {
        ItemContainerStackRestrictor.Restrictor = new NGUIItemStackRestrictionHandler();
        ItemConversion.converter = new GameObjectItemDataConverter();
        ItemStatsConverter.Converter = new SocialplayItemStatsConverter();
        if (itemDataPrefab == null) 
            Debug.LogWarning("Warning the item Data prefab is not set at Awake");
        ItemConversion.converter.ItemDataPrefab = itemDataPrefab;
    }
}
