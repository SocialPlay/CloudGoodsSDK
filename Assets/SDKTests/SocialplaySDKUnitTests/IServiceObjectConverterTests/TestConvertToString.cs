using UnityEngine;
using System.Collections;

public class TestConvertToString : MonoBehaviour {

	// Use this for initialization
	void Start () {
        WebserviceCalls.webservice.GetFreeCurrencyBalance("", 0, "", OnStringCallback);
	}

    void OnStringCallback(string stringCallback)
    {
        if (stringCallback == "1337")
            IntegrationTest.Pass(gameObject);
        else
            IntegrationTest.Fail(gameObject);
    }
}
