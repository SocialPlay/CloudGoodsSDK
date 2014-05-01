using UnityEngine;
using System.Collections;

public class BundleItemInfo : MonoBehaviour {

    public UILabel ItemName;
    public UILabel itemAmount;

    public UITexture itemImage;

    public void SetupBundleItemDisplay(BundleItem bundleItem)
    {
        ItemName.text = bundleItem.Name;
        itemAmount.text = "Amount: " + bundleItem.Quantity;

        ItemTextureCache.instance.GetItemTexture(bundleItem.Image, OnReceivedItemTexture);
    }

    void OnReceivedItemTexture(ItemTextureCache.ImageStatus imgStatus, Texture2D texture)
    {
        GetComponentInChildren<UITexture>().mainTexture = texture;
    }

}
