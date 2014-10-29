using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using SocialPlay.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ContainerDropItemAction : ContainerActions
{
    public Transform DropTransform;
    ItemDrop gameItemDrop;

    void Start()
    {
        gameItemDrop = gameObject.AddComponent<ItemDrop>();
    }

    public override void DoAction(ItemDataComponent itemObject)
    {
        ItemData itemData = itemObject.GetComponent<ItemDataComponent>().itemData;
        string itemStacks = ConvertContainerItemToSerializedJsonObject(itemData);

        CloudGoods.MoveItemStacks(itemStacks, CloudGoods.user.userID.ToString(), "Session", 0, OnItemMoved(itemData));
    }

    private System.Action<MoveMultipleItemsResponse> OnItemMoved(ItemData item)
    {
        return data => ItemMoved(data, item);
    }

    void ItemMoved(MoveMultipleItemsResponse moves, ItemData item)
    {
        foreach (MovedItemsInfo moveInfo in moves.movedItems)
        {
            DropMovedItem(item, moveInfo);
        }

        gameItemDrop.DropItemIntoWorld(item, DropTransform.position, CloudGoods.DefaultItemDrop);
    }

    private static void DropMovedItem(ItemData item, MovedItemsInfo moveInfo)
    {
        if (item.stackID == moveInfo.OriginalStackID)
        {
            if (item.ownerContainer != null)
                item.ownerContainer.Remove(item, true);

            item.stackID = moveInfo.NewStackID;
        }
    }

    private static string ConvertContainerItemToSerializedJsonObject(ItemData item)
    {
        MoveMultipleStacks stacks = new MoveMultipleStacks();
        stacks.StackInfos = new List<MoveItemStackInfo>();

        MoveItemStackInfo info = new MoveItemStackInfo();
        info.MoveAmount = item.stackSize;
        info.StackID = item.stackID;
        stacks.StackInfos.Add(info);

        string convert = JsonConvert.SerializeObject(stacks);
        return convert;
    }
}
