using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemDataDisplay : MonoBehaviour
{   
    internal ItemDataComponent itemObject;
    internal ItemContainerDisplay holdingContainer;
    public Text amountText;
    public Image itemImage;

     void OnMouseUpAsButton()//Click
    {
         holdingContainer.PerformSingleClick(itemObject);
    }

     void  OnMouseOver()
    {
        if (Input.GetMouseButton(1))
        {
            holdingContainer.PerformRightClick(itemObject);
        }
    }

    public void Start()
    {
        holdingContainer = this.transform.GetComponentInParent<ItemContainerDisplay>();
        itemObject=this.GetComponent<ItemDataComponent>();
      
        if (amountText != null) amountText.text =itemObject. itemData.stackSize.ToString();
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
