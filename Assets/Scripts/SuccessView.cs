using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SuccessView : SingletonMB<SuccessView>
{
	private const float c_PlayAnimDuration = 0.5f;

	public Button m_NextButton;

	public Text m_ClearText;

	public Text m_LevelText;

	public Image m_RewardImage;

	public Text m_RewardText;

	public int m_ChallengeReward;

	private Coroutine m_AppearCoroutine;

	private int m_Reward;

	private int m_SavedLevel;

	private CanvasGroup m_Group;

	private GameManager m_GameManager;

	private void Awake()
	{
		m_Group = GetComponent<CanvasGroup>();
		m_GameManager = SingletonMB<GameManager>.Instance;
		m_GameManager.onGamePhaseChanged += OnGamePhaseChanged;
	}

	private void OnGamePhaseChanged(GamePhase _Phase)
	{
		switch (_Phase)
		{
		case GamePhase.GAME:
		case GamePhase.SAVE_ME:
		case GamePhase.CONTINUE_GAME:
			break;
		case GamePhase.MAIN_MENU:
			if (m_AppearCoroutine != null)
			{
				m_GameManager.AddCurrency(m_Reward);
				StopCoroutine(m_AppearCoroutine);
			}
			m_Reward = 0;
			Enable(_Enable: false);
			break;
		case GamePhase.INTRO:
			m_SavedLevel = m_GameManager.GetLevel();
			Enable(_Enable: false);
			break;
		case GamePhase.FAILED:
			Enable(_Enable: false);
			break;
		case GamePhase.SUCCESS:
			if (!m_GameManager.isChallenge)
			{
				m_LevelText.text = "LEVEL " + m_SavedLevel.ToString();
				m_Reward = 20;
			}
			else
			{
				m_LevelText.text = "CHALLENGE";
				m_Reward = m_ChallengeReward;
			}
			m_RewardText.text = "x" + m_Reward.ToString();
			m_AppearCoroutine = StartCoroutine(Appear());
			break;
		}
	}

	private void Enable(bool _Enable)
	{
		m_Group.alpha = ((!_Enable) ? 0f : 1f);
		m_Group.interactable = _Enable;
		m_Group.blocksRaycasts = _Enable;
		m_NextButton.interactable = _Enable;
	}

	private IEnumerator Appear()
	{
		float time = 0f;
		while (time < 1f)
		{
			time += Time.deltaTime / 0.5f;
			m_Group.alpha = Mathf.Lerp(0f, 1f, time);
			yield return null;
		}
		Enable(_Enable: true);
		yield return new WaitForSeconds(0.5f);
		while (m_Reward > 0)
		{
			m_Reward--;
			m_RewardText.text = "x" + m_Reward.ToString();
			m_GameManager.AddCurrency(1);
			yield return new WaitForSeconds(0.05f);
		}
	}

	public void SetColor(Color _Color)
	{
		m_ClearText.color = _Color;
		m_LevelText.color = _Color;
	}

	public void OnNextButton()
	{
		if (!m_GameManager.isChallenge)
		{
			//if (VoodooSauce.GetPlayerCohort() == "BonusStage")
			{
				m_GameManager.CheckLevel();
			}
			//else
			//{
			//	m_GameManager.GoToNextLevel();
			//}
		}
		m_GameManager.ChangePhase(GamePhase.MAIN_MENU);
	}
}
