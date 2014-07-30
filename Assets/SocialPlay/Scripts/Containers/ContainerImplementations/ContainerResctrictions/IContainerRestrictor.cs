using UnityEngine;
using System.Collections;

public interface IContainerRestrictor {

    void Initialize();

    bool CanAddToContainer(ItemData itemData);
}
