using UnityEngine;
using System.Collections;


public class NGUIContainerGameItem : ItemDataComponent
{
    public UILabel itemAmountLabel;
    public UITexture uiTexture;
    public UISprite frameSprite;
    public UISprite updateSprite;

    int mLastStack;

    public override void SetData(ItemData item)
    {
        mData = item;
        frameSprite.color = ItemQuailityColorSelector.GetColorForItem(item);
        itemAmountLabel.text = item.stackSize.ToString();
        CloudGoods.GetItemTexture(item.imageName, OnReceivedTexture);

        if (mLastStack != item.stackSize)
        {
            mLastStack = item.stackSize;
            TweenAlpha.Begin(updateSprite.cachedGameObject, 1, 0).from = 1;
        }
        this.gameObject.name = item.itemName + " (" + item.varianceID + ")";
    }

    void OnReceivedTexture(ImageStatus statusMsg, Texture2D texture)
    {
        if (uiTexture.mainTexture != texture)
        {
            uiTexture.mainTexture = texture;
            TweenAlpha.Begin(uiTexture.cachedGameObject, 0.3f, 1).from = 0;
        }
    }
}
