using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;

public class StoreItem : MonoBehaviour
{
    public StoreItemInfo storeItemInfo;

    public UISprite itemImageSprite;
    UIButton button = null;

    void Awake()
    {
        if (button == null)
        {

        }
    }


    public void SetItemInfo(StoreItemInfo newStoreItemInfo)
    {
        storeItemInfo = newStoreItemInfo;

        GetItemTexture(storeItemInfo.imageURL);
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
    }


    public void SetItemImage(int newImageName)
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

}

public class StoreItemInfo
{
    public int ID = 0;
    public string itemName = "";
    public List<StoreItemDetail> itemDetail = new List<StoreItemDetail>();
    public DateTime addedDate;
    public string behaviours;
    public List<string> tags;
    public int itemID = 0;
    public int creditValue = 0;
    public int coinValue = 0;
    public string imageURL = "";
}

public class StoreItemDetail
{
    public string propertyName;
    public int propertyValue;
    public bool invertEnergy;
}
