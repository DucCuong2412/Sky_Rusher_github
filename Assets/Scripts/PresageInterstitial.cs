using UnityEngine;

public class PresageInterstitial
{
	public class InterstitialCallbackProxy : AndroidJavaProxy
	{
		private delegate void AdNotAvailable();

		private delegate void AdAvailable();

		private delegate void AdLoaded();

		private delegate void AdClosed();

		private delegate void AdError(int code);

		private delegate void AdDisplayed();

		private AdNotAvailable adNotAvailable;

		private AdAvailable adAvailable;

		private AdLoaded adLoaded;

		private AdClosed adClosed;

		private AdError adError;

		private AdDisplayed adDisplayed;

		public InterstitialCallbackProxy(PresageInterstitialCallback handler)
			: base("io.presage.ads.PresageInterstitial$PresageInterstitialCallback")
		{
			adNotAvailable = handler.OnAdNotAvailable;
			adAvailable = handler.OnAdAvailable;
			adLoaded = handler.OnAdLoaded;
			adClosed = handler.OnAdClosed;
			adError = handler.OnAdError;
			adDisplayed = handler.OnAdDisplayed;
		}

		private void onAdNotAvailable()
		{
			adNotAvailable();
		}

		private void onAdAvailable()
		{
			adAvailable();
		}

		private void onAdLoaded()
		{
			adLoaded();
		}

		private void onAdClosed()
		{
			adClosed();
		}

		private void onAdError(int code)
		{
			adError(code);
		}

		private void onAdDisplayed()
		{
			adDisplayed();
		}
	}

	public interface PresageInterstitialCallback
	{
		void OnAdAvailable();

		void OnAdNotAvailable();

		void OnAdLoaded();

		void OnAdClosed();

		void OnAdError(int code);

		void OnAdDisplayed();
	}

	private const string PRESAGE_INTERSTITIAL_ID = "io.presage.ads.PresageInterstitial";

	private const string HANDLER_ID = "io.presage.ads.PresageInterstitial$PresageInterstitialCallback";

	private AndroidJavaObject currentActivity;

	private AndroidJavaObject presageInterstitial;

	public PresageInterstitial()
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		currentActivity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		presageInterstitial = new AndroidJavaObject("io.presage.ads.PresageInterstitial", currentActivity);
	}

	public PresageInterstitial(string adUnitId)
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		currentActivity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		presageInterstitial = new AndroidJavaObject("io.presage.ads.PresageInterstitial", currentActivity, adUnitId);
	}

	public void SetPresageInterstitialCallback(PresageInterstitialCallback callback)
	{
		InterstitialCallbackProxy interstitialCallbackProxy = new InterstitialCallbackProxy(callback);
		presageInterstitial.Call("setPresageInterstitialCallback", interstitialCallbackProxy);
	}

	public void AdToServe()
	{
		currentActivity.Call("runOnUiThread", (AndroidJavaRunnable)delegate
		{
			presageInterstitial.Call("adToServe");
		});
	}

	public void Load()
	{
		currentActivity.Call("runOnUiThread", (AndroidJavaRunnable)delegate
		{
			presageInterstitial.Call("load");
		});
	}

	public void Load(int adsToPrecache)
	{
		currentActivity.Call("runOnUiThread", (AndroidJavaRunnable)delegate
		{
			presageInterstitial.Call("load", adsToPrecache);
		});
	}

	public bool CanShow()
	{
		return presageInterstitial.Call<bool>("canShow", new object[0]);
	}

	public void Show()
	{
		currentActivity.Call("runOnUiThread", (AndroidJavaRunnable)delegate
		{
			presageInterstitial.Call("show");
		});
	}
}
