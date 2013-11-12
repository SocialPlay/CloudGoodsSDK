using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class FilterNewestItems
{
    public enum SortTimeType { hours, days, na }

    int itemDisplayCount;
    public int ItemDisplayCount()
    {
        return itemDisplayCount;
    }
    public void SetItemDisplayCount(int count)
    {
        if (count > -1)
            itemDisplayCount = count;
        else
            throw new Exception("The display count should only be positive numbers.");
    }

    public List<JToken> FilterItems(List<JToken> items, SortTimeType timeType, int displayCount, int allowedDifference)
    {
        int displayItemCounter = 0;
        List<JToken> filteredItems = new List<JToken>();
        itemDisplayCount = displayCount;

        foreach (JToken item in items)
        {
            displayItemCounter++;
            if (displayCount != 0 && displayItemCounter > displayCount) break;

            string jtokenAddDateToString = item["AddDate"].ToString();
            DateTime itemDate = Convert.ToDateTime(jtokenAddDateToString);
            TimeSpan difference = DateTime.Now - itemDate;
            //Debug.Log(item["Name"] + ": " + difference.TotalDays);
            //Debug.Log(item["Name"] + ": " + difference.TotalHours);

            switch (timeType)
            {
                case SortTimeType.hours:
                    if (difference.TotalHours < allowedDifference + 1)
                        filteredItems.Add(item);
                    break;
                case SortTimeType.days:
                    if (difference.TotalDays < allowedDifference + 1)
                        filteredItems.Add(item);
                    break;
                case SortTimeType.na:
                    filteredItems.Add(item);
                    break;
                default:
                    break;
            }

            if (displayCount == 0) continue;
        }

        //List<JToken> sorted = new List<JToken>();
        //sorted.Sort((x, y) => DateTime.Compare(x["AddDate"], y["AddDate"]));

        //Debug.Log("filtered items count: " + filteredItems.Count);

        return filteredItems;
    }

}
