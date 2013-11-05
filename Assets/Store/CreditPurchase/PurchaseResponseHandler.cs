using UnityEngine;
using System.Collections;

public class PurchaseResponseHandler : MonoBehaviour
{
    public string failedPurchaseTitle = "Could Not Purchase";
    public string insufficientFundMessage = "You have insufficient funds, go farm!";
    public string amountErrorMessage = "Somthing went wrong with the amount, try again";
    public string failedPurchaseMessage = "Somthing went wrong :s Sorry! Please try again";
    public string purchaseSuccessTitle = "Item Purchased";
    public string purchaseSuccessMessage = "Purchase successful";

    public void HandlePurchaseSuccess()
    {
        //MessagePop.Instance.PopMessage(purchaseSuccessTitle, purchaseSuccessMessage);
    }

    public void HandlePurchaseAmountError()
    {
        ///MessagePop.Instance.PopMessage(failedPurchaseTitle, amountErrorMessage);
    }

    public void HandleGeneralPurchaseFail()
    {
        //MessagePop.Instance.PopMessage(failedPurchaseTitle, failedPurchaseMessage);
    }

    public void HandleNSF()
    {
        //MessagePop.Instance.PopMessage(failedPurchaseTitle, insufficientFundMessage);
    }
}
