using UnityEngine;
using System.Collections;
using System;

public class TestPlatformLogin : MonoBehaviour {

    public WebPlatformLogin webplatformLogin;

	// Use this for initialization
	void Start () {
        webplatformLogin.LoginWithPlatformUser(new Guid("2882f02f-4e91-4329-a051-0de9301b3e79"), 4, "abcdefg", "LionelAndrew");
	}

}
