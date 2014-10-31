using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class UnityUITooltipHandler : MonoBehaviour
{
    public Text tooltipText = null;
    public Image background = null;
    private bool isShowing = false;
    private Vector2 originalDelta = Vector2.zero;
    public bool isFollowMouse;

    void Awake()
    {
        if (tooltipText == null)
            tooltipText = this.GetComponentInChildren<Text>();
        if (background != null)
        {
            originalDelta = background.rectTransform.sizeDelta;
        }
        TooltipHandler.ChangeTooltip += TooltipHandler_ChangeTooltip;      
    }

    void Start()
    {
        TooltipHandler_ChangeTooltip(false, null);  
    }

    void TooltipHandler_ChangeTooltip(bool show, string tooltip)
    {     
        if (show)
        {        
            tooltipText.gameObject.SetActive(true);
            background.gameObject.SetActive(true);
            tooltipText.text = tooltip;
            isShowing = true;
        }
        else
        {  
            tooltipText.gameObject.SetActive(false);
            background.gameObject.SetActive(false);
            isShowing = false;
        }
    }  

    void Update()
    { 
        if (isFollowMouse)
        {
            Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.transform.localPosition.z);
            newPosition -= new Vector3(Screen.width / 2, Screen.height / 2, 0);
            this.transform.localPosition = newPosition;
        }
        if (background != null && isShowing)
        {
            background.rectTransform.sizeDelta = tooltipText.rectTransform.sizeDelta + originalDelta;
        }
    }
}
