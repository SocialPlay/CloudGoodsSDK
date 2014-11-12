using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemIDItemFilter : ItemDataSelector
{
    public override bool isItemSelected(ItemData item, IEnumerable ItemIDs, bool IsInverted = false)
    {
        foreach (int ItemID in ItemIDs)
        {
            if (ItemID == item.ItemID)
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
