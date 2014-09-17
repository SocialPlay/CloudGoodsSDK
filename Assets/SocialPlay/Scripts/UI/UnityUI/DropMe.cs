using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropMe : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public void OnDrop(PointerEventData data)
	{
        var originalObj = data.pointerDrag;
        if (originalObj == null)
            return;

        if(originalObj.GetComponent<ItemDataComponent>().itemData.ownerContainer != GetComponent<ItemContainer>())
            ItemContainerManager.MoveItem(originalObj.GetComponent<ItemDataComponent>().itemData, originalObj.GetComponent<ItemDataComponent>().itemData.ownerContainer, GetComponent<ItemContainer>());
	}

	public void OnPointerEnter(PointerEventData data)
	{
	}

	public void OnPointerExit(PointerEventData data)
	{
	}
	
}
