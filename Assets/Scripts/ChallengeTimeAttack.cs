using UnityEngine;

[CreateAssetMenu(fileName = "Challenge", menuName = "SkyRusher/Challenge/TimeAttack", order = 1)]
public sealed class ChallengeTimeAttack : ChallengeData
{
	public float m_TimeToBeat;

	public float m_Distance;

	public string m_PatternFolder;

	public float m_SpeedMultiplicator;

	public override void Init()
	{
		base.Init();
		SingletonMB<GameManager>.Instance.totalDistance = m_Distance;
		SingletonMB<GameManager>.Instance.SetTimeToBeat(_HasTime: true, m_TimeToBeat);
		SingletonMB<Generator>.Instance.ChangePatternPath(m_PatternFolder);
		SingletonMB<Player>.Instance.ChangePlayerSpeed(m_SpeedMultiplicator);
		SingletonMB<GameManager>.Instance.ChangePhase(GamePhase.INTRO);
		SingletonMB<SuccessView>.Instance.m_ChallengeReward = m_Reward;
	}

	public override void Success()
	{
		base.Success();
	}

	public override void Failed()
	{
		base.Failed();
	}

	public override void Reset()
	{
		base.Reset();
		SingletonMB<GameManager>.Instance.SetTimeToBeat(_HasTime: false);
	}
}
