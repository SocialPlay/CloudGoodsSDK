using UnityEngine;
using System.Collections;

public class NGUITooltip : MonoBehaviour {
   // GameObject currentCustomPanel;
    ITooltipSetup toolTipSetup;

    void OnTooltip(bool show)
    {
        if (show)
        {
            Display();
        }
        else
        {
            Hide();
        }
    }

    public void Display()
    {
        toolTipSetup = GetComponent(typeof(ITooltipSetup)) as ITooltipSetup;

		UITooltip.ShowText(toolTipSetup.Setup());
    }
  

    public void Hide()
    {
        UITooltip.ShowText(null);
    }
}
