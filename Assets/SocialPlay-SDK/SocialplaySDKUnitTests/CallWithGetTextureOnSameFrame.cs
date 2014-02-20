using UnityEngine;
using System.Collections;

public class CallWithGetTextureOnSameFrame : MonoBehaviour
{
    public ItemTextureCache itemTextureCache;

    bool isFirstCall = true;

    void Start()
    {
        ItemTextureCache.instance.ItemTextures.Clear();
        ItemTextureCache.instance.GetItemTexture("http://www.desicomments.com/dc3/01/209982/209982.gif", OnReceivedItemTexture);
        ItemTextureCache.instance.GetItemTexture("http://www.desicomments.com/dc3/01/209982/209982.gif", OnReceivedItemTexture); 
    }

    void OnReceivedItemTexture(ItemTextureCache.ImageStatus statusMsg, Texture2D texture)
    {
        if (isFirstCall)
        {
            if (statusMsg != ItemTextureCache.ImageStatus.Web)
                IntegrationTest.Fail(gameObject);

            isFirstCall = false;
        }
        else
        {
            if (statusMsg == ItemTextureCache.ImageStatus.Cache)
                IntegrationTest.Pass(gameObject);
            else
                IntegrationTest.Fail(gameObject);
        }
    }

}
