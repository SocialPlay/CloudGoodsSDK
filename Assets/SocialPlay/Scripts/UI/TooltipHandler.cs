using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class TooltipHandler : MonoBehaviour
{
    private static TooltipHandler handler = null;
    public bool isFollowMouse;
    public Text tooltipText = null;
    public Image background = null;
    private bool isShowing = false;
    private Vector2 originalDelta = Vector2.zero;

    void Awake()
    {
        if (tooltipText == null)
            tooltipText = this.GetComponentInChildren<Text>();
        if (background != null)
        {
            originalDelta = background.rectTransform.sizeDelta;
        }
        if (handler == null)
        {
            handler = this;
        }
        Hide();
    }

    public static void Show(string tooltip)
    {
        if (handler == null) return;
        handler.gameObject.SetActive(true);
        handler.tooltipText.text = tooltip;
        handler.isShowing = true;
    }

    public static void Hide()
    {
        if (handler == null) return;
        handler.gameObject.SetActive(false);
        handler.isShowing = false;
    }

    void Update()
    {
        if (handler == this && handler.isShowing)
        {
            if (isFollowMouse)
            {
                Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.transform.localPosition.z);
                newPosition -= new Vector3(Screen.width / 2, Screen.height / 2, 0);
                this.transform.localPosition = newPosition;
            }
            if (background != null)
            {
                background.rectTransform.sizeDelta = tooltipText.rectTransform.sizeDelta + originalDelta;
            }
        }
    }
}
