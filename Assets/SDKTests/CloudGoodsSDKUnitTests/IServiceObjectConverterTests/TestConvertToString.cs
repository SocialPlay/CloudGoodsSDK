using UnityEngine;
using System.Collections;

public class TestConvertToString : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CloudGoods.GetStandardCurrencyBalance(0, OnStringCallback);
	}

    void OnStringCallback(int stringCallback)
    {
        if (stringCallback == 1337)
            IntegrationTest.Pass(gameObject);
        else
            IntegrationTest.Fail(gameObject);
    }
}
