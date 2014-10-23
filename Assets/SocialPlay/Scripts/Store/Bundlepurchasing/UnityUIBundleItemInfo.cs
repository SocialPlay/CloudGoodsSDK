using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnityUIBundleItemInfo : MonoBehaviour {

    public Text ItemName;
    public Text itemAmount;
    public Text ItemStats;

    public RawImage itemImage;

    public BundleItem bundleItem;

    public void SetupBundleItemDisplay(BundleItem newBundleItem)
    {
        bundleItem = newBundleItem;

        ItemName.text = bundleItem.Name;
        itemAmount.text = "Amount: " + bundleItem.Quantity;

        ItemStats.text = "";

        foreach (BundleItemDetails details in newBundleItem.bundleItemDetails)
        {
            ItemStats.text += details.BundleDetailName + " : " + details.Value + " \n";
        }

        CloudGoods.GetItemTexture(bundleItem.Image, OnReceivedItemTexture);
    }

    void OnReceivedItemTexture(ImageStatus imgStatus, Texture2D texture)
    {
        itemImage.texture = texture;
    }

}
