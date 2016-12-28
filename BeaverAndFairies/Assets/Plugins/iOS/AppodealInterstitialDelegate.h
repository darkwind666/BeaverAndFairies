#import <Foundation/Foundation.h>
#import <Appodeal/Appodeal.h>

typedef void (*AppodealInterstitialCallbacks) ();

@interface AppodealInterstitialDelegate : NSObject <AppodealInterstitialDelegate>

@property (assign, nonatomic) AppodealInterstitialCallbacks interstitialDidLoadCallback;
@property (assign, nonatomic) AppodealInterstitialCallbacks interstitialDidFailToLoadAdCallback;
@property (assign, nonatomic) AppodealInterstitialCallbacks interstitialWillPresentCallback;
@property (assign, nonatomic) AppodealInterstitialCallbacks interstitialDidDismissCallback;
@property (assign, nonatomic) AppodealInterstitialCallbacks interstitialDidClickCallback;

@end