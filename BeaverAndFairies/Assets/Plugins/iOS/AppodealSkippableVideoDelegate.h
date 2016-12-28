#import <Foundation/Foundation.h>
#import <Appodeal/Appodeal.h>

typedef void (*AppodealSkippableVideoCallbacks) ();

@interface AppodealSkippableVideoDelegate : NSObject <AppodealSkippableVideoDelegate>

@property (assign, nonatomic) AppodealSkippableVideoCallbacks skippableVideoDidLoadAdCallback;
@property (assign, nonatomic) AppodealSkippableVideoCallbacks skippableVideoDidFailToLoadAdCallback;
@property (assign, nonatomic) AppodealSkippableVideoCallbacks skippableVideoDidPresentCallback;
@property (assign, nonatomic) AppodealSkippableVideoCallbacks skippableVideoWillDismissCallback;
@property (assign, nonatomic) AppodealSkippableVideoCallbacks skippableVideoDidFinishCallback;

@end
