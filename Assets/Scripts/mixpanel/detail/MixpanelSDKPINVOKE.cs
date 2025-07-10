using AOT;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace mixpanel.detail
{
	internal class MixpanelSDKPINVOKE
	{
		protected class SWIGExceptionHelper
		{
			public delegate void ExceptionDelegate(string message);

			public delegate void ExceptionArgumentDelegate(string message, string paramName);

			private static ExceptionDelegate applicationDelegate;

			private static ExceptionDelegate arithmeticDelegate;

			private static ExceptionDelegate divideByZeroDelegate;

			private static ExceptionDelegate indexOutOfRangeDelegate;

			private static ExceptionDelegate invalidCastDelegate;

			private static ExceptionDelegate invalidOperationDelegate;

			private static ExceptionDelegate ioDelegate;

			private static ExceptionDelegate nullReferenceDelegate;

			private static ExceptionDelegate outOfMemoryDelegate;

			private static ExceptionDelegate overflowDelegate;

			private static ExceptionDelegate systemDelegate;

			private static ExceptionArgumentDelegate argumentDelegate;

			private static ExceptionArgumentDelegate argumentNullDelegate;

			private static ExceptionArgumentDelegate argumentOutOfRangeDelegate;

			static SWIGExceptionHelper()
			{
				applicationDelegate = SetPendingApplicationException;
				arithmeticDelegate = SetPendingArithmeticException;
				divideByZeroDelegate = SetPendingDivideByZeroException;
				indexOutOfRangeDelegate = SetPendingIndexOutOfRangeException;
				invalidCastDelegate = SetPendingInvalidCastException;
				invalidOperationDelegate = SetPendingInvalidOperationException;
				ioDelegate = SetPendingIOException;
				nullReferenceDelegate = SetPendingNullReferenceException;
				outOfMemoryDelegate = SetPendingOutOfMemoryException;
				overflowDelegate = SetPendingOverflowException;
				systemDelegate = SetPendingSystemException;
				argumentDelegate = SetPendingArgumentException;
				argumentNullDelegate = SetPendingArgumentNullException;
				argumentOutOfRangeDelegate = SetPendingArgumentOutOfRangeException;
				SWIGRegisterExceptionCallbacks_MixpanelSDK(applicationDelegate, arithmeticDelegate, divideByZeroDelegate, indexOutOfRangeDelegate, invalidCastDelegate, invalidOperationDelegate, ioDelegate, nullReferenceDelegate, outOfMemoryDelegate, overflowDelegate, systemDelegate);
				SWIGRegisterExceptionArgumentCallbacks_MixpanelSDK(argumentDelegate, argumentNullDelegate, argumentOutOfRangeDelegate);
			}

			[DllImport("MixpanelSDK")]
			public static extern void SWIGRegisterExceptionCallbacks_MixpanelSDK(ExceptionDelegate applicationDelegate, ExceptionDelegate arithmeticDelegate, ExceptionDelegate divideByZeroDelegate, ExceptionDelegate indexOutOfRangeDelegate, ExceptionDelegate invalidCastDelegate, ExceptionDelegate invalidOperationDelegate, ExceptionDelegate ioDelegate, ExceptionDelegate nullReferenceDelegate, ExceptionDelegate outOfMemoryDelegate, ExceptionDelegate overflowDelegate, ExceptionDelegate systemExceptionDelegate);

			[DllImport("MixpanelSDK")]
			public static extern void SWIGRegisterExceptionArgumentCallbacks_MixpanelSDK(ExceptionArgumentDelegate argumentDelegate, ExceptionArgumentDelegate argumentNullDelegate, ExceptionArgumentDelegate argumentOutOfRangeDelegate);

			[MonoPInvokeCallback(typeof(ExceptionDelegate))]
			private static void SetPendingApplicationException(string message)
			{
				SWIGPendingException.Set(new ApplicationException(message, SWIGPendingException.Retrieve()));
			}

			[MonoPInvokeCallback(typeof(ExceptionDelegate))]
			private static void SetPendingArithmeticException(string message)
			{
				SWIGPendingException.Set(new ArithmeticException(message, SWIGPendingException.Retrieve()));
			}

			[MonoPInvokeCallback(typeof(ExceptionDelegate))]
			private static void SetPendingDivideByZeroException(string message)
			{
				SWIGPendingException.Set(new DivideByZeroException(message, SWIGPendingException.Retrieve()));
			}

			[MonoPInvokeCallback(typeof(ExceptionDelegate))]
			private static void SetPendingIndexOutOfRangeException(string message)
			{
				SWIGPendingException.Set(new IndexOutOfRangeException(message, SWIGPendingException.Retrieve()));
			}

			[MonoPInvokeCallback(typeof(ExceptionDelegate))]
			private static void SetPendingInvalidCastException(string message)
			{
				SWIGPendingException.Set(new InvalidCastException(message, SWIGPendingException.Retrieve()));
			}

			[MonoPInvokeCallback(typeof(ExceptionDelegate))]
			private static void SetPendingInvalidOperationException(string message)
			{
				SWIGPendingException.Set(new InvalidOperationException(message, SWIGPendingException.Retrieve()));
			}

			[MonoPInvokeCallback(typeof(ExceptionDelegate))]
			private static void SetPendingIOException(string message)
			{
				SWIGPendingException.Set(new IOException(message, SWIGPendingException.Retrieve()));
			}

			[MonoPInvokeCallback(typeof(ExceptionDelegate))]
			private static void SetPendingNullReferenceException(string message)
			{
				SWIGPendingException.Set(new NullReferenceException(message, SWIGPendingException.Retrieve()));
			}

			[MonoPInvokeCallback(typeof(ExceptionDelegate))]
			private static void SetPendingOutOfMemoryException(string message)
			{
				SWIGPendingException.Set(new OutOfMemoryException(message, SWIGPendingException.Retrieve()));
			}

			[MonoPInvokeCallback(typeof(ExceptionDelegate))]
			private static void SetPendingOverflowException(string message)
			{
				SWIGPendingException.Set(new OverflowException(message, SWIGPendingException.Retrieve()));
			}

			[MonoPInvokeCallback(typeof(ExceptionDelegate))]
			private static void SetPendingSystemException(string message)
			{
				SWIGPendingException.Set(new SystemException(message, SWIGPendingException.Retrieve()));
			}

			[MonoPInvokeCallback(typeof(ExceptionArgumentDelegate))]
			private static void SetPendingArgumentException(string message, string paramName)
			{
				SWIGPendingException.Set(new ArgumentException(message, paramName, SWIGPendingException.Retrieve()));
			}

			[MonoPInvokeCallback(typeof(ExceptionArgumentDelegate))]
			private static void SetPendingArgumentNullException(string message, string paramName)
			{
				Exception ex = SWIGPendingException.Retrieve();
				if (ex != null)
				{
					message = message + " Inner Exception: " + ex.Message;
				}
				SWIGPendingException.Set(new ArgumentNullException(paramName, message));
			}

			[MonoPInvokeCallback(typeof(ExceptionArgumentDelegate))]
			private static void SetPendingArgumentOutOfRangeException(string message, string paramName)
			{
				Exception ex = SWIGPendingException.Retrieve();
				if (ex != null)
				{
					message = message + " Inner Exception: " + ex.Message;
				}
				SWIGPendingException.Set(new ArgumentOutOfRangeException(paramName, message));
			}
		}

		public class SWIGPendingException
		{
			[ThreadStatic]
			private static Exception pendingException;

			private static int numExceptionsPending;

			public static bool Pending
			{
				get
				{
					bool result = false;
					if (numExceptionsPending > 0 && pendingException != null)
					{
						result = true;
					}
					return result;
				}
			}

			public static void Set(Exception e)
			{
				if (pendingException != null)
				{
					throw new ApplicationException("FATAL: An earlier pending exception from unmanaged code was missed and thus not thrown (" + pendingException.ToString() + ")", e);
				}
				pendingException = e;
				lock (typeof(MixpanelSDKPINVOKE))
				{
					numExceptionsPending++;
				}
			}

			public static Exception Retrieve()
			{
				Exception result = null;
				if (numExceptionsPending > 0 && pendingException != null)
				{
					result = pendingException;
					pendingException = null;
					lock (typeof(MixpanelSDKPINVOKE))
					{
						numExceptionsPending--;
						return result;
					}
				}
				return result;
			}
		}

		protected class SWIGStringHelper
		{
			public delegate string SWIGStringDelegate(string message);

			private static SWIGStringDelegate stringDelegate;

			static SWIGStringHelper()
			{
				stringDelegate = CreateString;
				SWIGRegisterStringCallback_MixpanelSDK(stringDelegate);
			}

			[DllImport("MixpanelSDK")]
			public static extern void SWIGRegisterStringCallback_MixpanelSDK(SWIGStringDelegate stringDelegate);

			[MonoPInvokeCallback(typeof(SWIGStringDelegate))]
			private static string CreateString(string cString)
			{
				return cString;
			}
		}

		protected static SWIGExceptionHelper swigExceptionHelper;

		protected static SWIGStringHelper swigStringHelper;

		static MixpanelSDKPINVOKE()
		{
			swigExceptionHelper = new SWIGExceptionHelper();
			swigStringHelper = new SWIGStringHelper();
		}

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_new_Value__SWIG_0___")]
		public static extern IntPtr new_Value__SWIG_0();

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_new_Value__SWIG_1___")]
		public static extern IntPtr new_Value__SWIG_1(int jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_new_Value__SWIG_2___")]
		public static extern IntPtr new_Value__SWIG_2(double jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_new_Value__SWIG_3___")]
		public static extern IntPtr new_Value__SWIG_3(float jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_new_Value__SWIG_4___")]
		public static extern IntPtr new_Value__SWIG_4(string jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_new_Value__SWIG_5___")]
		public static extern IntPtr new_Value__SWIG_5(bool jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_new_Value__SWIG_6___")]
		public static extern IntPtr new_Value__SWIG_6(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_delete_Value___")]
		public static extern void delete_Value(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_get__SWIG_0___")]
		public static extern IntPtr Value_get__SWIG_0(HandleRef jarg1, uint jarg2, HandleRef jarg3);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_isValidIndex___")]
		public static extern bool Value_isValidIndex(HandleRef jarg1, uint jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_append___")]
		public static extern IntPtr Value_append(HandleRef jarg1, HandleRef jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_get__SWIG_1___")]
		public static extern IntPtr Value_get__SWIG_1(HandleRef jarg1, string jarg2, HandleRef jarg3);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_removeMember___")]
		public static extern IntPtr Value_removeMember(HandleRef jarg1, string jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_isMember___")]
		public static extern bool Value_isMember(HandleRef jarg1, string jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_toStyledString___")]
		public static extern string Value_toStyledString(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_asString___")]
		public static extern string Value_asString(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_asInt___")]
		public static extern int Value_asInt(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_asFloat___")]
		public static extern float Value_asFloat(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_asDouble___")]
		public static extern double Value_asDouble(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_asBool___")]
		public static extern bool Value_asBool(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_isNull___")]
		public static extern bool Value_isNull(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_isBool___")]
		public static extern bool Value_isBool(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_isInt___")]
		public static extern bool Value_isInt(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_isIntegral___")]
		public static extern bool Value_isIntegral(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_isDouble___")]
		public static extern bool Value_isDouble(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_isNumeric___")]
		public static extern bool Value_isNumeric(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_isString___")]
		public static extern bool Value_isString(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_isArray___")]
		public static extern bool Value_isArray(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_isObject___")]
		public static extern bool Value_isObject(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_size___")]
		public static extern uint Value_size(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_empty___")]
		public static extern bool Value_empty(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_clear___")]
		public static extern void Value_clear(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_resize___")]
		public static extern void Value_resize(HandleRef jarg1, uint jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_at__SWIG_0___")]
		public static extern IntPtr Value_at__SWIG_0(HandleRef jarg1, string jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_at__SWIG_1___")]
		public static extern IntPtr Value_at__SWIG_1(HandleRef jarg1, int jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_set__SWIG_0___")]
		public static extern void Value_set__SWIG_0(HandleRef jarg1, int jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_set__SWIG_1___")]
		public static extern void Value_set__SWIG_1(HandleRef jarg1, string jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_set__SWIG_2___")]
		public static extern void Value_set__SWIG_2(HandleRef jarg1, double jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_set__SWIG_3___")]
		public static extern void Value_set__SWIG_3(HandleRef jarg1, float jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Value_set__SWIG_4___")]
		public static extern void Value_set__SWIG_4(HandleRef jarg1, HandleRef jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_new_Mixpanel__SWIG_0___")]
		public static extern IntPtr new_Mixpanel__SWIG_0(string jarg1, bool jarg2, bool jarg3);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_new_Mixpanel__SWIG_1___")]
		public static extern IntPtr new_Mixpanel__SWIG_1(string jarg1, bool jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_new_Mixpanel__SWIG_2___")]
		public static extern IntPtr new_Mixpanel__SWIG_2(string jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_new_Mixpanel__SWIG_3___")]
		public static extern IntPtr new_Mixpanel__SWIG_3(string jarg1, string jarg2, string jarg3, bool jarg4, bool jarg5);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_new_Mixpanel__SWIG_4___")]
		public static extern IntPtr new_Mixpanel__SWIG_4(string jarg1, string jarg2, string jarg3, bool jarg4);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_new_Mixpanel__SWIG_5___")]
		public static extern IntPtr new_Mixpanel__SWIG_5(string jarg1, string jarg2, string jarg3);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_delete_Mixpanel___")]
		public static extern void delete_Mixpanel(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_identify___")]
		public static extern void Mixpanel_identify(HandleRef jarg1, string jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_alias___")]
		public static extern void Mixpanel_alias(HandleRef jarg1, string jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_register____")]
		public static extern void Mixpanel_register_(HandleRef jarg1, string jarg2, HandleRef jarg3);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_register_properties___")]
		public static extern void Mixpanel_register_properties(HandleRef jarg1, HandleRef jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_register_once___")]
		public static extern bool Mixpanel_register_once(HandleRef jarg1, string jarg2, HandleRef jarg3);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_register_once_properties___")]
		public static extern bool Mixpanel_register_once_properties(HandleRef jarg1, HandleRef jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_unregister___")]
		public static extern bool Mixpanel_unregister(HandleRef jarg1, string jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_unregister_properties___")]
		public static extern bool Mixpanel_unregister_properties(HandleRef jarg1, HandleRef jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_get_super_properties___")]
		public static extern IntPtr Mixpanel_get_super_properties(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_clear_super_properties___")]
		public static extern void Mixpanel_clear_super_properties(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_start_timed_event___")]
		public static extern bool Mixpanel_start_timed_event(HandleRef jarg1, string jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_start_timed_event_once___")]
		public static extern bool Mixpanel_start_timed_event_once(HandleRef jarg1, string jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_clear_timed_event___")]
		public static extern bool Mixpanel_clear_timed_event(HandleRef jarg1, string jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_clear_timed_events___")]
		public static extern void Mixpanel_clear_timed_events(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_reset___")]
		public static extern void Mixpanel_reset(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_track__SWIG_0___")]
		public static extern void Mixpanel_track__SWIG_0(HandleRef jarg1, string jarg2, HandleRef jarg3);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_track__SWIG_1___")]
		public static extern void Mixpanel_track__SWIG_1(HandleRef jarg1, string jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_has_tracked_integration___")]
		public static extern bool Mixpanel_has_tracked_integration(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_set_tracked_integration___")]
		public static extern void Mixpanel_set_tracked_integration(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_has_opted_out___")]
		public static extern bool Mixpanel_has_opted_out(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_opt_out_tracking___")]
		public static extern void Mixpanel_opt_out_tracking(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_opt_in_tracking__SWIG_0___")]
		public static extern void Mixpanel_opt_in_tracking__SWIG_0(HandleRef jarg1, string jarg2, HandleRef jarg3);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_opt_in_tracking__SWIG_1___")]
		public static extern void Mixpanel_opt_in_tracking__SWIG_1(HandleRef jarg1, string jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_People_set___")]
		public static extern void Mixpanel_People_set(HandleRef jarg1, string jarg2, HandleRef jarg3);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_People_set_properties___")]
		public static extern void Mixpanel_People_set_properties(HandleRef jarg1, HandleRef jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_People_set_once___")]
		public static extern void Mixpanel_People_set_once(HandleRef jarg1, string jarg2, HandleRef jarg3);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_People_set_once_properties___")]
		public static extern void Mixpanel_People_set_once_properties(HandleRef jarg1, HandleRef jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_People_unset___")]
		public static extern void Mixpanel_People_unset(HandleRef jarg1, string jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_People_unset_properties___")]
		public static extern void Mixpanel_People_unset_properties(HandleRef jarg1, HandleRef jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_People_increment___")]
		public static extern void Mixpanel_People_increment(HandleRef jarg1, string jarg2, HandleRef jarg3);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_People_increment_properties___")]
		public static extern void Mixpanel_People_increment_properties(HandleRef jarg1, HandleRef jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_People_append___")]
		public static extern void Mixpanel_People_append(HandleRef jarg1, string jarg2, HandleRef jarg3);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_People_append_properties___")]
		public static extern void Mixpanel_People_append_properties(HandleRef jarg1, HandleRef jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_People_union____")]
		public static extern void Mixpanel_People_union_(HandleRef jarg1, string jarg2, HandleRef jarg3);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_People_union_properties___")]
		public static extern void Mixpanel_People_union_properties(HandleRef jarg1, HandleRef jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_People_track_charge__SWIG_0___")]
		public static extern void Mixpanel_People_track_charge__SWIG_0(HandleRef jarg1, double jarg2, HandleRef jarg3);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_People_track_charge__SWIG_1___")]
		public static extern void Mixpanel_People_track_charge__SWIG_1(HandleRef jarg1, double jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_People_clear_charges___")]
		public static extern void Mixpanel_People_clear_charges(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_People_delete_user___")]
		public static extern void Mixpanel_People_delete_user(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_People_set_push_id___")]
		public static extern void Mixpanel_People_set_push_id(HandleRef jarg1, string jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_People_set_first_name___")]
		public static extern void Mixpanel_People_set_first_name(HandleRef jarg1, string jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_People_set_last_name___")]
		public static extern void Mixpanel_People_set_last_name(HandleRef jarg1, string jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_People_set_name___")]
		public static extern void Mixpanel_People_set_name(HandleRef jarg1, string jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_People_set_email___")]
		public static extern void Mixpanel_People_set_email(HandleRef jarg1, string jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_People_set_phone___")]
		public static extern void Mixpanel_People_set_phone(HandleRef jarg1, string jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_delete_Mixpanel_People___")]
		public static extern void delete_Mixpanel_People(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_people_set___")]
		public static extern void Mixpanel_people_set(HandleRef jarg1, HandleRef jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_people_get___")]
		public static extern IntPtr Mixpanel_people_get(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_LogEntry_level_set___")]
		public static extern void Mixpanel_LogEntry_level_set(HandleRef jarg1, int jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_LogEntry_level_get___")]
		public static extern int Mixpanel_LogEntry_level_get(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_LogEntry_message_set___")]
		public static extern void Mixpanel_LogEntry_message_set(HandleRef jarg1, string jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_LogEntry_message_get___")]
		public static extern string Mixpanel_LogEntry_message_get(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_new_Mixpanel_LogEntry___")]
		public static extern IntPtr new_Mixpanel_LogEntry();

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_delete_Mixpanel_LogEntry___")]
		public static extern void delete_Mixpanel_LogEntry(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_set_minimum_log_level___")]
		public static extern void Mixpanel_set_minimum_log_level(HandleRef jarg1, int jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_get_next_log_entry___")]
		public static extern bool Mixpanel_get_next_log_entry(HandleRef jarg1, HandleRef jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_utc_now___")]
		public static extern string Mixpanel_utc_now();

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_on_reachability_changed___")]
		public static extern void Mixpanel_on_reachability_changed(HandleRef jarg1, int jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_set_maximum_queue_size___")]
		public static extern void Mixpanel_set_maximum_queue_size(HandleRef jarg1, uint jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_set_flush_interval___")]
		public static extern void Mixpanel_set_flush_interval(HandleRef jarg1, uint jarg2);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_flush_queue___")]
		public static extern void Mixpanel_flush_queue(HandleRef jarg1);

		[DllImport("MixpanelSDK", EntryPoint = "CSharp_mixpanelfdetail_Mixpanel_director_connect___")]
		public static extern void Mixpanel_director_connect(HandleRef jarg1);
	}
}
