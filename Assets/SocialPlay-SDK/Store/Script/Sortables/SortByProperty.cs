using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class SortByProperty : ISortItem
{

    public string propertyToSort;

    public override List<JToken> Sort(List<JToken> StoreItems,int direction)
    {
        this.direction = direction;
        if (string.IsNullOrEmpty(propertyToSort))
        {
            return StoreItems;
        }
        else
        {
            List<JToken> sorted = new List<JToken>(StoreItems);
            sorted.Sort(CompareItemsByPropertys);
            return sorted;
        }
    }


    public int CompareItemsByPropertys(JToken x, JToken y)
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

        JObject xDetails = JObject.Parse(x["Detail"].ToString());
        JObject YDetails = JObject.Parse(y["Detail"].ToString());
        bool xIsPropFound = false;
        bool yIsPropFound = false;
        int xValue = 0;
        int yValue = 0;

        foreach (var detail in xDetails)
        {
            if (detail.Key == propertyToSort)
            {
                xIsPropFound = true;
                try
                {
                    xValue = int.Parse(detail.Value.ToString());
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
            if (detail.Key == propertyToSort)
            {
                yIsPropFound = true;
                try
                {
                    yValue = int.Parse(detail.Value.ToString());
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


    int CheckDetailIsValid(JToken x, JToken y)
    {

        int returened = -10;
        if (string.IsNullOrEmpty(x["Detail"].ToString()))
        {
            if (string.IsNullOrEmpty(y["Detail"].ToString()))
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
            if (string.IsNullOrEmpty(y["Detail"].ToString()))
            {
                returened = -1;
            }
            else
            {
                returened = -10; // Not null;
            }
        }
        return returened;
    }


}
