using UnityEngine;

namespace Battlehub.HorizonBending
{
	public struct HBSettings
	{
		public float Curvature;

		public float Flatten;

		public float HorizonZOffset;

		public float HorizonYOffset;

		public float HorizonXOffset;

		public bool AttachToCameraInEditor;

		public BendingMode BendingMode
		{
			get;
			private set;
		}

		public Vector3 Mask
		{
			get;
			private set;
		}

		public Vector3 Gradient
		{
			get;
			private set;
		}

		public Vector3 Up
		{
			get;
			private set;
		}

		public HBSettings(BendingMode bendingMode, float curvature = 0f, float flatten = 0f, float horizonXOffset = 0f, float horizonYOffset = 0f, float horizonZOffset = 0f, bool relativeToCamera = false)
		{
			BendingMode = bendingMode;
			Curvature = curvature;
			Flatten = flatten;
			HorizonZOffset = horizonZOffset;
			HorizonYOffset = horizonYOffset;
			HorizonXOffset = horizonXOffset;
			AttachToCameraInEditor = relativeToCamera;
			switch (bendingMode)
			{
			case BendingMode._HB_XY_ZUP:
				Mask = new Vector3(1f, 1f, 0f);
				Gradient = new Vector3(1f, 0f, 0f);
				Up = new Vector3(0f, 0f, 1f);
				break;
			case BendingMode._HB_X_ZUP:
				Mask = new Vector3(1f, 1f, 0f);
				Gradient = new Vector3(0f, 1f, 0f);
				Up = new Vector3(0f, 0f, 1f);
				break;
			case BendingMode._HB_Y_ZUP:
				Mask = new Vector3(1f, 1f, 0f);
				Gradient = new Vector3(1f, 0f, 0f);
				Up = new Vector3(0f, 0f, 1f);
				break;
			case BendingMode._HB_XZ_YUP:
				Mask = new Vector3(1f, 0f, 1f);
				Gradient = new Vector3(1f, 0f, 0f);
				Up = new Vector3(0f, 1f, 0f);
				break;
			case BendingMode._HB_X_YUP:
				Mask = new Vector3(1f, 0f, 1f);
				Gradient = new Vector3(0f, 0f, 1f);
				Up = new Vector3(0f, 1f, 0f);
				break;
			case BendingMode._HB_Z_YUP:
				Mask = new Vector3(1f, 0f, 1f);
				Gradient = new Vector3(1f, 0f, 0f);
				Up = new Vector3(0f, 1f, 0f);
				break;
			case BendingMode._HB_YZ_XUP:
				Mask = new Vector3(0f, 1f, 1f);
				Gradient = new Vector3(0f, 1f, 0f);
				Up = new Vector3(1f, 0f, 0f);
				break;
			case BendingMode._HB_Y_XUP:
				Mask = new Vector3(0f, 1f, 1f);
				Gradient = new Vector3(0f, 0f, 1f);
				Up = new Vector3(1f, 0f, 0f);
				break;
			case BendingMode._HB_Z_XUP:
				Mask = new Vector3(0f, 1f, 1f);
				Gradient = new Vector3(0f, 1f, 0f);
				Up = new Vector3(1f, 0f, 0f);
				break;
			default:
				Mask = new Vector3(0f, 0f, 0f);
				Gradient = new Vector3(0f, 0f, 0f);
				Up = new Vector3(0f, 0f, 0f);
				break;
			}
		}
	}
}
