using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SlotData))]
public class SlotKeybinding : MonoBehaviour {

    public KeyCode bindingKey;


    public event Action<ItemData> bindingPressed;

    void Update()
    {
        if (Input.GetKeyDown(bindingKey))
        {
            if (this.GetComponentInChildren<ItemData>())
            {
                ItemData itemEquipt = this.GetComponentInChildren<ItemData>();
                if (bindingPressed != null)
                {
                    bindingPressed(itemEquipt);
                }
            }
        }
    }
}
