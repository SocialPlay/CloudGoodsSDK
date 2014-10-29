using UnityEngine;
using System.Collections;

public class DisableFBButtonOnLogin : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        if (FB.IsLoggedIn)
        {
            gameObject.SetActive(false);
        }
	}
}
