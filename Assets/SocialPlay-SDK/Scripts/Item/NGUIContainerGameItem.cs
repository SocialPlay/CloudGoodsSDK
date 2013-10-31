using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;

public class NGUIContainerGameItem : MonoBehaviour
{

    public ItemData itemData;
    public UILabel itemAmountLabel;
    public GameObject NGUITexture;
    public UISprite Image;

    void OnEnable()
    {
        UIEventListener.Get(gameObject).onClick += ItemClicked;
        UIEventListener.Get(gameObject).onDoubleClick += ItemDoubleClicked;
    }

    void OnDisable()
    {
        UIEventListener.Get(gameObject).onClick -= ItemClicked;
        UIEventListener.Get(gameObject).onDoubleClick -= ItemDoubleClicked;
    }

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

        GetItemTexture(itemData.imageName);
    }

    public void GetItemTexture(string URL)
    {
        WWW www = new WWW(URL);

        StartCoroutine(OnReceivedItemTexture(www));
    }

    IEnumerator OnReceivedItemTexture(WWW www)
    {
        yield return www;

        UITexture uiTexture = gameObject.GetComponentInChildren<UITexture>();
        uiTexture.material = new Material(uiTexture.material.shader);
        uiTexture.mainTexture = www.texture;
        uiTexture.transform.localPosition -= Vector3.forward * 2;
    }

    public void ItemClicked(GameObject go)
    {
        itemData.ownerContainer.OnItemSingleClick(itemData);
    }

    public void ItemDoubleClicked(GameObject go)
    {
        itemData.ownerContainer.OnItemDoubleCliked(itemData);
    }

    void Update()
    {
        if (itemData == null)
            itemData = GetComponent<ItemData>();

        itemAmountLabel.text = itemData.stackSize.ToString();
    }
}
