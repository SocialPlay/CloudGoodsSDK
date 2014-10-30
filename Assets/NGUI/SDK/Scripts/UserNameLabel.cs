using UnityEngine;
using System.Collections;

public class UserNameLabel : MonoBehaviour
{
    public UILabel label;

    void OnEnable()
    {
		CloudGoods.OnUserAuthorized += OnUserAuthorized;
    }
    void OnDisable()
    {
		CloudGoods.OnUserAuthorized -= OnUserAuthorized;
    }

	void OnUserAuthorized(CloudGoodsUser user)
    {
        label.text = CloudGoods.user.userName;
    }
}
