using UnityEngine;
using System.Collections;

public class ItemDropperFactory : MonoBehaviour {

    public GameObject itemDroppper;
    public Vector3 position = new Vector3(0, 1, -5);

    ItemGetter itemGetter { get { return itemGetterObj.GetComponent<ItemGetter>(); } }
    GameObject itemGetterObj { get { if (mGetter == null) mGetter = (GameObject)GameObject.Instantiate(itemDroppper); return mGetter; } }
    GameObject mGetter;

    public void CreateItemDropper()
    {
        itemGetterObj.transform.position = position + Random.onUnitSphere;
        itemGetterObj.transform.rotation = Random.rotation;
        itemGetter.GetItems();
    }
}
