//
//  MyVKSdk.h
//  Unity-iPhone
//
//  Created by Admin on 09.04.16.
//
//

#import <Foundation/Foundation.h>

@interface PGVKSdk : NSObject
+ (BOOL)vkAppMayExists;
+ (void)authorizeWithVkApp:(NSString *)authUrl;
+ (void)logout;
@end
/*
*/