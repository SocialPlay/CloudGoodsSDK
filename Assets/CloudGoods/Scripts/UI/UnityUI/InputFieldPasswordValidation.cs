using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputFieldPasswordValidation : InputFieldValidation {

    public int passwordRequiredLength = 6;
    public InputField requiredMatchUI = null;

    protected override bool Validate(string currentInput, bool isSecondcheck = false)
    {

        if (requiredMatchUI != null)
        {
            if (!isSecondcheck)
            {
                foreach (InputFieldValidation validation in requiredMatchUI.GetComponentsInChildren<InputFieldValidation>())
                {
                    validation.IsValidCheck(true);
                }
            }
            if (requiredMatchUI.text != currentInput)
            {
                return false;
            }
        }

        if (string.IsNullOrEmpty(currentInput))
        {
            return false;
        }

        if (currentInput.Length < passwordRequiredLength)
        {
            return false;
        }

        return true;
    }
}
