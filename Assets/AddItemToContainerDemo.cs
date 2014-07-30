using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocialPlay.Data;

public class AddItemToContainerDemo : MonoBehaviour {

    public ItemContainer itemContainerOne;
    public ItemContainer itemContainerTwo;

    void Start()
    {
        ItemDataList itemDataList = new ItemDataList();
        itemDataList.Add(CreateNewTestItem());
        itemDataList.Add(CreateNewTestItem());

        List<ItemData> items = ItemConversion.converter.ConvertItems(itemDataList);

        foreach (ItemData item in items)
        {
            ItemData itemObj = ItemConverter.ConvertItemDataToNGUIItemDataObject(item);
            Destroy(item.gameObject);
            itemContainerOne.Add(itemObj);
        }
    }

    SocialPlay.Data.ItemData CreateNewTestItem()
    {
        SocialPlay.Data.ItemData exampleDemoItem = new SocialPlay.Data.ItemData();
        exampleDemoItem.AssetBundleName = "";
        exampleDemoItem.BaseItemEnergy = 100;

        BehaviourDefinition behaviourDefinition = new BehaviourDefinition();
        behaviourDefinition.ID = 10;
        behaviourDefinition.Name = "Strength";
        behaviourDefinition.Energy = 500;
        behaviourDefinition.Description = "Applys Strength to Player";

        List<BehaviourDefinition> behaviourDefinitions = new List<BehaviourDefinition>();
        behaviourDefinitions.Add(behaviourDefinition);
        exampleDemoItem.Behaviours = "";

        exampleDemoItem.Type = 1;
        exampleDemoItem.Description = "This is an item";
        exampleDemoItem.Image = "http://socialplay.blob.core.windows.net/images/CreditBundleIcon.png";
        exampleDemoItem.ItemID = 101;
        exampleDemoItem.Name = "Test Item";
        exampleDemoItem.Location = 0;
        exampleDemoItem.Quality = 1;
        exampleDemoItem.SellPrice = 100;
        exampleDemoItem.StackLocationID = new System.Guid();
        exampleDemoItem.Amount = 10;
        exampleDemoItem.Tags = null;
        exampleDemoItem.Energy = 50;
        exampleDemoItem.ItemID = 10;
        exampleDemoItem.BaseItemEnergy = 10;

        return exampleDemoItem;
    }
}
