using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectionIDItemFilter : ItemDataSelector
{
    public override bool isItemSelected(ItemData item, IEnumerable CollectionIDs, bool IsInverted = false)
    {
        foreach (int CollectionID in CollectionIDs)
        {
            if (CollectionID == item.CollectionID)
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
