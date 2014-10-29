using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NGUIItemBundle : MonoBehaviour
{

    public BundlePurchasing bundlePurchasing;
    public ItemBundle itemBundle;

    public UITexture BundleImage;

    public void SetupNGUIItemBundle(ItemBundle newItemBundle, BundlePurchasing purchasing)
    {
        itemBundle = newItemBundle;
        bundlePurchasing = purchasing;
        if (BundleImage != null)
            CloudGoods.GetItemTexture(itemBundle.Image, OnReceivedItemTexture);
    }

    public void OnClickedItemBundle()
    {
        bundlePurchasing.gameObject.SetActive(true);
        bundlePurchasing.SetupBundlePurchaseDetails(itemBundle);
    }


    void OnReceivedItemTexture(ImageStatus status, Texture2D texture)
    {

        BundleImage.mainTexture = texture;
    }
}
