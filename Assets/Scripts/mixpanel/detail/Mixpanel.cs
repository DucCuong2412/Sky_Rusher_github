using System;
using System.Runtime.InteropServices;

namespace mixpanel.detail
{
	public class Mixpanel : IDisposable
	{
		public class People : IDisposable
		{
			private HandleRef swigCPtr;

			protected bool swigCMemOwn;

			internal People(IntPtr cPtr, bool cMemoryOwn)
			{
				swigCMemOwn = cMemoryOwn;
				swigCPtr = new HandleRef(this, cPtr);
			}

			internal static HandleRef getCPtr(People obj)
			{
				return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
			}

			~People()
			{
				Dispose();
			}

			public virtual void Dispose()
			{
				lock (this)
				{
					if (swigCPtr.Handle != IntPtr.Zero)
					{
						if (swigCMemOwn)
						{
							swigCMemOwn = false;
							MixpanelSDKPINVOKE.delete_Mixpanel_People(swigCPtr);
						}
						swigCPtr = new HandleRef(null, IntPtr.Zero);
					}
					GC.SuppressFinalize(this);
				}
			}

			public void set(string property, Value to)
			{
				MixpanelSDKPINVOKE.Mixpanel_People_set(swigCPtr, property, Value.getCPtr(to));
				if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
				{
					throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
				}
			}

			public void set_properties(Value properties)
			{
				MixpanelSDKPINVOKE.Mixpanel_People_set_properties(swigCPtr, Value.getCPtr(properties));
				if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
				{
					throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
				}
			}

			public void set_once(string property, Value to)
			{
				MixpanelSDKPINVOKE.Mixpanel_People_set_once(swigCPtr, property, Value.getCPtr(to));
				if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
				{
					throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
				}
			}

			public void set_once_properties(Value properties)
			{
				MixpanelSDKPINVOKE.Mixpanel_People_set_once_properties(swigCPtr, Value.getCPtr(properties));
				if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
				{
					throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
				}
			}

			public void unset(string property)
			{
				MixpanelSDKPINVOKE.Mixpanel_People_unset(swigCPtr, property);
				if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
				{
					throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
				}
			}

			public void unset_properties(Value properties)
			{
				MixpanelSDKPINVOKE.Mixpanel_People_unset_properties(swigCPtr, Value.getCPtr(properties));
				if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
				{
					throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
				}
			}

			public void increment(string property, Value by)
			{
				MixpanelSDKPINVOKE.Mixpanel_People_increment(swigCPtr, property, Value.getCPtr(by));
				if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
				{
					throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
				}
			}

			public void increment_properties(Value properties)
			{
				MixpanelSDKPINVOKE.Mixpanel_People_increment_properties(swigCPtr, Value.getCPtr(properties));
				if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
				{
					throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
				}
			}

			public void append(string list_name, Value value)
			{
				MixpanelSDKPINVOKE.Mixpanel_People_append(swigCPtr, list_name, Value.getCPtr(value));
				if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
				{
					throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
				}
			}

			public void append_properties(Value properties)
			{
				MixpanelSDKPINVOKE.Mixpanel_People_append_properties(swigCPtr, Value.getCPtr(properties));
				if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
				{
					throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
				}
			}

			public void union_(string list_name, Value values)
			{
				MixpanelSDKPINVOKE.Mixpanel_People_union_(swigCPtr, list_name, Value.getCPtr(values));
				if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
				{
					throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
				}
			}

			public void union_properties(Value properties)
			{
				MixpanelSDKPINVOKE.Mixpanel_People_union_properties(swigCPtr, Value.getCPtr(properties));
				if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
				{
					throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
				}
			}

			public void track_charge(double amount, Value properties)
			{
				MixpanelSDKPINVOKE.Mixpanel_People_track_charge__SWIG_0(swigCPtr, amount, Value.getCPtr(properties));
				if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
				{
					throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
				}
			}

			public void track_charge(double amount)
			{
				MixpanelSDKPINVOKE.Mixpanel_People_track_charge__SWIG_1(swigCPtr, amount);
			}

			public void clear_charges()
			{
				MixpanelSDKPINVOKE.Mixpanel_People_clear_charges(swigCPtr);
			}

			public void delete_user()
			{
				MixpanelSDKPINVOKE.Mixpanel_People_delete_user(swigCPtr);
			}

			public void set_push_id(string token)
			{
				MixpanelSDKPINVOKE.Mixpanel_People_set_push_id(swigCPtr, token);
				if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
				{
					throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
				}
			}

			public void set_first_name(string to)
			{
				MixpanelSDKPINVOKE.Mixpanel_People_set_first_name(swigCPtr, to);
				if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
				{
					throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
				}
			}

			public void set_last_name(string to)
			{
				MixpanelSDKPINVOKE.Mixpanel_People_set_last_name(swigCPtr, to);
				if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
				{
					throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
				}
			}

			public void set_name(string to)
			{
				MixpanelSDKPINVOKE.Mixpanel_People_set_name(swigCPtr, to);
				if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
				{
					throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
				}
			}

			public void set_email(string to)
			{
				MixpanelSDKPINVOKE.Mixpanel_People_set_email(swigCPtr, to);
				if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
				{
					throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
				}
			}

			public void set_phone(string to)
			{
				MixpanelSDKPINVOKE.Mixpanel_People_set_phone(swigCPtr, to);
				if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
				{
					throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
				}
			}
		}

		public class LogEntry : IDisposable
		{
			public enum Level
			{
				LL_TRACE,
				LL_DEBUG,
				LL_INFO,
				LL_WARNING,
				LL_ERROR,
				LL_NONE
			}

			private HandleRef swigCPtr;

			protected bool swigCMemOwn;

			public Level level
			{
				get
				{
					return (Level)MixpanelSDKPINVOKE.Mixpanel_LogEntry_level_get(swigCPtr);
				}
				set
				{
					MixpanelSDKPINVOKE.Mixpanel_LogEntry_level_set(swigCPtr, (int)value);
				}
			}

			public string message
			{
				get
				{
					string result = MixpanelSDKPINVOKE.Mixpanel_LogEntry_message_get(swigCPtr);
					if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
					{
						throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
					}
					return result;
				}
				set
				{
					MixpanelSDKPINVOKE.Mixpanel_LogEntry_message_set(swigCPtr, value);
					if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
					{
						throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
					}
				}
			}

			internal LogEntry(IntPtr cPtr, bool cMemoryOwn)
			{
				swigCMemOwn = cMemoryOwn;
				swigCPtr = new HandleRef(this, cPtr);
			}

			public LogEntry()
				: this(MixpanelSDKPINVOKE.new_Mixpanel_LogEntry(), cMemoryOwn: true)
			{
			}

			internal static HandleRef getCPtr(LogEntry obj)
			{
				return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
			}

			~LogEntry()
			{
				Dispose();
			}

			public virtual void Dispose()
			{
				lock (this)
				{
					if (swigCPtr.Handle != IntPtr.Zero)
					{
						if (swigCMemOwn)
						{
							swigCMemOwn = false;
							MixpanelSDKPINVOKE.delete_Mixpanel_LogEntry(swigCPtr);
						}
						swigCPtr = new HandleRef(null, IntPtr.Zero);
					}
					GC.SuppressFinalize(this);
				}
			}
		}

		public enum NetworkReachability
		{
			NotReachable,
			ReachableViaCarrierDataNetwork,
			ReachableViaLocalAreaNetwork
		}

		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		public People people
		{
			get
			{
				IntPtr intPtr = MixpanelSDKPINVOKE.Mixpanel_people_get(swigCPtr);
				return (!(intPtr == IntPtr.Zero)) ? new People(intPtr, cMemoryOwn: false) : null;
			}
			set
			{
				MixpanelSDKPINVOKE.Mixpanel_people_set(swigCPtr, People.getCPtr(value));
			}
		}

		internal Mixpanel(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		public Mixpanel(string token, bool enable_log_queue, bool opt_out)
			: this(MixpanelSDKPINVOKE.new_Mixpanel__SWIG_0(token, enable_log_queue, opt_out), cMemoryOwn: true)
		{
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}

		public Mixpanel(string token, bool enable_log_queue)
			: this(MixpanelSDKPINVOKE.new_Mixpanel__SWIG_1(token, enable_log_queue), cMemoryOwn: true)
		{
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}

		public Mixpanel(string token)
			: this(MixpanelSDKPINVOKE.new_Mixpanel__SWIG_2(token), cMemoryOwn: true)
		{
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}

		public Mixpanel(string token, string distinct_id, string storage_directory, bool enable_log_queue, bool opt_out)
			: this(MixpanelSDKPINVOKE.new_Mixpanel__SWIG_3(token, distinct_id, storage_directory, enable_log_queue, opt_out), cMemoryOwn: true)
		{
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}

		public Mixpanel(string token, string distinct_id, string storage_directory, bool enable_log_queue)
			: this(MixpanelSDKPINVOKE.new_Mixpanel__SWIG_4(token, distinct_id, storage_directory, enable_log_queue), cMemoryOwn: true)
		{
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}

		public Mixpanel(string token, string distinct_id, string storage_directory)
			: this(MixpanelSDKPINVOKE.new_Mixpanel__SWIG_5(token, distinct_id, storage_directory), cMemoryOwn: true)
		{
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}

		internal static HandleRef getCPtr(Mixpanel obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}

		~Mixpanel()
		{
			Dispose();
		}

		public virtual void Dispose()
		{
			lock (this)
			{
				if (swigCPtr.Handle != IntPtr.Zero)
				{
					if (swigCMemOwn)
					{
						swigCMemOwn = false;
						MixpanelSDKPINVOKE.delete_Mixpanel(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		public void identify(string unique_id)
		{
			MixpanelSDKPINVOKE.Mixpanel_identify(swigCPtr, unique_id);
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
		}

		public void alias(string alias)
		{
			MixpanelSDKPINVOKE.Mixpanel_alias(swigCPtr, alias);
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
		}

		public void register_(string key, Value value)
		{
			MixpanelSDKPINVOKE.Mixpanel_register_(swigCPtr, key, Value.getCPtr(value));
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
		}

		public void register_properties(Value properties)
		{
			MixpanelSDKPINVOKE.Mixpanel_register_properties(swigCPtr, Value.getCPtr(properties));
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
		}

		public bool register_once(string key, Value value)
		{
			bool result = MixpanelSDKPINVOKE.Mixpanel_register_once(swigCPtr, key, Value.getCPtr(value));
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}

		public bool register_once_properties(Value properties)
		{
			bool result = MixpanelSDKPINVOKE.Mixpanel_register_once_properties(swigCPtr, Value.getCPtr(properties));
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}

		public bool unregister(string key)
		{
			bool result = MixpanelSDKPINVOKE.Mixpanel_unregister(swigCPtr, key);
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}

		public bool unregister_properties(Value properties)
		{
			bool result = MixpanelSDKPINVOKE.Mixpanel_unregister_properties(swigCPtr, Value.getCPtr(properties));
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}

		public Value get_super_properties()
		{
			return new Value(MixpanelSDKPINVOKE.Mixpanel_get_super_properties(swigCPtr), cMemoryOwn: true);
		}

		public void clear_super_properties()
		{
			MixpanelSDKPINVOKE.Mixpanel_clear_super_properties(swigCPtr);
		}

		public bool start_timed_event(string event_name)
		{
			bool result = MixpanelSDKPINVOKE.Mixpanel_start_timed_event(swigCPtr, event_name);
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}

		public bool start_timed_event_once(string event_name)
		{
			bool result = MixpanelSDKPINVOKE.Mixpanel_start_timed_event_once(swigCPtr, event_name);
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}

		public bool clear_timed_event(string event_name)
		{
			bool result = MixpanelSDKPINVOKE.Mixpanel_clear_timed_event(swigCPtr, event_name);
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}

		public void clear_timed_events()
		{
			MixpanelSDKPINVOKE.Mixpanel_clear_timed_events(swigCPtr);
		}

		public void reset()
		{
			MixpanelSDKPINVOKE.Mixpanel_reset(swigCPtr);
		}

		public void track(string arg0, Value properties)
		{
			MixpanelSDKPINVOKE.Mixpanel_track__SWIG_0(swigCPtr, arg0, Value.getCPtr(properties));
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
		}

		public void track(string arg0)
		{
			MixpanelSDKPINVOKE.Mixpanel_track__SWIG_1(swigCPtr, arg0);
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
		}

		public bool has_tracked_integration()
		{
			return MixpanelSDKPINVOKE.Mixpanel_has_tracked_integration(swigCPtr);
		}

		public void set_tracked_integration()
		{
			MixpanelSDKPINVOKE.Mixpanel_set_tracked_integration(swigCPtr);
		}

		public bool has_opted_out()
		{
			return MixpanelSDKPINVOKE.Mixpanel_has_opted_out(swigCPtr);
		}

		public void opt_out_tracking()
		{
			MixpanelSDKPINVOKE.Mixpanel_opt_out_tracking(swigCPtr);
		}

		public void opt_in_tracking(string distinct_id, Value properties)
		{
			MixpanelSDKPINVOKE.Mixpanel_opt_in_tracking__SWIG_0(swigCPtr, distinct_id, Value.getCPtr(properties));
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
		}

		public void opt_in_tracking(string distinct_id)
		{
			MixpanelSDKPINVOKE.Mixpanel_opt_in_tracking__SWIG_1(swigCPtr, distinct_id);
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
		}

		public void set_minimum_log_level(LogEntry.Level level)
		{
			MixpanelSDKPINVOKE.Mixpanel_set_minimum_log_level(swigCPtr, (int)level);
		}

		public bool get_next_log_entry(LogEntry entry)
		{
			bool result = MixpanelSDKPINVOKE.Mixpanel_get_next_log_entry(swigCPtr, LogEntry.getCPtr(entry));
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}

		public static string utc_now()
		{
			return MixpanelSDKPINVOKE.Mixpanel_utc_now();
		}

		public void on_reachability_changed(NetworkReachability network_reachability)
		{
			MixpanelSDKPINVOKE.Mixpanel_on_reachability_changed(swigCPtr, (int)network_reachability);
		}

		public void set_maximum_queue_size(uint maximum_size)
		{
			MixpanelSDKPINVOKE.Mixpanel_set_maximum_queue_size(swigCPtr, maximum_size);
		}

		public void set_flush_interval(uint seconds)
		{
			MixpanelSDKPINVOKE.Mixpanel_set_flush_interval(swigCPtr, seconds);
		}

		public void flush_queue()
		{
			MixpanelSDKPINVOKE.Mixpanel_flush_queue(swigCPtr);
		}

		private void SwigDirectorConnect()
		{
			MixpanelSDKPINVOKE.Mixpanel_director_connect(swigCPtr);
		}
	}
}
