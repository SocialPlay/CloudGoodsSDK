using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContainerRefresh : MonoBehaviour
{
    public List<GameObject> containerObjects = new List<GameObject>();

    /// <summary>
    /// Updates the listed containers to the latest changes.
    /// </summary>
    public void RefreshContianer()
    {
        for (int c = 0; c < containerObjects.Count; c++)
        {
            containerObjects[c].GetComponent<ItemContainer>().Clear();
            containerObjects[c].GetComponentInChildren<PersistentItemContainer>().LoadItems();
        }
    }
}
