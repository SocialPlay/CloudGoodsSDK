using UnityEngine;
using System.Collections;

public class FailWithInvalidURLTest : MonoBehaviour {

    void Start()
    {
        SP.ItemTextures.Clear();
        SP.GetItemTexture("", OnReceivedItemTexture);
    }

    void OnReceivedItemTexture(ImageStatus statusMsg, Texture2D texture)
    {
        if (statusMsg == ImageStatus.Error)
            IntegrationTest.Pass(gameObject);
        else
            IntegrationTest.Fail(gameObject);
    }
}
