using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocialPlay.Data;
using System;

public class ItemDropperParentingTest : MonoBehaviour {

    public ItemGenerator itemGetter;

	void Start () {

        ItemDataList droppedItems = new ItemDataList();

        for (int i = 0; i < 3; i++)
        {
            SocialPlay.Data.ItemData newItem = new SocialPlay.Data.ItemData();
            newItem.Amount = 0;
            newItem.AssetBundleName = "h";
            newItem.BaseItemEnergy = 0;
            newItem.BaseItemID = 0;
            newItem.Behaviours = "k";
            newItem.Description = "";
            newItem.Detail = "";
            newItem.Energy = 0;
            newItem.Image = "";
            newItem.ItemID = 0;
            newItem.Location = 0;
            newItem.Name = "";
            newItem.Quality = 0;
            newItem.SellPrice = 0;
            newItem.StackLocationID = Guid.Empty;
            newItem.Tags = "";
            newItem.Type = 0;

            droppedItems.Add(newItem);
        }

        List<ItemData> items = CloudGoods.itemDataConverter.ConvertItems(droppedItems);
        
        itemGetter.OnReceivedGeneratedItems(items);
	}

	void Update () {
	
	}
}
