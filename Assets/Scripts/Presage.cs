using UnityEngine;

public class Presage
{
	private const string PRESAGE_ID = "io.presage.Presage";

	public static void Initialize()
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaObject androidJavaObject = @static.Call<AndroidJavaObject>("getApplicationContext", new object[0]);
		AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("io.presage.Presage");
		AndroidJavaObject androidJavaObject2 = androidJavaClass2.CallStatic<AndroidJavaObject>("getInstance", new object[0]);
		androidJavaObject2.Call("setContext", androidJavaObject);
		androidJavaObject2.Call("start");
	}

	public static void Initialize(string apiKey)
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaObject androidJavaObject = @static.Call<AndroidJavaObject>("getApplicationContext", new object[0]);
		AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("io.presage.Presage");
		AndroidJavaObject androidJavaObject2 = androidJavaClass2.CallStatic<AndroidJavaObject>("getInstance", new object[0]);
		androidJavaObject2.Call("setContext", androidJavaObject);
		androidJavaObject2.Call("start", apiKey);
	}
}
