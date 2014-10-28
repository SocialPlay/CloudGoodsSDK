using UnityEngine;
using System.Collections;

public interface IContainerAddAction {
    void AddItem(ItemData addItem, int amount, bool isSave);
}
