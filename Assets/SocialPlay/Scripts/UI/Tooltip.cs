using UnityEngine;
using System.Collections;

public class Tooltip : MonoBehaviour
{
    static float delay = 0f;
    static Tooltip currentTooltip;
    ITooltipSetup toolTipSetup;


    void OnMouseEnter()
    {
        delay = 0f;
    }


    void OnMouseExit()
    {
        TooltipHandler.Hide();
    }

    void OnMouseOver()
    {
        if (currentTooltip == this)
        {
            return;
        }
        delay += Time.deltaTime;
        if (delay < 3)
        {
            OnTooltip(true);
            currentTooltip = this;
        }
    }

    void OnTooltip(bool show)
    {
     
        if (show)
        {
            Display();
        }
        else
        {
            TooltipHandler.Hide();
        }
    }

    public void Display()
    {
        toolTipSetup = GetComponent(typeof(ITooltipSetup)) as ITooltipSetup;

        TooltipHandler.Show(toolTipSetup.Setup());
    }
}
