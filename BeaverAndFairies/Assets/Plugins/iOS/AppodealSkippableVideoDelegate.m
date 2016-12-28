#import "AppodealSkippableVideoDelegate.h"

@implementation AppodealSkippableVideoDelegate

-(void) skippableVideoDidLoadAd {
    if(self.skippableVideoDidLoadAdCallback) {
        self.skippableVideoDidLoadAdCallback();
    }
}

-(void) skippableVideoDidFailToLoadAd {
    if(self.skippableVideoDidFailToLoadAdCallback) {
        self.skippableVideoDidFailToLoadAdCallback();
    }
}

-(void) skippableVideoDidClick { }

-(void) skippableVideoDidFinish {
    if(self.skippableVideoDidFinishCallback) {
        self.skippableVideoDidFinishCallback();
    }
}

-(void) skippableVideoDidPresent {
    if(self.skippableVideoDidPresentCallback) {
        self.skippableVideoDidPresentCallback();
    }
}

-(void) skippableVideoWillDismiss {
    if(self.skippableVideoWillDismissCallback) {
        self.skippableVideoWillDismissCallback();
    }
}

@end