using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;

public class UIStoreItem : MonoBehaviour
{
	public int itemId = 0;
	public GameObject loader;
	public StoreItem storeItem { get; private set; }

    //public UISprite itemImageSprite;
	

	void Awake()
	{
		NGUITools.SetActive(loader, true);
		if (itemId != 0)
		{
			SP.OnStoreListLoaded += OnStoreListLoaded;
		}
	}

	void OnStoreListLoaded(List<StoreItem> storeList)
	{
		SetItemData(SP.GetStoreItem(itemId));
	}

    void GetItemTexture(string URL)
    {
        if (!this.gameObject.activeInHierarchy) return;
        WWW www = new WWW(URL);
        StartCoroutine(OnReceivedItemTexture(www));
    }

    IEnumerator OnReceivedItemTexture(WWW www)
    {
        yield return www;
        UITexture uiTexture = gameObject.GetComponentInChildren<UITexture>();
        uiTexture.mainTexture = www.texture;
		NGUITools.SetActive(loader, false);
    }

	public virtual void SetItemData(StoreItem item)
	{
		storeItem = item;
		GetItemTexture(storeItem.imageURL);
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
