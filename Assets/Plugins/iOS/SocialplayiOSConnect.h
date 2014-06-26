//
//  SocialplayiOSConnect.h
//  Unity-iPhone
//
//  Created by TejGill on 2014-06-25.
//
//

#import <Foundation/Foundation.h>
#import <StoreKit/StoreKit.h>

@interface SocialplayiOSConnect : NSObject

-(id)init;

- (void)validateProductIdentifiers:(NSArray *)productIdentifiers;

- (void)productsRequest:(SKProductsRequest *)request;

@end
