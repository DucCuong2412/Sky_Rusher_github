using GameAnalyticsSDK.Events;
using GameAnalyticsSDK.Setup;
using GameAnalyticsSDK.State;
using GameAnalyticsSDK.Wrapper;
using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine;

namespace GameAnalyticsSDK
{
	[RequireComponent(typeof(GA_SpecialEvents))]
	[ExecuteInEditMode]
	public class GameAnalytics : MonoBehaviour
	{
		private static Settings _settings;

		private static GameAnalytics _instance;

		private static bool _hasInitializeBeenCalled;

		[CompilerGenerated]
		private static Application.LogCallback _003C_003Ef__mg_0024cache0;

		public static Settings SettingsGA
		{
			get
			{
				if (_settings == null)
				{
					InitAPI();
				}
				return _settings;
			}
			private set
			{
				_settings = value;
			}
		}

		public void Awake()
		{
			if (Application.isPlaying)
			{
				if (_instance != null)
				{
					UnityEngine.Debug.LogWarning("Destroying duplicate GameAnalytics object - only one is allowed per scene!");
					UnityEngine.Object.Destroy(base.gameObject);
					return;
				}
				_instance = this;
				UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
				Application.logMessageReceived += GA_Debug.HandleLog;
				InternalInitialize();
			}
		}

		private void OnDestroy()
		{
			if (Application.isPlaying && _instance == this)
			{
				_instance = null;
			}
		}

		private void OnApplicationQuit()
		{
		}

		private static void InitAPI()
		{
			try
			{
				_settings = (Settings)Resources.Load("GameAnalytics/Settings", typeof(Settings));
				GAState.Init();
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.Log("Error getting Settings in InitAPI: " + ex.Message);
			}
		}

		private static void InternalInitialize()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			if (SettingsGA.InfoLogBuild)
			{
				GA_Setup.SetInfoLog(enabled: true);
			}
			if (SettingsGA.VerboseLogBuild)
			{
				GA_Setup.SetVerboseLog(enabled: true);
			}
			int platformIndex = GetPlatformIndex();
			GA_Wrapper.SetUnitySdkVersion("unity " + Settings.VERSION);
			GA_Wrapper.SetUnityEngineVersion("unity " + GetUnityVersion());
			if (platformIndex >= 0)
			{
				if (SettingsGA.UsePlayerSettingsBuildNumber)
				{
					for (int i = 0; i < SettingsGA.Platforms.Count; i++)
					{
						if (SettingsGA.Platforms[i] == RuntimePlatform.Android || SettingsGA.Platforms[i] == RuntimePlatform.IPhonePlayer)
						{
							SettingsGA.Build[i] = Application.version;
						}
					}
				}
				GA_Wrapper.SetBuild(SettingsGA.Build[platformIndex]);
			}
			if (SettingsGA.CustomDimensions01.Count > 0)
			{
				GA_Setup.SetAvailableCustomDimensions01(SettingsGA.CustomDimensions01);
			}
			if (SettingsGA.CustomDimensions02.Count > 0)
			{
				GA_Setup.SetAvailableCustomDimensions02(SettingsGA.CustomDimensions02);
			}
			if (SettingsGA.CustomDimensions03.Count > 0)
			{
				GA_Setup.SetAvailableCustomDimensions03(SettingsGA.CustomDimensions03);
			}
			if (SettingsGA.ResourceItemTypes.Count > 0)
			{
				GA_Setup.SetAvailableResourceItemTypes(SettingsGA.ResourceItemTypes);
			}
			if (SettingsGA.ResourceCurrencies.Count > 0)
			{
				GA_Setup.SetAvailableResourceCurrencies(SettingsGA.ResourceCurrencies);
			}
			if (SettingsGA.UseManualSessionHandling)
			{
				SetEnabledManualSessionHandling(enabled: true);
			}
		}

		public static void Initialize()
		{
			int platformIndex = GetPlatformIndex();
			if (platformIndex >= 0)
			{
				GA_Wrapper.Initialize(SettingsGA.GetGameKey(platformIndex), SettingsGA.GetSecretKey(platformIndex));
				_hasInitializeBeenCalled = true;
			}
			else
			{
				_hasInitializeBeenCalled = true;
				UnityEngine.Debug.LogWarning("GameAnalytics: Unsupported platform (events will not be sent in editor; or missing platform in settings): " + Application.platform);
			}
		}

		public static void NewBusinessEvent(string currency, int amount, string itemType, string itemId, string cartType)
		{
			if (!_hasInitializeBeenCalled)
			{
				UnityEngine.Debug.LogWarning("GameAnalytics: REMEMBER THE SDK NEEDS TO BE MANUALLY INITIALIZED NOW");
			}
			GA_Business.NewEvent(currency, amount, itemType, itemId, cartType);
		}

		public static void NewBusinessEventGooglePlay(string currency, int amount, string itemType, string itemId, string cartType, string receipt, string signature)
		{
			if (!_hasInitializeBeenCalled)
			{
				UnityEngine.Debug.LogWarning("GameAnalytics: REMEMBER THE SDK NEEDS TO BE MANUALLY INITIALIZED NOW");
			}
			GA_Business.NewEventGooglePlay(currency, amount, itemType, itemId, cartType, receipt, signature);
		}

		public static void NewDesignEvent(string eventName)
		{
			if (!_hasInitializeBeenCalled)
			{
				UnityEngine.Debug.LogWarning("GameAnalytics: REMEMBER THE SDK NEEDS TO BE MANUALLY INITIALIZED NOW");
			}
			GA_Design.NewEvent(eventName);
		}

		public static void NewDesignEvent(string eventName, float eventValue)
		{
			if (!_hasInitializeBeenCalled)
			{
				UnityEngine.Debug.LogWarning("GameAnalytics: REMEMBER THE SDK NEEDS TO BE MANUALLY INITIALIZED NOW");
			}
			GA_Design.NewEvent(eventName, eventValue);
		}

		public static void NewProgressionEvent(GAProgressionStatus progressionStatus, string progression01)
		{
			if (!_hasInitializeBeenCalled)
			{
				UnityEngine.Debug.LogWarning("GameAnalytics: REMEMBER THE SDK NEEDS TO BE MANUALLY INITIALIZED NOW");
			}
			GA_Progression.NewEvent(progressionStatus, progression01);
		}

		public static void NewProgressionEvent(GAProgressionStatus progressionStatus, string progression01, string progression02)
		{
			if (!_hasInitializeBeenCalled)
			{
				UnityEngine.Debug.LogWarning("GameAnalytics: REMEMBER THE SDK NEEDS TO BE MANUALLY INITIALIZED NOW");
			}
			GA_Progression.NewEvent(progressionStatus, progression01, progression02);
		}

		public static void NewProgressionEvent(GAProgressionStatus progressionStatus, string progression01, string progression02, string progression03)
		{
			if (!_hasInitializeBeenCalled)
			{
				UnityEngine.Debug.LogWarning("GameAnalytics: REMEMBER THE SDK NEEDS TO BE MANUALLY INITIALIZED NOW");
			}
			GA_Progression.NewEvent(progressionStatus, progression01, progression02, progression03);
		}

		public static void NewProgressionEvent(GAProgressionStatus progressionStatus, string progression01, int score)
		{
			if (!_hasInitializeBeenCalled)
			{
				UnityEngine.Debug.LogWarning("GameAnalytics: REMEMBER THE SDK NEEDS TO BE MANUALLY INITIALIZED NOW");
			}
			GA_Progression.NewEvent(progressionStatus, progression01, score);
		}

		public static void NewProgressionEvent(GAProgressionStatus progressionStatus, string progression01, string progression02, int score)
		{
			if (!_hasInitializeBeenCalled)
			{
				UnityEngine.Debug.LogWarning("GameAnalytics: REMEMBER THE SDK NEEDS TO BE MANUALLY INITIALIZED NOW");
			}
			GA_Progression.NewEvent(progressionStatus, progression01, progression02, score);
		}

		public static void NewProgressionEvent(GAProgressionStatus progressionStatus, string progression01, string progression02, string progression03, int score)
		{
			if (!_hasInitializeBeenCalled)
			{
				UnityEngine.Debug.LogWarning("GameAnalytics: REMEMBER THE SDK NEEDS TO BE MANUALLY INITIALIZED NOW");
			}
			GA_Progression.NewEvent(progressionStatus, progression01, progression02, progression03, score);
		}

		public static void NewResourceEvent(GAResourceFlowType flowType, string currency, float amount, string itemType, string itemId)
		{
			if (!_hasInitializeBeenCalled)
			{
				UnityEngine.Debug.LogWarning("GameAnalytics: REMEMBER THE SDK NEEDS TO BE MANUALLY INITIALIZED NOW");
			}
			GA_Resource.NewEvent(flowType, currency, amount, itemType, itemId);
		}

		public static void NewErrorEvent(GAErrorSeverity severity, string message)
		{
			if (!_hasInitializeBeenCalled)
			{
				UnityEngine.Debug.LogWarning("GameAnalytics: REMEMBER THE SDK NEEDS TO BE MANUALLY INITIALIZED NOW");
			}
			GA_Error.NewEvent(severity, message);
		}

		public static void SetFacebookId(string facebookId)
		{
			GA_Setup.SetFacebookId(facebookId);
		}

		public static void SetGender(GAGender gender)
		{
			GA_Setup.SetGender(gender);
		}

		public static void SetBirthYear(int birthYear)
		{
			GA_Setup.SetBirthYear(birthYear);
		}

		public static void SetCustomId(string userId)
		{
			UnityEngine.Debug.Log("Initializing with custom id: " + userId);
			GA_Wrapper.SetCustomUserId(userId);
		}

		public static void SetEnabledManualSessionHandling(bool enabled)
		{
			GA_Wrapper.SetEnabledManualSessionHandling(enabled);
		}

		public static void StartSession()
		{
			GA_Wrapper.StartSession();
		}

		public static void EndSession()
		{
			GA_Wrapper.EndSession();
		}

		public static void SetCustomDimension01(string customDimension)
		{
			GA_Setup.SetCustomDimension01(customDimension);
		}

		public static void SetCustomDimension02(string customDimension)
		{
			GA_Setup.SetCustomDimension02(customDimension);
		}

		public static void SetCustomDimension03(string customDimension)
		{
			GA_Setup.SetCustomDimension03(customDimension);
		}

		private static string GetUnityVersion()
		{
			string text = string.Empty;
			string[] array = Application.unityVersion.Split('.');
			for (int i = 0; i < array.Length; i++)
			{
				if (int.TryParse(array[i], out int result))
				{
					text = ((i != 0) ? (text + "." + array[i]) : array[i]);
					continue;
				}
				string[] array2 = Regex.Split(array[i], "[^\\d]+");
				if (array2.Length > 0 && int.TryParse(array2[0], out result))
				{
					text = text + "." + array2[0];
				}
			}
			return text;
		}

		private static int GetPlatformIndex()
		{
			int num = -1;
			RuntimePlatform platform = Application.platform;
			switch (platform)
			{
			case RuntimePlatform.IPhonePlayer:
				if (!SettingsGA.Platforms.Contains(platform))
				{
					return SettingsGA.Platforms.IndexOf(RuntimePlatform.tvOS);
				}
				return SettingsGA.Platforms.IndexOf(platform);
			case RuntimePlatform.tvOS:
				if (!SettingsGA.Platforms.Contains(platform))
				{
					return SettingsGA.Platforms.IndexOf(RuntimePlatform.IPhonePlayer);
				}
				return SettingsGA.Platforms.IndexOf(platform);
			default:
				if (platform != RuntimePlatform.MetroPlayerARM && platform != RuntimePlatform.MetroPlayerX64 && platform != RuntimePlatform.MetroPlayerX86)
				{
					return SettingsGA.Platforms.IndexOf(platform);
				}
				goto case RuntimePlatform.MetroPlayerX86;
			case RuntimePlatform.MetroPlayerX86:
			case RuntimePlatform.MetroPlayerX64:
			case RuntimePlatform.MetroPlayerARM:
				return SettingsGA.Platforms.IndexOf(RuntimePlatform.MetroPlayerARM);
			}
		}

		public static void SetBuildAllPlatforms(string build)
		{
			for (int i = 0; i < SettingsGA.Build.Count; i++)
			{
				SettingsGA.Build[i] = build;
			}
		}
	}
}
