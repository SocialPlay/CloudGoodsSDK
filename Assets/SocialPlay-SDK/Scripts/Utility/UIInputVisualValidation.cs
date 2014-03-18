using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UIInput))]
public abstract class UIInputVisualValidation : MonoBehaviour
{

    UIInput uiInput;

    void Start()
    {
        if (this.GetComponent<UIInput>())
        {
            uiInput = this.GetComponent<UIInput>();
        }
    }

    void OnInput(string input)
    {
        IsValidCheck();
    }

   


    public bool IsValidCheck(bool isSecondcheck = false)
    {
        if (Validate(uiInput.value, isSecondcheck))
        {
            foreach (UISprite sprite in uiInput.GetComponentsInChildren<UISprite>())
            {
                sprite.color = Color.white;
            }
            return true;
        }
        else
        {
            foreach (UISprite sprite in uiInput.GetComponentsInChildren<UISprite>())
            {
                sprite.color = Color.red;
            }
            return false;
        }
    }



    protected abstract bool Validate(string currentInput, bool isSecondcheck = false);

}
