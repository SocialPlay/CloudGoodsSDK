using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnityUIPageInfo : MonoBehaviour {
    public int pageNumber = 0;
    public Text pageLabel;

    public void SetPage(int pageNum)
    {
        pageNumber = pageNum;
        pageLabel.text = (pageNumber + 1).ToString();
    }
}
