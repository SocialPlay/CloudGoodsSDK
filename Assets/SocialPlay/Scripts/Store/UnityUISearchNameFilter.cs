using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class UnityUISearchNameFilter : MonoBehaviour {


    InputField input;
    string lastText = "";

    public static event Action<string> searchUpdate;

    void Awake()
    {
        input = GetComponentInChildren<InputField>();
    }

    void Update()
    {
        if (input != null && lastText != input.value)
        {
            lastText = input.value;
            if (searchUpdate != null)
            {
                searchUpdate(input.value);
            }
        }
    }
    
}
