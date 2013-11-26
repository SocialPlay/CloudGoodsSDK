using Newtonsoft.Json.Linq;
using SocialPlay.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectItemDataConverter : ItemDataConverter
{

    protected override List<ItemData> ConvertItems(ItemDataList generatedItems)
    {
        List<ItemData> convertedItems = new List<ItemData>();

        foreach (SocialPlay.Data.ItemData item in generatedItems)
        {
            GameObject go = new GameObject();
            ItemData itemData = go.AddComponent<ItemData>();
            go.name = item.Name;
            itemData.baseEnergy = item.BaseItemEnergy;

            itemData.behaviours = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BehaviourDefinition>>(item.Behaviours);
            itemData.description = item.Description;
            itemData.itemName = item.Name;
            itemData.imageName = item.Image;
            itemData.classID = item.Type;
            itemData.quality = item.Quality;
            itemData.salePrice = item.SellPrice;
            itemData.varianceID = item.ItemID;
            itemData.itemID = int.Parse(item.BaseItemID.ToString());
            itemData.stackSize = item.Amount;
            itemData.totalEnergy = item.Energy;
            itemData.stackID = item.StackLocationID;
            itemData.stats = ConvertItemDetail(item);
            itemData.assetURL = item.AssetBundleName;
            itemData.tags = ConvertTags(item);
            itemData.persistantLocation = item.Location;
            convertedItems.Add(itemData);

            Resources.UnloadUnusedAssets();
        }

       // ItemComponentInitalizer.InitializeItemWithComponents(convertedItems);

        return convertedItems;
    }

    protected List<String> ConvertTags(SocialPlay.Data.ItemData item)
    {

        List<string> tags = new List<string>();
        if (String.IsNullOrEmpty(item.Tags) || item.Tags == "\"[]\"")
        {
            return tags;
        }
        JArray tagsArray = JArray.Parse(item.Tags);

        foreach (JToken tag in tagsArray)
        {
            tags.Add(tag.ToString());
        }

        return tags;
    }

    protected override Dictionary<string, float> ConvertItemDetail(SocialPlay.Data.ItemData item)
    {
        Dictionary<string, float> statPair = new Dictionary<string, float>();

        if (string.IsNullOrEmpty(item.Detail))
        {
            return statPair;
        }
        JArray statsArray = JArray.Parse(item.Detail);

        return ItemStatsConverter.Converter.Generate(statsArray);

    }

    public ItemDetails DeserializeObject(string jsonString)
    {
        string data = JToken.Parse(jsonString).ToString();

        ItemDetails itemDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<ItemDetails>(data);

        return itemDetails;
    }

    public override List<ItemData> convertToItemDataFromString(string itemData)
    {
        string data = "";
        ItemDataList itemDetails = null;

        if (itemData == "\"[]\"")
        {
            List<ItemData> convertedItems = new List<ItemData>();
            return convertedItems;
        }

        data = JToken.Parse(itemData).ToString();

        itemDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<ItemDataList>(data);
        return ConvertItems(itemDetails);
    }
}

[System.Serializable]
public class BehaviourDefinition
{
    public string Name;
    public int ID;
    public string Description;
    public int Energy;
}



