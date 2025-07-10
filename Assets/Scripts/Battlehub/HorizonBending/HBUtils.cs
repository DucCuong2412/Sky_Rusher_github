using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battlehub.HorizonBending
{
	public static class HBUtils
	{
		public enum BlendMode
		{
			Opaque,
			Cutout,
			Fade,
			Transparent
		}

		public static string ShaderRoot = "Battlehub/";

		public static void HBCurvature(float curvature)
		{
			Shader.SetGlobalFloat("_Curvature", curvature);
		}

		public static void HBFlatten(float flatten)
		{
			Shader.SetGlobalFloat("_Flatten", flatten);
		}

		public static void HBHorizonOffset(float horizonXOffset, float horizonYOffset, float horizonZOffset, Transform transform = null)
		{
			if (transform == null)
			{
				Shader.SetGlobalFloat("_HorizonZOffset", horizonZOffset);
				Shader.SetGlobalFloat("_HorizonYOffset", horizonYOffset);
				Shader.SetGlobalFloat("_HorizonXOffset", horizonXOffset);
				return;
			}
			Vector3 position = transform.position;
			Shader.SetGlobalFloat("_HorizonZOffset", position.z + horizonZOffset);
			Vector3 position2 = transform.position;
			Shader.SetGlobalFloat("_HorizonYOffset", position2.y + horizonYOffset);
			Vector3 position3 = transform.position;
			Shader.SetGlobalFloat("_HorizonXOffset", position3.x + horizonXOffset);
		}

		public static void HBMode(BendingMode mode)
		{
			IEnumerator enumerator = Enum.GetValues(typeof(BendingMode)).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Shader.DisableKeyword(((BendingMode)enumerator.Current).ToString());
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			Shader.EnableKeyword(mode.ToString());
		}

		public static void HorizonBend(HBSettings settings, Transform transform = null)
		{
			HBCurvature(settings.Curvature);
			HBFlatten(settings.Flatten);
			HBHorizonOffset(settings.HorizonXOffset, settings.HorizonYOffset, settings.HorizonZOffset, transform);
		}

		public static void ReplaceShaders(Material[] materials, string[] names, bool withDefault)
		{
			Dictionary<string, Shader> dictionary = FindReplacementShaders(names, withDefault);
			foreach (Material material in materials)
			{
				if (!(material == null))
				{
					if (dictionary.ContainsKey(material.shader.name))
					{
						material.shader = dictionary[material.shader.name];
					}
					if (material.shader.name == "Battlehub/HB_Standard" || material.shader.name == "Battlehub/HB_Standard (Specular setup)" || material.shader.name == "Standard" || material.shader.name == "Standard (Specular setup)")
					{
						SetupMaterialWithBlendMode(material, (BlendMode)material.GetFloat("_Mode"));
					}
				}
			}
		}

		private static void SetupMaterialWithBlendMode(Material material, BlendMode blendMode)
		{
			switch (blendMode)
			{
			case BlendMode.Opaque:
				material.SetOverrideTag("RenderType", string.Empty);
				material.SetInt("_SrcBlend", 1);
				material.SetInt("_DstBlend", 0);
				material.SetInt("_ZWrite", 1);
				material.DisableKeyword("_ALPHATEST_ON");
				material.DisableKeyword("_ALPHABLEND_ON");
				material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
				material.renderQueue = -1;
				break;
			case BlendMode.Cutout:
				material.SetOverrideTag("RenderType", "TransparentCutout");
				material.SetInt("_SrcBlend", 1);
				material.SetInt("_DstBlend", 0);
				material.SetInt("_ZWrite", 1);
				material.EnableKeyword("_ALPHATEST_ON");
				material.DisableKeyword("_ALPHABLEND_ON");
				material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
				material.renderQueue = 2450;
				break;
			case BlendMode.Fade:
				material.SetOverrideTag("RenderType", "Transparent");
				material.SetInt("_SrcBlend", 5);
				material.SetInt("_DstBlend", 10);
				material.SetInt("_ZWrite", 0);
				material.DisableKeyword("_ALPHATEST_ON");
				material.EnableKeyword("_ALPHABLEND_ON");
				material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
				material.renderQueue = 3000;
				break;
			case BlendMode.Transparent:
				material.SetOverrideTag("RenderType", "Transparent");
				material.SetInt("_SrcBlend", 1);
				material.SetInt("_DstBlend", 10);
				material.SetInt("_ZWrite", 0);
				material.DisableKeyword("_ALPHATEST_ON");
				material.DisableKeyword("_ALPHABLEND_ON");
				material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
				material.renderQueue = 3000;
				break;
			}
		}

		private static Dictionary<string, Shader> FindReplacementShaders(string[] names, bool replaceWithDefault = false)
		{
			Dictionary<string, Shader> dictionary = new Dictionary<string, Shader>();
			if (replaceWithDefault)
			{
				foreach (string text in names)
				{
					string name = text.Replace(ShaderRoot, string.Empty).Replace("HB_", string.Empty);
					Shader shader = Shader.Find(name);
					if (shader != null)
					{
						dictionary.Add(text, shader);
					}
				}
			}
			else
			{
				foreach (string text2 in names)
				{
					string key = text2.Replace("Battlehub/", string.Empty).Replace("HB_", string.Empty);
					Shader shader2 = Shader.Find(text2);
					if (shader2 != null)
					{
						dictionary.Add(key, shader2);
					}
				}
			}
			return dictionary;
		}

		public static Material[] Trim(Material[] materials, Dictionary<Mesh, Dictionary<TransformToHash, List<Renderer>>> groups, Shader[] integratedShaders)
		{
			HashSet<string> hashSet = new HashSet<string>(from s in integratedShaders
				where s != null
				select s.name);
			List<Material> list = new List<Material>(materials);
			for (int num = list.Count - 1; num >= 0; num--)
			{
				Material material2 = list[num];
				if (!(material2 == null) && !material2.shader.name.Contains(ShaderRoot) && !hashSet.Contains(material2.shader.name))
				{
					list.RemoveAt(num);
				}
			}
			HashSet<Material> materialsHS = new HashSet<Material>();
			for (int i = 0; i < list.Count; i++)
			{
				Material item = list[i];
				if (!materialsHS.Contains(item))
				{
					materialsHS.Add(item);
				}
			}
			List<Mesh> list2 = new List<Mesh>();
			foreach (KeyValuePair<Mesh, Dictionary<TransformToHash, List<Renderer>>> group in groups)
			{
				Dictionary<TransformToHash, List<Renderer>> value = group.Value;
				List<TransformToHash> list3 = new List<TransformToHash>();
				foreach (KeyValuePair<TransformToHash, List<Renderer>> item2 in value)
				{
					List<Renderer> value2 = item2.Value;
					for (int num2 = value2.Count - 1; num2 >= 0; num2--)
					{
						Renderer renderer = value2[num2];
						if (!renderer.sharedMaterials.Any((Material material) => materialsHS.Contains(material)))
						{
							value2.RemoveAt(num2);
						}
					}
					if (value2.Count == 0)
					{
						list3.Add(item2.Key);
					}
				}
				for (int j = 0; j < list3.Count; j++)
				{
					value.Remove(list3[j]);
				}
				if (value.Count == 0)
				{
					list2.Add(group.Key);
				}
			}
			for (int k = 0; k < list2.Count; k++)
			{
				groups.Remove(list2[k]);
			}
			return list.ToArray();
		}

		public static void Find(out Material[] materials, out Dictionary<Mesh, Dictionary<TransformToHash, List<Renderer>>> groups, GameObject[] excludeGameObjects)
		{
			HashSet<Material> hashSet = new HashSet<Material>();
			HashSet<Renderer> hashSet2 = new HashSet<Renderer>();
			foreach (GameObject gameObject in excludeGameObjects)
			{
				Renderer[] components = gameObject.GetComponents<Renderer>();
				foreach (Renderer item in components)
				{
					if (!hashSet2.Contains(item))
					{
						hashSet2.Add(item);
					}
				}
			}
			Mesh mesh = new Mesh();
			groups = new Dictionary<Mesh, Dictionary<TransformToHash, List<Renderer>>>();
			Renderer[] array = UnityEngine.Object.FindObjectsOfType<Renderer>();
			for (int k = 0; k < array.Length; k++)
			{
				Mesh mesh2 = null;
				Renderer renderer = array[k];
				if (hashSet2.Contains(renderer))
				{
					continue;
				}
				Material[] sharedMaterials = renderer.sharedMaterials;
				foreach (Material item2 in sharedMaterials)
				{
					if (!hashSet.Contains(item2))
					{
						hashSet.Add(item2);
					}
				}
				if (renderer is MeshRenderer)
				{
					HBFixBounds component = renderer.GetComponent<HBFixBounds>();
					if (component != null)
					{
						mesh2 = component.OriginalMesh;
					}
					else
					{
						MeshFilter component2 = renderer.GetComponent<MeshFilter>();
						if (component2 != null)
						{
							mesh2 = component2.sharedMesh;
						}
					}
				}
				else if (renderer is SkinnedMeshRenderer)
				{
					SkinnedMeshRenderer skinnedMeshRenderer = (SkinnedMeshRenderer)renderer;
					mesh2 = skinnedMeshRenderer.sharedMesh;
				}
				else if (renderer is ParticleSystemRenderer)
				{
					mesh2 = mesh;
				}
				if (mesh2 != null)
				{
					if (!groups.ContainsKey(mesh2))
					{
						groups.Add(mesh2, new Dictionary<TransformToHash, List<Renderer>>());
					}
					Dictionary<TransformToHash, List<Renderer>> dictionary = groups[mesh2];
					TransformToHash key = new TransformToHash(renderer.gameObject.transform);
					if (!dictionary.ContainsKey(key))
					{
						dictionary.Add(key, new List<Renderer>());
					}
					List<Renderer> list = dictionary[key];
					list.Add(renderer);
				}
			}
			materials = hashSet.ToArray();
		}

		public static void CameraToRays(Transform cameraTransform, float stride, float curvatureRadius, HBSettings settings, out Ray[] rays, out float[] maxDistances)
		{
			Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
			RayToRays(cameraTransform, ray, stride, curvatureRadius, settings, out rays, out maxDistances);
		}

		public static void ScreenPointToRays(Camera camera, float stride, float curvatureRadius, HBSettings settings, out Ray[] rays, out float[] maxDistances)
		{
			Ray ray = camera.ScreenPointToRay(UnityEngine.Input.mousePosition);
			RayToRays(camera.transform, ray, stride, curvatureRadius, settings, out rays, out maxDistances);
		}

		private static void RayToRays(Transform cameraTransform, Ray ray, float stride, float curvatureRadius, HBSettings settings, out Ray[] rays, out float[] maxDistances)
		{
			if (stride <= 0f)
			{
				throw new ArgumentOutOfRangeException("stride", "stride <= 0.0");
			}
			float num = stride;
			int num2 = Mathf.CeilToInt(Mathf.Max(curvatureRadius - settings.Flatten, 0f) / stride);
			if (settings.Flatten > 0f)
			{
				num = settings.Flatten;
				num2++;
			}
			rays = new Ray[num2];
			maxDistances = new float[num2];
			Vector3 vector = ray.origin + GetOffset(ray.origin, settings, cameraTransform);
			int num3 = 0;
			while (true)
			{
				if (num3 < num2)
				{
					if (!GetNextRayOrigin(ray, num, curvatureRadius, settings, cameraTransform, out Vector3 nextRayOrigin))
					{
						break;
					}
					rays[num3] = new Ray(vector, nextRayOrigin - vector);
					maxDistances[num3] = (nextRayOrigin - vector).magnitude;
					vector = nextRayOrigin;
					num += stride;
					num3++;
					continue;
				}
				return;
			}
			rays[num3] = new Ray(vector, ray.direction);
			maxDistances[maxDistances.Length - 1] = float.PositiveInfinity;
			Array.Resize(ref rays, num3 + 1);
			Array.Resize(ref maxDistances, num3 + 1);
		}

		private static bool GetNextRayOrigin(Ray ray, float stride, float curvatureRadius, HBSettings settings, Transform camera, out Vector3 nextRayOrigin)
		{
			Vector3 a = Vector3.Scale(ray.direction, settings.Mask);
			a.Normalize();
			Vector3 vector = a * stride + ray.origin;
			Plane plane = new Plane(-a, vector);
			Vector3 offset = GetOffset(vector, settings, camera);
			if (plane.Raycast(ray, out float enter))
			{
				Vector3 vector2 = nextRayOrigin = ray.GetPoint(enter) + offset;
				return true;
			}
			nextRayOrigin = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
			return false;
		}

		public static Vector3 GetOffset(HBSettings settings, float radius)
		{
			Vector3 point = Vector3.Scale(new Vector3(settings.HorizonXOffset, settings.HorizonYOffset, settings.HorizonZOffset), settings.Mask) + settings.Gradient * radius;
			return GetOffset(point, settings);
		}

		public static Vector3 GetOffset(Vector3 point, HBSettings settings, Transform camera = null)
		{
			if (settings.AttachToCameraInEditor)
			{
				if (camera == null)
				{
					throw new ArgumentNullException("camera");
				}
				point -= camera.position;
			}
			switch (settings.BendingMode)
			{
			case BendingMode._HB_XY_ZUP:
			{
				float num11 = Mathf.Max(0f, Mathf.Abs(settings.HorizonXOffset - point.x) - settings.Flatten);
				float num12 = Mathf.Max(0f, Mathf.Abs(settings.HorizonYOffset - point.y) - settings.Flatten);
				float z3 = (num11 * num11 + num12 * num12) * settings.Curvature;
				return new Vector3(0f, 0f, z3);
			}
			case BendingMode._HB_X_ZUP:
			{
				float num10 = Mathf.Max(0f, Mathf.Abs(settings.HorizonYOffset - point.y) - settings.Flatten);
				float z2 = num10 * num10 * settings.Curvature;
				return new Vector3(0f, 0f, z2);
			}
			case BendingMode._HB_Y_ZUP:
			{
				float num9 = Mathf.Max(0f, Mathf.Abs(settings.HorizonXOffset - point.x) - settings.Flatten);
				float z = num9 * num9 * settings.Curvature;
				return new Vector3(0f, 0f, z);
			}
			case BendingMode._HB_XZ_YUP:
			{
				float num7 = Mathf.Max(0f, Mathf.Abs(settings.HorizonXOffset - point.x) - settings.Flatten);
				float num8 = Mathf.Max(0f, Mathf.Abs(settings.HorizonZOffset - point.z) - settings.Flatten);
				float y3 = (num7 * num7 + num8 * num8) * settings.Curvature;
				return new Vector3(0f, y3, 0f);
			}
			case BendingMode._HB_X_YUP:
			{
				float num6 = Mathf.Max(0f, Mathf.Abs(settings.HorizonZOffset - point.z) - settings.Flatten);
				float y2 = num6 * num6 * settings.Curvature;
				return new Vector3(0f, y2, 0f);
			}
			case BendingMode._HB_Z_YUP:
			{
				float num5 = Mathf.Max(0f, Mathf.Abs(settings.HorizonXOffset - point.x) - settings.Flatten);
				float y = num5 * num5 * settings.Curvature;
				return new Vector3(0f, y, 0f);
			}
			case BendingMode._HB_YZ_XUP:
			{
				float num3 = Mathf.Max(0f, Mathf.Abs(settings.HorizonYOffset - point.y) - settings.Flatten);
				float num4 = Mathf.Max(0f, Mathf.Abs(settings.HorizonZOffset - point.z) - settings.Flatten);
				float x3 = (num3 * num3 + num4 * num4) * settings.Curvature;
				return new Vector3(x3, 0f, 0f);
			}
			case BendingMode._HB_Y_XUP:
			{
				float num2 = Mathf.Max(0f, Mathf.Abs(settings.HorizonZOffset - point.z) - settings.Flatten);
				float x2 = num2 * num2 * settings.Curvature;
				return new Vector3(x2, 0f, 0f);
			}
			case BendingMode._HB_Z_XUP:
			{
				float num = Mathf.Max(0f, Mathf.Abs(settings.HorizonYOffset - point.y) - settings.Flatten);
				float x = num * num * settings.Curvature;
				return new Vector3(x, 0f, 0f);
			}
			default:
				return new Vector3(0f, 0f, 0f);
			}
		}

		public static void FixSkinned(SkinnedMeshRenderer skinnedMesh, Bounds originalAABB, Transform transform, Vector3 hbOffset)
		{
			Vector3 b = (!(skinnedMesh.rootBone != null)) ? transform.InverseTransformVector(hbOffset) : skinnedMesh.rootBone.worldToLocalMatrix.MultiplyVector(hbOffset);
			b.x = Mathf.Abs(b.x);
			b.y = Mathf.Abs(b.y);
			b.z = Mathf.Abs(b.z);
			Vector3 extents = originalAABB.extents + b;
			FixSkinned(skinnedMesh, originalAABB, extents);
		}

		public static void FixSkinned(SkinnedMeshRenderer skinnedMesh, Bounds originalAABB, Vector3 extents)
		{
			Bounds bounds = originalAABB;
			skinnedMesh.localBounds = new Bounds(bounds.center, extents * 2f);
		}

		public static Mesh FixMesh(Mesh originalMesh, Transform transform, Vector3 hbOffset)
		{
			Bounds bounds = originalMesh.bounds;
			Vector3 b = transform.InverseTransformVector(hbOffset);
			b.x = Mathf.Abs(b.x);
			b.y = Mathf.Abs(b.y);
			b.z = Mathf.Abs(b.z);
			Vector3 extents = bounds.extents + b;
			return FixMesh(originalMesh, extents);
		}

		public static Mesh FixMesh(Mesh originalMesh, Vector3 extents)
		{
			Bounds bounds = new Bounds(originalMesh.bounds.center, extents * 2f);
			Mesh mesh = BoundsToMesh(bounds);
			CombineInstance combineInstance = default(CombineInstance);
			combineInstance.mesh = mesh;
			combineInstance.subMeshIndex = 0;
			combineInstance.transform = Matrix4x4.identity;
			CombineInstance[] array = new CombineInstance[originalMesh.subMeshCount];
			for (int i = 0; i < originalMesh.subMeshCount; i++)
			{
				CombineInstance combineInstance2 = default(CombineInstance);
				combineInstance2.mesh = Subdivider.ExtractSubmesh(originalMesh, i);
				combineInstance2.transform = Matrix4x4.identity;
				CombineInstance[] combine = new CombineInstance[2]
				{
					combineInstance,
					combineInstance2
				};
				Mesh mesh2 = new Mesh();
				mesh2.CombineMeshes(combine, mergeSubMeshes: true);
				CombineInstance combineInstance3 = default(CombineInstance);
				combineInstance3.mesh = mesh2;
				combineInstance3.transform = Matrix4x4.identity;
				combineInstance3.subMeshIndex = 0;
				array[i] = combineInstance3;
			}
			Mesh mesh3 = new Mesh();
			mesh3.CombineMeshes(array, mergeSubMeshes: false);
			return mesh3;
		}

		public static Mesh FixBounds(Mesh originalMesh, Transform transform, Vector3 hbOffset)
		{
			Vector3 b = transform.InverseTransformVector(hbOffset);
			b.x = Mathf.Abs(b.x);
			b.y = Mathf.Abs(b.y);
			b.z = Mathf.Abs(b.z);
			Vector3 extents = originalMesh.bounds.extents + b;
			return FixBounds(originalMesh, extents);
		}

		public static Mesh FixBounds(Mesh originalMesh, Vector3 extents)
		{
			Bounds bounds = originalMesh.bounds;
			Mesh mesh = UnityEngine.Object.Instantiate(originalMesh);
			mesh.bounds = new Bounds(bounds.center, extents * 2f);
			return mesh;
		}

		public static Mesh BoundsToMesh(Bounds bounds)
		{
			Vector3 extents = bounds.extents;
			Vector3 center = bounds.center;
			List<Vector3> list = new List<Vector3>();
			list.Add(center + new Vector3(0f - extents.x, 0f - extents.y, 0f - extents.z));
			list.Add(center + new Vector3(0f - extents.x, 0f - extents.y, 0f - extents.z));
			list.Add(center + new Vector3(0f - extents.x, 0f - extents.y, 0f - extents.z));
			list.Add(center + new Vector3(0f - extents.x, 0f - extents.y, extents.z));
			list.Add(center + new Vector3(0f - extents.x, 0f - extents.y, extents.z));
			list.Add(center + new Vector3(0f - extents.x, 0f - extents.y, extents.z));
			list.Add(center + new Vector3(0f - extents.x, extents.y, 0f - extents.z));
			list.Add(center + new Vector3(0f - extents.x, extents.y, 0f - extents.z));
			list.Add(center + new Vector3(0f - extents.x, extents.y, 0f - extents.z));
			list.Add(center + new Vector3(0f - extents.x, extents.y, extents.z));
			list.Add(center + new Vector3(0f - extents.x, extents.y, extents.z));
			list.Add(center + new Vector3(0f - extents.x, extents.y, extents.z));
			list.Add(center + new Vector3(extents.x, 0f - extents.y, 0f - extents.z));
			list.Add(center + new Vector3(extents.x, 0f - extents.y, 0f - extents.z));
			list.Add(center + new Vector3(extents.x, 0f - extents.y, 0f - extents.z));
			list.Add(center + new Vector3(extents.x, 0f - extents.y, extents.z));
			list.Add(center + new Vector3(extents.x, 0f - extents.y, extents.z));
			list.Add(center + new Vector3(extents.x, 0f - extents.y, extents.z));
			list.Add(center + new Vector3(extents.x, extents.y, 0f - extents.z));
			list.Add(center + new Vector3(extents.x, extents.y, 0f - extents.z));
			list.Add(center + new Vector3(extents.x, extents.y, 0f - extents.z));
			list.Add(center + new Vector3(extents.x, extents.y, extents.z));
			list.Add(center + new Vector3(extents.x, extents.y, extents.z));
			list.Add(center + new Vector3(extents.x, extents.y, extents.z));
			Mesh mesh = new Mesh();
			mesh.SetVertices(list);
			mesh.triangles = new int[24]
			{
				0,
				1,
				2,
				3,
				4,
				5,
				6,
				7,
				8,
				9,
				10,
				11,
				12,
				13,
				14,
				15,
				16,
				17,
				18,
				19,
				20,
				21,
				22,
				23
			};
			return mesh;
		}
	}
}
