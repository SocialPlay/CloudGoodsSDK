using UnityEngine;
using System.Collections;


public class ClubItemComponent : MonoBehaviour, IItemComponent
{   

    public void UseItem(ItemData item)
    {
        Debug.Log("Use Club Item");
    }
}