//
//  PlayGenesisUtilities.h
//  Unity-iPhone
//
//  Created by Admin on 09.04.16.
//
//

#import <Foundation/Foundation.h>

@interface PGUtilities : NSObject
+(NSMutableDictionary*)parseVkAuthUrl:(const char*) authUrl;
+(NSString*)convertToNSString:(const char*) CString;
+(void) ParseTokenAndSendToUnity:(NSString*) url;
+(void) sendTokenInfoToUnity:(NSString*)accesstoken userid:(NSString*)userid expirationTime:(NSString*)expiresIn;
@end
