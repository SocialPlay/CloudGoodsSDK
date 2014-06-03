using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;



class ItemDataLoader:MonoBehaviour
{
    //private ItemDataLoader insatnce;

    void Awake()
    {
        //insatnce = this;
    }

    public static void LoadItemDataAtLocation(int location, Action<List<ItemData>> callback)
    {
            
    }

    public void gotItem(string itemJsonData)
    {

    }

}

