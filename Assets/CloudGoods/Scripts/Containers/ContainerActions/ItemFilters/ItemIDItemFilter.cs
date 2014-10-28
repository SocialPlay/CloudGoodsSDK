using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemIDItemFilter : ItemDataSelector
{
    public override bool isItemSelected(ItemData item, IEnumerable itemIDs, bool IsInverted = false)
    {
        foreach (int itemID in itemIDs)
        {
            if (itemID == item.itemID)
            {
                if (!IsInverted)
                    return true;
                else
                {
                    return false;
                }
            }
        }

        if (!IsInverted)
            return false;
        else
        {
            return true;
        }
    }
}
