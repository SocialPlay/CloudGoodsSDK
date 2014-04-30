using UnityEngine;
using System.Collections;

public class HasReceivedItemTextureFromWebTest : MonoBehaviour {

    public ItemTextureCache itemTextureCache;

    void Start()
    {
        ItemTextureCache.instance.ItemTextures.Clear();
        ItemTextureCache.instance.GetItemTexture("http://www.desicomments.com/dc3/01/209982/209982.gif", OnReceivedItemTexture);
    }

    void OnReceivedItemTexture(ItemTextureCache.ImageStatus statusMsg, Texture2D texture)
    {
        if (statusMsg == ItemTextureCache.ImageStatus.Web)
            IntegrationTest.Pass(gameObject);
        else
            IntegrationTest.Fail(gameObject);
    }
}
