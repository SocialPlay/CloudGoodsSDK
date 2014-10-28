using UnityEngine;
using System.Collections;

public abstract class ContainerItemFilter : MonoBehaviour
{
    public enum InvertedState
    {
        required, excluded
    }

    public InvertedState type;

    public abstract bool IsItemFilteredIn(ItemData item);
}
