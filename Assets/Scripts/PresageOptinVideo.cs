using UnityEngine;

public class PresageOptinVideo
{
	public class OptinVideoCallbackProxy : AndroidJavaProxy
	{
		private delegate void AdNotAvailable();

		private delegate void AdAvailable();

		private delegate void AdLoaded();

		private delegate void AdClosed();

		private delegate void AdError(int code);

		private delegate void AdDisplayed();

		private delegate void AdRewarded(RewardItem rewardItem);

		private AdNotAvailable adNotAvailable;

		private AdAvailable adAvailable;

		private AdLoaded adLoaded;

		private AdClosed adClosed;

		private AdError adError;

		private AdDisplayed adDisplayed;

		private AdRewarded adRewarded;

		public OptinVideoCallbackProxy(PresageOptinVideoCallback handler)
			: base("io.presage.ads.PresageOptinVideo$PresageOptinVideoCallback")
		{
			adNotAvailable = handler.OnAdNotAvailable;
			adAvailable = handler.OnAdAvailable;
			adLoaded = handler.OnAdLoaded;
			adClosed = handler.OnAdClosed;
			adError = handler.OnAdError;
			adDisplayed = handler.OnAdDisplayed;
			adRewarded = handler.OnAdRewarded;
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

		private void onAdRewarded(AndroidJavaObject rewardItemJava)
		{
			adRewarded(new RewardItem(rewardItemJava.Call<string>("getType", new object[0]), rewardItemJava.Call<string>("getAmount", new object[0])));
		}
	}

	public interface PresageOptinVideoCallback
	{
		void OnAdAvailable();

		void OnAdNotAvailable();

		void OnAdLoaded();

		void OnAdClosed();

		void OnAdError(int code);

		void OnAdDisplayed();

		void OnAdRewarded(RewardItem rewardItem);
	}

	public class RewardItem
	{
		public const string REWARD_ITEM_ID = "io.presage.ads.optinvideo.RewardItem";

		private AndroidJavaObject javaObject;

		public RewardItem(string rewardType, string amount)
		{
			javaObject = new AndroidJavaObject("io.presage.ads.optinvideo.RewardItem", rewardType, amount);
		}

		public string GetRewardType()
		{
			return javaObject.Call<string>("getType", new object[0]);
		}

		public string GetAmount()
		{
			return javaObject.Call<string>("getAmount", new object[0]);
		}
	}

	private const string PRESAGE_OPTINVIDEO_ID = "io.presage.ads.PresageOptinVideo";

	private const string HANDLER_ID = "io.presage.ads.PresageOptinVideo$PresageOptinVideoCallback";

	private AndroidJavaObject currentActivity;

	private AndroidJavaObject javaObject;

	public PresageOptinVideo(string adUnitId)
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		currentActivity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		javaObject = new AndroidJavaObject("io.presage.ads.PresageOptinVideo", currentActivity, adUnitId);
	}

	public PresageOptinVideo(string adUnitId, RewardItem rewardItem)
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		currentActivity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		javaObject = new AndroidJavaObject("io.presage.ads.PresageOptinVideo", currentActivity, adUnitId, createJavaRewardItemFromCS(rewardItem));
	}

	public PresageOptinVideo(string adUnitId, RewardItem rewardItem, string userId)
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		currentActivity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		javaObject = new AndroidJavaObject("io.presage.ads.PresageOptinVideo", currentActivity, adUnitId, createJavaRewardItemFromCS(rewardItem), userId);
	}

	public void SetRewardItem(RewardItem rewardItem)
	{
		javaObject.Call("setRewardItem", createJavaRewardItemFromCS(rewardItem));
	}

	public RewardItem GetRewardItem()
	{
		return createRewardItemFromJava(javaObject.Call<AndroidJavaObject>("getRewardItem", new object[0]));
	}

	private RewardItem createRewardItemFromJava(AndroidJavaObject javaObject)
	{
		return new RewardItem(javaObject.Call<string>("getType", new object[0]), javaObject.Call<string>("getAmount", new object[0]));
	}

	private AndroidJavaObject createJavaRewardItemFromCS(RewardItem rewardItem)
	{
		return new AndroidJavaObject("io.presage.ads.optinvideo.RewardItem", rewardItem.GetType(), rewardItem.GetAmount());
	}

	public void SetPresageOptinVideoCallback(PresageOptinVideoCallback callback)
	{
		OptinVideoCallbackProxy optinVideoCallbackProxy = new OptinVideoCallbackProxy(callback);
		javaObject.Call("setPresageOptinVideoCallback", optinVideoCallbackProxy);
	}

	public void AdToServe()
	{
		currentActivity.Call("runOnUiThread", (AndroidJavaRunnable)delegate
		{
			javaObject.Call("adToServe");
		});
	}

	public void Load()
	{
		currentActivity.Call("runOnUiThread", (AndroidJavaRunnable)delegate
		{
			javaObject.Call("load");
		});
	}

	public void Load(int adsToPrecache)
	{
		currentActivity.Call("runOnUiThread", (AndroidJavaRunnable)delegate
		{
			javaObject.Call("load", adsToPrecache);
		});
	}

	public bool CanShow()
	{
		return javaObject.Call<bool>("canShow", new object[0]);
	}

	public void Show()
	{
		currentActivity.Call("runOnUiThread", (AndroidJavaRunnable)delegate
		{
			javaObject.Call("show");
		});
	}
}
