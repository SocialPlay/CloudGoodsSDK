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
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(200, 200, 0));
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

            RaycastHit Hit;

            if (Physics.Raycast(ray, out Hit, 100))
            {
                if (Hit.collider.gameObject.GetComponent<ItemContainer>())
                {
                    Debug.Log("Hit container");
                }
            }

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
