using UnityEngine;
using System.Collections;
using System;


public class TooltipHandler {

    public static event Action<bool, string> ChangeTooltip;    

    public static void Show(string tooltip)
    {       
        if (ChangeTooltip != null)
        {
            ChangeTooltip(true, tooltip);
        }
    }

    public static void Hide()
    {
        if (ChangeTooltip != null)
        {
            ChangeTooltip(false, null);
        }
    } 
}
