using UnityEngine;
using System.Collections;

public class BundleItemInfo : MonoBehaviour {

    public UILabel ItemName;
    public UILabel itemAmount;

    public UITexture itemImage;

    public BundleItem bundleItem;

    public void SetupBundleItemDisplay(BundleItem newBundleItem)
    {

        bundleItem = newBundleItem;

        ItemName.text = bundleItem.Name;
        itemAmount.text = "Amount: " + bundleItem.Quantity;

        CloudGoods.GetItemTexture(bundleItem.Image, OnReceivedItemTexture);
    }

    void OnReceivedItemTexture(ImageStatus imgStatus, Texture2D texture)
    {
        GetComponentInChildren<UITexture>().mainTexture = texture;
    }

}
