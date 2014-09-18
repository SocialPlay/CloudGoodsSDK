using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System;

public class SearchNameFilter : InputValueChange
{
    UIInput input;

    void Awake()
    {
        input = GetComponentInChildren<UIInput>();
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
        return input.value;
    }
}
