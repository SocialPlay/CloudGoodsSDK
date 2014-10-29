using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnityUIBundleItemScroll : MonoBehaviour {

    public Text ItemName;
    public Text ItemDetails;
    public RawImage ItemImage;

    public UnityUIBundlePurchasing bundlePurchasing;

    ItemBundle itemBundle;

    int currentBundleIndex = 0;

    void Start()
    {
        currentBundleIndex = 0;
        itemBundle = bundlePurchasing.currentItemBundle;
        SetBundleItemToDisplay(currentBundleIndex);
    }

    void Update()
    {
        if (bundlePurchasing.currentItemBundle != itemBundle)
        {
            currentBundleIndex = 0;
            itemBundle = bundlePurchasing.currentItemBundle;
            SetBundleItemToDisplay(currentBundleIndex);
        }
    }

    void SetBundleItemToDisplay(int index)
    {
        BundleItem bundleitem = itemBundle.bundleItems[index];

        ItemName.text = bundleitem.Name;

        string formated = "";

        foreach (BundleItemDetails detail in bundleitem.bundleItemDetails)
        {
            formated = string.Format("{0}{1}: {2}\n", formated, detail.BundleDetailName, detail.Value.ToString());
        }

        ItemDetails.text = formated;

        CloudGoods.GetItemTexture(bundleitem.Image, OnReceivedItemTexture);
    }

    void OnReceivedItemTexture(ImageStatus imageStatus, Texture2D newTexture)
    {
        ItemImage.texture = newTexture;
    }

    public void DisplayNextBundleItem()
    {
        if (currentBundleIndex >= itemBundle.bundleItems.Count - 1)
            return;

        else
        {
            currentBundleIndex++;
            SetBundleItemToDisplay(currentBundleIndex);
        }
    }

    public void DisplayPreviousBundleItem()
    {
        if (currentBundleIndex <= 0)
            return;

        else
        {
            currentBundleIndex--;
            SetBundleItemToDisplay(currentBundleIndex);
        }
    }
}
