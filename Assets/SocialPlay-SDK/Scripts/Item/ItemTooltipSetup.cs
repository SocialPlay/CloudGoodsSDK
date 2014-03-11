using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocialPlay.ItemSystems;

public class ItemTooltipSetup : MonoBehaviour, ITooltipSetup
{
    ItemData item;

    public string Setup()
    {
        item = GetComponent<ItemData>();
        string formated = "[" + NGUITools.EncodeColor(ItemQuailityColorSelector.GetColorForItem(item)) + "]" + item.name;
        
        foreach (KeyValuePair<string, float> pair in item.stats)
        {
            if (pair.Key == "Not Available") continue;

            formated = string.Format("{0}\n [" + NGUITools.EncodeColor(Color.white) + "]{1}: {2}", formated, pair.Key, pair.Value);
        }
        return formated;
    }
}
