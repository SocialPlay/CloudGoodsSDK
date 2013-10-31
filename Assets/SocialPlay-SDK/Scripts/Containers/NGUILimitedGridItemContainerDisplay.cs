using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;

public class NGUILimitedGridItemContainerDisplay : ContainerDisplay
{
    internal UIGrid viewArea;

    public override void SetupWindow()
    {
        base.SetupWindow();
        viewArea = containerDisplay.GetComponentInChildren<UIGrid>();  
    }

    public override void AddDisplayItem(ItemData itemData, Transform parent)
    {
        //itemData.ShowEffect();
        itemData.transform.parent = viewArea.transform;
        itemData.transform.localPosition = new Vector3(0, 0, -1);
        itemData.transform.localScale = Vector3.one;
        viewArea.repositionNow = true;
    }

    public override void RemoveDisplayItem(ItemData itemData)
    {
        Debug.Log("remove display item");
        Destroy(itemData.gameObject);
        Invoke("RepositionGrid", 0.2f);
    }

    void RepositionGrid()
    {
        viewArea.repositionNow = true;
    }
 
}
