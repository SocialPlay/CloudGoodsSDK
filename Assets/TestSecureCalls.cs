using UnityEngine;
using System.Collections;

public class TestSecureCalls : MonoBehaviour {

    public WebserviceCalls webservicecalls;

	// Use this for initialization
	void Start () {
        webservicecalls.GetToken(GameAuthentication.GetAppID(), "1", OnReceivedToken);
	}

    void OnReceivedToken(string token)
    {
        Debug.Log("Return from secure call: " + token);
    }

}
