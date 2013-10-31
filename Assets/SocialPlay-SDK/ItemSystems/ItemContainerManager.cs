using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocialPlay.ItemSystems;
using UnityEngine;

[System.Serializable]
public class ItemContainerManager
{
    public static ContainerAddState.ActionState MoveItem(ItemData movingItemData, ItemContainer targetContainer)
    {
        try
        {
            if (movingItemData == null)
                throw new Exception("Can Not Move null item");

            if (targetContainer == null)
                throw new Exception("Can not move item to null container");

            ContainerAddState targetAddState = targetContainer.GetContainerAddState(movingItemData);

            switch (targetAddState.actionState)
            {
                case ContainerAddState.ActionState.Add:
                    if (movingItemData.ownerContainer != null)
                    {
                        movingItemData.ownerContainer.Remove(movingItemData, true, targetAddState.possibleAddAmount);
                    }
                    targetContainer.Add(ItemConverter.ConvertItemDataToNGUIItemDataObject(movingItemData), targetAddState.possibleAddAmount);
                    break;
                case ContainerAddState.ActionState.Swap:
                    if (movingItemData.ownerContainer != null)
                    {
                        CheckSwapability(ItemConverter.ConvertItemDataToNGUIItemDataObject(movingItemData), targetContainer, targetAddState);
                    }
                    else
                    {
                        return ContainerAddState.ActionState.No;
                    }
                    break;
                case ContainerAddState.ActionState.No:
                    break;
                default:
                    break;
            }

            return targetAddState.actionState;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);

            return ContainerAddState.ActionState.No;
        }
    }

    private static void CheckSwapability(ItemData movingItemData, ItemContainer targetContainer, ContainerAddState possibleSwapState)
    {
        if (possibleSwapState.possibleSwapItem == null)
            return;

        movingItemData.ownerContainer.Remove(movingItemData, true, possibleSwapState.possibleAddAmount);
        ContainerAddState sourceAddState = movingItemData.ownerContainer.GetContainerAddState(possibleSwapState.possibleSwapItem);
        ItemContainer SourceContainer = movingItemData.ownerContainer;

        if (sourceAddState.actionState == ContainerAddState.ActionState.Add)
        {
            SourceContainer.Add(possibleSwapState.possibleSwapItem, -1);
            targetContainer.Remove(possibleSwapState.possibleSwapItem, true);
            targetContainer.Add(movingItemData, -1);
            return;
        }
        else
        {
            movingItemData.ownerContainer.Add(movingItemData, possibleSwapState.possibleAddAmount);
        }

    }
}

