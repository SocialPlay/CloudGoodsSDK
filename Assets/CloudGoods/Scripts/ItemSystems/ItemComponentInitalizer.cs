using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class ItemComponentInitalizer : MonoBehaviour
{
    private static ItemComponentInitalizer intilizer = null;
    public List<ItemComponentSetup> itemSetupList;

    void Awake()
    {
        if (ItemComponentInitalizer.intilizer == null)
        {
            ItemComponentInitalizer.intilizer = this;
        }
    }

    public static void InitializeItemWithComponents(List<ItemData> items, AddComponetTo sourceToAddTo)
    {
        if (ItemComponentInitalizer.intilizer != null)
        {
            ItemComponentInitalizer.intilizer.AddComponentToItems(items, sourceToAddTo);
        }
    }

    public static void InitializeItemWithComponents(ItemData item, AddComponetTo sourceToAddTo)
    {
        if (ItemComponentInitalizer.intilizer != null)
        {
            List<ItemData> items = new List<ItemData>();
            items.Add(item);
            ItemComponentInitalizer.intilizer.AddComponentToItems(items, sourceToAddTo);
        }
    }


    private void AddComponentToItems(List<ItemData> items, AddComponetTo sourceToAddTo)
    {
        //todo only one for each or 2-3 nests max per function
        foreach (ItemData item in items)
        {
            foreach (ItemComponentSetup itemSetup in itemSetupList)
            {
                if (itemSetup == null) continue;

                if (itemSetup.filter.IsSelected(item))
                {
                    foreach (ItemComponentSetup.ComponentPair pair in itemSetup.itemComponents)
                    {
                        if (pair.destination == sourceToAddTo || pair.destination == AddComponetTo.both)
                        {
                            //Type type = pair.component.GetType();
                            //item.gameObject.AddComponent(type.ToString());
                        }
                        else
                        {
                            Debug.Log("skipping adding " + pair.destination + " because the source was " + sourceToAddTo);
                            continue;
                        }
                    }
                }
            }
        }
    }
}

