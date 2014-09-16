using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class ItemContainerDisplay : MonoBehaviour
{
    public ItemContainer myContainer;
   // public GameObject displayObject;
    public Transform childTarget;

    private List<ItemDataDisplay> currentDisplayObjects = new List<ItemDataDisplay>();

    /// <summary>
    /// Called After the user clicks an item
    /// </summary>
    public event Action<ItemDataComponent> ItemSingleClicked;

    /// <summary>
    /// Called after the user double clicks an item
    /// </summary>
    public event Action<ItemDataComponent> ItemDoubleClicked;

    /// <summary>
    /// Called after the user right clicks an item
    /// </summary>
    public event Action<ItemDataComponent> ItemRightClicked;

    /// <summary>
    /// Called after a user presses a key that is linked to the Container(only for slotted containers)
    /// </summary>
    public event Action<ItemDataComponent> ItemKeyBindingClicked;


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
        ItemDataDisplay selected = FindDisplayMatch(arg1);
        if (selected != null)
        {
            currentDisplayObjects.Remove(selected);
            Destroy(selected.gameObject);
        }

    }



    void myContainer_AddedItem(ItemData itemData, bool arg2)
    {
        GameObject newItem = GameObject.Instantiate(SocialPlaySettings.DefaultUIItem) as GameObject;
        ItemDataDisplay newDisplay = newItem.GetComponent<ItemDataDisplay>();
        newItem.GetComponent<ItemDataComponent>().itemData = itemData;
        newItem.name = itemData.itemName;
        newItem.transform.parent = childTarget;
        newItem.transform.localPosition = Vector3.zero;
        newItem.transform.localScale = Vector3.one;
        currentDisplayObjects.Add(newDisplay);
    }

    void Start()
    {
        if (SocialPlaySettings.DefaultUIItem == null)
        {
            Debug.LogError("Default UI Item is not set int he settigns file");
            this.gameObject.SetActive(false);
        }
    }

    public ItemDataDisplay FindDisplayMatch(ItemData item)
    {
        foreach (ItemDataDisplay itemDisplay in currentDisplayObjects)
        {
            if (itemDisplay.itemObject.itemData == item)
            {
                return itemDisplay;
            }
        }
        return null;
    }


    public void PerformSingleClick(ItemDataComponent item)
    {
        if (ItemSingleClicked != null)
        {
            ItemSingleClicked(item);
        }
    }

    public void PerformDoubleClick(ItemDataComponent item)
    {
        if (ItemDoubleClicked != null)
        {
            ItemDoubleClicked(item);
        }
    }

    public void PerformRightClick(ItemDataComponent item)
    {
        if (ItemRightClicked != null)
        {
            ItemRightClicked(item);
        }
    }

    public void PerformKeybindingClick(ItemDataComponent item)
    {
        if (ItemKeyBindingClicked != null)
        {
            ItemKeyBindingClicked(item);
        }
    }
}
