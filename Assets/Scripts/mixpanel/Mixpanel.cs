using mixpanel.detail;
using mixpanel.platform;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace mixpanel
{
	public class Mixpanel : MonoBehaviour
	{
		public class People
		{
			private global::mixpanel.detail.Mixpanel mixpanel;

			private bool tracking_enabled;

			public string Email
			{
				set
				{
					if (tracking_enabled)
					{
						mixpanel.people.set_email(value);
					}
				}
			}

			public string FirstName
			{
				set
				{
					if (tracking_enabled)
					{
						mixpanel.people.set_first_name(value);
					}
				}
			}

			public string LastName
			{
				set
				{
					if (tracking_enabled)
					{
						mixpanel.people.set_last_name(value);
					}
				}
			}

			public string Name
			{
				set
				{
					if (tracking_enabled)
					{
						mixpanel.people.set_name(value);
					}
				}
			}

			public string Phone
			{
				set
				{
					if (tracking_enabled)
					{
						mixpanel.people.set_phone(value);
					}
				}
			}

			public string PushDeviceToken
			{
				set
				{
					if (tracking_enabled)
					{
						mixpanel.people.set_push_id(value);
					}
				}
			}

			public People(global::mixpanel.detail.Mixpanel mixpanel, bool tracking_enabled)
			{
				this.mixpanel = mixpanel;
				this.tracking_enabled = tracking_enabled;
			}

			public void Append(Value properties)
			{
				if (tracking_enabled)
				{
					mixpanel.people.append_properties(properties);
				}
			}

			public void Append(string listName, Value value)
			{
				if (tracking_enabled)
				{
					mixpanel.people.append(listName, value);
				}
			}

			public void ClearCharges()
			{
				if (tracking_enabled)
				{
					mixpanel.people.clear_charges();
				}
			}

			public void Increment(Value properties)
			{
				if (tracking_enabled)
				{
					mixpanel.people.increment_properties(properties);
				}
			}

			public void Increment(string property, Value by)
			{
				if (tracking_enabled)
				{
					mixpanel.people.increment(property, by);
				}
			}

			private void SetAutomaticPeopleProperties()
			{
				mixpanel.people.set("$android_app_version_string", MixpanelUnityPlatform.get_android_version_name());
				mixpanel.people.set("$android_app_build_number", MixpanelUnityPlatform.get_android_version_code());
			}

			public void Set(Value properties)
			{
				if (tracking_enabled)
				{
					mixpanel.people.set_properties(properties);
					SetAutomaticPeopleProperties();
				}
			}

			public void Set(string property, Value to)
			{
				if (tracking_enabled)
				{
					mixpanel.people.set(property, to);
					SetAutomaticPeopleProperties();
				}
			}

			public void SetOnce(string property, Value to)
			{
				if (tracking_enabled)
				{
					mixpanel.people.set_once(property, to);
					SetAutomaticPeopleProperties();
				}
			}

			public void SetOnce(Value properties)
			{
				if (tracking_enabled)
				{
					mixpanel.people.set_once_properties(properties);
					SetAutomaticPeopleProperties();
				}
			}

			public void TrackCharge(double amount)
			{
				if (tracking_enabled)
				{
					mixpanel.people.track_charge(amount, new Value());
				}
			}

			public void TrackCharge(double amount, Value properties)
			{
				if (tracking_enabled)
				{
					mixpanel.people.track_charge(amount, properties);
				}
			}

			public void Union(Value properties)
			{
				if (tracking_enabled)
				{
					mixpanel.people.union_properties(properties);
				}
			}

			public void Union(string listName, Value values)
			{
				if (tracking_enabled)
				{
					mixpanel.people.union_(listName, values);
				}
			}

			public void Unset(Value properties)
			{
				if (tracking_enabled)
				{
					mixpanel.people.unset_properties(properties);
				}
			}

			public void Unset(string property)
			{
				if (tracking_enabled)
				{
					mixpanel.people.unset(property);
				}
			}
		}

		[Header("Project")]
		[Tooltip("The token of the Mixpanel project.")]
		public string token = string.Empty;

		[Tooltip("Used when the DEBUG compile flag is set or when in the editor. Useful if you want to use different tokens for test builds.")]
		public string debugToken = string.Empty;

		[Header("Debugging")]
		[Tooltip("Also send out data when inside the Unity editor.")]
		public bool trackInEditor;

		[Tooltip("The minimum log level you're interested in. If set to LL_NONE, logging will be disabled.")]
		public mixpanel.detail.Mixpanel.LogEntry.Level minLogLevel = mixpanel.detail.Mixpanel.LogEntry.Level.LL_WARNING;

		[Header("Configuration")]
		[Tooltip("How frequently (in seconds) to send data to Mixpanel.")]
		[Range(1f, 600f)]
		public int flushInterval = 60;

		private int maxQueueSizeInMB = 5;

		private bool useIosIfa;

		private static mixpanel.detail.Mixpanel mp_interface;

		private static bool tracking_enabled = true;

		private mixpanel.detail.Mixpanel.LogEntry le = new mixpanel.detail.Mixpanel.LogEntry();

		private NetworkReachability reachability = NetworkReachability.ReachableViaLocalAreaNetwork;

		private static People people_;

		public static People people
		{
			get
			{
				if (people_ == null)
				{
					people_ = new People(instance, tracking_enabled);
				}
				return people_;
			}
		}

		private static mixpanel.detail.Mixpanel instance => mp_interface;

		public static void Identify(string uniqueId)
		{
			if (tracking_enabled)
			{
				instance.identify(uniqueId);
			}
		}

		public static void Alias(string alias)
		{
			if (tracking_enabled)
			{
				instance.alias(alias);
			}
		}

		public static void ClearSuperProperties()
		{
			if (tracking_enabled)
			{
				instance.clear_super_properties();
			}
		}

		public static void ClearTimedEvents()
		{
			if (tracking_enabled)
			{
				instance.clear_timed_events();
			}
		}

		public static bool ClearTimedEvent(string eventName)
		{
			if (tracking_enabled)
			{
				return instance.clear_timed_event(eventName);
			}
			return false;
		}

		public static void FlushQueue()
		{
			if (tracking_enabled)
			{
				instance.flush_queue();
			}
		}

		public static void Register(string key, Value value)
		{
			if (tracking_enabled)
			{
				instance.register_(key, value);
			}
		}

		public static bool RegisterOnce(string key, Value value)
		{
			if (tracking_enabled)
			{
				return instance.register_once(key, value);
			}
			return false;
		}

		public static void Reset()
		{
			if (tracking_enabled)
			{
				instance.reset();
			}
		}

		public static bool StartTimedEvent(string eventName)
		{
			if (tracking_enabled)
			{
				return instance.start_timed_event(eventName);
			}
			return false;
		}

		public static bool StartTimedEventOnce(string eventName)
		{
			if (tracking_enabled)
			{
				return instance.start_timed_event_once(eventName);
			}
			return false;
		}

		public static void OptOutTracking()
		{
			if (tracking_enabled)
			{
				instance.opt_out_tracking();
			}
		}

		public static void OptInTracking()
		{
			if (tracking_enabled)
			{
				instance.opt_in_tracking(string.Empty, new Value());
			}
		}

		public static void OptInTracking(string distinct_id)
		{
			if (tracking_enabled)
			{
				instance.opt_in_tracking(distinct_id, new Value());
			}
		}

		public static void OptInTracking(string distinct_id, Value properties)
		{
			if (tracking_enabled)
			{
				instance.opt_in_tracking(distinct_id, properties);
			}
		}

		public static bool hasOptedOut()
		{
			if (tracking_enabled)
			{
				return instance.has_opted_out();
			}
			return false;
		}

		public static void Track(string eventName)
		{
			if (tracking_enabled)
			{
				instance.track(eventName, new Value());
			}
		}

		public static void Track(string eventName, Value properties)
		{
			if (tracking_enabled)
			{
				instance.track(eventName, properties);
			}
		}

		public static bool Unregister(string key)
		{
			if (tracking_enabled)
			{
				return instance.unregister(key);
			}
			return false;
		}

		private string BuildTrackIntegrationRequestURL()
		{
			string s = "{\"event\":\"Integration\",\"properties\":{\"token\":\"85053bf24bba75239b16a601d9387e17\",\"mp_lib\":\"unity\",\"distinct_id\":\"" + token + "\"}}";
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			string str = Convert.ToBase64String(bytes);
			return "https://api.mixpanel.com/track/?data=" + str;
		}

		private IEnumerator<WWW> WaitForRequest(WWW request)
		{
			yield return request;
			instance.set_tracked_integration();
		}

		private void TrackIntegrationEvent()
		{
			if (!instance.has_tracked_integration())
			{
				string url = BuildTrackIntegrationRequestURL();
				WWW request = new WWW(url);
				StartCoroutine(WaitForRequest(request));
			}
		}

		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(this);
			if (tracking_enabled && mp_interface == null)
			{
				mp_interface = new mixpanel.detail.Mixpanel(token, MixpanelUnityPlatform.get_distinct_id(), MixpanelUnityPlatform.get_storage_directory(), enable_log_queue: true, opt_out: false);
				mp_interface.set_minimum_log_level(minLogLevel);
				mp_interface.set_maximum_queue_size((uint)(maxQueueSizeInMB * 1024 * 1024));
				Register("$screen_width", Screen.width);
				Register("$screen_height", Screen.height);
				Register("$screen_dpi", Screen.dpi);
				Register("$app_build_number", MixpanelUnityPlatform.get_android_version_code());
				Register("$app_version_string", MixpanelUnityPlatform.get_android_version_name());
				if (flushInterval < 0)
				{
					UnityEngine.Debug.LogError("batchSendInterval must be greater or equal zo zero");
					flushInterval = 0;
				}
				mp_interface.set_flush_interval((uint)flushInterval);
				TrackIntegrationEvent();
			}
		}

		private void OnDestroy()
		{
			if (tracking_enabled)
			{
				mp_interface.Dispose();
			}
		}

		private void Update()
		{
			if (!tracking_enabled)
			{
				return;
			}
			while (mp_interface.get_next_log_entry(le))
			{
				string message = $"Mixpanel[{le.level}]: {le.message}";
				switch (le.level)
				{
				case mixpanel.detail.Mixpanel.LogEntry.Level.LL_ERROR:
					UnityEngine.Debug.LogError(message);
					break;
				case mixpanel.detail.Mixpanel.LogEntry.Level.LL_WARNING:
					UnityEngine.Debug.LogWarning(message);
					break;
				default:
					UnityEngine.Debug.Log(message);
					break;
				}
			}
			if (reachability != Application.internetReachability)
			{
				reachability = Application.internetReachability;
				switch (reachability)
				{
				case NetworkReachability.NotReachable:
					mp_interface.on_reachability_changed(mixpanel.detail.Mixpanel.NetworkReachability.NotReachable);
					break;
				case NetworkReachability.ReachableViaCarrierDataNetwork:
					mp_interface.on_reachability_changed(mixpanel.detail.Mixpanel.NetworkReachability.ReachableViaCarrierDataNetwork);
					break;
				case NetworkReachability.ReachableViaLocalAreaNetwork:
					mp_interface.on_reachability_changed(mixpanel.detail.Mixpanel.NetworkReachability.ReachableViaLocalAreaNetwork);
					break;
				}
			}
		}
	}
}
