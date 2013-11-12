using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ItemPrefabInitilizer : MonoBehaviour
{

    private static ItemPrefabInitilizer instance;
    public List<DropPrefab> dropPrefabs = new List<DropPrefab>();


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public GameObject GetPrefabToInstantiate(ItemData itemData, GameObject defaultPrefab = null)
    {
        Debug.Log("Prefab init");
        var prefab = defaultPrefab;
        if (instance == null) return prefab;
        foreach (var dropPrefab in instance.dropPrefabs)
        {
            if (IsPrefabForItem(itemData, dropPrefab))
            {
                prefab = dropPrefab.prefab;
            }
        }
        return prefab;
    }

    bool IsPrefabForItem(ItemData itemData, DropPrefab dropPrefab)
    {
        foreach (ItemFilterSystem filter in dropPrefab.itemFilters)
        {
            if (filter.IsSelected(itemData))
            {
                return true;
            }
        }
        return false;
    }


    [System.Serializable]
    public class DropPrefab
    {
        public GameObject prefab;
        public List<ItemFilterSystem> itemFilters = new List<ItemFilterSystem>();
    }

}




