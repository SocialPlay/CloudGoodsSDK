using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnityItemDataDisplay : ItemDataDisplay
{

    public Text amountText;
    public RawImage itemImage;
    public Image itemFrame;

    public override void UpdateTexture(Texture2D newTexture)
    {
        if (itemImage != null)
        {
            itemImage.texture = newTexture;
        }
    }

    public override void SetFrameColor(Color newColor)
    {
        if (itemFrame != null) itemFrame.color = newColor;
    }

    public override void SetAmountText(string newAmount)
    {
        if (amountText != null) amountText.text = newAmount;
    }
}
