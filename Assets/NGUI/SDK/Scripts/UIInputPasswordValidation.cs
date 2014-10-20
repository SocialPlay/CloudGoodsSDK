using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class UIInputPasswordValidation : UIInputVisualValidation
{
    public int passwordRequiredLength = 6;
    public UIInput requiredMatchUI = null;

    protected override bool Validate(string currentInput, bool isSecondcheck = false)
    {

        if (requiredMatchUI != null)
        {
            if (!isSecondcheck)
            {
                foreach (UIInputVisualValidation validation in requiredMatchUI.GetComponentsInChildren<UIInputVisualValidation>())
                {
                    validation.IsValidCheck(true);
                }
            }
            if (requiredMatchUI.value != currentInput)
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
