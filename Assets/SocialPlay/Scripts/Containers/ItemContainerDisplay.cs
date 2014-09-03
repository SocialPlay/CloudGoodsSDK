using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemContainerDisplay : MonoBehaviour {


    public ItemContainer myContainer;
    public GameObject displayObject;


    void OnEnable()
    {
        myContainer.AddedItem += myContainer_AddedItem;
        myContainer.RemovedItem += myContainer_RemovedItem;
    }

    void OnDisable()
    {
        myContainer.AddedItem += myContainer_AddedItem;
        myContainer.RemovedItem += myContainer_RemovedItem;
    }

    void myContainer_RemovedItem(ItemData arg1, int arg2, bool arg3)
    {
   
    }

    void myContainer_AddedItem(ItemData itemData, bool arg2)
    {
        GameObject newItem = GameObject.Instantiate(displayObject) as GameObject;
        newItem.GetComponent<ItemDataComponent>().itemData = itemData;
        newItem.name = itemData.itemName;
        newItem.transform.parent = this.transform;
    }
}
