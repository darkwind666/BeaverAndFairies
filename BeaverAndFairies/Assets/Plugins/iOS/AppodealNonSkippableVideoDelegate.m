#import "AppodealNonSkippableVideoDelegate.h"

@implementation AppodealNonSkippableVideoDelegate

-(void) nonSkippableVideoDidLoadAd {
    if(self.nonSkippableVideoDidLoadAdCallback) {
        self.nonSkippableVideoDidLoadAdCallback();
    }
}

-(void) nonSkippableVideoDidFailToLoadAd {
    if(self.nonSkippableVideoDidFailToLoadAdCallback) {
        self.nonSkippableVideoDidFailToLoadAdCallback();
    }
}

-(void) nonSkippableVideoDidClick { }

-(void) nonSkippableVideoDidFinish {
    if(self.nonSkippableVideoDidFinishCallback) {
        self.nonSkippableVideoDidFinishCallback();
    }
}

-(void) nonSkippableVideoDidPresent {
    if(self.nonSkippableVideoDidPresentCallback) {
        self.nonSkippableVideoDidPresentCallback();
    }
}

-(void) nonSkippableVideoWillDismiss {
    if(self.nonSkippableVideoWillDismissCallback) {
        self.nonSkippableVideoWillDismissCallback();
    }
}

@end