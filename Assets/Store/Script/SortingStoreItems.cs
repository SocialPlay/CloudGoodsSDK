using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class SortingStoreItems : MonoBehaviour {

    public NGUIStoreLoader storeLoader;

    float time = 0.0f;

    void Update()
    {
        time += Time.deltaTime;

        if (time >= 3.0f)
        {
            SortPageByType(storeLoader.GetStoreItemList(), "CreditValue", true);
            time = 0.0f;
        }
    }

    void SortPageByType(List<JToken> storeItems, string propertySort, bool isDesc)
    {
        for (int x = 0; x < storeItems.Count; x++)
        {
            for (int y = 0; y < storeItems.Count - 1; y++)
            {
                Debug.Log(storeItems[y][propertySort].ToString());
                Debug.Log(storeItems[y + 1][propertySort].ToString());
                //if (int.Parse(storeItems[y][propertySort].ToString()) > int.Parse(storeItems[y + 1][propertySort].ToString()))
                //{
                //    JToken temp = storeItems[y + 1];

                //    storeItems[y + 1] = storeItems[y];

                //    storeItems[y] = temp;
                //}
            }
        }

        storeLoader.LoadStoreWithPaging(storeItems, 0);
    }
}
