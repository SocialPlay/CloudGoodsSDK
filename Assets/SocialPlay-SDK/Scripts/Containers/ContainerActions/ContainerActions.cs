using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;

public abstract class ContainerActions : MonoBehaviour
{
    public virtual void DoAction(ItemData item) { }
}