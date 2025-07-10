using UnityEngine;

[CreateAssetMenu(fileName = "Challenge", menuName = "SkyRusher/Challenge/SpeedAttack", order = 1)]
public class ChallengeNewSpeed : ChallengeData
{
	public float m_SpeedMultiplicator;

	public string m_PatternFolder;

	public float m_Distance;

	public override void Init()
	{
		base.Init();
		SingletonMB<GameManager>.Instance.totalDistance = m_Distance;
		SingletonMB<Player>.Instance.ChangePlayerSpeed(m_SpeedMultiplicator);
		SingletonMB<GameManager>.Instance.SetSpeedAttackChallenge(_Value: true);
		SingletonMB<Generator>.Instance.ChangePatternPath(m_PatternFolder);
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
		SingletonMB<GameManager>.Instance.SetSpeedAttackChallenge(_Value: false);
	}
}
