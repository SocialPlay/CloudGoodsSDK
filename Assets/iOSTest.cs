using UnityEngine;
using System.Collections;

public class iOSTest : MonoBehaviour {

	public void PurchaseTest()
	{
		iOSConnect.RequestInAppPurchase ("com.socialplay.store.credit15");
	}
}
