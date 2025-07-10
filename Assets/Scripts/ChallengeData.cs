using UnityEngine;

public abstract class ChallengeData : ScriptableObject
{
	public string m_SaveId;

	public int m_Reward;

	public string m_DescriptionId;

	public virtual void Init()
	{
	}

	public virtual void Success()
	{
	}

	public virtual void Failed()
	{
	}

	public virtual void Reset()
	{
	}
}
