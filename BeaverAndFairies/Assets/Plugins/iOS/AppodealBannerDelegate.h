#import <Foundation/Foundation.h>
#import <Appodeal/Appodeal.h>

typedef void (*AppodealBannerCallbacks) ();

@interface AppodealBannerDelegate : NSObject <AppodealBannerDelegate>

@property (assign, nonatomic) AppodealBannerCallbacks bannerDidLoadAdCallback;
@property (assign, nonatomic) AppodealBannerCallbacks bannerDidFailToLoadAdCallback;
@property (assign, nonatomic) AppodealBannerCallbacks bannerDidClickCallback;
@property (assign, nonatomic) AppodealBannerCallbacks bannerDidShowCallback;

@end