using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;


public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    static float delay = 0f;
    static Tooltip currentTooltip;
    ITooltipSetup toolTipSetup;

    bool isOver = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer enter");
        delay = 0f;
        isOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        currentTooltip = null;
        TooltipHandler.Hide();
        isOver = false;
    }

    public void Update()
    {
        if (!isOver || currentTooltip == this) return;

        delay += Time.deltaTime;
        if (delay < 3)
        {
            Debug.Log("show tooltip");
            Display();
            currentTooltip = this;
        }
    }

    public void Display()
    {
        toolTipSetup = GetComponent(typeof(ITooltipSetup)) as ITooltipSetup;

        TooltipHandler.Show(toolTipSetup.Setup());
    }


}
