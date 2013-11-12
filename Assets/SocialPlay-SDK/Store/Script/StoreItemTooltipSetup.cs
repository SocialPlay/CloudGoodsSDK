using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;

public class StoreItemTooltipSetup : MonoBehaviour, ITooltipSetup
{
    ItemInfo item;

    public string Setup()
    {
        item = GetComponent<ItemInfo>();
        string formated = "[" + NGUITools.EncodeColor(GetColorQuality(1)) + "]" + item.itemName;

        JArray statsArray = JArray.Parse(item.itemDetail);

        Debug.Log(item.itemDetail);


        for (int i = 0; i < statsArray.Count; i++)
        {
            formated = string.Format("{0}\n [" + NGUITools.EncodeColor(Color.white) + "]{1}: {2}", formated, statsArray[i]["Name"], statsArray[i]["Value"]);
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
