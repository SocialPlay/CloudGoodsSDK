using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;

public class MusketItemComponent : MonoBehaviour, IItemComponent {

    public void UseItem(ItemData item)
    {
        Debug.Log("Use Musket Item");
    }
}
