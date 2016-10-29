package com.playgenesis.vkunityplugin;

import com.unity3d.player.UnityPlayer;
import com.vk.sdk.R;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.Intent;
import android.database.CursorIndexOutOfBoundsException;
import android.os.Bundle;
import android.view.WindowManager;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.webkit.WebViewClient;

@SuppressLint("SetJavaScriptEnabled")
public class WebViewActivity extends Activity {
	Boolean lock=false;
	String openUrl;
	String closeUrl;
	String currnetUrl;

	public void closeWebView() {
		if(!lock){
			UnityPlayer.UnitySendMessage("VkApi", "WebViewDone", currnetUrl);
			lock=true;
		}
		WebViewActivity.this.finish();
	}

	@Override
	public void onBackPressed() {
		currnetUrl=AddQuestionMark(currnetUrl) +"&cancel=1";
		closeWebView();
	}
	public String AddQuestionMark(String url){
		if(url.contains("?")){
			return url;
		}else{
			return url+"?";
		}
	}
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);
		setContentView(R.layout.activity_web_view);

		Intent passedData = getIntent();
		openUrl = passedData.getStringExtra("openUrl");
		closeUrl = passedData.getStringExtra("closeUrl");

		WebView myWebView = (WebView) findViewById(R.id.webview);
		WebSettings webSettings = myWebView.getSettings();
		webSettings.setJavaScriptEnabled(true);
		myWebView.setWebViewClient(new WebViewClient() {

			@Override
			public void onPageFinished(WebView view, String url) {
				currnetUrl = url;
				if (url.startsWith(closeUrl)) {
					closeWebView();
					
				}
			}

			@Override
			public void onReceivedError(WebView view, int errorCode, String description, String failingUrl) {
				
				currnetUrl=AddQuestionMark(failingUrl) + "&" + "network_error=1";
				closeWebView();
			}

			@Override
			public boolean shouldOverrideUrlLoading(WebView view, String url) {
				return false;
			}

		});
		myWebView.loadUrl(openUrl);
	}
}
