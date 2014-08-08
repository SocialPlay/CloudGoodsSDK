using UnityEngine;
using System.Collections;

public class CallWithGetTextureOnSameFrame : MonoBehaviour
{
    bool isFirstCall = true;

    void Start()
    {
        SP.ItemTextures.Clear();
        SP.GetItemTexture("http://www.desicomments.com/dc3/01/209982/209982.gif", OnReceivedItemTexture);
        SP.GetItemTexture("http://www.desicomments.com/dc3/01/209982/209982.gif", OnReceivedItemTexture); 
    }

    void OnReceivedItemTexture(ImageStatus statusMsg, Texture2D texture)
    {
        if (isFirstCall)
        {
            if (statusMsg != ImageStatus.Web)
                IntegrationTest.Fail(gameObject);

            isFirstCall = false;
        }
        else
        {
            if (statusMsg == ImageStatus.Cache)
                IntegrationTest.Pass(gameObject);
            else
                IntegrationTest.Fail(gameObject);
        }
    }

}
