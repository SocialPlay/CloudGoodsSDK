using UnityEngine;
using System.Collections;

public class ItemDropperTest : MonoBehaviour {

    public ItemGetter itemGetter;

	// Use this for initialization
	void Start () {
        itemGetter.GetItems();
	}
}
