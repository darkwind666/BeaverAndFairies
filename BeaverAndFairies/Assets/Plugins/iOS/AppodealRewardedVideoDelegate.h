#import <Foundation/Foundation.h>
#import <Appodeal/Appodeal.h>

typedef void (*AppodealRewardedVideoCallbacks) ();
typedef void (*AppodealRewardedVideoDidFinishCallback) (int, const char *);

@interface AppodealRewardedVideoDelegate : NSObject <AppodealRewardedVideoDelegate>

@property (assign, nonatomic) AppodealRewardedVideoCallbacks rewardedVideoDidLoadAdCallback;
@property (assign, nonatomic) AppodealRewardedVideoCallbacks rewardedVideoDidFailToLoadAdCallback;
@property (assign, nonatomic) AppodealRewardedVideoCallbacks rewardedVideoWillDismissCallback;
@property (assign, nonatomic) AppodealRewardedVideoCallbacks rewardedVideoDidPresentCallback;
@property (assign, nonatomic) AppodealRewardedVideoDidFinishCallback rewardedVideoDidFinishCallback;

@end
