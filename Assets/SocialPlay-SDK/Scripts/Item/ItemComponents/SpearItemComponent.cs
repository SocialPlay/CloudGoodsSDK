using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;

public class SpearItemComponent : MonoBehaviour, IItemComponent
{
    public void UseItem(ItemData item)
    {
        Debug.Log("Use Spear Item");
    }
}