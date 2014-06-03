using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using SocialPlay.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ContainerDropItemAction : ContainerActions
{
    public Transform DropTransform;
    public GameObject DropObjModel;

    ItemDrop gameItemDrop;

    void Start()
    {
        gameItemDrop = gameObject.AddComponent<ItemDrop>();
    }

    public override void DoAction(ItemData item)
    {
        string itemStacks = ConvertContainerItemToSerializedJsonObject(item);

        SP.MoveItemStacks(itemStacks, ItemSystemGameData.UserID.ToString(), "Session", ItemSystemGameData.AppID, 0, OnItemMoved(item));
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

        gameItemDrop.DropItemIntoWorld(item, DropTransform.position, DropObjModel);
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
