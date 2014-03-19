using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;

public class NGUILimitlessGridItemContainerDisplay :  ContainerDisplay
{

    internal UIGrid viewArea;

    protected override void SetupWindow()
    {
        base.SetupWindow();
        viewArea = ContainerDisplayObject.GetComponentInChildren<UIGrid>();  
    }

    public override void AddDisplayItem(ItemData itemData, Transform parent)
    {       
        itemData.transform.parent = viewArea.transform;
        itemData.transform.localPosition = new Vector3(0, 0, -1);
        itemData.transform.localScale = Vector3.one;       
        foreach (UIWidget item in itemData.GetComponentsInChildren<UIWidget>())
        {
            item.enabled = true;
        }
        foreach (MonoBehaviour item in itemData.GetComponentsInChildren<MonoBehaviour>())
        {
            if (item != null)
            {
                item.enabled = true;
            }
        }
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
