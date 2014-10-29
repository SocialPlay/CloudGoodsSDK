using UnityEngine;
using System.Collections;


public class MusketItemComponent : MonoBehaviour, IItemComponent {

    public void UseItem(ItemData item)
    {
        Debug.Log("Use Musket Item");
    }
}
