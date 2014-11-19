using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class NGUIItemDataDisplay : ItemDataDisplay
{
    public UILabel itemAmountLabel;
    public UITexture uiTexture;
    public UISprite frameSprite;

 

    public override void UpdateTexture(Texture2D newTexture)
    {
        if (uiTexture.mainTexture != newTexture)
        {
            uiTexture.mainTexture = newTexture;
            TweenAlpha.Begin(uiTexture.cachedGameObject, 0.3f, 1).from = 0;
        }       
    }

    public override void SetFrameColor(Color newColor)
    {
        frameSprite.color = newColor;
    }

    public override void SetAmountText(string newAmount)
    {
        itemAmountLabel.text = newAmount;
    }
}
