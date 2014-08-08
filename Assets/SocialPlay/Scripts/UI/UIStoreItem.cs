using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;

public class UIStoreItem : MonoBehaviour
{
	public int itemID = 0;
	public GameObject loader;
	public UILabel nameLabel;
	public UILabel descriptionLabel;
	public StoreItem storeItem { get; private set; }

    //public UISprite itemImageSprite;
	

	protected virtual void OnEnable()
	{
		if (loader != null) NGUITools.SetActive(loader, true);

		if (itemID != 0) SP.OnStoreListLoaded += OnStoreListLoaded;
	}

	protected virtual void OnDisable()
	{
		if (itemID != 0) SP.OnStoreListLoaded -= OnStoreListLoaded;
	}

	protected virtual void OnStoreListLoaded(List<StoreItem> storeList)
	{
		SetItemData(SP.GetStoreItem(itemID));
	}

    void OnReceivedItemTexture(ItemTextureCache.ImageStatus imageStatus, Texture2D texture)
    {
        UITexture uiTexture = gameObject.GetComponentInChildren<UITexture>();
        uiTexture.mainTexture = texture;
		if (loader != null) NGUITools.SetActive(loader, false);

        TweenAlpha.Begin(uiTexture.cachedGameObject, 0.3f, 1).from = 0;
    }

	public virtual void SetItemData(StoreItem item)
	{
		if (nameLabel != null) nameLabel.text = item.itemName;
		//if(descriptionLabel != null) descriptionLabel.text = item. <-- There is no description on StoreItems. This is a must have.
		storeItem = item;
        ItemTextureCache.instance.GetItemTexture(storeItem.imageURL, OnReceivedItemTexture);
	}


    /*public void SetItemImage(int newImageName)
    {
        string imageName = newImageName.ToString();

        if (itemImageSprite != null && !string.IsNullOrEmpty(imageName))
        {
            string tmpImageName = imageName;

            if (imageName.Contains(".jpg")) tmpImageName = imageName.Remove(imageName.Length - 4);

            if (itemImageSprite.atlas != null && itemImageSprite.atlas.GetSprite(tmpImageName) != null)
            {
                itemImageSprite.spriteName = tmpImageName;
            }
        }
    }
	*/
}
