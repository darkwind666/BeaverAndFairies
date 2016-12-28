#import "AppodealInterstitialDelegate.h"

@implementation AppodealInterstitialDelegate

-(void) interstitialDidLoadAdisPrecache:(BOOL)precache {
    if(self.interstitialDidLoadCallback) {
        self.interstitialDidLoadCallback();
    }
}

-(void) interstitialDidClick {
    if(self.interstitialDidClickCallback) {
        self.interstitialDidClickCallback();
    }
}

-(void) interstitialDidDismiss {
    if(self.interstitialDidDismissCallback) {
        self.interstitialDidDismissCallback();
    }
}

-(void) interstitialDidFailToLoadAd {
    if(self.interstitialDidFailToLoadAdCallback) {
        self.interstitialDidFailToLoadAdCallback();
    }
}

-(void) interstitialWillPresent {
    if(self.interstitialWillPresentCallback) {
        self.interstitialWillPresentCallback();
    }
}

@end
