using UnityEngine;
using System.Collections;

public class TestSecureCalls : MonoBehaviour 
{
	void Start () 
    {
        SP.GetToken("1", OnReceivedToken);
	}

    void OnReceivedToken(string token)
    {
        Debug.Log("Return from secure call: " + token);
    }

}
