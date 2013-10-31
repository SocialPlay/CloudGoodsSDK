using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocialPlay.ItemSystems;

public class DependentContainers : MonoBehaviour
{


    public List<ItemContainer> dependentContainers = new List<ItemContainer>();

    ItemContainer myContainer;

    void Awake()
    {
        myContainer = GetComponent<ItemContainer>();
    }

    void Update()
    {
        if (myContainer == null)
            return;

        CheckIfDependantContainerIsActive();
    }

    private void CheckIfDependantContainerIsActive()
    {
        foreach (ItemContainer container in dependentContainers)
        {
            if (container == null) continue;

            if (container.IsActive)
            {
                myContainer.IsActive = true;
                return;
            }
        }
        myContainer.IsActive = false;
    }
}
