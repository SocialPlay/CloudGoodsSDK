using UnityEngine;
using System.Collections;

public class PlainTextTooltip : MonoBehaviour, ITooltipSetup
{
    public string tooltipText;

    public string Setup()
    {
        return tooltipText;
    }
}
