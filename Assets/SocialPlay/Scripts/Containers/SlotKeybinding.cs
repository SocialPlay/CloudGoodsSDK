using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SlotData))]
public class SlotKeybinding : MonoBehaviour {

    public KeyCode BindingKey;


    public event Action<ItemData> BindingPressed;

    void Update()
    {
        if (Input.GetKeyDown(BindingKey))
        {
            if (this.GetComponentInChildren<ItemData>())
            {
                ItemData itemEquipt = this.GetComponentInChildren<ItemData>();
                if (BindingPressed != null)
                {
                    BindingPressed(itemEquipt);
                }
            }
        }
    }
}
