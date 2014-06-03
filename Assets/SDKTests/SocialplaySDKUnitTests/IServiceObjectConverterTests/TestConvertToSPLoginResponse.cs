using UnityEngine;
using System.Collections;
using System;

public class TestConvertToSPLoginResponse : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SP.Login("", "", OnReceivedSPLoginResponse);
	}

    void OnReceivedSPLoginResponse(SP.UserResponse response)
    {
        if (response.code == 0)
            IntegrationTest.Pass(gameObject);
        else
            IntegrationTest.Fail(gameObject);
    }
}
