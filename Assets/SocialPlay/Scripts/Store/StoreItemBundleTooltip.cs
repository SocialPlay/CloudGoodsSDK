using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoreItemBundleTooltip : MonoBehaviour, ITooltipSetup {

    UnityUIStoreItem item;

    public string Setup()
    {
        item = GetComponent<UnityUIStoreItem>();
        string formated = item.storeItem.itemName;

        foreach (StoreItemDetail detail in item.storeItem.itemDetail)
        {
            formated = string.Format("{0}\n{1}: {2}", formated, detail.propertyName, detail.propertyValue.ToString());
        }

        return formated;
    }
}
