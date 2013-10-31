using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ItemPrefabInitilizer : MonoBehaviour
{

    public List<DropPrefab> dropPrefabs = new List<DropPrefab>();

    public GameObject GetPrefabToInstantiate(ItemData itemData, GameObject defaultPrefab = null)
    {
        var prefab = defaultPrefab;

        foreach (var dropPrefab in dropPrefabs)
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




