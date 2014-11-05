using UnityEngine;
using System.Collections;
using System;

public class DomainCheck : MonoBehaviour
{
    public event Action<string> onRecivedDomain;

    void Awake()
    {
        Debug.Log("Checking Domain");
        Application.ExternalEval("UnityObject2.instances[0].getUnity().SendMessage(\"" + name + "\", \"ReceiveURL\", document.URL);");
    }

    public void ReceiveURL(string url)
    {
        if (onRecivedDomain != null)
        {
            onRecivedDomain(url);
        }
        Destroy(this);
    }
}
