using System;
using System.Runtime.InteropServices;

namespace mixpanel.detail
{
	public class SWIGTYPE_p_std__wstring
	{
		private HandleRef swigCPtr;

		internal SWIGTYPE_p_std__wstring(IntPtr cPtr, bool futureUse)
		{
			swigCPtr = new HandleRef(this, cPtr);
		}

		protected SWIGTYPE_p_std__wstring()
		{
			swigCPtr = new HandleRef(null, IntPtr.Zero);
		}

		internal static HandleRef getCPtr(SWIGTYPE_p_std__wstring obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	}
}
