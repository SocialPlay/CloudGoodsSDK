using UnityEngine;
using System.Collections;

public class NGUIStoreKeybinding : MonoBehaviour
{

    public KeyCode toggleKey;


    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            NGUIStore.ToggleStore(true);
        }
    }
}
