using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PrioritySelector : ISlotSelector
{
    public List<SlottedContainerSlotData> slotsList;

    public SlottedContainerSlotData PickBestSlot(Dictionary<int, SlottedContainerSlotData> pickedSlots)
    {
        if (pickedSlots == null)
            throw new System.Exception("Slots cannot be null.");

        slotsList = pickedSlots.Values.ToList<SlottedContainerSlotData>();

        slotsList.Sort((a, b) => a.priority.CompareTo(b.priority));

        foreach (SlottedContainerSlotData selectedSlot in slotsList)//Check for empty slots
        {
            if (selectedSlot.slotData == null)
            {
                return selectedSlot;
            }
        }
        if (slotsList.Count != 0)
        {
            return null;
        }
        return null;
    }
}

