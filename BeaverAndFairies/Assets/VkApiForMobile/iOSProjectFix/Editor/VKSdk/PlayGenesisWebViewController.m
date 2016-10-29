//
//  ViewController.m
//  Unity-iPhone
//
//  Created by Admin on 05.11.14.
//
//

#import "PlayGenesisWebViewController.h"

@interface PlayGenesisWebViewController ()
@property (nonatomic) BOOL lock;
@end

@implementation PlayGenesisWebViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    self.lock=false;
    self.webv.delegate=self;
    
    self.grear.alpha=1.0;
    self.webv.alpha=0;
    self.cancel_btn.alpha=0;
    [UIView animateWithDuration:0.5 animations:^(){
        self.webv.alpha=1;
        self.cancel_btn.alpha=1;
    }];
   }

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (void)loadWebPageWithUrl:(NSString *) url
{
    NSURL * _url=[[NSURL alloc]initWithString:url];
    NSURLRequest * request =[[NSURLRequest alloc] initWithURL:_url];
    [self.webv loadRequest:request];
}

- (IBAction)cancelBClick:(id)sender {
    
    const char* message=[[NSString stringWithFormat:@"%@*cancel=1",self.webv.request.URL.absoluteString] cStringUsingEncoding:NSASCIIStringEncoding];
    
    UnitySendMessage("VkApi","WebViewDone",message);
    self.lock=true;
    [self CloseWebView];
}

- (void)webViewDidStartLoad:(UIWebView *)webView
{
    [self.grear startAnimating];
    
}

- (void)webViewDidFinishLoad:(UIWebView *)webView
{
     if(self.lock) return;
    [self.grear stopAnimating];
    
        CGSize contentSize = webView.scrollView.contentSize;
        CGSize viewSize = webView.bounds.size;
        
        float rw = viewSize.width / contentSize.width;
        
        webView.scrollView.minimumZoomScale = rw;
        webView.scrollView.maximumZoomScale = rw;
        webView.scrollView.zoomScale = rw;
        NSLog(@"%@", webView.request.URL.absoluteString);
        NSString * currenturl =webView.request.URL.absoluteString;
        if([currenturl hasPrefix:self.closeuri])
        {
            self.lock=true;
            [self PrepareWebDataAndSendToUnity:currenturl];
            [self CloseWebView];
        }
    
    
    
}
-(void) PrepareWebDataAndSendToUnity:(NSString*)url{
    NSMutableString *webviewdata=[[[NSMutableString alloc]initWithString:@""]autorelease];
    [webviewdata appendString:url];
   
    
    UnitySendMessage("VkApi",
                     "WebViewDone",
                     [webviewdata cStringUsingEncoding:NSASCIIStringEncoding]);
}
-(void) CloseWebView{
    
    [self dismissViewControllerAnimated:YES completion:^{
        [self removeFromParentViewController];
        [self release];
        
    }];
}
- (void)webView:(UIWebView *)webView didFailLoadWithError:(NSError *)error
{
    const char* message=[[NSString stringWithFormat:@"%@*%@", webView.request.URL.absoluteString,@"*network_error=1"] cStringUsingEncoding:NSASCIIStringEncoding];
    
    UnitySendMessage("VkApi","WebViewDone",message);
    self.lock=true;
    
    [self CloseWebView];
}




- (void)dealloc {
    [_grear release];
    [_webv release];
    
    [_cancel_btn release];
    
    [super dealloc];
    
}
@end
