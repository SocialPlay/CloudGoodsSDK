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

- (id)init
{
    self = [super init];
    return self;
}

// Custom method
- (void)validateProductIdentifiers:(NSArray *)productIdentifiers
{
    SKProductsRequest *productsRequest = [[SKProductsRequest alloc]
                                          initWithProductIdentifiers:[NSSet setWithArray:productIdentifiers]];
    productsRequest.delegate = self;
    [productsRequest start];
}

// SKProductsRequestDelegate protocol method
- (void)productsRequest:(SKProductsRequest *)request
     didReceiveResponse:(SKProductsResponse *)response
{
    products = response.products;
    
    if((sizeof response.invalidProductIdentifiers) > 0)
        UnitySendMessage("iOSConnect", "ReceivedMessageFromXCode", "Invalid identifier found");
    else
    {
        UnitySendMessage("iOSConnect", "ReceivedMessageFromXCode", "Valid Product found, do something else");
    }
    
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
    
    void _PrintMessageFromUnity(const char* message){
        
        NSString *unityMessage = CreateNSString(message);
        NSLog(@"Unity message : %@", unityMessage);
        
        productArray = [NSMutableArray array];
        [productArray addObject:unityMessage];
        
        SocialplayiOSConnect *socialplay = [[SocialplayiOSConnect alloc] init];
        [socialplay validateProductIdentifiers:productArray];

    }
}