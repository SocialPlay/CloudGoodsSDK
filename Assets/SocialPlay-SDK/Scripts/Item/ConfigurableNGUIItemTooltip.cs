using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;
using System.Collections.Generic;

public class ConfigurableNGUIItemTooltip : MonoBehaviour, ITooltipSetup
{

    public List<DisaplyOption> displayOptions = new List<DisaplyOption>();
    public bool isQualityColorUsed;



    public enum DisaplyOption
    {
        name, stats, quantity, description, salePrice, behaviour, behaviourPlus, space, varianceID, itemID, energy, classID, stackID
    }

    ItemData item;

    public string Setup()
    {
        item = GetComponent<ItemData>();
        string formated = "";


        foreach (DisaplyOption selectedOption in displayOptions)
        {
            switch (selectedOption)
            {
                case DisaplyOption.name:
                    if (isQualityColorUsed)
                    {
                        formated += "[" + NGUITools.EncodeColor(ItemQuailityColorSelector.GetColorForItem(item)) + "]";
                    }
                    else
                    {
                        formated += "[" + NGUITools.EncodeColor(Color.white) + "]";
                    }
                    formated += item.name;
                    break;
                case DisaplyOption.stats:
                    foreach (KeyValuePair<string, float> pair in item.stats)
                    {
                        if (pair.Key == "Not Available") continue;

                        formated = string.Format("{0}\n[" + NGUITools.EncodeColor(Color.white) + "]{1}: {2}", formated, pair.Key, pair.Value);
                    }
                    break;
                case DisaplyOption.quantity:
                    formated = string.Format("{0}\n[" + NGUITools.EncodeColor(Color.white) + "]{1}", formated, item.stackSize);
                    break;
                case DisaplyOption.description:
                    if (!string.IsNullOrEmpty(item.description))
                    {
                        formated = string.Format("{0}\n[" + NGUITools.EncodeColor(Color.white) + "]{1}", formated, item.description);
                    }
                    break;

                case DisaplyOption.salePrice:
                    formated = string.Format("{0}\n[" + NGUITools.EncodeColor(Color.white) + "]{1}", formated, item.salePrice);
                    break;
                case DisaplyOption.behaviour:
                    foreach (BehaviourDefinition behaviour in item.behaviours)
                    {
                        formated = string.Format("{0}\n[" + NGUITools.EncodeColor(Color.white) + "]{1}", formated, behaviour.Name);
                    }

                    break;
                case DisaplyOption.behaviourPlus:
                    foreach (BehaviourDefinition behaviour in item.behaviours)
                    {
                        formated = string.Format("{0}\n[" + NGUITools.EncodeColor(Color.white) + "]{1}: {2}", formated, behaviour.Name, behaviour.Description);
                    }
                    break;
                case DisaplyOption.space:
                    formated = string.Format("{0}\n", formated);
                    break;
                case DisaplyOption.varianceID:
                    formated = string.Format("{0}\n[" + NGUITools.EncodeColor(Color.white) + "]{1}", formated, item.varianceID);
                    break;
                case DisaplyOption.itemID:
                    formated = string.Format("{0}\n[" + NGUITools.EncodeColor(Color.white) + "]{1}", formated, item.itemID);
                    break;
                case DisaplyOption.classID:
                    formated = string.Format("{0}\n[" + NGUITools.EncodeColor(Color.white) + "]{1}", formated, item.classID);
                    break;
                case DisaplyOption.energy:
                    formated = string.Format("{0}\n[" + NGUITools.EncodeColor(Color.white) + "]{1}", formated, item.totalEnergy);
                    break;
                case DisaplyOption.stackID:
                    formated = string.Format("{0}\n[" + NGUITools.EncodeColor(Color.white) + "]{1}", formated, item.stackID.ToString());
                    break;

                default: break;
            }
        }
        return formated;
    }
}


