using System;
using UnityEngine;

namespace Battlehub.HorizonBending
{
	public class TransformToHash
	{
		private int m_hashCode;

		private Vector3 m_r;

		private Vector3 m_s;

		public TransformToHash(Transform transform)
		{
			m_r = transform.rotation.eulerAngles;
			m_s = transform.localScale;
			m_hashCode = new
			{
				Rx = Math.Round(m_r.x, 4),
				Ry = Math.Round(m_r.y, 4),
				Rz = Math.Round(m_r.z, 4),
				Sx = Math.Round(m_s.x, 4),
				Sy = Math.Round(m_s.y, 4),
				Sz = Math.Round(m_s.z, 4)
			}.GetHashCode();
		}

		public override int GetHashCode()
		{
			return m_hashCode;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}
			TransformToHash transformToHash = (TransformToHash)obj;
			return transformToHash.m_s == m_s && transformToHash.m_r == m_r;
		}
	}
}
