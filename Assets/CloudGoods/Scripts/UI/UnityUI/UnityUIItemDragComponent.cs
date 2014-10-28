using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnityUIItemDragComponent : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public bool dragOnSurfaces = true;
	
	private RectTransform m_DraggingPlane;

    Transform lastParent;

	public void OnBeginDrag(PointerEventData eventData)
	{
		var canvas = FindInParents<Canvas>(gameObject);
		if (canvas == null)
			return;

		// We have clicked something that can be dragged.
		// What we want to do is create an icon for this.
        lastParent = gameObject.transform.parent;
		gameObject.transform.SetParent (canvas.transform, false);
        gameObject.transform.SetAsLastSibling();
		
		// The icon will be under the cursor.
		// We want it to be ignored by the event system.
        gameObject.AddComponent<IgnoreRaycast>();

		
		if (dragOnSurfaces)
			m_DraggingPlane = transform as RectTransform;
		else
			m_DraggingPlane = canvas.transform as RectTransform;
		
		SetDraggedPosition(eventData);
	}

	public void OnDrag(PointerEventData data)
	{
        if (gameObject != null)
			SetDraggedPosition(data);
	}

	private void SetDraggedPosition(PointerEventData data)
	{
		if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
			m_DraggingPlane = data.pointerEnter.transform as RectTransform;

        var rt = gameObject.GetComponent<RectTransform>();
		Vector3 globalMousePos;
		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
		{
			rt.position = globalMousePos;
			rt.rotation = m_DraggingPlane.rotation;
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
        //if (gameObject != null)
        //    Destroy(gameObject);
        gameObject.transform.SetParent(lastParent);

        if(GetComponent<IgnoreRaycast>())
            Destroy(GetComponent<IgnoreRaycast>());
	}

	static public T FindInParents<T>(GameObject go) where T : Component
	{
		if (go == null) return null;
		var comp = go.GetComponent<T>();

		if (comp != null)
			return comp;
		
		Transform t = go.transform.parent;
		while (t != null && comp == null)
		{
			comp = t.gameObject.GetComponent<T>();
			t = t.parent;
		}
		return comp;
	}
}
