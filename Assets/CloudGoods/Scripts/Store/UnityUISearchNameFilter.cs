using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class UnityUISearchNameFilter : InputValueChange
{
     public InputField input;

    void Awake()
    {
        input = GetComponent<InputField>();
        if (input == null)
        {
            input = GetComponentInChildren<InputField>();
        }
        
    }

    protected override void Update()
    {
        if (input != null)
        {
            base.Update();
        }
    }

    protected override string GetCurrentValue()
    {     
        return input.text;
    }
}
