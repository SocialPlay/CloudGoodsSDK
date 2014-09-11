﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ItemDataComponent : MonoBehaviour
{
    public Action<ItemData> onPickup;
    public bool pickupOnClick = true;
    public bool destroyOnPickup = true;
    [HideInInspector]
    public bool isValid = true;

    public ItemData itemData
    {
        get
        {
            if (mData == null)
            {
                mData = new ItemData();
                mData.uiReference = this;
            }
            return mData;
        }
        set
        {
            mData = value;
            mData.uiReference = this;
            SetData(mData);
        }
    }

    ItemData mData;

    /// <summary>
    /// if pickupOnClick is true the item can be picked up on Click event.
    /// </summary>

    void OnClick()
    {
        if(pickupOnClick) Pickup();
    }

    public virtual void SetData(ItemData itemData){}

    /// <summary>
    /// Convenient method to use it to pickup items.
    /// </summary>

    public void Pickup()
    {

#if UNITY_EDITOR
        Debug.Log("ItemData Pickup " + itemData);
#endif

        if (onPickup != null) onPickup(itemData);

        GameItemContainerInserter.instance.PutGameItem(new List<ItemData>(new ItemData[1] { itemData }));

        if (destroyOnPickup) GameObject.Destroy(gameObject);
    }
}
