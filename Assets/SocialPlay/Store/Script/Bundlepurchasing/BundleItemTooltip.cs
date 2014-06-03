using UnityEngine;
using System.Collections;

public class BundleItemTooltip : MonoBehaviour, ITooltipSetup
{
    BundleItemInfo bundleInfo;

    void Start()
    {
        bundleInfo = GetComponent<BundleItemInfo>();
    } 

    public string Setup()
    {
        if (bundleInfo.bundleItem == null)
            return "Hello";

        string formated = "[" + NGUIText.EncodeColor(ItemQuailityColorSelector.GetColorQuality(bundleInfo.bundleItem.Quality)) + "]" + bundleInfo.bundleItem.Name;

        foreach (BundleItemDetails detail in bundleInfo.bundleItem.bundleItemDetails)
        {

            formated = string.Format("{0}\n [" + NGUIText.EncodeColor(Color.white) + "]{1}: {2}", formated, detail.BundleDetailName, detail.Value);
        }
        return formated;
    }
}
