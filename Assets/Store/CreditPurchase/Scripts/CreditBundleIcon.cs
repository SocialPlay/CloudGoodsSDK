using UnityEngine;
using System;
using System.Collections;

public class CreditBundleIcon
{
    public UISprite currentCurrencyIcon;

    public void SetCurrentCurrencyIcon(UISprite icon)
    {
        if (icon)
            currentCurrencyIcon = icon;
        else
            throw new Exception("UISprite for currency icon is null.");
    }

    public bool VerifyValidAtlas(UIAtlas atlas)
    {
        if (atlas == currentCurrencyIcon.atlas)
            return true;
        else
            return false;
    }

    public UISprite Get(string bundleAmount, UISprite icon)
    {
        //Debug.Log("icon atlas = " + icon.atlas + ", other atlas = " + currentCurrencyIcon.atlas);
        SetCurrentCurrencyIcon(icon);

        if (VerifyValidAtlas(icon.atlas))
        {
            foreach (UISpriteData sprite in icon.atlas.spriteList)
            {
                if (sprite.name.Contains(bundleAmount))
                {
                    icon.spriteName = sprite.name;

                    return icon;
                }
            }
        }

        throw new Exception("Sprite atlas does not contain required sprite!");
        return null;
    }
}
