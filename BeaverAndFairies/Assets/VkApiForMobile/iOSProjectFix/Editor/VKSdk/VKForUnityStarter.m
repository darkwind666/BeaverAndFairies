//
//  Starter.m
//  Unity-iPhone
//
//  Created by Admin on 25.05.15.
//
//

#import "VKForUnityStarter.h"
#import "PlayGenesisWebViewController.h"
#import "PGVKSdk.h"

extern UIViewController*	UnityGetGLViewController();
static VKForUnityStarter * sdkHandler;

void _VkAuthorization(const char* authUrl){
    
    if(sdkHandler==nil)
    sdkHandler = [[VKForUnityStarter alloc] init];
    
    [sdkHandler autorize:authUrl];
}
bool _IsVkAppPresent(){
    return [PGVKSdk vkAppMayExists];
}
void _OpenWebView(const char * redirectUrl,const char * closeWhenNavigetedTo )
{
    UIViewController *main=UnityGetGLViewController();
    
    PlayGenesisWebViewController *vc = [[PlayGenesisWebViewController alloc]init];
    
    vc.modalPresentationStyle=	UIModalPresentationOverCurrentContext;
    
    vc.closeuri=[[[NSString alloc] initWithCString:closeWhenNavigetedTo
                                          encoding:NSUTF8StringEncoding]autorelease];
    
    vc.openuri=[[[NSString alloc] initWithCString:redirectUrl
                                         encoding:NSUTF8StringEncoding]autorelease];
    [main presentViewController:vc animated:YES completion:
     ^(void){
         [vc loadWebPageWithUrl:[[[NSString alloc] initWithString:vc.openuri]autorelease]];
         
     }];
}

void _doLogOutUser(){
    [PGVKSdk logout];
}
@interface VKForUnityStarter ()<UIAlertViewDelegate>

@end

@implementation VKForUnityStarter

- (id)init {
    self=[super init];
    [self registerNotificationReciever];
    return self;
}

/*- (void)vkSdkAccessAuthorizationFinishedWithResult:(VKAuthorizationResult *)result;
{
    if(result==nil || result.token==nil){
        NSString* unityMessage=[NSString stringWithFormat:@"%ld/%@/%@", (long)1, @"#",  @"AuthorizationFailed"];
        UnitySendMessage("VkApi", "AccessDeniedMessage",[unityMessage cStringUsingEncoding:NSASCIIStringEncoding]);
        [self release];
    }else{
        
        [self parseAccessTokenAndSendToUnity:result.token];
    }
    
}*/



-(void) registerNotificationReciever{
    [[NSNotificationCenter defaultCenter] addObserver:self
                                             selector:@selector(receiveOpenUrl:)
                                                 name:@"kUnityOnOpenURL"
                                               object:nil];
}
- (void) receiveOpenUrl:(NSNotification *) notification
{
    
    NSDictionary *userInfo = notification.userInfo;
    NSString * notificationName=[notification name];
    NSURL * url = [userInfo objectForKey:@"url"];
    if(![notificationName containsString: @"kUnityOnOpenURL"] || url.absoluteString.length==0)
    {
        return;
    }
    if([url.absoluteString containsString:@"error="])
    {
        NSString* unityMessage=[NSString stringWithFormat:@"%ld/%@/%@", (long)1, @"#",  @"AuthorizationFailed"];
        UnitySendMessage("VkApi", "AccessDeniedMessage",[unityMessage cStringUsingEncoding:NSASCIIStringEncoding]);
        return;
    }
    [PGUtilities ParseTokenAndSendToUnity:url.absoluteString];
}

-(void)autorize:(const char*) authUrl{
    
    NSMutableDictionary* queryStringDictionary=[PGUtilities parseVkAuthUrl:authUrl];

    BOOL forceoauth=[[queryStringDictionary objectForKey:@"forceOAuth"] boolValue];
    
    if(forceoauth || ![PGVKSdk vkAppMayExists]) {
        UnitySendMessage("VkApi","NoVkApp",[@"" cStringUsingEncoding:NSASCIIStringEncoding]);

        return;
    }
    [PGVKSdk authorizeWithVkApp:[[PGUtilities convertToNSString:authUrl] autorelease]];
}

-(void)dealloc{
    [super dealloc];
    
}


@end

