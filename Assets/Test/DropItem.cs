using UnityEngine;
using System.Collections;

public class DropItem : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.D))
        {
            ItemGetter itemGetter = GetComponent<ItemGetter>();
            itemGetter.GetItems();
        }
	}
}
