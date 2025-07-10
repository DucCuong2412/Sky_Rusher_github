using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FailedView : SingletonMB<FailedView>
{
	private const float c_PlayAnimDuration = 0.5f;

	public Button m_RetryButton;

	public Button m_HomeBUtton;

	public Image m_RetryImage;

	public Image m_HomeImage;

	public Text m_FailedText;

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
			Enable(_Enable: false);
			break;
		case GamePhase.INTRO:
			Enable(_Enable: false);
			break;
		case GamePhase.FAILED:
			StartCoroutine(Appear());
			if (m_GameManager.isChallenge)
			{
				m_FailedText.text = "CHALLENGE FAILED!";
			}
			else
			{
				m_FailedText.text = "LEVEL FAILED!";
			}
			break;
		case GamePhase.SUCCESS:
			Enable(_Enable: false);
			break;
		}
	}

	private void Enable(bool _Enable)
	{
		m_Group.alpha = ((!_Enable) ? 0f : 1f);
		m_Group.interactable = _Enable;
		m_Group.blocksRaycasts = _Enable;
		m_RetryButton.interactable = _Enable;
		m_HomeBUtton.interactable = _Enable;
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
	}

	public void SetColor(Color _Color)
	{
		m_RetryImage.color = _Color;
		m_FailedText.color = _Color;
		m_HomeImage.color = _Color;
	}

	public void OnRetryButton()
	{
		StartCoroutine(Replay());
	}

	public void OnHomeButton()
	{
		m_GameManager.ChangePhase(GamePhase.MAIN_MENU);
		if (m_GameManager.isChallenge)
		{
			SingletonMB<ChallengeManager>.Instance.ChallengeHasCompleted(_Won: false);
		}
	}

	private IEnumerator Replay()
	{
		yield return null;
		m_GameManager.ChangePhase(GamePhase.MAIN_MENU);
		if (m_GameManager.isChallenge)
		{
			SingletonMB<ChallengeManager>.Instance.RestartChallenge();
		}
		else
		{
			m_GameManager.ChangePhase(GamePhase.INTRO);
		}
	}
}
