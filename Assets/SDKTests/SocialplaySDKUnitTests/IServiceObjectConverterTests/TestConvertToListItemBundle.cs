using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestConvertToListItemBundle : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SP.GetItemBundles(OnReceivedItemBundles);
	}

    void OnReceivedItemBundles(List<ItemBundle> itemBundles)
    {
        if (itemBundles[0].Name == "Test Bundle")
            IntegrationTest.Pass(gameObject);
        else
            IntegrationTest.Fail(gameObject);
    }
}
