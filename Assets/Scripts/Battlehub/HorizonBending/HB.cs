using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Battlehub.HorizonBending
{
	[ExecuteInEditMode]
	public class HB : MonoBehaviour
	{
		public const float CURVATURE_FACTOR = 0.001f;

		public float Curvature = 5f;

		public float Flatten;

		public float HorizonZOffset;

		public float HorizonYOffset;

		public float HorizonXOffset;

		public BendingMode BendingMode;

		public float RaycastStride = 1f;

		public float FixBoundsRadius = 50f;

		public float FixFieldOfView;

		public float FixOrthographicSize;

		public Camera FixLightsPositionCamera;

		public GameObject[] ExcludeGameObjects;

		public Material[] Materials;

		public bool LockMaterials;

		public static HB Instance;

		private void OnEnable()
		{
			if (Instance != this && Instance != null)
			{
				UnityEngine.Debug.LogError("Another instance of HB exists in scene");
				return;
			}
			Instance = this;
			CreateHBCameras();
			ApplyAll(Curvature, Flatten, HorizonXOffset, HorizonYOffset, HorizonZOffset);
		}

		private void OnDisable()
		{
			DestroyHBCameras();
			Instance = null;
		}

		public static void ChangeCurvature(float delta)
		{
			HB instance = Instance;
			if (!(instance == null))
			{
				ApplyCurvature(instance.Curvature + delta);
			}
		}

		public static void ChangeFlatten(float delta)
		{
			HB instance = Instance;
			if (!(instance == null))
			{
				ApplyFlatten(instance.Flatten + delta);
			}
		}

		public static void ChangeHorizonOffset(float deltaX, float deltaY, float deltaZ, Transform transform = null)
		{
			HB instance = Instance;
			if (!(instance == null))
			{
				ApplyHorizonOffset(instance.HorizonXOffset + deltaX, instance.HorizonYOffset + deltaY, instance.HorizonZOffset + deltaZ, transform);
			}
		}

		public static void ApplyCurvature(float curvature)
		{
			HB instance = Instance;
			if (!(instance == null))
			{
				instance.Curvature = curvature;
				HBUtils.HBCurvature(curvature * 0.001f);
			}
		}

		public static void ApplyFlatten(float flatten)
		{
			HB instance = Instance;
			if (!(instance == null))
			{
				instance.Flatten = flatten;
				HBUtils.HBFlatten(flatten);
			}
		}

		public static void ApplyHorizonOffset(float horizonX, float horizonY, float horizonZ, Transform transform = null)
		{
			HB instance = Instance;
			if (!(instance == null))
			{
				instance.HorizonXOffset = horizonX;
				instance.HorizonYOffset = horizonY;
				instance.HorizonZOffset = horizonZ;
				HBUtils.HBHorizonOffset(horizonX, horizonY, horizonZ, transform);
			}
		}

		public static void ApplyAll(float curvature, float flatten, float horizonX, float horizonY, float horizonZ, Transform transform = null)
		{
			HB instance = Instance;
			if (!(instance == null))
			{
				instance.Curvature = curvature;
				instance.Flatten = flatten;
				instance.HorizonXOffset = horizonX;
				instance.HorizonYOffset = horizonY;
				instance.HorizonZOffset = horizonZ;
				HBSettings settings = GetSettings();
				HBUtils.HorizonBend(settings, transform);
			}
		}

		public static void FixSkinned(SkinnedMeshRenderer skinned, Vector3 extents)
		{
			HBUtils.FixSkinned(skinned, skinned.localBounds, extents);
		}

		public static void FixSkinned(SkinnedMeshRenderer skinned, float fixBoundsRadius)
		{
			HB instance = Instance;
			if (!(instance == null))
			{
				Vector3 offset = HBUtils.GetOffset(GetSettings(), fixBoundsRadius);
				HBUtils.FixSkinned(skinned, skinned.localBounds, skinned.transform, offset);
			}
		}

		public static void FixBounds(MeshFilter meshFilter, Vector3 extents)
		{
			meshFilter.sharedMesh = HBUtils.FixBounds(meshFilter.sharedMesh, extents);
		}

		public static void FixBounds(MeshFilter meshFilter, float fixBoundsRadius)
		{
			HB instance = Instance;
			if (!(instance == null))
			{
				Vector3 offset = HBUtils.GetOffset(GetSettings(), fixBoundsRadius);
				meshFilter.sharedMesh = HBUtils.FixBounds(meshFilter.sharedMesh, meshFilter.transform, offset);
			}
		}

		public static void FixMesh(MeshFilter meshFilter, Vector3 extents)
		{
			meshFilter.sharedMesh = HBUtils.FixMesh(meshFilter.sharedMesh, extents);
		}

		public static void FixMesh(MeshFilter meshFilter, float fixBoundsRadius)
		{
			HB instance = Instance;
			if (!(instance == null))
			{
				Vector3 offset = HBUtils.GetOffset(GetSettings(), fixBoundsRadius);
				meshFilter.sharedMesh = HBUtils.FixMesh(meshFilter.sharedMesh, meshFilter.transform, offset);
			}
		}

		public static void FixLineRenderer(LineRenderer lineRenderer, NavMeshPath path)
		{
			HB instance = Instance;
			if (instance == null || path.corners.Length == 0)
			{
				lineRenderer.positionCount = path.corners.Length;
				for (int i = 0; i < path.corners.Length; i++)
				{
					lineRenderer.SetPosition(i, path.corners[i]);
				}
				return;
			}
			List<Vector3> list = new List<Vector3>();
			for (int j = 0; j < path.corners.Length - 1; j++)
			{
				Vector3 start = path.corners[j];
				Vector3 end = path.corners[j + 1];
				Vector3[] array = SubdivideLine(start, end);
				for (int k = 0; k < array.Length - 1; k++)
				{
					list.Add(array[k]);
				}
			}
			list.Add(path.corners[path.corners.Length - 1]);
			lineRenderer.positionCount = list.Count;
			for (int l = 0; l < list.Count; l++)
			{
				lineRenderer.SetPosition(l, list[l]);
			}
		}

		public static void FixLineRenderer(LineRenderer lineRenderer, Vector3 start, Vector3 end)
		{
			HB instance = Instance;
			if (instance == null)
			{
				lineRenderer.SetPositions(new Vector3[2]
				{
					start,
					end
				});
				return;
			}
			Vector3[] array = SubdivideLine(start, end);
			lineRenderer.positionCount = array.Length;
			lineRenderer.SetPositions(array);
		}

		private static Vector3[] SubdivideLine(Vector3 start, Vector3 end)
		{
			HB instance = Instance;
			float magnitude = (end - start).magnitude;
			int num = Mathf.Max(0, Mathf.CeilToInt((magnitude - instance.RaycastStride) / instance.RaycastStride));
			Vector3[] array = new Vector3[num + 2];
			Vector3 b = (end - start).normalized * instance.RaycastStride;
			for (int i = 0; i <= num; i++)
			{
				array[i] = start;
				start += b;
			}
			array[num + 1] = end;
			return array;
		}

		public static HBSettings GetSettings(bool attachToCameraInEditor)
		{
			HB instance = Instance;
			if (instance == null)
			{
				return new HBSettings(BendingMode._HB_OFF);
			}
			float horizonXOffset;
			float horizonYOffset;
			float horizonZOffset;
			if (attachToCameraInEditor)
			{
				horizonXOffset = instance.HorizonXOffset;
				horizonYOffset = instance.HorizonYOffset;
				horizonZOffset = instance.HorizonZOffset;
			}
			else
			{
				float horizonXOffset2 = instance.HorizonXOffset;
				Vector3 position = instance.transform.position;
				horizonXOffset = horizonXOffset2 + position.x;
				float horizonYOffset2 = instance.HorizonYOffset;
				Vector3 position2 = instance.transform.position;
				horizonYOffset = horizonYOffset2 + position2.y;
				float horizonZOffset2 = instance.HorizonZOffset;
				Vector3 position3 = instance.transform.position;
				horizonZOffset = horizonZOffset2 + position3.z;
			}
			return new HBSettings(instance.BendingMode, instance.Curvature * 0.001f, instance.Flatten, horizonXOffset, horizonYOffset, horizonZOffset, attachToCameraInEditor);
		}

		public static HBSettings GetSettings()
		{
			HB instance = Instance;
			if (instance == null)
			{
				return new HBSettings(BendingMode._HB_OFF);
			}
			bool attachToCameraInEditor = true;
			return GetSettings(attachToCameraInEditor);
		}

		public static Vector3 GetOffset(Vector3 atPosition, Transform cameraTransform)
		{
			HB instance = Instance;
			if (instance == null)
			{
				return new Vector3(0f, 0f, 0f);
			}
			HBSettings settings = GetSettings();
			return HBUtils.GetOffset(atPosition, settings, cameraTransform);
		}

		public static void CameraToRays(Transform camerTransform, out Ray[] rays, out float[] maxDistances)
		{
			HB instance = Instance;
			if (instance == null)
			{
				Ray ray = new Ray(camerTransform.position, camerTransform.forward);
				rays = new Ray[1]
				{
					ray
				};
				maxDistances = new float[1]
				{
					float.PositiveInfinity
				};
			}
			else
			{
				HBUtils.CameraToRays(camerTransform, instance.RaycastStride, instance.FixBoundsRadius, GetSettings(), out rays, out maxDistances);
			}
		}

		public static void ScreenPointToRays(Camera camera, out Ray[] rays, out float[] maxDistances)
		{
			HB instance = Instance;
			if (instance == null)
			{
				Ray ray = camera.ScreenPointToRay(UnityEngine.Input.mousePosition);
				rays = new Ray[1]
				{
					ray
				};
				maxDistances = new float[1]
				{
					float.PositiveInfinity
				};
			}
			else
			{
				HBUtils.ScreenPointToRays(camera, instance.RaycastStride, instance.FixBoundsRadius, GetSettings(), out rays, out maxDistances);
			}
		}

		public static RaycastHit FixRaycastHit(RaycastHit hit, Transform cameraTransform, Vector3 rayOrigin)
		{
			hit.point -= GetOffset(hit.point, cameraTransform);
			hit.distance = (rayOrigin - hit.point).magnitude;
			return hit;
		}

		public static RaycastHit FixRaycastHitDistance(RaycastHit hit, Vector3 rayOrigin)
		{
			hit.distance = (rayOrigin - hit.point).magnitude;
			return hit;
		}

		public static bool Raycast(Ray[] rays, out RaycastHit hitInfo, float[] maxDistances, int layerMask = int.MaxValue)
		{
			if (rays == null)
			{
				throw new ArgumentNullException("rays");
			}
			if (maxDistances == null)
			{
				throw new ArgumentNullException("maxDistances");
			}
			int num = Math.Min(rays.Length, maxDistances.Length);
			for (int i = 0; i < num; i++)
			{
				if (Physics.Raycast(rays[i], out hitInfo, maxDistances[i], layerMask))
				{
					return true;
				}
			}
			hitInfo = default(RaycastHit);
			return false;
		}

		public static List<RaycastHit> RaycastAll(Ray[] rays, float[] maxDistances, int layerMask)
		{
			if (rays == null)
			{
				throw new ArgumentNullException("rays");
			}
			if (maxDistances == null)
			{
				throw new ArgumentNullException("maxDistances");
			}
			List<RaycastHit> list = new List<RaycastHit>();
			int num = Math.Min(rays.Length, maxDistances.Length);
			for (int i = 0; i < num; i++)
			{
				list.AddRange(Physics.RaycastAll(rays[i], maxDistances[i], layerMask));
			}
			return list;
		}

		private void CreateHBCameras()
		{
			Camera[] array = UnityEngine.Object.FindObjectsOfType<Camera>();
			Camera[] array2 = array;
			foreach (Camera camera in array2)
			{
				HBCamera component = camera.GetComponent<HBCamera>();
				if (component == null)
				{
					camera.gameObject.AddComponent<HBCamera>();
				}
			}
		}

        private void DestroyHBCameras()
        {
            Camera[] cameras = UnityEngine.Object.FindObjectsOfType<Camera>();
            foreach (Camera camera in cameras)
            {
                HBCamera component = camera.GetComponent<HBCamera>();
                if (component != null)
                {
                    if (Application.isPlaying)
                    {
                        UnityEngine.Object.Destroy(component);
                    }
                    else
                    {
                        UnityEngine.Object.DestroyImmediate(component);
                    }
                }
            }
        }


        public static void DebugScreenPointsToRay(Camera camera)
		{
		}

		public static void DebugCameraToRays(Transform cameraTransform)
		{
		}
	}
}
