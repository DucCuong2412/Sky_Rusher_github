using UnityEngine;

namespace Battlehub.HorizonBending
{
	[ExecuteInEditMode]
	public class HBFixBounds : MonoBehaviour
	{
		[HideInInspector]
		public MeshFilter MeshFilter;

		[HideInInspector]
		public Mesh OriginalMesh;

		[HideInInspector]
		public SkinnedMeshRenderer SkinnedMesh;

		[HideInInspector]
		public Bounds OriginalSkinnedAABB;

		[HideInInspector]
		public bool IsMeshFixed;

		[HideInInspector]
		public bool IsBoundsFixed;

		private MeshRenderer m_renderer;

		public float Curvature = 5f;

		public float Flatten;

		public float HorizonZOffset;

		public float HorizonYOffset;

		public float HorizonXOffset;

		public float FixBoundsRadius;

		public BendingMode BendingMode;

		public bool OverrideBounds;

		public Vector3 SkinnedBounds;

		public Vector3 Bounds;

		public bool Lock;

		public Vector3 HBOffset()
		{
			HBSettings settings = new HBSettings(BendingMode, Curvature * 0.001f, Flatten, HorizonXOffset, HorizonYOffset, HorizonZOffset);
			return HBUtils.GetOffset(settings, FixBoundsRadius);
		}

		private void Awake()
		{
			if (MeshFilter == null)
			{
				MeshFilter = GetComponent<MeshFilter>();
				if (MeshFilter != null)
				{
					OriginalMesh = MeshFilter.sharedMesh;
					Bounds = OriginalMesh.bounds.extents;
				}
			}
			if (SkinnedMesh == null)
			{
				SkinnedMesh = GetComponent<SkinnedMeshRenderer>();
				if (SkinnedMesh != null)
				{
					OriginalSkinnedAABB = SkinnedMesh.localBounds;
					SkinnedBounds = OriginalSkinnedAABB.extents;
				}
			}
			if (Application.isPlaying && SkinnedMesh != null)
			{
				FixBoundsSkinned();
			}
		}

		private void FixBoundsSkinned()
		{
			if (OverrideBounds)
			{
				HBUtils.FixSkinned(SkinnedMesh, OriginalSkinnedAABB, SkinnedBounds);
			}
			else
			{
				HBUtils.FixSkinned(SkinnedMesh, OriginalSkinnedAABB, base.transform, HBOffset());
			}
		}
	}
}
