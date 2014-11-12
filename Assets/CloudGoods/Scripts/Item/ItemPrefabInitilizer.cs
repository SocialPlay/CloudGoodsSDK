using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ItemPrefabInitilizer : MonoBehaviour
{

<<<<<<< HEAD



    public static GameObject GetPrefabToInstantiate(ItemData itemData, GameObject defaultPrefab = null)
    {
        var prefab = (defaultPrefab != null ? defaultPrefab : CloudGoodsSettings.DefaultItemDrop);
        foreach (var dropPrefab in CloudGoodsSettings.ExtraItemPrefabs)
=======
    public GameObject GetPrefabToInstantiate(ItemData itemData, GameObject defaultPrefab = null)
    {
        var prefab = defaultPrefab;
        foreach (var dropPrefab in CloudGoodsSettings.instance.itemInitializerPrefabs)
>>>>>>> b1e21c1f703b4d6b9bd208a9727fc19df2589341
        {
            if (IsPrefabForItem(itemData, dropPrefab))
            {
                prefab = dropPrefab.prefab;
            }
        }
        return prefab;
    }

    static bool IsPrefabForItem(ItemData itemData, DropPrefab dropPrefab)
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




