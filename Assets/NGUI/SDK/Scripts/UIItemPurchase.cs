using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UIItemPurchase : UIStoreItem
{
    public UILabel amountLabel;
    public UILabel valueLabel;
    public int amount = 1;
    public int location = 0;
    public CurrencyType currency = CurrencyType.Premium;
    public string amountFormat = "X {0}";
    public bool localize = true;
    public UILabel buttonLabel;
    public string useString = "Use";
    public string buyString = "Buy";

    int mAmount = 0;
    ItemData mItem;
    StoreItem mStoreItem;
    UILocalize mLoc;
    bool mInitiated;

    void Awake()
    {
        if (localize && buttonLabel != null)
        {
            mLoc = buttonLabel.cachedGameObject.GetComponent<UILocalize>();
            if (mLoc == null)
                mLoc = buttonLabel.cachedGameObject.AddComponent<UILocalize>();

            mLoc.key = buyString;
        }
    }

    void Start()
    {
        if (!mInitiated)
        {
            mInitiated = true;
            SP.GetItems(null);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        if (itemID != 0) SP.OnItemsLoaded += OnItemsLoaded;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if (itemID != 0) SP.OnItemsLoaded -= OnItemsLoaded;
    }

    void OnItemsLoaded(List<ItemData> items)
    {
        Debug.Log("OnItemsLoaded " + items.Count);

        mItem = SP.GetItem(itemID);

        Debug.Log("Item found: " + mItem != null);

        if (mItem != null)
        {
            Debug.Log("Item " + mItem.itemName);
            if (nameLabel != null) nameLabel.text = mItem.itemName;
            if (amountLabel != null) amountLabel.text = string.Format(amountFormat, mItem.stackSize);

            mAmount = mItem.stackSize;
            if (localize) mLoc.key = useString;
            else buttonLabel.text = useString;
        }
        else
        {
            if (amountLabel != null) amountLabel.text = string.Format(amountFormat, 0);
            if (localize) mLoc.key = buyString;
            else buttonLabel.text = buyString;
        }
    }

    protected override void OnStoreListLoaded(List<StoreItem> storeList)
    {
        mStoreItem = SP.GetStoreItem(itemID);
        if (nameLabel != null) nameLabel.text = mStoreItem.itemName;
        if (amountLabel != null) amountLabel.text = mItem == null ? string.Format(amountFormat, 0) : string.Format(amountFormat, mItem.stackSize);
    }

    public void BuyOrUse()
    {
        if (mAmount > 0) SP.UseItem(mItem, (string message) => { Debug.Log("Use Item: " + message); });
        else SP.StoreItemPurchase(itemID, amount, currency, location);
    }
}
