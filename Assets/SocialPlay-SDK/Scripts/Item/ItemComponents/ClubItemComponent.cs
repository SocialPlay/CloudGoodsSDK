using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;

public class ClubItemComponent : MonoBehaviour, IItemComponent
{   

    public void UseItem(ItemData item)
    {
        Debug.Log("Use Club Item");
    }
}