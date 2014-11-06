using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;
using SocialPlay.Data;

public class GetItemsContainerInserter : MonoBehaviour, IGetItems
{

    static public GetItemsContainerInserter instance;

    public ItemContainer container;

    public event Action<List<ItemData>> onReciveItems;

    void Awake()
    {
        instance = this;
    }

    public void GetGameItem(List<ItemData> items)
    {

        if (container == null)
            throw new Exception("You must provide a container to your GameItemContainerInserter");
        else
        {
            if (onReciveItems != null)
            {
                onReciveItems(items);
            }

            foreach (ItemData item in items)
            {
                ItemContainerManager.MoveItem(item, null, container);               
            }
        }
    }

}