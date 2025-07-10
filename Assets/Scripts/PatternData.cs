using UnityEngine;

[CreateAssetMenu(fileName = "PatternData", menuName = "SkyRusher/Pattern", order = 1)]
public class PatternData : ScriptableObject
{
	public int m_MinLevel = -1;

	public int m_MaxLevel = -1;

	public float m_PatternLength = 50f;

	public GameObject m_Prefab;
}
