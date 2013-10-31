using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;


[RequireComponent(typeof(TweenPosition))]
public class AutoFlyIn : MonoBehaviour
{
    private TweenPosition tween = null;
    private ItemContainer myItemContainer = null;
    bool isCurrentlyActive = false;
    public bool hasAlternatePos = false;

    public int inDirPosition, outDirPosition;

    public Vector3 alternatOffset = Vector3.zero;
    private Vector3 outPos, inPos;

    public direction myMoveDirection;

    private Vector3 startPos;

    public enum direction
    {
        horizontal, vertical, top, bottom, right, left
    }

    void Awake()
    {
        if (myItemContainer == null)
        {
            myItemContainer = gameObject.GetIComponentInChildren<ItemContainer>();
        }

        if (myItemContainer == null)
        {
            myItemContainer = gameObject.GetIComponent<ItemContainer>();
        }

        startPos = this.transform.localPosition;
        UpdatePos();


        if (myItemContainer != null)
        {
            if (myItemContainer.IsActive)
                this.transform.localPosition = inPos;
            else
                this.transform.localPosition = outPos;
        }

        if (tween == null)
        {
            tween = this.GetComponent<TweenPosition>();
            tween.enabled = false;
        }
    }

    void Update()
    {
        if (myItemContainer != null)
        {
            if (myItemContainer.IsActive && !isCurrentlyActive)
            {
                UpdatePos();
                tween.from = this.transform.localPosition;
                tween.to = inPos;
                tween.Reset();
                tween.enabled = true;
                isCurrentlyActive = true;
            }
            else if (!myItemContainer.IsActive && isCurrentlyActive)
            {
                UpdatePos();
                tween.from = this.transform.localPosition;
                tween.to = outPos;
                tween.Reset();
                tween.enabled = true;
                isCurrentlyActive = false;
            }
        }
    }

    void UpdatePos()
    {
        inPos = outPos = startPos;
        switch (myMoveDirection)
        {
            case direction.horizontal:
                inPos.x = inDirPosition;
                outPos.x = outDirPosition;
                break;
            case direction.vertical:
                inPos.y = inDirPosition;
                outPos.y = outDirPosition;
                break;
            case direction.top:
                inPos.y = inDirPosition;
                outPos.y = inDirPosition + Screen.height;
                break;
            case direction.bottom:
                inPos.y = inDirPosition;
                outPos.y = inDirPosition - Screen.height;
                break;
            case direction.right:
                inPos.x = inDirPosition;
                outPos.x = inDirPosition + Screen.height;
                break;
            case direction.left:
                inPos.x = inDirPosition;
                outPos.x = inDirPosition - Screen.height;
                break;
            default:
                break;
        }
    }
}
