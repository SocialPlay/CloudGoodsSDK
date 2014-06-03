using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public abstract class ISortItem : MonoBehaviour
{
    public string displayName;

    protected int direction = 1;
    public abstract List<StoreItemInfo> Sort(List<StoreItemInfo> StoreItems, int direction);


    public int CheckIfNull(StoreItemInfo x, StoreItemInfo y)
    {
        if (x == null)
        {
            if (y == null)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
        else
        {
            if (y == null)
            {
                return 1;
            }
            else
            {
                return -10; // Not null;
            }
        }
    }
}
