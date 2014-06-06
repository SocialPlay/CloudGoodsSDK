using UnityEngine;
using System.Collections;

public class PurchaseResponsePopupHandler : MonoBehaviour
{
    public StatusWindow statusWindow; 

    public string failedPurchaseTitle = "Could Not Purchase";
    public string insufficientFundMessage = "You have insufficient funds, go farm!";
    public string amountErrorMessage = "Somthing went wrong with the amount, try again";
    public string failedPurchaseMessage = "Somthing went wrong :s Sorry! Please try again";
    public string purchaseSuccessTitle = "Item Purchased";
    public string purchaseSuccessMessage = "Purchase successful";

    public void OnPurchaseSuccess()
    {
        //Logic For on purchase Success
        statusWindow.SetStatusMessage(purchaseSuccessTitle, purchaseSuccessMessage);
    }

    public void HandlePurchaseAmountError()
    {
       //Logic For on purchase Error
        statusWindow.SetStatusMessage("Error", "There was an error with your purchase");
    }

    public void OnPurchaseFail()
    {
        //logic for CallWithGetTextureOnSameFrame purchase fail
    }

    public void HandleNSF()
    {
        //logic for not enough resources
    }
}
