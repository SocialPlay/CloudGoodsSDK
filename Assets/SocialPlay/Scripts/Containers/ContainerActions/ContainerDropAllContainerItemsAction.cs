using UnityEngine;
using System.Collections;
using SocialPlay.Data;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ContainerDropAllContainerItemsAction : ContainerActions
{
    public ItemContainer DropContainer;
    public Transform TransformForDropPosition;
    public GameObject DropObjDefaultModel;

    ItemDrop itemDrop;

    void Start()
    {
        itemDrop = gameObject.AddComponent<ItemDrop>();
    }

    public override void DoAction(ItemDataComponent itemObject)
    {
        string convertedDropItems = ConvertContainerItemsToSerializedJsonObject();

        CloudGoods.MoveItemStacks(convertedDropItems, CloudGoods.user.userID.ToString(), "Session", 0, MovedItems);
    }

    void MovedItems(MoveMultipleItemsResponse moveItemResponses)
    {
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
        ItemData[] tmpContainerItems = new ItemData[DropContainer.containerItems.Count];
        DropContainer.containerItems.CopyTo(tmpContainerItems);
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

                itemDrop.DropItemIntoWorld(data, TransformForDropPosition.position, DropObjDefaultModel);
            }
        }
    }

    //todo some duplicate caseCode with ContainerDropItemAction
    private string ConvertContainerItemsToSerializedJsonObject()
    {
        MoveMultipleStacks stacks = new MoveMultipleStacks();
        stacks.StackInfos = new List<MoveItemStackInfo>();

        foreach (ItemData containerItem in DropContainer.containerItems)
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
