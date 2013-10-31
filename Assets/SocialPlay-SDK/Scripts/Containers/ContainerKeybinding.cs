using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;

public class ContainerKeybinding : MonoBehaviour {

    public KeyCode binding;
    public ActionType actiontype = ActionType.toggle;
    private ItemContainer container;

    public enum ActionType
    {
        toggle,open,close
    }

    void Awake()
    {
        container = gameObject.GetIComponent<ItemContainer>();
    }

	void Update () {
   
        if (Input.GetKeyDown(binding)){
            switch (actiontype)
            {
                case ActionType.toggle:
                    container.IsActive = !container.IsActive;
                    break;
                case ActionType.open:
                    container.IsActive = true;
                    break;
                case ActionType.close:
                    container.IsActive = false;
                    break;
                default:
                    break;
            }
        }
	}
}
