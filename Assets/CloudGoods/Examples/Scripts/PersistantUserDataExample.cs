using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PersistantUserDataExample : MonoBehaviour
{

    public InputField SaveKey;
    public InputField SaveValue;
    public Text saveResponse;

    public InputField RetriveKey;
    public Text loadResponse;

    public InputField DeleteKey;
    public Text DeleteResponse;

    public InputField RetriveAllUserKey;
    public Text RetriveAllUserResponse;

    public InputField RetriveAllValuesOfKey;
    public Text RetriveAllValuesOfResponse;


    void Awake()
    {
        CloudGoods.OnRegisteredUserToSession += (r) => { ShowChilder(true); };
        ShowChilder(false);
    }

    void ShowChilder(bool isShown)
    {
        for (int childIndex = 0; childIndex < this.transform.childCount; childIndex++)
        {
            this.transform.GetChild(childIndex).gameObject.SetActive(isShown);
        }
    }


    public void SaveUserData()
    {
        //string key, string value
        CloudGoods.SaveUserData(SaveKey.text, SaveValue.text, (r) => { saveResponse.text = r.ToString(); });


    }

    void RetriveUserDataValue()
    {
        //Guid userID, string key
    }



    void DeleteUserDateValue()
    {
        //Guid userID, string key
    }



    void RetriveAllUserDataOfKey()
    {
        //Guid appID, string Key
    }
}
