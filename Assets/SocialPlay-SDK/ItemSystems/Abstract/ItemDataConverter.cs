using SocialPlay.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public abstract class ItemDataConverter
{   
    public GameObject ItemDataPrefab;

    public abstract List<ItemData> convertToItemDataFromString(string itemData);

    public abstract List<ItemData> ConvertItems(ItemDataList serverDetails);

    public abstract Dictionary<string, float> ConvertItemDetail(SocialPlay.Data.ItemData detail);
}

