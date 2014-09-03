using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemDataDisplay : MonoBehaviour {
    internal ItemData itemData;

    public Text nameDisplay;


    public void Start()
    {
        itemData = this.GetComponent<ItemDataComponent>().itemData;
        nameDisplay.text = itemData.itemName;
    }
}
