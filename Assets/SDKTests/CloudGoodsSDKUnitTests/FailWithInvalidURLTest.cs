using UnityEngine;
using System.Collections;

public class FailWithInvalidURLTest : MonoBehaviour {

    void Start()
    {
        CloudGoods.ItemTextures.Clear();
        CloudGoods.GetItemTexture("", OnReceivedItemTexture);
    }

    void OnReceivedItemTexture(ImageStatus statusMsg, Texture2D texture)
    {
        if (statusMsg == ImageStatus.Error)
            IntegrationTest.Pass(gameObject);
        else
            IntegrationTest.Fail(gameObject);
    }
}
