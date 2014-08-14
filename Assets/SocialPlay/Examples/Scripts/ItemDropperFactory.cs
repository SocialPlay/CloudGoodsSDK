using UnityEngine;
using System.Collections;

public class ItemDropperFactory : MonoBehaviour {

    public GameObject itemdroppper;

    public void CreateItemDropper()
    {
        GameObject itemGetterObj = (GameObject)GameObject.Instantiate(itemdroppper);
        itemGetterObj.transform.position = new Vector3(0, 1, -5);
        ItemGetter itemGetter = itemGetterObj.GetComponent<ItemGetter>();

        itemGetter.GetItems();
    }
}
