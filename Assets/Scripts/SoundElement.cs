using System;
using UnityEngine;

[Serializable]
public class SoundElement
{
	public ESoundType m_SoundType;

	public AudioClip m_Sound;

	[Range(0f, 1f)]
	public float m_Volume;

	public bool m_Loop;
}
