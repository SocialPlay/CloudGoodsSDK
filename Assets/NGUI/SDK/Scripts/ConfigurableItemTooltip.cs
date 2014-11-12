using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class ConfigurableItemTooltip : MonoBehaviour, ITooltipSetup
{

    public List<DisaplyOption> displayOptions = new List<DisaplyOption>();
    public bool isQualityColorUsed = true;
    public bool ShowBlankLineForEmptySelection = false;



    public enum DisaplyOption
    {
        name, stats, quantity, description, tag, behaviour, behaviourWithDescription, space, varianceID, itemID, energy, classID, stackID
    }

    ItemData item;


    public string Setup()
    {
        item = GetComponent<ItemDataComponent>().itemData;
        string formated = "";


        foreach (DisaplyOption selectedOption in displayOptions)
        {
            switch (selectedOption)
            {
                case DisaplyOption.name:
                    if (isQualityColorUsed)
                    {
                        formated += item.itemName.ToRichColor(ItemQuailityColorSelector.GetColorForItem(item));
                    }
                    else
                    {
                        formated += item.itemName;
                    }
                    break;
                case DisaplyOption.stats:
                    foreach (KeyValuePair<string, float> pair in item.stats)
                    {
                        if (pair.Key == "Not Available") continue;

                        formated = string.Format("{0}\n{1}: {2}", formated, pair.Key, pair.Value.ToString().ToRichColor(Color.yellow));
                    }
                    break;
                case DisaplyOption.quantity:
                    formated = string.Format("{0}\n{1}", formated, item.stackSize);
                    break;
                case DisaplyOption.description:
                    if (!string.IsNullOrEmpty(item.description))
                    {
                        formated = string.Format("{0}\n{1}", formated, item.description);
                    }
                    break;

                case DisaplyOption.tag:

                    string tags = "";
                    foreach (string tg in item.tags)
                    {
                        if (string.IsNullOrEmpty(tags))
                        {
                            tags = tg;
                        }
                        else
                        {
                            tags = string.Format("{0}, {1}", tags, tg);
                        }
                    }
                    if (item.tags.Count == 0)
                    {
                        if (ShowBlankLineForEmptySelection)
                            formated = string.Format("{0}\n");
                    }
                    else
                    {
                        formated = string.Format("{0}\n{1}", formated, tags);
                    }
                    break;
                case DisaplyOption.behaviour:

                    foreach (BehaviourDefinition behaviour in item.behaviours)
                    {
                        formated = string.Format("{0}\n{1}", formated, behaviour.Name);
                    }

                    if (item.behaviours.Count == 0 && ShowBlankLineForEmptySelection)
                    {
                        formated = string.Format("{0}\n", formated);
                    }
                    break;
                case DisaplyOption.behaviourWithDescription:
                    foreach (BehaviourDefinition behaviour in item.behaviours)
                    {
                        formated = string.Format("{0}\n{1}: {2}", formated, behaviour.Name.ToRichColor(Color.white), behaviour.Description.ToRichColor(Color.grey));
                    }
                    if (item.behaviours.Count == 0 && ShowBlankLineForEmptySelection)
                    {
                        formated = string.Format("{0}\n", formated);
                    }
                    break;
                case DisaplyOption.space:
                    formated = string.Format("{0}\n", formated);
                    break;
                case DisaplyOption.varianceID:
                    formated = string.Format("{0}\n{1}", formated, item.ItemID);
                    break;
                case DisaplyOption.itemID:
                    formated = string.Format("{0}\n{1}", formated, item.CollectionID);
                    break;
                case DisaplyOption.classID:
                    formated = string.Format("{0}\n{1}", formated, item.classID);
                    break;
                case DisaplyOption.energy:
                    formated = string.Format("{0}\n{1}", formated, item.totalEnergy);
                    break;
                case DisaplyOption.stackID:
                    formated = string.Format("{0}\n{1}", formated, item.stackID.ToString());
                    break;

                default: break;
            }
        }
        return formated;
    }
}


