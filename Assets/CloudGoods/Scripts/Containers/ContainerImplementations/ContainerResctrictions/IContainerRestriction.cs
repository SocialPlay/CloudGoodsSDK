using UnityEngine;
using System.Collections;

public interface IContainerRestriction {

    bool IsRestricted(ContainerAction action, ItemData itemData);
}

public enum ContainerAction
{
    add,
    remove
}
