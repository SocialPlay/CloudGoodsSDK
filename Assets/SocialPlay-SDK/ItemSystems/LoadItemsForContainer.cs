using System;
using System.Collections.Generic;
using UnityEngine;

public class LoadItemsForContainer : ContainerItemLoader
{
    public int sourceLocation;

    public override void LoadItems()
    {
        WebserviceCalls.webservice.GetOwnerItems(GetOwnerID(), SourceOwnerType.ToString(), sourceLocation, ItemSystemGameData.AppID, RecivedItems);
    }
}

