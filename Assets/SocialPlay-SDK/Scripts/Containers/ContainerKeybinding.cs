using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;
using System.Collections.Generic;

public class ContainerKeybinding : MonoBehaviour
{

    static bool isDisabaled = false;

    public KeyCode binding;
    public ActionType actiontype = ActionType.toggle;
    private ContainerDisplay containerDisplay;

    private static List<string> systemsLockingKeys = new List<string>();

    public enum ActionType
    {
        toggle, open, close
    }

    void Awake()
    {
        containerDisplay = gameObject.GetComponent<ContainerDisplay>();
        Debug.Log(containerDisplay);
    }

    void Update()
    {
  
        if (!isDisabaled && Input.GetKeyDown(binding))
        {
            switch (actiontype)
            {
                case ActionType.toggle:

                    containerDisplay.SetWindowIsActive(!containerDisplay.IsWindowActive());

                    break;
                case ActionType.open:
                    containerDisplay.SetWindowIsActive(true);
                    break;
                case ActionType.close:
                    containerDisplay.SetWindowIsActive(false);
                    break;
                default:
                    break;
            }
        }
    }

    public static void DisableKeybinding(string systemName)
    {
        if (!systemsLockingKeys.Contains(systemName))
            systemsLockingKeys.Add(systemName);

        isDisabaled = true;
    }

    public static void EnableKeybinding(string systemName)
    {
        systemsLockingKeys.Remove(systemName);
        if (systemsLockingKeys.Count == 0)
            isDisabaled = false;
    }
}
