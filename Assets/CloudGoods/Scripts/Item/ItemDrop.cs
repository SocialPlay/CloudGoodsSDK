using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;

public class ItemDrop : MonoBehaviour
{
    static public GameObject dropParentObject { get { if (mDrop == null) mDrop = new GameObject("DroppedItems"); return mDrop; } }
    static GameObject mDrop;

    public IGameObjectAction postDropObjectAction;

    public void DropItemIntoWorld(ItemData item, Vector3 dropPosition, GameObject dropModelDefault)
    {
        if (item != null)
        {
            Debug.Log("Loading asset :" + item.assetURL.ToRichColor(Color.white));
            item.AssetBundle((UnityEngine.Object bundleObj) =>
                {
                    GameObject dropObject = GameObject.Instantiate(bundleObj != null ? bundleObj : dropModelDefault) as GameObject;

                    ItemData itemData = dropObject.AddComponent<ItemDataComponent>().itemData;
                    itemData.SetItemData(item);

                    dropObject.name = item.itemName + " (ID: " + item.CollectionID + ")";

                    ItemComponentInitalizer.InitializeItemWithComponents(dropObject.GetComponent<ItemDataComponent>().itemData, AddComponetTo.prefab);

                    dropObject.transform.position = dropPosition;
                    dropObject.transform.parent = dropParentObject.transform;
      
                    if (postDropObjectAction != null)
                        postDropObjectAction.DoGameObjectAction(dropObject);
                }
            );
        }
    }
}
