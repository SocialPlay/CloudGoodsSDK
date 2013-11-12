using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocialPlay.ItemSystems;
using System;

public class ItemDrop : MonoBehaviour
{

    public IGameObjectAction postDropObjectAction;

    public void DropItemIntoWorld(ItemData item, Vector3 dropPosition, GameObject dropModelDefault)
    {
        if (item != null)
        {
            //todo should not have to make list, ItemConverter should have wrapper taking one item

            // todo should not have boolean as a parameter
            //List<ItemData> items = new List<ItemData>();
            //items.Add(item);
            //List<GameObject> dropItems = ItemConverter.ConvertToItemDropObject(items, true);

            //foreach (GameObject dropItem in dropItems)
            //  {


            item.AssetBundle(
                (UnityEngine.Object bundleObj) =>
                {
                    GameObject dropObject;

                    if (bundleObj != null)
                        dropObject = GameObjectInitilizer.initilizer.InitilizeGameObject(bundleObj);
                    else
                        dropObject = GameObjectInitilizer.initilizer.InitilizeGameObject(dropModelDefault);

                    ItemData itemData = dropObject.AddComponent<ItemData>();

                    itemData.SetItemData(item);


                    ItemComponentInitalizer.InitializeItemWithComponents(dropObject.GetComponent<ItemData>(), AddComponetTo.prefab);

                    dropObject.transform.position = dropPosition;

                    if (postDropObjectAction != null)
                        postDropObjectAction.DoGameObjectAction(dropObject);
                }
            );
            //  }

        }
    }
}
