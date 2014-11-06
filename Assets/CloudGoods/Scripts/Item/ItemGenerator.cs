using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;
using SocialPlay.Data;
using LitJson;

public class ItemGenerator : MonoBehaviour
{
    public int minEnergy = 1;
    public int MaxEnergy = 100;

    public List<string> AndTags;
    public List<string> OrTags;

    IGetItems itemPutter;

    Transform previousParent;

    void Awake()
    {
        //we do this so that if the object which sends out the packet gets deleted ( e.g. a mining node disapears after it is mined ), yet we still need this script to exist to receive the packet.
        previousParent = transform.parent;

        SetupGetItems();
    }

    private void SetupGetItems()
    {
        itemPutter = GetComponent(typeof(IGetItems)) as IGetItems;

        if (itemPutter == null)
            throw new MissingComponentException("This object requires a component which implements " + typeof(IGetItems) + " attached.");
    }

    public void GenerateItems()
    {
        transform.parent = null;

        string andTagsString = "";
        if (AndTags.Count > 0)
        {
            foreach (string tag in AndTags)
            {
                andTagsString += tag + ",";
            }

            andTagsString = andTagsString.Remove(andTagsString.Length - 1);

        }

        CloudGoods.GenerateItemsAtLocation("Session", 0, minEnergy, MaxEnergy, OnReceivedGeneratedItems, andTagsString);
    }

    public void OnReceivedGeneratedItems(List<ItemData> generatedItems)
    {
        ReattachToGameObject();

        itemPutter.GetGameItem(generatedItems);
    }

    void ReattachToGameObject()
    {
        if (previousParent != null)
        {
            transform.parent = previousParent;
            transform.localPosition = Vector3.zero;
        }
        //else Destroy(gameObject);
    }


    
}
