using UnityEngine;
using System.Collections;

public class ContainerAmountLabelDisplay : MonoBehaviour {

    public LimitedItemContainer itemContainer;

    UILabel label;

    void Start()
    {
        label = GetComponent<UILabel>();
    }
	
	// Update is called once per frame
	void Update () {
        label.text = itemContainer.containerItems.Count.ToString() + " / " + itemContainer.containerMaxSize.ToString();
	}
}
