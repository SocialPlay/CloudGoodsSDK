//
//  SocialplayiOSConnect.m
//  Unity-iPhone
//
//  Created by TejGill on 2014-06-25.
//
//

#import "SocialplayiOSConnect.h"

@implementation SocialplayiOSConnect

NSArray *products;
SKProduct *Product;
SKProductsRequest *productsRequest;

- (id)init
{
    self = [super init];
    [[SKPaymentQueue defaultQueue] addTransactionObserver:self];
    return self;
}

- (void)requestProductData:(NSString *)productID
{
    UnitySendMessage("iOSConnect", "ReceivedMessageFromXCode", "requesting product");
    
    NSSet *productIdentifiers = [NSSet setWithObject:productID ];
    productsRequest = [[SKProductsRequest alloc] initWithProductIdentifiers:productIdentifiers];
    productsRequest.delegate = self;
    [productsRequest start];
}

#pragma mark -
#pragma mark SKProductsRequestDelegate methods

- (void)productsRequest:(SKProductsRequest *)request didReceiveResponse:(SKProductsResponse *)response
{
    NSArray *products = response.products;
    Product = [products count] == 1 ? [[products firstObject] retain] : nil;
    if (Product)
    {
        NSLog(@"Product title: %@" , Product.localizedTitle);
        NSLog(@"Product description: %@" , Product.localizedDescription);
        NSLog(@"Product price: %@" , Product.price);
        NSLog(@"Product id: %@" , Product.productIdentifier);
        
        SKMutablePayment *payment = [SKMutablePayment paymentWithProduct:Product];
        payment.quantity = 1;
        
        [[SKPaymentQueue defaultQueue] addPayment:payment];

    }
    
    for (NSString *invalidProductId in response.invalidProductIdentifiers)
    {
        NSLog(@"Invalid product id: %@" , invalidProductId);
        
        UnitySendMessage("iOSConnect", "ReceivedMessageFromXCode", "Invalid ProductID");
    }
    
    [productsRequest release];
    
    [[NSNotificationCenter defaultCenter] postNotificationName:kInAppPurchaseManagerProductsFetchedNotification object:self userInfo:nil];
}

- (void)paymentQueue:(SKPaymentQueue *)queue
 updatedTransactions:(NSArray *)transactions
{
    for (SKPaymentTransaction *transaction in transactions) {
        switch (transaction.transactionState) {
                // Call the appropriate custom method.
            case SKPaymentTransactionStatePurchased:
                [self completeTransaction:transaction];
                break;
            case SKPaymentTransactionStateFailed:
                [self failedTransaction:transaction];
                break;
            case SKPaymentTransactionStateRestored:
                [self restoreTransaction:transaction];
            default:
                break;
        }
    }
}

//
// removes the transaction from the queue and posts a notification with the transaction result
//
- (void)finishTransaction:(SKPaymentTransaction *)transaction wasSuccessful:(BOOL)wasSuccessful
{
    NSLog(@"finish transaction");
    
    // remove the transaction from the payment queue.
    [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
    
    if (wasSuccessful)
    {
        // send out a notification that we’ve finished the transaction
        UnitySendMessage("iOSConnect", "ReceivedMessageFromXCode", "Success");
    }
    else
    {
        // send out a notification for the failed transactionß
        UnitySendMessage("iOSConnect", "ReceivedMessageFromXCode", "Failed");
    }
}

-(void)completeTransaction:(SKPaymentTransaction *) finishedTransaction
{
    NSLog(@"Complete transaction");
    
    [self finishTransaction:finishedTransaction wasSuccessful:YES];
}

-(void)failedTransaction:(SKPaymentTransaction *) finishedTransaction
{
    NSLog(@"failed transaction");
    
    if (finishedTransaction.error.code != SKErrorPaymentCancelled)
    {
        // error!
        NSLog(@"Error occured");
        [self finishTransaction:finishedTransaction wasSuccessful:NO];
    }
    else
    {
        NSLog(@"Cancel happened, finish transaction");
        // this is fine, the user just cancelled, so don’t notify
        UnitySendMessage("iOSConnect", "ReceivedMessageFromXCode", "Cancelled");
        [[SKPaymentQueue defaultQueue] finishTransaction:finishedTransaction];
    }
}


-(void)restoreTransaction:(SKPaymentTransaction *) finishedTransaction
{
    NSLog(@"restore transaction");
}


@end

NSString* CreateNSString (const char* string)
{
	if (string)
		return [NSString stringWithUTF8String: string];
	else
		return [NSString stringWithUTF8String: ""];
}


extern "C"{
    
    NSMutableArray *productArray;
    SocialplayiOSConnect *socialplay;
    
    void _PrintMessageFromUnity(const char* message){
        
        NSString *unityMessage = CreateNSString(message);
        NSLog(@"Unity message : %@", unityMessage);
        
        if(socialplay == nil)
        {
            NSLog(@"Socialplay object not initialized, init now");
            socialplay = [[SocialplayiOSConnect alloc] init];
        }
        
        [socialplay requestProductData:unityMessage];

    }
}