using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;

public class NGUILimitedGridItemContainerDisplay : ContainerDisplay
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
        viewArea.repositionNow = true;

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
    }

    public override void RemoveDisplayItem(ItemData itemData)
    {
        Destroy(itemData.gameObject);
        Invoke("RepositionGrid", 0.2f);
    }

    void RepositionGrid()
    {
        viewArea.repositionNow = true;
    }



}
