using UnityEngine;
using System.Collections;

using SocialPlay.Data;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ContainerMoveAllItemsAction : ContainerActions
{
    public ItemContainer SourceContainer;
    public ItemContainer DestinationContainer;
    public int DestinationLocation;

    public override void DoAction(ItemDataComponent itemObject)
    {
        ItemData itemData = itemObject.GetComponent<ItemDataComponent>().itemData;
        ///Tmp moves all to vault on backend
        MoveMultipleStacks stacks = new MoveMultipleStacks();
        stacks.StackInfos = new List<MoveItemStackInfo>();
        foreach (ItemData item in SourceContainer.containerItems)
        {
            MoveItemStackInfo info = new MoveItemStackInfo();
            info.MoveAmount = item.stackSize;
            info.StackID = item.stackID;
            stacks.StackInfos.Add(info);
        }
        string convert = JsonConvert.SerializeObject(stacks);
        CloudGoods.MoveItemStacks(convert, CloudGoods.user.userID.ToString(), "User", DestinationLocation, delegate(MoveMultipleItemsResponse x)
        {
            ItemData[] containerItems = new ItemData[SourceContainer.containerItems.Count];
            SourceContainer.containerItems.CopyTo(containerItems);

            foreach (MovedItemsInfo info in x.movedItems)
            {
                foreach (ItemData data in containerItems)
                {
                    if (data.stackID == info.OriginalStackID)
                    {
                        data.stackID = info.NewStackID;
                        SourceContainer.Remove(data, true);
                        DestinationContainer.Add(data, -1, false);
                    }
                }
            }
        });

    }


}
