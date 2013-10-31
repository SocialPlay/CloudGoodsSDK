using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClassIDItemSelector : ItemDataSelector
{
    public override bool isItemSelected(ItemData item, IEnumerable classIDs, bool IsInverted = false)
    {
        foreach (int classID in classIDs)
        {
            if (classID == item.classID)
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
