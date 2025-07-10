using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeView : SingletonMB<ChallengeView>
{
	private const float c_PlayAnimDuration = 0.5f;

	public Transform m_Content;

	public Text m_Title;

	public Image[] m_Arrow;

	private CanvasGroup m_Group;

	private GameManager m_GameManager;

	private ChallengeData m_SelectedChallenge;

	private ChallengeManager m_ChallengeManager;

	private MainMenuView m_MainMenuView;

	private void Awake()
	{
		m_Group = GetComponent<CanvasGroup>();
		m_GameManager = SingletonMB<GameManager>.Instance;
		m_ChallengeManager = SingletonMB<ChallengeManager>.Instance;
		m_MainMenuView = SingletonMB<MainMenuView>.Instance;
		Enable(_Enable: false);
		m_GameManager.onGamePhaseChanged += OnGamePhaseChanged;
	}

	private void OnGamePhaseChanged(GamePhase _Phase)
	{
		switch (_Phase)
		{
		case GamePhase.MAIN_MENU:
			if (m_GameManager.isChallenge)
			{
				Enable(_Enable: true);
			}
			break;
		case GamePhase.INTRO:
			if (m_GameManager.isChallenge)
			{
				Enable(_Enable: false);
			}
			break;
		}
	}

	public void Enable(bool _Enable)
	{
		m_Group.alpha = ((!_Enable) ? 0f : 1f);
		m_Group.interactable = _Enable;
		m_Group.blocksRaycasts = _Enable;
	}

	public void ReturnMainMenu()
	{
		m_GameManager.isChallenge = false;
		Enable(_Enable: false);
		m_MainMenuView.ReturnToMainMenu();
		m_GameManager.ChangePhase(GamePhase.MAIN_MENU);
	}

	public void GotoChallengeView()
	{
		m_GameManager.isChallenge = true;
		StartCoroutine(Appear());
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
		yield return null;
	}

	public void OnChallengeSelected(ChallengeData _ChallengeData)
	{
		m_ChallengeManager.OnChallengeSelected(_ChallengeData);
	}

	public void SetColor(Color _color)
	{
		m_Title.color = _color;
		for (int i = 0; i < m_Arrow.Length; i++)
		{
			m_Arrow[i].color = _color;
		}
	}
}
