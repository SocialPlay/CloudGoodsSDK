using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemDragdrop : MonoBehaviour {

    Transform canvasObject;
    Transform lastParent;

    Vector3 lastPosition;

    bool isDragging = false;

    void Start()
    {
        canvasObject = GameObject.Find("Canvas").transform;
    }

    void OnMouseDrag()
    {
        if (isDragging == false)
        {
            isDragging = true;
            lastPosition = transform.localPosition;
            lastParent = transform.parent;
        }

        Vector3 newPosition = Input.mousePosition;

        newPosition -= new Vector3(Screen.width / 2, Screen.height / 2, 0);
        transform.parent = canvasObject;

        transform.localPosition = newPosition;
    }

    void OnMouseUp()
    {
        if (isDragging)
        {
            Collider col = collider;
            if (col != null) col.enabled = true;

            transform.parent = lastParent;
            transform.localPosition = lastPosition;
            Debug.Log("Item Dropped");
            isDragging = false;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("collided with: " + collider.gameObject.name);
    }
}
