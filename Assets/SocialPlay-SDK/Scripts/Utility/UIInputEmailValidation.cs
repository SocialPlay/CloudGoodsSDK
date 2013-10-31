using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class UIInputEmailValidation : UIInputVisualValidation
{


    protected override bool Validate(string currentInput, bool isSecondcheck = false)
    {
        if (string.IsNullOrEmpty(currentInput))
        {
            return false;
        }
        bool isEmail = Regex.IsMatch(currentInput, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
        return isEmail;
    }
}
