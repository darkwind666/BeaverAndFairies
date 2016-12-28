#import "AppodealRewardedVideoDelegate.h"

@implementation AppodealRewardedVideoDelegate

-(void) rewardedVideoDidLoadAd {
    if(self.rewardedVideoDidLoadAdCallback) {
        self.rewardedVideoDidLoadAdCallback();
    }
}

-(void) rewardedVideoDidFailToLoadAd {
    if(self.rewardedVideoDidFailToLoadAdCallback) {
        self.rewardedVideoDidFailToLoadAdCallback();
    }
}

-(void) rewardedVideoDidClick { }

-(void) rewardedVideoDidPresent {
    if(self.rewardedVideoDidPresentCallback) {
        self.rewardedVideoDidPresentCallback();
    }
}

-(void) rewardedVideoWillDismiss {
    if(self.rewardedVideoWillDismissCallback) {
        self.rewardedVideoWillDismissCallback();
    }
}

- (void)rewardedVideoDidFinish:(NSUInteger)rewardAmount name:(NSString *)rewardName {
    if (self.rewardedVideoDidFinishCallback) {
        self.rewardedVideoDidFinishCallback((int)rewardAmount, [rewardName UTF8String]);
    }
}

@end
