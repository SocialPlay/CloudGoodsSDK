using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ItemDataDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    internal ItemDataComponent itemObject;
    internal ItemContainerDisplay holdingContainer;
    public Text amountText;
    public RawImage itemImage;
    public Image itemFrame;


    //MouseClicks
    bool isOver = false;
    public bool first_click = false;
    public float running_timer = 0;
    private float delay = 0.3f;


    public void OnPointerEnter(PointerEventData eventData)
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

    void Update()
    {
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
                Debug.Log("Second click");
                Debug.Log
                    ("Double click: " + (running_timer <= delay));
                first_click = false;
                if (running_timer <= delay)
                {
                    holdingContainer.PerformDoubleClick(itemObject);
                }
            }
            else
            {
                first_click = true;
                Debug.Log("First click");
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            holdingContainer.PerformRightClick(itemObject);
        }

        if (amountText != null) amountText.text = itemObject.itemData.stackSize.ToString();
    }


    public void Start()
    {
        holdingContainer = this.transform.GetComponentInParent<ItemContainerDisplay>();
        itemObject = this.GetComponent<ItemDataComponent>();

        if (amountText != null) amountText.text = itemObject.itemData.stackSize.ToString();
        CloudGoods.GetItemTexture(itemObject.itemData.imageName, OnReceivedItemTexture);

        if (itemFrame != null) itemFrame.color = ItemQuailityColorSelector.GetColorForItem(itemObject.itemData);
    }


    void OnReceivedItemTexture(ImageStatus statusMsg, Texture2D texture)
    {
        if (statusMsg != ImageStatus.Error && itemImage != null)
        {
            itemImage.texture = texture;
        }
    }





}
