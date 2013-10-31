using UnityEngine;
using System.Collections;
using SocialPlay.Data;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ContainerDropAllContainerItemsAction : ContainerActions
{
    public ItemContainer dropContainer;
    public Transform TransformForDropPosition;
    public GameObject dropObjDefaultModel;

    ItemDrop itemDrop;

    void Start()
    {
        itemDrop = gameObject.AddComponent<ItemDrop>();
    }

    public override void DoAction(ItemData item)
    {
        string convertedDropItems = ConvertContainerItemsToSerializedJsonObject();

        ItemServiceManager.service.MoveItemStacks(convertedDropItems, ItemSystemGameData.UserID.ToString(), "Session", ItemSystemGameData.AppID, 0, MovedItems);
    }

    void MovedItems(string returnData)
    {
        MoveMultipleItemsResponse moveItemResponses = JsonToMoveMultipleItemsResponse(returnData);

        var itemsToDrop = CreateDropItemArray();

        foreach (MovedItemsInfo moveInfo in moveItemResponses.movedItems)
        {
            DropMovedItems(itemsToDrop, moveInfo);
        }
    }

    private static MoveMultipleItemsResponse JsonToMoveMultipleItemsResponse(string data)
    {
        JToken token = JToken.Parse(data);
        MoveMultipleItemsResponse moveItemResponses = JsonConvert.DeserializeObject<MoveMultipleItemsResponse>(token.ToString());
        return moveItemResponses;
    }

    private ItemData[] CreateDropItemArray()
    {
        ItemData[] tmpContainerItems = new ItemData[dropContainer.containerItems.Count];
        dropContainer.containerItems.CopyTo(tmpContainerItems);
        return tmpContainerItems;
    }

    private void DropMovedItems(ItemData[] containerItems, MovedItemsInfo moveInfo)
    {
        foreach (ItemData data in containerItems)
        {
            if (data.stackID == moveInfo.OriginalStackID)
            {
                if (data.ownerContainer != null)
                    data.ownerContainer.Remove(data, true);

                data.stackID = moveInfo.NewStackID;

                itemDrop.DropItemIntoWorld(data, TransformForDropPosition.position, dropObjDefaultModel);
            }
        }
    }

    //todo some duplicate caseCode with ContainerDropItemAction
    private string ConvertContainerItemsToSerializedJsonObject()
    {
        MoveMultipleStacks stacks = new MoveMultipleStacks();
        stacks.StackInfos = new List<MoveItemStackInfo>();

        foreach (ItemData containerItem in dropContainer.containerItems)
        {
            MoveItemStackInfo info = new MoveItemStackInfo();
            info.MoveAmount = containerItem.stackSize;
            info.StackID = containerItem.stackID;
            stacks.StackInfos.Add(info);
        }

        string convert = JsonConvert.SerializeObject(stacks);
        return convert;
    }
}
