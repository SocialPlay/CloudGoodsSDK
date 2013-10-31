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
        string formated = "[" + NGUITools.EncodeColor(GetColorQuality(item.quality)) + "]" + item.name;
        
        foreach (KeyValuePair<string, float> pair in item.stats)
        {
            if (pair.Key == "Not Available") continue;

            formated = string.Format("{0}\n [" + NGUITools.EncodeColor(Color.white) + "]{1}: {2}", formated, pair.Key, pair.Value);
        }
        return formated;
    }

    Color GetColorQuality(int colorQuality)
    {
        switch (colorQuality)
        {
            case 0:
                return Color.gray;
            case 1:
                return Color.white;
            case 2:
                return Color.green;
            case 3:
                return Color.blue;
            case 4:
                return Color.yellow;
            default:
                return Color.white;
        }
    }
}
