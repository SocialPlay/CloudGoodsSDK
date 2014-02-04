using UnityEngine;
using System.Collections;

public class HasReceivedItemTextureTest : MonoBehaviour {

    public ItemTextureCache itemTextureCache;

    void Start()
    {
        itemTextureCache.ItemTextures.Clear();
        itemTextureCache.GetItemTexture("http://www.desicomments.com/dc3/01/209982/209982.gif", OnReceivedItemTexture);
    }

    void OnReceivedItemTexture(ItemTextureCache.ImageStatus statusMsg, Texture2D texture)
    {
        if (statusMsg == ItemTextureCache.ImageStatus.Web)
            IntegrationTest.Pass(gameObject);
        else
            IntegrationTest.Fail(gameObject);
    }
}
