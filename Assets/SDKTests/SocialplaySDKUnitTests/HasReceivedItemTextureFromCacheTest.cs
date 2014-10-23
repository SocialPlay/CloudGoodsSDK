using UnityEngine;
using System.Collections;

public class HasReceivedItemTextureFromCacheTest : MonoBehaviour {

    void Start()
    {
        CloudGoods.ItemTextures.Clear();
        CloudGoods.GetItemTexture("http://www.desicomments.com/dc3/01/209982/209982.gif", OnReceivedItemTexture);
    }

    void OnReceivedItemTexture(ImageStatus statusMsg, Texture2D texture)
    {
        if (statusMsg == ImageStatus.Web)
            CloudGoods.GetItemTexture("http://www.desicomments.com/dc3/01/209982/209982.gif", OnReceivedImageTextureTwo);
        else
            IntegrationTest.Fail(gameObject);
    }

    void OnReceivedImageTextureTwo(ImageStatus statusMsg, Texture2D texture)
    {
        if (statusMsg == ImageStatus.Cache)
            IntegrationTest.Pass(gameObject);
        else
            IntegrationTest.Fail(gameObject);
    }
}
