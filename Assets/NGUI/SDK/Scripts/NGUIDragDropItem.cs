
using UnityEngine;

[RequireComponent(typeof(ItemDataComponent))]
public class NGUIDragDropItem : MonoBehaviour
{
    public static bool IsDraggingItem = false;

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
        Debug.Log("Draging");
        if (enabled && UICamera.currentTouchID > -2)
        {
            mTrans.parent = UIDragDropRoot.root;
            mIsDragging = true;        
            mTrans.localPosition += (Vector3)delta;
            IsDraggingItem = true;
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
        IsDraggingItem = false;
    }
}
