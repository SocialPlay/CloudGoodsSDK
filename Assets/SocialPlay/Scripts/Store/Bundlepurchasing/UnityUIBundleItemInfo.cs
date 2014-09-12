using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnityUIBundleItemInfo : MonoBehaviour {

    public Text ItemName;
    public Text itemAmount;

    public RawImage itemImage;

    public BundleItem bundleItem;

    public void SetupBundleItemDisplay(BundleItem newBundleItem)
    {

        bundleItem = newBundleItem;

        ItemName.text = bundleItem.Name;
        itemAmount.text = "Amount: " + bundleItem.Quantity;

        SP.GetItemTexture(bundleItem.Image, OnReceivedItemTexture);
    }

    void OnReceivedItemTexture(ImageStatus imgStatus, Texture2D texture)
    {
        itemImage.texture = texture;
    }

}
