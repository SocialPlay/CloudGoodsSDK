using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UnityUIStoreItem : MonoBehaviour {

    public StoreItem storeItem { get; private set; }

    UnityUIStoreLoader storeLoader;

    //public UISprite itemImageSprite;


    //protected virtual void OnEnable()
    //{
    //    if (itemID != 0) SP.OnStoreListLoaded += OnStoreListLoaded;
    //}

    //protected virtual void OnDisable()
    //{
    //    if (itemID != 0) SP.OnStoreListLoaded -= OnStoreListLoaded;
    //}

    //protected virtual void OnStoreListLoaded(List<StoreItem> storeList)
    //{
    //    SetItemData(SP.GetStoreItem(itemID));
    //}

    void OnReceivedItemTexture(ImageStatus imageStatus, Texture2D texture)
    {
        if (gameObject == null) return;

        RawImage uiTexture = gameObject.GetComponentInChildren<RawImage>();
        uiTexture.texture = texture;
        //if (loader != null) NGUITools.SetActive(loader, false);

        //TweenAlpha.Begin(uiTexture.cachedGameObject, 0.3f, 1).from = 0;
    }

    public virtual void Init(StoreItem item, UnityUIStoreLoader unityStoreLoader)
    {
        //if (nameLabel != null) nameLabel.text = item.itemName;
        //if(descriptionLabel != null) descriptionLabel.text = item. <-- There is no description on StoreItems. This is a must have.
        storeItem = item;
        storeLoader = unityStoreLoader;
        CloudGoods.GetItemTexture(storeItem.imageURL, OnReceivedItemTexture);
    }

    public void OnStoreItemClicked()
    {
        storeLoader.DisplayItemPurchasePanel(gameObject);
    }
}
