//
//  ViewController.h
//  Unity-iPhone
//
//  Created by Admin on 05.11.14.
//
//

#import <UIKit/UIKit.h>
#import "PGUtilities.h"

@interface PlayGenesisWebViewController : UIViewController<UIWebViewDelegate>
@property (retain, nonatomic) IBOutlet UIActivityIndicatorView *grear;
@property (retain, nonatomic) IBOutlet UIWebView *webv;
@property (retain, nonatomic) IBOutlet UIButton *cancel_btn;
@property(nonatomic,retain) NSString *openuri;
@property(nonatomic,retain) NSString *closeuri;



- (void)loadWebPageWithUrl:(NSString *) url;
- (IBAction)cancelBClick:(id)sender;
@end
