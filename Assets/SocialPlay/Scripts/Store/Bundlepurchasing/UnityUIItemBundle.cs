using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnityUIItemBundle : MonoBehaviour {

    public UnityUIBundlePurchasing bundlePurchasing;
    public ItemBundle itemBundle;

    public RawImage BundleImage;

    public void SetupUnityUIItemBundle(ItemBundle newItemBundle, UnityUIBundlePurchasing purchasing)
    {
        itemBundle = newItemBundle;
        bundlePurchasing = purchasing;

        SP.GetItemTexture(itemBundle.Image, OnReceivedItemTexture);

        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClickedItemBundle);
    }

    public void OnClickedItemBundle()
    {
        bundlePurchasing.gameObject.SetActive(true);
        bundlePurchasing.SetupBundlePurchaseDetails(itemBundle);
    }

    void OnReceivedItemTexture(ImageStatus status, Texture2D texture)
    {
        BundleImage.texture = texture;
    }
}
