using System;
using System.Collections.Generic;
using UnityEngine;

public class LoadItemsForContainer : ContainerItemLoader
{
    public int sourceLocation;

    public override void LoadItems()
    {
		Debug.Log("LoadItems " + GetOwnerID() + " / " + SourceOwnerType.ToString() + " / " + sourceLocation);
        SP.GetOwnerItems(GetOwnerID(), SourceOwnerType.ToString(), sourceLocation, RecivedItems);
    }
}

