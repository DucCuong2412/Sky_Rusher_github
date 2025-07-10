using System.Collections.Generic;
using UnityEngine;

public class StartRamp : SingletonMB<StartRamp>
{
	public GameObject m_ArrowPrefab;

	public int m_ArrowCount;

	public float m_ZStart;

	public float m_ZOffset;

	public Transform m_LeftPoint;

	public Transform m_RightPoint;

	private List<MeshRenderer> m_LeftRamps;

	private List<MeshRenderer> m_RightRamps;

	private Color m_ZoningColor;

	private Vector3 m_PosBuffer;

	public void SetColor(Color _Color)
	{
		m_ZoningColor = _Color;
	}

	private void Awake()
	{
		m_LeftRamps = new List<MeshRenderer>();
		m_RightRamps = new List<MeshRenderer>();
		Quaternion rotation = m_ArrowPrefab.transform.rotation;
		m_PosBuffer = Vector3.zero;
		for (int i = 0; i < m_ArrowCount; i++)
		{
			float z = m_ZStart + (float)i * m_ZOffset;
			m_PosBuffer = m_LeftPoint.position;
			m_PosBuffer.z = z;
			GameObject gameObject = Object.Instantiate(m_ArrowPrefab, m_PosBuffer, rotation);
			m_LeftRamps.Add(gameObject.GetComponent<MeshRenderer>());
			m_PosBuffer = m_RightPoint.position;
			m_PosBuffer.z = z;
			GameObject gameObject2 = Object.Instantiate(m_ArrowPrefab, m_PosBuffer, rotation);
			m_RightRamps.Add(gameObject2.GetComponent<MeshRenderer>());
		}
	}

	private void Update()
	{
		for (int i = 0; i < m_LeftRamps.Count; i++)
		{
			float f = Time.time * 10f - (float)i;
			m_ZoningColor.a = Mathf.Lerp(0f, 1f, Mathf.Sin(f));
			m_LeftRamps[i].material.color = m_ZoningColor;
			m_LeftRamps[i].material.SetColor("_EmissionColor", m_ZoningColor);
			m_RightRamps[i].material.color = m_ZoningColor;
			m_RightRamps[i].material.SetColor("_EmissionColor", m_ZoningColor);
		}
	}
}
