using System;
using System.Runtime.InteropServices;

namespace mixpanel.detail
{
	public class Value : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		public Value this[int idx]
		{
			get
			{
				return at(idx);
			}
			set
			{
				at(idx).set(value);
			}
		}

		public Value this[string idx]
		{
			get
			{
				return at(idx);
			}
			set
			{
				at(idx).set(value);
			}
		}

		internal Value(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		public Value()
			: this(MixpanelSDKPINVOKE.new_Value__SWIG_0(), cMemoryOwn: true)
		{
		}

		public Value(int value)
			: this(MixpanelSDKPINVOKE.new_Value__SWIG_1(value), cMemoryOwn: true)
		{
		}

		public Value(double value)
			: this(MixpanelSDKPINVOKE.new_Value__SWIG_2(value), cMemoryOwn: true)
		{
		}

		public Value(float value)
			: this(MixpanelSDKPINVOKE.new_Value__SWIG_3(value), cMemoryOwn: true)
		{
		}

		public Value(string value)
			: this(MixpanelSDKPINVOKE.new_Value__SWIG_4(value), cMemoryOwn: true)
		{
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
		}

		public Value(bool value)
			: this(MixpanelSDKPINVOKE.new_Value__SWIG_5(value), cMemoryOwn: true)
		{
		}

		public Value(Value other)
			: this(MixpanelSDKPINVOKE.new_Value__SWIG_6(getCPtr(other)), cMemoryOwn: true)
		{
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
		}

		internal static HandleRef getCPtr(Value obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}

		~Value()
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
						MixpanelSDKPINVOKE.delete_Value(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		public Value get(uint index, Value defaultValue)
		{
			Value result = new Value(MixpanelSDKPINVOKE.Value_get__SWIG_0(swigCPtr, index, getCPtr(defaultValue)), cMemoryOwn: true);
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}

		public bool isValidIndex(uint index)
		{
			return MixpanelSDKPINVOKE.Value_isValidIndex(swigCPtr, index);
		}

		public Value append(Value value)
		{
			Value result = new Value(MixpanelSDKPINVOKE.Value_append(swigCPtr, getCPtr(value)), cMemoryOwn: false);
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}

		public Value get(string key, Value defaultValue)
		{
			Value result = new Value(MixpanelSDKPINVOKE.Value_get__SWIG_1(swigCPtr, key, getCPtr(defaultValue)), cMemoryOwn: true);
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}

		public Value removeMember(string key)
		{
			Value result = new Value(MixpanelSDKPINVOKE.Value_removeMember(swigCPtr, key), cMemoryOwn: true);
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}

		public bool isMember(string key)
		{
			bool result = MixpanelSDKPINVOKE.Value_isMember(swigCPtr, key);
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}

		public string toStyledString()
		{
			return MixpanelSDKPINVOKE.Value_toStyledString(swigCPtr);
		}

		public string asString()
		{
			return MixpanelSDKPINVOKE.Value_asString(swigCPtr);
		}

		public int asInt()
		{
			return MixpanelSDKPINVOKE.Value_asInt(swigCPtr);
		}

		public float asFloat()
		{
			return MixpanelSDKPINVOKE.Value_asFloat(swigCPtr);
		}

		public double asDouble()
		{
			return MixpanelSDKPINVOKE.Value_asDouble(swigCPtr);
		}

		public bool asBool()
		{
			return MixpanelSDKPINVOKE.Value_asBool(swigCPtr);
		}

		public bool isNull()
		{
			return MixpanelSDKPINVOKE.Value_isNull(swigCPtr);
		}

		public bool isBool()
		{
			return MixpanelSDKPINVOKE.Value_isBool(swigCPtr);
		}

		public bool isInt()
		{
			return MixpanelSDKPINVOKE.Value_isInt(swigCPtr);
		}

		public bool isIntegral()
		{
			return MixpanelSDKPINVOKE.Value_isIntegral(swigCPtr);
		}

		public bool isDouble()
		{
			return MixpanelSDKPINVOKE.Value_isDouble(swigCPtr);
		}

		public bool isNumeric()
		{
			return MixpanelSDKPINVOKE.Value_isNumeric(swigCPtr);
		}

		public bool isString()
		{
			return MixpanelSDKPINVOKE.Value_isString(swigCPtr);
		}

		public bool isArray()
		{
			return MixpanelSDKPINVOKE.Value_isArray(swigCPtr);
		}

		public bool isObject()
		{
			return MixpanelSDKPINVOKE.Value_isObject(swigCPtr);
		}

		public uint size()
		{
			return MixpanelSDKPINVOKE.Value_size(swigCPtr);
		}

		public bool empty()
		{
			return MixpanelSDKPINVOKE.Value_empty(swigCPtr);
		}

		public void clear()
		{
			MixpanelSDKPINVOKE.Value_clear(swigCPtr);
		}

		public void resize(uint size)
		{
			MixpanelSDKPINVOKE.Value_resize(swigCPtr, size);
		}

		public Value at(string key)
		{
			Value result = new Value(MixpanelSDKPINVOKE.Value_at__SWIG_0(swigCPtr, key), cMemoryOwn: false);
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}

		public Value at(int index)
		{
			return new Value(MixpanelSDKPINVOKE.Value_at__SWIG_1(swigCPtr, index), cMemoryOwn: false);
		}

		public void set(int x)
		{
			MixpanelSDKPINVOKE.Value_set__SWIG_0(swigCPtr, x);
		}

		public void set(string x)
		{
			MixpanelSDKPINVOKE.Value_set__SWIG_1(swigCPtr, x);
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
		}

		public void set(double x)
		{
			MixpanelSDKPINVOKE.Value_set__SWIG_2(swigCPtr, x);
		}

		public void set(float x)
		{
			MixpanelSDKPINVOKE.Value_set__SWIG_3(swigCPtr, x);
		}

		public void set(Value x)
		{
			MixpanelSDKPINVOKE.Value_set__SWIG_4(swigCPtr, getCPtr(x));
			if (MixpanelSDKPINVOKE.SWIGPendingException.Pending)
			{
				throw MixpanelSDKPINVOKE.SWIGPendingException.Retrieve();
			}
		}

		public static implicit operator string(Value v)
		{
			return v.asString();
		}

		public static implicit operator Value(string v)
		{
			return new Value(v);
		}

		public static implicit operator int(Value v)
		{
			return v.asInt();
		}

		public static implicit operator Value(int v)
		{
			return new Value(v);
		}

		public static implicit operator double(Value v)
		{
			return v.asDouble();
		}

		public static implicit operator Value(double v)
		{
			return new Value(v);
		}

		public static implicit operator float(Value v)
		{
			return v.asFloat();
		}

		public static implicit operator Value(float v)
		{
			return new Value(v);
		}

		public static implicit operator bool(Value v)
		{
			return v.asBool();
		}

		public static implicit operator Value(bool v)
		{
			return new Value(v);
		}
	}
}
