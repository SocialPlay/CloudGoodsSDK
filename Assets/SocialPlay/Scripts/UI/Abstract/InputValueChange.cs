using UnityEngine;
using System.Collections;
using System;

public abstract class InputValueChange : MonoBehaviour
{

    string lastText = "";
    string currentText = "";

    public event Action<string> searchUpdate;

    protected virtual void Update()
    {
        currentText = GetCurrentValue();
        if (lastText != currentText)
        {
            lastText = currentText;
            if (searchUpdate != null)
            {
                searchUpdate(currentText);
            }
        }
    }

    void OnSubmit(string currentString)
    {
        if (searchUpdate != null)
        {
            searchUpdate(currentString);
        }
    }

    protected abstract string GetCurrentValue();
}
