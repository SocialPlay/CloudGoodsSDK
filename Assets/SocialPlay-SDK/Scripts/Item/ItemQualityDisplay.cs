using UnityEngine;
using System.Collections;

public class ItemQualityDisplay : MonoBehaviour {

    public UISprite frameSprite;

    void Start()
    {
        NGUIContainerGameItem containerItem = GetComponent<NGUIContainerGameItem>();

        frameSprite.color = ItemQuailityColorSelector.GetColorForItem(containerItem.itemData);
    }
}
