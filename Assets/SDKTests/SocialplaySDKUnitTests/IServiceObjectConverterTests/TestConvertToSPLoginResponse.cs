using UnityEngine;
using System.Collections;
using System;

public class TestConvertToSPLoginResponse : MonoBehaviour {

	// Use this for initialization
	void Start () {
        WebserviceCalls.webservice.SPLogin_UserLogin(Guid.Empty, "", "", OnReceivedSPLoginResponse);
	}

    void OnReceivedSPLoginResponse(SPLogin.SPLogin_Responce response)
    {
        if (response.code == 0)
            IntegrationTest.Pass(gameObject);
        else
            IntegrationTest.Fail(gameObject);
    }
}
