using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;

public class ItemInfo : MonoBehaviour
{

    public int ID = 0;
    public string itemName = "";
    public string itemDetail = "";
    public string itemTags = "";
    public int itemID = 0;
    public int creditValue = 0;
    public int coinValue = 0;
    public string imageURL = "";

    public UISprite itemImageSprite;
    UIButton button = null;

    void Awake()
    {
        if (button == null)
        {

        }
    }


    public void SetItemInfo(JToken itemToken)
    {
        ID = int.Parse(itemToken["ID"].ToString());
        itemName = itemToken["Name"].ToString();
        itemID = int.Parse(itemToken["ItemID"].ToString());
        creditValue = int.Parse(itemToken["CreditValue"].ToString());
        coinValue = int.Parse(itemToken["CoinValue"].ToString());
        itemDetail = itemToken["Detail"].ToString();
        itemTags = itemToken["tags"].ToString();
        imageURL = itemToken["Image"].ToString();

        GetItemTexture(imageURL);
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
