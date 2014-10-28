using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ItemPrefabInitilizer))]
public class GlobalPrefabInitilizer : MonoBehaviour
{


    public static ItemPrefabInitilizer prefabInit = null;

    void Awake()
    {
        if (prefabInit == null)
        {
            prefabInit = this.GetComponent<ItemPrefabInitilizer>();
        }
    }
}
