using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public abstract class ItemDataDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    internal ItemDataComponent itemObject;
    internal ItemContainerDisplay holdingContainer;



    //MouseClicks
    private bool isOver = false;
    private bool first_click = false;
    private float running_timer = 0;
    private float delay = 0.3f;

  


    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        isOver = true;
        first_click = false;
        running_timer = 0;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOver = false;
        first_click = false;
        running_timer = 0;
    }

    protected void Update()
    {
        SetAmountText(itemObject.itemData.stackSize.ToString());

        if (first_click)
        {
            running_timer += Time.deltaTime;
        }
        if (!isOver) return;
        if (Input.GetMouseButtonUp(0))
        {
            holdingContainer.PerformSingleClick(itemObject);
            if (!first_click)
            {
                first_click = false;
                if (running_timer <= delay)
                {
                    holdingContainer.PerformDoubleClick(itemObject);
                }
            }
            else
            {
                first_click = true;
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            holdingContainer.PerformRightClick(itemObject);
        }
    }


    public void Start()
    {
        holdingContainer = this.transform.GetComponentInParent<ItemContainerDisplay>();
        itemObject = this.GetComponent<ItemDataComponent>();


        SetAmountText(itemObject.itemData.stackSize.ToString());
        CloudGoods.GetItemTexture(itemObject.itemData.imageName, OnReceivedItemTexture);
        SetFrameColor(ItemQuailityColorSelector.GetColorForItem(itemObject.itemData));


    }


    void OnReceivedItemTexture(ImageStatus statusMsg, Texture2D texture)
    {
        if (statusMsg != ImageStatus.Error)
        {
            UpdateTexture(texture);
        }
    }

    public abstract void UpdateTexture(Texture2D newTexture);

    public abstract void SetFrameColor(Color newColor);

    public abstract void SetAmountText(string newAmount);

}
