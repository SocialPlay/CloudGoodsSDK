using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[System.Serializable]
public class SlottedContainerSlotData
{
    public event Action OnItemChanged;
    public string slotNameID = "";
    public ItemData slotData = null;
    public int slotMaxCountLimit = 1;
    public int priority = 0;
    public int persistantID;
    public List<ItemFilterSystem> filters;

    public void OnItemChangedWrapper()
    {
        if (OnItemChanged != null)
        {
            OnItemChanged();
        }
    }
}