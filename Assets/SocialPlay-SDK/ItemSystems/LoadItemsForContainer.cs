using System;
using System.Collections.Generic;
using UnityEngine;

public class LoadItemsForContainer : ContainerItemLoader
{
    public int sourceLocation;

    public override void LoadItems()
    {
        ItemServiceManager.service.GetOwnerItems(GetOwnerID(), sourceOwnerType.ToString(), sourceLocation, ItemSystemGameData.AppID, RecivedItems);
    }
}

