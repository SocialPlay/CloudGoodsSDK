using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnityUIPageInfo : MonoBehaviour {
    public int pageNumber = 0;
    public Text pageLabel;
    public UnityUIStoreLoader storeLoader;

    public void Init(int pageNum, UnityUIStoreLoader unityStoreLoader)
    {
        storeLoader = unityStoreLoader;
        pageNumber = pageNum;
        pageLabel.text = (pageNumber + 1).ToString();
    }

    public void StoreCurrentPage()
    {
        Debug.Log("StoreCurrentPage: " + pageNumber);
        storeLoader.SetPage(pageNumber);
    }
}
