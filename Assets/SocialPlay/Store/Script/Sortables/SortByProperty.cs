using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class SortByProperty : ISortItem
{

    public string propertyToSort;

    public override List<StoreItem> Sort(List<StoreItem> StoreItems, int direction)
    {
        this.direction = direction;
        if (string.IsNullOrEmpty(propertyToSort))
        {
            return StoreItems;
        }
        else
        {
            List<StoreItem> sorted = new List<StoreItem>(StoreItems);
            sorted.Sort(CompareItemsByPropertys);
            return sorted;
        }
    }


    public int CompareItemsByPropertys(StoreItem x, StoreItem y)
    {
        int nullcheck = CheckIfNull(x, y);
        if (nullcheck != -10)
        {
            return nullcheck * direction;
        }
        int checkDetails = CheckDetailIsValid(x, y);
        if (checkDetails != -10)
        {
            return checkDetails * direction;
        }

        List<StoreItemDetail> xDetails = x.itemDetail;
        List<StoreItemDetail> YDetails = y.itemDetail;
        bool xIsPropFound = false;
        bool yIsPropFound = false;
        int xValue = 0;
        int yValue = 0;

        foreach (StoreItemDetail detail in xDetails)
        {
            if (detail.propertyName == propertyToSort)
            {
                xIsPropFound = true;
                try
                {
                    xValue = int.Parse(detail.propertyValue.ToString());
                }
                catch
                {
                    xValue = 0;
                }
                break;
            }
        }

        foreach (var detail in YDetails)
        {
            if (detail.propertyName == propertyToSort)
            {
                yIsPropFound = true;
                try
                {
                    yValue = int.Parse(detail.propertyValue.ToString());
                }
                catch
                {
                    yValue = 0;
                }
                break;
            }
        }

        int checkPropertyFound = CheckIsPropertyFound(xIsPropFound, yIsPropFound);
        if (checkPropertyFound != -10)
        {
            return checkPropertyFound;
        }

        if (xValue == yValue)
        {
            return 0;
        }
        else if (xValue < yValue)
        {
            return 1 * direction;
        }
        else
        {
            return -1 * direction;
        }
    }


    int CheckIsPropertyFound(bool x, bool y)
    {
        int returened = -10;
        if (!x)
        {
            if (!y)
            {
                returened = 0;
            }
            else
            {
                returened = 1;
            }
        }
        else
        {
            if (!y)
            {
                returened = -1;
            }
            else
            {
                returened = -10;
            }
        }   
        return returened;
    }


    int CheckDetailIsValid(StoreItem x, StoreItem y)
    {
        //TODO Check for validation
        int returened = -10;
        //if (string.IsNullOrEmpty(x["Detail"].ToString()))
        //{
        //    if (string.IsNullOrEmpty(y["Detail"].ToString()))
        //    {
        //        returened = 0;
        //    }
        //    else
        //    {
        //        returened = 1;
        //    }
        //}
        //else
        //{
        //    if (string.IsNullOrEmpty(y["Detail"].ToString()))
        //    {
        //        returened = -1;
        //    }
        //    else
        //    {
        //        returened = -10; // Not null;
        //    }
        //}
        return returened;
    }


}
