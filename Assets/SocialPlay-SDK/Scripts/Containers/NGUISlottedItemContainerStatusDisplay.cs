using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocialPlay.ItemSystems;

public class NGUISlottedItemContainerStatusDisplay : MonoBehaviour
{
    [System.Serializable]
    public class StatsPairs
    {
        public string statName;
        public UILabel label;
    }
 
    public SlottedItemContainer container;
    public List<StatsPairs> statsDisplays = new List<StatsPairs>();


    void OnEnable()
    {
        container.AddedItem += AddItem;
        container.RemovedItem += RemoveItem;
    }

    void OnDisable()
    {
        container.AddedItem -= AddItem;
        container.RemovedItem -= RemoveItem;
    }

    void Start()
    {       
        SetStats();
    }


    void AddItem(ItemData data, bool isSave)
    {    
        SetStats();
    }

    void SetStats()
    {
        foreach (StatsPairs sta in statsDisplays)
        {
            if (string.IsNullOrEmpty(sta.statName) || sta.label == null)
                continue;
            sta.label.text = sta.statName + ": ";
            if (container.stats.ContainsKey(sta.statName))
            {
                sta.label.text += container.stats[sta.statName].ToString();
            }
            else
            {
                sta.label.text += "0";
            }
        }
    }

    void RemoveItem(ItemData data,int amount, bool isMovedToAnotherContainer)
    {      
        SetStats();
    }      

}
