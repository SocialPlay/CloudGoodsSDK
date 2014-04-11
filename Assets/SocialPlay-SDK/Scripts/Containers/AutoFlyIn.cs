using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;


[RequireComponent(typeof(TweenPosition))]
public class AutoFlyIn : MonoBehaviour
{
    private TweenPosition tween = null;
    private ContainerDisplay myContainerDisplay = null;
    bool isCurrentlyActive = false;

    public int inDirPosition, outDirPosition;

    private Vector3 outPos, inPos;

    public direction myMoveDirection;

    private Vector3 startPos;

    public enum direction
    {
        horizontal, vertical, top, bottom, right, left
    }

    void Awake()
    {
        if (myContainerDisplay == null)
        {
            myContainerDisplay = gameObject.GetComponentInChildren<ContainerDisplay>();
        }

        if (myContainerDisplay == null)
        {
            myContainerDisplay = gameObject.GetComponent<ContainerDisplay>();
        }

        startPos = this.transform.localPosition;
        UpdatePos();


        if (myContainerDisplay != null)
        {
            if (myContainerDisplay.IsWindowActive())
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

    void Start()
    {
        if (myContainerDisplay.IsWindowActive())
        {
            this.transform.position = inPos;
            MoveIn();
        }
        else
        {
            this.transform.position = outPos;
            MoveOut();
        }
    }

    void Update()
    {
        if (myContainerDisplay != null)
        {
            if (myContainerDisplay.IsWindowActive() && !isCurrentlyActive)
            {
                MoveIn();
            }
            else if (!myContainerDisplay.IsWindowActive() && isCurrentlyActive)
            {
                MoveOut();
            }
        }
    }

    void MoveIn()
    {
        UpdatePos();
        tween.from = this.transform.localPosition;
        tween.to = inPos;
        tween.PlayForward();
        tween.enabled = true;
        isCurrentlyActive = true;
    }

    void MoveOut()
    {
        UpdatePos();
        tween.from = this.transform.localPosition;
        tween.to = outPos;
        tween.PlayForward();
        tween.enabled = true;
        isCurrentlyActive = false;
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
