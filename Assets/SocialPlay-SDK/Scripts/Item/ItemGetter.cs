using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;
using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;
using SocialPlay.Data;
using LitJson;

public class ItemGetter : MonoBehaviour
{
    public int minEnergy = 1;
    public int MaxEnergy = 100;

    public List<string> AndTags;
    public List<string> OrTags;

    IItemPutter itemPutter;

    
    Transform previousParent;

    void Start()
    {
        //we do this so that if the object which sends out the packet gets deleted ( e.g. a mining node disapears after it is mined ), yet we still need this script to exist to receive the packet.
        previousParent = transform.parent;

        SetupItemPutter();
    }

    private void SetupItemPutter()
    {
        itemPutter = GetComponent(typeof(IItemPutter)) as IItemPutter;

        if (itemPutter == null)
            throw new MissingComponentException("This object requires a component which implements " + typeof(IItemPutter) + " attached.");
    }

    public void GetItems()
    {
        transform.parent = null;

        //todo why are we passing in a new guid for owner each call?

        //todo 2nd parameter that sets owner type, needs to be an enum.
        string andTagsString = "";
        if (AndTags.Count > 0)
        {
            foreach (string tag in AndTags)
            {
                andTagsString += tag + ",";
            }

            andTagsString = andTagsString.Remove(andTagsString.Length - 1);

        }

        Debug.Log("andtags: " + andTagsString);

        WebserviceCalls.webservice.GenerateItemsAtLocation(ItemSystemGameData.SessionID.ToString(), "Session", 0, ItemSystemGameData.AppID, minEnergy, MaxEnergy, OnReceivedGeneratedItems, andTagsString);
    }

    void OnReceivedGeneratedItems(string generatedItemsJson)
    {
        ReattachToGameObject();
        List<ItemData> items = ItemConversion.converter.convertToItemDataFromString(generatedItemsJson);
        itemPutter.PutGameItem(items);
    }

    void ReattachToGameObject()
    {
        if (previousParent != null)
        {
            transform.parent = previousParent;
            transform.localPosition = Vector3.zero;
        }
        else
            Destroy(gameObject);

    }

}
