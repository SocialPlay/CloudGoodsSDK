using UnityEngine;
using System.Collections;

public class UISocialPlayNotification : MonoBehaviour
{
	public UIPanel panel;
	public UILabel titleLabel;
	public UILabel statusMessage;
	public UIButton button;

    public string failedPurchaseTitle = "Could Not Purchase";
    public string insufficientFundMessage = "You have insufficient funds, go farm!";
    public string amountErrorMessage = "Somthing went wrong with the amount, try again";
    public string failedPurchaseMessage = "Somthing went wrong :s Sorry! Please try again";
    public string purchaseSuccessTitle = "Item Purchased";
    public string purchaseSuccessMessage = "Purchase successful";

	public void Show(string title, string message)
	{
		titleLabel.text = title;
		statusMessage.text = message;
		NGUITools.SetActive(panel.cachedGameObject, true);
		TweenAlpha.Begin(panel.cachedGameObject, 0.1f, 1).from = 0;
	}

	public void Hide()
	{
		TweenAlpha ta = TweenAlpha.Begin(panel.cachedGameObject, 0.3f, 1);
		EventDelegate.Add(ta.onFinished, OnFinish, true);
	}

	void OnFinish()
	{
		NGUITools.SetActive(panel.cachedGameObject, false);
	}

    public void OnPurchaseSuccess()
    {
        //Logic For on purchase Success
		Show(purchaseSuccessTitle, purchaseSuccessMessage);
    }

    public void OnPurchaseAmountError()
    {
       //Logic For on purchase Error
		Show("Error", "There was an error with your purchase");
    }

    public void OnPurchaseFail()
    {
        //logic for CallWithGetTextureOnSameFrame purchase fail
		Show("Error", "There was an error with your purchase");
    }

    public void OnNotEnoughFunds()
    {
        //logic for not enough resources
		Show("Error", "You don't have enough funds");
    }
}
