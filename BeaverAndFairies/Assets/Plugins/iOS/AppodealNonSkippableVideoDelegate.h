#import <Foundation/Foundation.h>
#import <Appodeal/Appodeal.h>

typedef void (*AppodealNonSkippableVideoCallbacks) ();

@interface AppodealNonSkippableVideoDelegate : NSObject <AppodealNonSkippableVideoDelegate>

@property (assign, nonatomic) AppodealNonSkippableVideoCallbacks nonSkippableVideoDidLoadAdCallback;
@property (assign, nonatomic) AppodealNonSkippableVideoCallbacks nonSkippableVideoDidFailToLoadAdCallback;
@property (assign, nonatomic) AppodealNonSkippableVideoCallbacks nonSkippableVideoDidPresentCallback;
@property (assign, nonatomic) AppodealNonSkippableVideoCallbacks nonSkippableVideoWillDismissCallback;
@property (assign, nonatomic) AppodealNonSkippableVideoCallbacks nonSkippableVideoDidFinishCallback;

@end
