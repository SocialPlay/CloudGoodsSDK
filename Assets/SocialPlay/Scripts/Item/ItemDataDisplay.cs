using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemDataDisplay : MonoBehaviour
{
    internal ItemDataComponent itemObject;
    internal ItemContainerDisplay holdingContainer;
    public Text amountText;
    public Image itemImage;

    //MouseClicks
    public bool first_click = false;
    public float running_timer = 0;
    private float delay = 0.3f;

    void OnMouseUpAsButton()//Click
    {
        holdingContainer.PerformSingleClick(itemObject);
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(1))
        {
            holdingContainer.PerformRightClick(itemObject);
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (!first_click)
            {
                first_click = true;
                running_timer = 0;
            }
            else
            {
                first_click = false;
                if (running_timer < delay)
                {
                    ///Double click
                    holdingContainer.PerformDoubleClick(itemObject);
                }
            }
        }
        if (first_click)
        {
            running_timer += Time.deltaTime;
        }
    }

    void OnMouseExit()
    {
        first_click = false;
        running_timer = 0;
    }

    public void Start()
    {
        holdingContainer = this.transform.GetComponentInParent<ItemContainerDisplay>();
        itemObject = this.GetComponent<ItemDataComponent>();

        if (amountText != null) amountText.text = itemObject.itemData.stackSize.ToString();
        SP.GetItemTexture(itemObject.itemData.imageName, OnReceivedItemTexture);
    }


    void OnReceivedItemTexture(ImageStatus statusMsg, Texture2D texture)
    {
        if (statusMsg != ImageStatus.Error && itemImage != null)
        {
            Debug.Log("Load Image");
            itemImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }
    }
}
