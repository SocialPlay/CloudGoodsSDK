using UnityEngine;
using System.Collections;

public class PurchaseButtonDisaply : MonoBehaviour
{
    public UIButton ActiveButton;
    public UILabel InactiveLabel;
    public string ReplacmentText = "";


    public void SetInactive()
    {
        if (!string.IsNullOrEmpty(ReplacmentText) && ReplacmentText != InactiveLabel.text)
        {
            InactiveLabel.text = ReplacmentText;
        }
        ActiveButton.gameObject.SetActive(false);
        InactiveLabel.gameObject.SetActive(true);
    }

    public void SetActive()
    {
        if (!string.IsNullOrEmpty(ReplacmentText) && ReplacmentText != InactiveLabel.text)
        {
            InactiveLabel.text = ReplacmentText;
        }
        ActiveButton.gameObject.SetActive(true);
        InactiveLabel.gameObject.SetActive(false);
    }

    public void SetState(bool state)
    {
        if (state)
        {
            SetActive();
        }
        else
        {
            SetInactive();
        }
    }
}
