//
//  MyVKSdk.m
//  Unity-iPhone
//
//  Created by Admin on 09.04.16.
//
//

#import "PGVKSdk.h"

@implementation PGVKSdk

NSString *AUTHORIZE_URL_STRING = @"vkauthorize://authorize?sdk_version=1.3.16&";

+ (BOOL)vkAppMayExists {

    return [[UIApplication sharedApplication] canOpenURL:[NSURL URLWithString:AUTHORIZE_URL_STRING]];

}

+ (void)logout{
    NSArray *cookies = [[NSHTTPCookieStorage sharedHTTPCookieStorage] cookies];
    
    for (NSHTTPCookie *cookie in cookies)
        if (NSNotFound != [cookie.domain rangeOfString:@"vk.com"].location) {
            [[NSHTTPCookieStorage sharedHTTPCookieStorage]
             deleteCookie:cookie];
        }
}

+ (void)authorizeWithVkApp:(NSString *)authUrl{
    
    
    NSString* pre1UrlString = [authUrl stringByReplacingOccurrencesOfString:@"https://oauth.vk.com/authorize?" withString:AUTHORIZE_URL_STRING];
    
    NSString* pre2UrlString = [pre1UrlString stringByReplacingOccurrencesOfString:@"," withString:@"%2C"];
   
    
    NSString* pre3UrlString = [pre2UrlString stringByReplacingOccurrencesOfString:@"&redirect_uri=https://oauth.vk.com/blank.html&display=mobile&forceOAuth=False" withString:@"&"];
    
    NSString* pre4UrlString = [pre3UrlString stringByReplacingOccurrencesOfString:@"&response_type=token" withString:@""];
    
    NSString* urlString;
    if([pre1UrlString containsString:@"revokeAccess=False"])
    {
        urlString = [pre4UrlString stringByReplacingOccurrencesOfString:@"revokeAccess=False" withString:@"revoke=0"];
    }else
    {
        urlString = [pre4UrlString stringByReplacingOccurrencesOfString:@"revokeAccess=True" withString:@"revoke=1"];
    }
    
    //&response_type=token
    //&redirect_uri=https://oauth.vk.com/blank.html&display=mobile&forceOAuth=False
    
    
    NSURL *urlToOpen = [NSURL URLWithString:urlString];
   
    //NSURL *urlToOpen = [NSURL URLWithString:@"http://google.com"];
   
    [[UIApplication sharedApplication] openURL:urlToOpen];
   
}
@end
