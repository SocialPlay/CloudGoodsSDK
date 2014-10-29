
using UnityEngine;

[RequireComponent(typeof(ItemDataComponent))]
public class NGUIDragDropItem : MonoBehaviour
{

    public ItemData myItemData
    {
        get
        {
            if (data == null) data = this.GetComponent<ItemDataComponent>().itemData;
            return data;
        }
        set
        {
            data = value;
        }
    }
    private ItemData data;

    Transform mTrans;
    bool mIsDragging = false;
    //bool mSticky = false;
    Transform lastParent;
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
        ReturnToPreviousPossition();
        Collider col = UICamera.lastHit.collider;
        Debug.Log(col);
        ItemContainer container = (col != null) ? col.gameObject.GetComponent<ItemContainer>() : null;


        if (container == null)
        {
            container = (col != null) ? col.transform.parent.parent.gameObject.GetComponent<ItemContainer>() : null;
        }

        if (container != null)
        {
            if (myItemData.ownerContainer != container)
            {
                ItemContainerManager.MoveItem(myItemData, myItemData.ownerContainer, container);
            }
        }
        // Notify the table of this change
        UpdateTable();

        // Make all widgets update their parents
        NGUITools.MarkParentAsChanged(gameObject);

    }


    void ReturnToPreviousPossition()
    {
        mTrans.parent = lastParent;
        mTrans.position = lastPosition;
    }

    /// <summary>
    /// Cache the transform.
    /// </summary>

    void Awake()
    {
        mTrans = transform;
    }

    /// <summary>
    /// Start the drag event and perform the dragging.
    /// </summary>

    void OnDrag(Vector2 delta)
    {
        if (enabled && UICamera.currentTouchID > -2)
        {
            //if (!mIsDragging)
            //{
            mIsDragging = true;
            //    mParent = mTrans.parent;
            //    mTrans.parent = UIDragDropRoot.root;

            //    //Vector3 pos = mTrans.localPosition;
            //    //pos.z = 0f;
            //    //mTrans.localPosition = pos;

            //    NGUITools.MarkParentAsChanged(gameObject);

            //    mTrans.localPosition += (Vector3)delta;

            //}
            //else
            //{
            mTrans.localPosition += (Vector3)delta;
            //}
        }
    }


    void OnDragStart()
    {
        collider.enabled = false;
        lastParent = this.transform.parent;
        lastPosition = this.transform.position;
    }

    void OnDragEnd()
    {
        Collider col = collider;
        if (col != null) col.enabled = true;
        if (mIsDragging) Drop();
        mIsDragging = false;
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
