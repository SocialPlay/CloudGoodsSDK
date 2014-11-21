using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class PersistantUserDataExample : MonoBehaviour
{

    public InputField SaveKey;
    public InputField SaveValue;
    public Text saveResponse;

    public InputField RetriveKey;
    public Text loadResponse;

    public InputField DeleteKey;
    public Text DeleteResponse;

    public Text RetriveAllUserResponse;

    public InputField RetriveAllValuesOfKey;
    public Text RetriveAllValuesOfKeyResponse;


    void Awake()
    {
        CloudGoods.OnRegisteredUserToSession += (r) => { ShowChildren(true); };
        ShowChildren(false);
    }

    void ShowChildren(bool isShown)
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

    public void RetriveUserDataValue()
    {
        //Guid userID, string key
        CloudGoods.RetriveUserDataValue(RetriveKey.text, (r) => { loadResponse.text = r; });
    }



    public void DeleteUserDateValue()
    {
        //Guid userID, string key
        CloudGoods.DeleteUserDateValue(DeleteKey.text, (r) => { DeleteResponse.text = r.ToRichColor(); });
    }

    public void RetriveAllUsersData()
    {
        CloudGoods.RetriveAllUserDataValues((r) =>
        {
            RetriveAllUserResponse.text = "";
            foreach (KeyValuePair<string, string> data in r)
            {
                RetriveAllUserResponse.text += data.Key.ToRichColor(Color.white) + ":" + (data.Value != null ? data.Value : "Null") + "\n";
            }

        });
    }

    public void RetriveAllUserDataOfKey()
    {
        CloudGoods.RetriveAllUserDataOfKey(RetriveAllValuesOfKey.text, (r) => {
            RetriveAllValuesOfKeyResponse.text = "";
            for (int i = 0; i < r.Count; i++)
            {             
                RetriveAllValuesOfKeyResponse.text += r[i].user.userName.ToRichColor(Color.white) + " : " + r[i].value + "\n";
            }
            //RetriveAllValuesOfKeyResponse.text = r;
        });
    }


}
