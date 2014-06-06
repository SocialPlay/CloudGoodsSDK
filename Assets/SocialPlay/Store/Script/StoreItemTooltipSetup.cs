using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;

public class StoreItemTooltipSetup : MonoBehaviour, ITooltipSetup
{
	UIStoreItem item;

    public string Setup()
    {
		item = GetComponent<UIStoreItem>();
        string formated = "[" + NGUIText.EncodeColor(GetColorQuality(1)) + "]" + item.storeItem.itemName;

		foreach (StoreItemDetail detail in item.storeItem.itemDetail)
        {
            Debug.Log(detail.propertyName);
            formated = string.Format("{0}\n [" + NGUIText.EncodeColor(Color.white) + "]{1}: {2}", formated, detail.propertyName, detail.propertyValue.ToString());
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
