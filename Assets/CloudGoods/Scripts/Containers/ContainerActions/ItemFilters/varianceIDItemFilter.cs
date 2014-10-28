using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class varianceIDItemFilter : ItemDataSelector
{
    public override bool isItemSelected(ItemData item, IEnumerable varianceIDs, bool IsInverted = false)
    {
        foreach (int varianceID in varianceIDs)
        {
            if (varianceID == item.varianceID)
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
