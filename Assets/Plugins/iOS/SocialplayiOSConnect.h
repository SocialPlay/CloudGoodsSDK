//
//  SocialplayiOSConnect.h
//  Unity-iPhone
//
//  Created by TejGill on 2014-06-25.
//
//

#import <Foundation/Foundation.h>
#import <StoreKit/StoreKit.h>

#define kInAppPurchaseManagerTransactionFailedNotification @"kInAppPurchaseManagerTransactionFailedNotification"
#define kInAppPurchaseManagerTransactionSucceededNotification @"kInAppPurchaseManagerTransactionSucceededNotification"


#define kInAppPurchaseManagerProductsFetchedNotification @"kInAppPurchaseManagerProductsFetchedNotification"

@interface SocialplayiOSConnect : NSObject <SKProductsRequestDelegate, SKPaymentTransactionObserver>

-(id)init;
- (void)productsRequest:(SKProductsRequest *)request;


@end
