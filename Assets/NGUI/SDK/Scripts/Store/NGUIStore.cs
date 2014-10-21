using UnityEngine;
using System.Collections;

public class NGUIStore : MonoBehaviour
{
	private static NGUIStore store;

	public GameObject StoreRootObject;

	bool isStoreCurrentActive = false;

	void Awake()
	{
		if (store == null)
		{
			store = this;
		}
	}


	public static void ActivateStore(bool loadItems)
	{
		if (store.isStoreCurrentActive) return;
		store.isStoreCurrentActive = true;
		store.StoreRootObject.SetActive(true);
		ContainerKeybinding.DisableKeybinding("Store");
		/*if (loadItems)
		{
			store.userCurrencyBalance.GetCurrencyBalance(null);
		}*/
	}

	public static void DeactivateStore()
	{
		if (!store.isStoreCurrentActive) return;
		store.isStoreCurrentActive = false;
		store.StoreRootObject.SetActive(false);
		ContainerKeybinding.EnableKeybinding("Store");
	}

	public static void ToggleStore(bool loadItems)
	{
		if (store.isStoreCurrentActive)
		{
			DeactivateStore();
		}
		else
		{
			ActivateStore(loadItems);
		}
	}
}


