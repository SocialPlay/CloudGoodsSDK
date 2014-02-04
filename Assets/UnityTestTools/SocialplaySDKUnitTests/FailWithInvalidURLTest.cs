using UnityEngine;
using System.Collections;

public class FailWithInvalidURLTest : MonoBehaviour {

    public ItemTextureCache itemTextureCache;

    void Start()
    {
        itemTextureCache.ItemTextures.Clear();
        itemTextureCache.GetItemTexture("", OnReceivedItemTexture);
    }

    void OnReceivedItemTexture(ItemTextureCache.ImageStatus statusMsg, Texture2D texture)
    {
        if (statusMsg == ItemTextureCache.ImageStatus.Web)
            IntegrationTest.Pass(gameObject);
        else
            IntegrationTest.Fail(gameObject);
    }
}
