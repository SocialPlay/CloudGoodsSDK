using UnityEngine;
using System.Collections;

public class PageInfo : MonoBehaviour {

    public int pageNumber = 0;
    public UILabel pageLabel;

    public void SetPage(int pageNum)
    {
        pageNumber = pageNum;
        pageLabel.text = (pageNumber + 1).ToString();
    }
}
