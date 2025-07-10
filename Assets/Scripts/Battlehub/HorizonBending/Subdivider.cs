using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battlehub.HorizonBending
{
	public class Subdivider : MonoBehaviour
	{
		private static List<Vector3> vertices;

		private static List<Vector3> normals;

		private static List<Vector4> tangents;

		private static List<Color> colors;

		private static List<Vector2> uv;

		private static List<Vector2> uv2;

		private static List<Vector2> uv3;

		private static List<Vector2> uv4;

		private static List<int> indices;

		private static Dictionary<uint, int> newVectices;

		private static void InitArrays(Mesh mesh)
		{
			vertices = new List<Vector3>(mesh.vertices);
			normals = new List<Vector3>(mesh.normals);
			tangents = new List<Vector4>(mesh.tangents);
			colors = new List<Color>(mesh.colors);
			uv = new List<Vector2>(mesh.uv);
			uv2 = new List<Vector2>(mesh.uv2);
			uv3 = new List<Vector2>(mesh.uv3);
			uv4 = new List<Vector2>(mesh.uv4);
			indices = new List<int>();
		}

		private static void CleanUp()
		{
			vertices = null;
			normals = null;
			colors = null;
			uv = null;
			uv2 = null;
			uv3 = null;
			uv4 = null;
			indices = null;
		}

		private static int GetNewVertex4(int i1, int i2)
		{
			int count = vertices.Count;
			uint key = (uint)((i1 << 16) | i2);
			uint key2 = (uint)((i2 << 16) | i1);
			if (newVectices.ContainsKey(key2))
			{
				return newVectices[key2];
			}
			if (newVectices.ContainsKey(key))
			{
				return newVectices[key];
			}
			newVectices.Add(key, count);
			vertices.Add((vertices[i1] + vertices[i2]) * 0.5f);
			if (normals.Count > 0)
			{
				normals.Add((normals[i1] + normals[i2]).normalized);
			}
			if (tangents.Count > 0)
			{
				Vector4 normalized = Vector4.Lerp(tangents[i1], tangents[i2], 0.5f).normalized;
				Vector4 vector = tangents[i1];
				float w = vector.w;
				Vector4 vector2 = tangents[i2];
				normalized.w = Mathf.Lerp(w, vector2.w, 0.5f);
				tangents.Add(normalized);
			}
			if (colors.Count > 0)
			{
				colors.Add((colors[i1] + colors[i2]) * 0.5f);
			}
			if (uv.Count > 0)
			{
				uv.Add((uv[i1] + uv[i2]) * 0.5f);
			}
			if (uv2.Count > 0)
			{
				uv2.Add((uv2[i1] + uv2[i2]) * 0.5f);
			}
			if (uv3.Count > 0)
			{
				uv3.Add((uv3[i1] + uv3[i2]) * 0.5f);
			}
			if (uv4.Count > 0)
			{
				uv4.Add((uv4[i1] + uv4[i2]) * 0.5f);
			}
			return count;
		}

		private static void Subdivide4Submesh(Mesh mesh)
		{
			newVectices = new Dictionary<uint, int>();
			InitArrays(mesh);
			int[] triangles = mesh.triangles;
			for (int i = 0; i < triangles.Length; i += 3)
			{
				int num = triangles[i];
				int num2 = triangles[i + 1];
				int num3 = triangles[i + 2];
				int newVertex = GetNewVertex4(num, num2);
				int newVertex2 = GetNewVertex4(num2, num3);
				int newVertex3 = GetNewVertex4(num3, num);
				indices.Add(num);
				indices.Add(newVertex);
				indices.Add(newVertex3);
				indices.Add(num2);
				indices.Add(newVertex2);
				indices.Add(newVertex);
				indices.Add(num3);
				indices.Add(newVertex3);
				indices.Add(newVertex2);
				indices.Add(newVertex);
				indices.Add(newVertex2);
				indices.Add(newVertex3);
			}
			mesh.vertices = vertices.ToArray();
			if (normals.Count > 0)
			{
				mesh.normals = normals.ToArray();
			}
			if (tangents.Count > 0)
			{
				mesh.tangents = tangents.ToArray();
			}
			if (colors.Count > 0)
			{
				mesh.colors = colors.ToArray();
			}
			if (uv.Count > 0)
			{
				mesh.uv = uv.ToArray();
			}
			if (uv2.Count > 0)
			{
				mesh.uv2 = uv2.ToArray();
			}
			if (uv3.Count > 0)
			{
				mesh.uv3 = uv3.ToArray();
			}
			if (uv4.Count > 0)
			{
				mesh.uv4 = uv4.ToArray();
			}
			mesh.triangles = indices.ToArray();
			CleanUp();
		}

		private static int GetNewVertex9(int i1, int i2, int i3)
		{
			int count = vertices.Count;
			if (i3 == i1 || i3 == i2)
			{
				uint key = (uint)((i1 << 16) | i2);
				if (newVectices.ContainsKey(key))
				{
					return newVectices[key];
				}
				newVectices.Add(key, count);
			}
			vertices.Add((vertices[i1] + vertices[i2] + vertices[i3]) / 3f);
			if (normals.Count > 0)
			{
				normals.Add((normals[i1] + normals[i2] + normals[i3]).normalized);
			}
			if (tangents.Count > 0)
			{
				Vector4 normalized = Vector4.Lerp(Vector4.Lerp(tangents[i1], tangents[i2], 0.5f), tangents[i3], 0.5f).normalized;
				Vector4 vector = tangents[i1];
				float w = vector.w;
				Vector4 vector2 = tangents[i2];
				float num = w + vector2.w;
				Vector4 vector3 = tangents[i3];
				normalized.w = (num + vector3.w) / 3f;
				tangents.Add(normalized);
			}
			if (colors.Count > 0)
			{
				colors.Add((colors[i1] + colors[i2] + colors[i3]) / 3f);
			}
			if (uv.Count > 0)
			{
				uv.Add((uv[i1] + uv[i2] + uv[i3]) / 3f);
			}
			if (uv2.Count > 0)
			{
				uv2.Add((uv2[i1] + uv2[i2] + uv2[i3]) / 3f);
			}
			if (uv3.Count > 0)
			{
				uv3.Add((uv3[i1] + uv3[i2] + uv3[i3]) / 3f);
			}
			if (uv4.Count > 0)
			{
				uv4.Add((uv4[i1] + uv4[i2] + uv4[i3]) / 3f);
			}
			return count;
		}

		private static void Subdivide9Submesh(Mesh mesh)
		{
			newVectices = new Dictionary<uint, int>();
			InitArrays(mesh);
			int[] triangles = mesh.triangles;
			for (int i = 0; i < triangles.Length; i += 3)
			{
				int num = triangles[i];
				int num2 = triangles[i + 1];
				int num3 = triangles[i + 2];
				int newVertex = GetNewVertex9(num, num2, num);
				int newVertex2 = GetNewVertex9(num2, num, num2);
				int newVertex3 = GetNewVertex9(num2, num3, num2);
				int newVertex4 = GetNewVertex9(num3, num2, num3);
				int newVertex5 = GetNewVertex9(num3, num, num3);
				int newVertex6 = GetNewVertex9(num, num3, num);
				int newVertex7 = GetNewVertex9(num, num2, num3);
				indices.Add(num);
				indices.Add(newVertex);
				indices.Add(newVertex6);
				indices.Add(num2);
				indices.Add(newVertex3);
				indices.Add(newVertex2);
				indices.Add(num3);
				indices.Add(newVertex5);
				indices.Add(newVertex4);
				indices.Add(newVertex7);
				indices.Add(newVertex);
				indices.Add(newVertex2);
				indices.Add(newVertex7);
				indices.Add(newVertex3);
				indices.Add(newVertex4);
				indices.Add(newVertex7);
				indices.Add(newVertex5);
				indices.Add(newVertex6);
				indices.Add(newVertex7);
				indices.Add(newVertex6);
				indices.Add(newVertex);
				indices.Add(newVertex7);
				indices.Add(newVertex2);
				indices.Add(newVertex3);
				indices.Add(newVertex7);
				indices.Add(newVertex4);
				indices.Add(newVertex5);
			}
			mesh.vertices = vertices.ToArray();
			if (normals.Count > 0)
			{
				mesh.normals = normals.ToArray();
			}
			if (tangents.Count > 0)
			{
				mesh.tangents = tangents.ToArray();
			}
			if (colors.Count > 0)
			{
				mesh.colors = colors.ToArray();
			}
			if (uv.Count > 0)
			{
				mesh.uv = uv.ToArray();
			}
			if (uv2.Count > 0)
			{
				mesh.uv2 = uv2.ToArray();
			}
			if (uv3.Count > 0)
			{
				mesh.uv3 = uv3.ToArray();
			}
			if (uv4.Count > 0)
			{
				mesh.uv4 = uv4.ToArray();
			}
			mesh.triangles = indices.ToArray();
			CleanUp();
		}

		private static void SubdivideSubmesh(Mesh mesh, int level)
		{
			if (level < 2)
			{
				return;
			}
			while (level > 1)
			{
				while (level % 3 == 0)
				{
					Subdivide9Submesh(mesh);
					level /= 3;
				}
				while (level % 2 == 0)
				{
					Subdivide4Submesh(mesh);
					level /= 2;
				}
				if (level > 3)
				{
					level++;
				}
			}
		}

		private static Mesh Subdivide(Mesh originalMesh, Action<Mesh> subdivideAction)
		{
			if (originalMesh.subMeshCount == 1)
			{
				Mesh mesh = DuplicateMesh(originalMesh);
				subdivideAction(mesh);
				return mesh;
			}
			CombineInstance[] array = new CombineInstance[originalMesh.subMeshCount];
			for (int i = 0; i < originalMesh.subMeshCount; i++)
			{
				CombineInstance combineInstance = default(CombineInstance);
				Mesh mesh2 = ExtractSubmesh(originalMesh, i);
				subdivideAction(mesh2);
				combineInstance.mesh = mesh2;
				combineInstance.transform = Matrix4x4.identity;
				combineInstance.subMeshIndex = 0;
				array[i] = combineInstance;
			}
			Mesh mesh3 = new Mesh();
			mesh3.CombineMeshes(array, mergeSubMeshes: false);
			return mesh3;
		}

		public static Mesh Subdivide4(Mesh originalMesh)
		{
			return Subdivide(originalMesh, delegate(Mesh m)
			{
				Subdivide4Submesh(m);
			});
		}

		public static Mesh Subdivide9(Mesh originalMesh)
		{
			return Subdivide(originalMesh, delegate(Mesh m)
			{
				Subdivide9Submesh(m);
			});
		}

		public static Mesh Subdivide(Mesh originalMesh, int level)
		{
			return Subdivide(originalMesh, delegate(Mesh m)
			{
				SubdivideSubmesh(m, level);
			});
		}

		public static Mesh DuplicateMesh(Mesh mesh)
		{
			return UnityEngine.Object.Instantiate(mesh);
		}

		public static Mesh ExtractSubmesh(Mesh mesh, int submeshIndex)
		{
			MeshTopology topology = mesh.GetTopology(submeshIndex);
			if (topology != 0)
			{
				UnityEngine.Debug.LogWarningFormat("Extract Submesh method could handle triangle topology only. Current topology is {0}. Mesh name {1} submeshIndex {2}", topology, mesh, submeshIndex);
				return mesh;
			}
			int[] triangles = mesh.GetTriangles(submeshIndex);
			int[] array = new int[triangles.Length];
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			int num = 0;
			for (int i = 0; i < triangles.Length; i++)
			{
				int key = triangles[i];
				if (!dictionary.ContainsKey(key))
				{
					array[i] = num;
					dictionary.Add(key, num);
					num++;
				}
				else
				{
					array[i] = dictionary[key];
				}
			}
			Vector3[] array2 = mesh.vertices;
			Vector3[] array3 = new Vector3[num];
			foreach (KeyValuePair<int, int> item in dictionary)
			{
				array3[item.Value] = array2[item.Key];
			}
			Mesh mesh2 = new Mesh();
			mesh2.vertices = array3;
			Color[] array4 = mesh.colors;
			if (array4.Length == array2.Length)
			{
				Color[] array5 = new Color[num];
				foreach (KeyValuePair<int, int> item2 in dictionary)
				{
					array5[item2.Value] = array4[item2.Key];
				}
				mesh2.colors = array5;
			}
			else if (array4.Length != 0)
			{
				UnityEngine.Debug.LogWarning("colors.Length != vertices.Length");
			}
			Color32[] colors = mesh.colors32;
			if (colors.Length == array2.Length)
			{
				Color32[] array6 = new Color32[num];
				foreach (KeyValuePair<int, int> item3 in dictionary)
				{
					array6[item3.Value] = colors[item3.Key];
				}
				mesh2.colors32 = array6;
			}
			else if (colors.Length != 0)
			{
				UnityEngine.Debug.LogWarning("colors32.Length != vertices.Length");
			}
			BoneWeight[] boneWeights = mesh.boneWeights;
			if (boneWeights.Length == array2.Length)
			{
				BoneWeight[] array7 = new BoneWeight[num];
				foreach (KeyValuePair<int, int> item4 in dictionary)
				{
					array7[item4.Value] = boneWeights[item4.Key];
				}
				mesh2.boneWeights = array7;
			}
			else if (boneWeights.Length != 0)
			{
				UnityEngine.Debug.LogWarning("boneWeights.Length != vertices.Length");
			}
			Vector3[] array8 = mesh.normals;
			if (array8.Length == array2.Length)
			{
				Vector3[] array9 = new Vector3[num];
				foreach (KeyValuePair<int, int> item5 in dictionary)
				{
					array9[item5.Value] = array8[item5.Key];
				}
				mesh2.normals = array9;
			}
			else if (array8.Length != 0)
			{
				UnityEngine.Debug.LogWarning("normals.Length != vertices.Length");
			}
			Vector4[] array10 = mesh.tangents;
			if (array10.Length == array2.Length)
			{
				Vector4[] array11 = new Vector4[num];
				foreach (KeyValuePair<int, int> item6 in dictionary)
				{
					array11[item6.Value] = array10[item6.Key];
				}
				mesh2.tangents = array11;
			}
			else if (array10.Length != 0)
			{
				UnityEngine.Debug.LogWarning("tangents.Length != vertices.Length");
			}
			Vector2[] array12 = mesh.uv;
			if (array12.Length == array2.Length)
			{
				Vector2[] array13 = new Vector2[num];
				foreach (KeyValuePair<int, int> item7 in dictionary)
				{
					array13[item7.Value] = array12[item7.Key];
				}
				mesh2.uv = array13;
			}
			else if (array12.Length != 0)
			{
				UnityEngine.Debug.LogWarning("uv.Length != vertices.Length");
			}
			Vector2[] array14 = mesh.uv2;
			if (array14.Length == array2.Length)
			{
				Vector2[] array15 = new Vector2[num];
				foreach (KeyValuePair<int, int> item8 in dictionary)
				{
					array15[item8.Value] = array14[item8.Key];
				}
				mesh2.uv2 = array15;
			}
			else if (array14.Length != 0)
			{
				UnityEngine.Debug.LogWarning("uv2.Length != vertices.Length");
			}
			Vector2[] array16 = mesh.uv3;
			if (array16.Length == array2.Length)
			{
				Vector2[] array17 = new Vector2[num];
				foreach (KeyValuePair<int, int> item9 in dictionary)
				{
					array17[item9.Value] = array16[item9.Key];
				}
				mesh2.uv3 = array17;
			}
			else if (array16.Length != 0)
			{
				UnityEngine.Debug.LogWarning("uv3.Length != vertices.Length");
			}
			Vector2[] array18 = mesh.uv4;
			if (array18.Length == array2.Length)
			{
				Vector2[] array19 = new Vector2[num];
				foreach (KeyValuePair<int, int> item10 in dictionary)
				{
					array19[item10.Value] = array18[item10.Key];
				}
				mesh2.uv4 = array19;
			}
			else if (array18.Length != 0)
			{
				UnityEngine.Debug.LogWarning("uv4.Length != vertices.Length");
			}
			mesh2.triangles = array;
			return mesh2;
		}
	}
}
