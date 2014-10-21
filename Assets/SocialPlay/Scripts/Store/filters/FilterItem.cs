using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public abstract class FilterItem : MonoBehaviour
{
    public bool isActive = false;
    //public UICheckbox checkBox;
    //public UILabel filterLabel;
    public string filterDisplayName;
    public string filterBy;

    public virtual void init()
    {
        //filterLabel.text = filterDisplayName;
    }

    public abstract List<StoreItem> FilterStoreList(List<StoreItem> storeList);

}
