using UnityEngine;
using System.Collections;

public class FailWithInvalidURLTest : MonoBehaviour {

    public ItemTextureCache itemTextureCache;

    void Start()
    {
        ItemTextureCache.instance.ItemTextures.Clear();
        ItemTextureCache.instance.GetItemTexture("", OnReceivedItemTexture);
    }

    void OnReceivedItemTexture(ItemTextureCache.ImageStatus statusMsg, Texture2D texture)
    {
        if (statusMsg == ItemTextureCache.ImageStatus.Error)
            IntegrationTest.Pass(gameObject);
        else
            IntegrationTest.Fail(gameObject);
    }
}
