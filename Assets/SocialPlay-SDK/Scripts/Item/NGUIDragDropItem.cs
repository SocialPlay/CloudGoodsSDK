
using UnityEngine;

[RequireComponent(typeof(ItemData))]
public class NGUIDragDropItem : MonoBehaviour
{

    ItemData myItemData;
    Transform mTrans;
    bool mIsDragging = false;
    bool mSticky = false;
    Transform mParent;
    Vector3 lastPosition = Vector3.zero;


    /// <summary>
    /// Update the table, if there is one.
    /// </summary>

    void UpdateTable()
    {
        UITable table = NGUITools.FindInParents<UITable>(gameObject);
        if (table != null) table.repositionNow = true;
    }

    /// <summary>
    /// Drop the dragged object.
    /// </summary>

    void Drop()
    {
        Collider col = UICamera.lastHit.collider;
        Debug.Log(col);
        ItemContainer container = (col != null) ? col.gameObject.GetComponent<ItemContainer>() : null;
        ReturnToPreviousPossition();
        if (container != null)
        {
            if (myItemData.ownerContainer != container)
            {
                ItemContainerManager.MoveItem(myItemData, container);
            }
        }
        // Notify the table of this change
        UpdateTable();

        // Make all widgets update their parents
        NGUITools.MarkParentAsChanged(gameObject);

    }


    void ReturnToPreviousPossition()
    {
        mTrans.parent = mParent;
        mTrans.position = lastPosition;
    }

    /// <summary>
    /// Cache the transform.
    /// </summary>

    void Awake()
    {
        mTrans = transform;
        myItemData = this.GetComponent<ItemData>();
    }

    /// <summary>
    /// Start the drag event and perform the dragging.
    /// </summary>

    void OnDrag(Vector2 delta)
    {
        if (enabled && UICamera.currentTouchID > -2)
        {
            if (!mIsDragging)
            {
                mIsDragging = true;
                mParent = mTrans.parent;
                mTrans.parent = UIDragDropRoot.root;

                Vector3 pos = mTrans.localPosition;
                pos.z = 0f;
                mTrans.localPosition = pos;

                NGUITools.MarkParentAsChanged(gameObject);
            }
            else
            {
                mTrans.localPosition += (Vector3)delta;
            }
        }
    }


    void OnDragStart()
    {
        Debug.Log("Test");
        collider.enabled = false;
        lastPosition = this.transform.position;
    }

    void OnDragEnd()
    {
        Collider col = collider;
        if (col != null) col.enabled = true;
        if ( mIsDragging) Drop();
    }

    ///// <summary>
    ///// Start or stop the drag operation.
    ///// </summary>

    //void OnPress(bool isPressed)
    //{
    //    if (isPressed)
    //    {
    //        lastPosition = this.transform.position;
    //    }

    //    if (enabled)
    //    {
    //        if (isPressed)
    //        {
    //            if (!UICamera.current.stickyPress)
    //            {
    //                mSticky = true;
    //                UICamera.current.stickyPress = true;
    //            }
    //        }
    //        else if (mSticky)
    //        {
    //            mSticky = false;
    //            UICamera.current.stickyPress = false;
    //        }


    //        Collider col = collider;
    //        if (col != null) col.enabled = !isPressed;
    //        if (!isPressed && mIsDragging) Drop();

    //        mIsDragging = false;
    //    }
    //}
}
