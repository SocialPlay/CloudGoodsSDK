using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;

public class NGUILimitlessGridItemContainerDisplay :  ContainerDisplay
{

    internal UIGrid viewArea;

    protected override void SetupWindow()
    {
        base.SetupWindow();
        viewArea = containerDisplay.GetComponentInChildren<UIGrid>();  
    }

    public override void AddDisplayItem(ItemData itemData, Transform parent)
    {       
        itemData.transform.parent = viewArea.transform;
        itemData.transform.localPosition = new Vector3(0, 0, -1);
        itemData.transform.localScale = Vector3.one;
        viewArea.repositionNow = true;
    }

    public override void RemoveDisplayItem(ItemData itemData)
    {
        Destroy(itemData.gameObject);
        viewArea.repositionNow = true;
    }

    public void PublicSetupWindow()
    {
        SetupWindow();
    }


}
