#import "PGUtilities.h"

@implementation PGUtilities

+(NSMutableDictionary*)parseVkAuthUrl:(const char*) authUrl{
    NSMutableDictionary *queryStringDictionary = [[NSMutableDictionary alloc] init];
    
    NSString * urlstring=[NSString stringWithUTF8String:authUrl];
    
    NSArray* preUrlparams= [urlstring componentsSeparatedByString:@"?"];
    NSArray *urlparams = [[preUrlparams objectAtIndex:1] componentsSeparatedByString:@"&"];
    
    for (NSString *keyValuePair in urlparams)
    {
        NSArray *pairComponents = [keyValuePair componentsSeparatedByString:@"="];
        NSString *key = [[pairComponents firstObject] stringByRemovingPercentEncoding];
        NSString *value = [[pairComponents lastObject] stringByRemovingPercentEncoding];
        
        
        [queryStringDictionary setObject:value forKey:key];
    }
    return queryStringDictionary;
}
+(NSString*)convertToNSString:(const char*) CString
{
    return [[NSString alloc] initWithUTF8String:CString];
}
+(void) ParseTokenAndSendToUnity:(NSString*) url
{
    NSArray * stringSplit=[url componentsSeparatedByString:@"#"];
    
    NSArray* secondSplit=[stringSplit[1] componentsSeparatedByString:@"&"];
    
    NSString* accesstoken = nil;
    NSString* userid = nil;
    NSString* expiresIn = nil;
    
    for (id param in secondSplit) {
        
        NSString *paramName = [[NSString alloc]initWithString:[param componentsSeparatedByString:@"="][0]] ;
        
        if([paramName containsString: @"access_token"]){
            accesstoken=[param componentsSeparatedByString:@"="][1];
        }else if([paramName containsString: @"expires_in"]){
            expiresIn=[param componentsSeparatedByString:@"="][1];
        }else if([paramName containsString:@"user_id"]){
            userid =[param componentsSeparatedByString:@"="][1];
        }
    }
    
    [PGUtilities sendTokenInfoToUnity:accesstoken userid:userid expirationTime:expiresIn];
    
}

+(void) sendTokenInfoToUnity:(NSString*)accesstoken
                      userid:(NSString*)userid
              expirationTime:(NSString*)expiresIn
{
    if(expiresIn==nil)
    {
        expiresIn=@"0";
    }
    NSString* unityMessage=[NSString stringWithFormat:@"%@%@%@%@%@", accesstoken,@"#",expiresIn,@"#",userid];
    UnitySendMessage("VkApi", "ReceiveNewTokenMessage",[unityMessage cStringUsingEncoding:NSUTF8StringEncoding]);
    
}

@end
