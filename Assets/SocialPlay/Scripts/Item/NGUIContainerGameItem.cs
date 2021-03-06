using UnityEngine;
using System.Collections;


public class NGUIContainerGameItem : MonoBehaviour
{

    GameObject socialplayObj;

    public ItemData itemData;
    public UILabel itemAmountLabel;
    public GameObject NGUITexture;
    public UISprite Image;

    //void OnEnable()
    //{
    //    UIEventListener.Get(gameObject).onClick += ItemClicked;
    //    UIEventListener.Get(gameObject).onDoubleClick += ItemDoubleClicked;
    //}

    //void OnDisable()
    //{
    //    UIEventListener.Get(gameObject).onClick -= ItemClicked;
    //    UIEventListener.Get(gameObject).onDoubleClick -= ItemDoubleClicked;
    //}

    void Start()
    {
        if (Image != null)
        {
            if (Image.atlas.GetSprite(itemData.itemID.ToString()) != null)
            {
                Image.spriteName = itemData.itemID.ToString();
            }
            else
            {
                Destroy(Image.gameObject);
                SetImageTexture();
            }
        }
    }

    private void SetImageTexture()
    {
        GameObject newNGUITexture = GameObject.Instantiate(NGUITexture) as GameObject;
        newNGUITexture.transform.parent = transform;
        newNGUITexture.transform.localPosition = Vector3.zero;
        newNGUITexture.transform.localScale = new Vector3(1, 1, 1);

        SP.GetItemTexture(itemData.imageName, OnReceivedTexture);
    }

    void OnReceivedTexture(ImageStatus statusMsg, Texture2D texture)
    {
        UITexture uiTexture = gameObject.GetComponentInChildren<UITexture>();
        uiTexture.material = new Material(uiTexture.material.shader);
        uiTexture.mainTexture = texture;
        uiTexture.transform.localPosition -= Vector3.forward * 2;

        TweenAlpha.Begin(uiTexture.cachedGameObject, 0.3f, 1).from = 0;
    }

    //public void ItemClicked(GameObject go)
    //{
    //    itemData.ownerContainer.OnItemSingleClick(itemData);
    //}

    //public void ItemDoubleClicked(GameObject go)
    //{
    //    itemData.ownerContainer.OnItemDoubleCliked(itemData);
    //}

    void Update()
    {
        if (itemData == null)
            itemData = GetComponent<ItemDataComponent>().itemData;

        itemAmountLabel.text = itemData.stackSize.ToString();
    }
}
