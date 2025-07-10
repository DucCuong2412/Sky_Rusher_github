using UnityEngine;

[CreateAssetMenu(fileName = "Challenge", menuName = "SkyRusher/Challenge/Survival", order = 1)]
public class ChallengeSurvival : ChallengeData
{
	public float m_TimeToSurvive;

	public float m_Distance;

	public string m_PatternFolder;

	public float m_SpeedMultiplicator;

	public override void Init()
	{
		base.Init();
		SingletonMB<GameManager>.Instance.SetTimeToSurvive(_HasTime: true, m_TimeToSurvive);
		SingletonMB<GameManager>.Instance.totalDistance = m_Distance;
		SingletonMB<Player>.Instance.ChangePlayerSpeed(m_SpeedMultiplicator);
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
		UnityEngine.Debug.Log("ICI");
		SingletonMB<GameManager>.Instance.SetTimeToSurvive(_HasTime: false);
	}
}
