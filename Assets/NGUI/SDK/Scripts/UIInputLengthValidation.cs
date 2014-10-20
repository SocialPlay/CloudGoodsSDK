using UnityEngine;
using System.Collections;

public class UIInputLengthValidation : UIInputVisualValidation
{

    public int lengthRequired = 3;
    protected override bool Validate(string currentInput, bool isSecondcheck = false)
    {
        return currentInput.Trim().Length >= lengthRequired;
    }

}
