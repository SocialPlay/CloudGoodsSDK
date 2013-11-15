using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocialPlay.ItemSystems;

public class DependentContainers : MonoBehaviour
{


    public List<ContainerDisplay> dependentContainers = new List<ContainerDisplay>();

    ContainerDisplay myContainer;

    void Awake()
    {
        myContainer = GetComponent<ContainerDisplay>();
    }

    void Update()
    {
        if (myContainer == null)
            return;

        CheckIfDependantContainerIsActive();
    }

    private void CheckIfDependantContainerIsActive()
    {
        if (dependentContainers.Count == 0) return;
        foreach (ContainerDisplay containerDisplay in dependentContainers)
        {
            if (containerDisplay == null) continue;

            if (containerDisplay.IsWindowActive())
            {
                myContainer.SetWindowIsActive(true);
                return;
            }
        }
        myContainer.SetWindowIsActive(false);
    }
}
