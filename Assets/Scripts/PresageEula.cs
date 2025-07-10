using UnityEngine;

public class PresageEula
{
	public class IEulaHandlerProxy : AndroidJavaProxy
	{
		private delegate void EulaFound();

		private delegate void EulaNotFound();

		private delegate void EulaClosed();

		private EulaFound eulaFound;

		private EulaNotFound eulaNotFound;

		private EulaClosed eulaClosed;

		public IEulaHandlerProxy(IEulaHandler handler)
			: base("io.presage.IEulaHandler")
		{
			eulaFound = handler.OnEulaFound;
			eulaNotFound = handler.OnEulaNotFound;
			eulaClosed = handler.OnEulaClosed;
		}

		private void onEulaFound()
		{
			eulaFound();
		}

		private void onEulaNotFound()
		{
			eulaNotFound();
		}

		private void onEulaClosed()
		{
			eulaClosed();
		}
	}

	public interface IEulaHandler
	{
		void OnEulaFound();

		void OnEulaNotFound();

		void OnEulaClosed();
	}

	private const string EULA_ID = "io.presage.IEulaHandler";

	private AndroidJavaObject currentActivity;

	private AndroidJavaObject presageEula;

	public PresageEula()
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		currentActivity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		presageEula = new AndroidJavaObject("io.presage.IEulaHandler", currentActivity);
	}

	public void SetIEulaHandler(IEulaHandler iEulaHandler)
	{
		IEulaHandlerProxy eulaHandlerProxy = new IEulaHandlerProxy(iEulaHandler);
		presageEula.Call("setIEulaHandler", eulaHandlerProxy);
	}

	public void LaunchWithEula()
	{
		currentActivity.Call("runOnUiThread", (AndroidJavaRunnable)delegate
		{
			presageEula.Call("launchWithEula");
		});
	}
}
