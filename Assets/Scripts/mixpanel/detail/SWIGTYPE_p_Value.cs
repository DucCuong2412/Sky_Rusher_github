using System;
using System.Runtime.InteropServices;

namespace mixpanel.detail
{
	public class SWIGTYPE_p_Value
	{
		private HandleRef swigCPtr;

		internal SWIGTYPE_p_Value(IntPtr cPtr, bool futureUse)
		{
			swigCPtr = new HandleRef(this, cPtr);
		}

		protected SWIGTYPE_p_Value()
		{
			swigCPtr = new HandleRef(null, IntPtr.Zero);
		}

		internal static HandleRef getCPtr(SWIGTYPE_p_Value obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	}
}
