public class BonusStage : SingletonMB<BonusStage>
{
	private float m_Distance = 500f;

	private string m_PatternFolder = "PatternsBonusStage";

	private float m_SpeedMultiplicator = 1.3f;

	public void Init()
	{
		SingletonMB<GameManager>.Instance.totalDistance = m_Distance;
		SingletonMB<Player>.Instance.ChangePlayerSpeed(m_SpeedMultiplicator);
		SingletonMB<Generator>.Instance.ChangePatternPath(m_PatternFolder);
		SingletonMB<SuccessView>.Instance.m_ChallengeReward = 0;
	}
}
