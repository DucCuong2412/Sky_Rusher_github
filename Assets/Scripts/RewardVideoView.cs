using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RewardVideoView : SingletonMB<RewardVideoView>
{
	private const float c_PlayAnimDuration = 0.5f;

	public Button m_RewardButton;

	public Button m_NoneRewardButton;

	public Image m_LoadingBar;

	public Image m_VideoImg;

	public Text m_SaveMeText;

	public Text m_TapToContinue;

	private CanvasGroup m_Group;

	private GameManager m_GameManager;

	private InfoView m_InfoView;

	private float m_Timer;

	private void Awake()
	{
		m_Group = GetComponent<CanvasGroup>();
		m_GameManager = SingletonMB<GameManager>.Instance;
		m_InfoView = SingletonMB<InfoView>.Instance;
		m_GameManager.onGamePhaseChanged += OnGamePhaseChanged;
	}

	private void Start()
	{
		Enable(_Enable: false);
	}

	private void Update()
	{
		if (m_GameManager.currentPhase == GamePhase.SAVE_ME)
		{
			if (m_Timer < 100f)
			{
				m_Timer += 20f * Time.deltaTime;
				m_LoadingBar.fillAmount = m_Timer / 100f;
			}
			else
			{
				m_GameManager.ChangePhase(GamePhase.FAILED);
			}
		}
	}

	private void OnGamePhaseChanged(GamePhase _Phase)
	{
		switch (_Phase)
		{
		case GamePhase.SAVE_ME:
			//if (VoodooSauce.IsRewardedVideoAvailable())
			{
				m_Timer = 0f;
				StartCoroutine(Appear());
			}
			//else
			//{
			//	m_GameManager.ChangePhase(GamePhase.FAILED);
			//}
			break;
		case GamePhase.CONTINUE_GAME:
			Enable(_Enable: false);
			break;
		case GamePhase.FAILED:
			Enable(_Enable: false);
			break;
		}
	}

	private void Enable(bool _Enable)
	{
		m_Group.alpha = ((!_Enable) ? 0f : 1f);
		m_Group.interactable = _Enable;
		m_Group.blocksRaycasts = _Enable;
		m_RewardButton.interactable = _Enable;
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
		m_LoadingBar.color = _Color;
		m_SaveMeText.color = _Color;
		m_VideoImg.color = _Color;
		m_TapToContinue.color = _Color;
	}

	public void OnSaveMeButton()
	{
		//VoodooSauce.ShowRewardedVideo(RewardVideoCallBack);
	}

	private void RewardVideoCallBack(bool _Looked)
	{
		if (_Looked)
		{
			m_GameManager.ChangePhase(GamePhase.CONTINUE_GAME);
		}
		else
		{
			m_GameManager.ChangePhase(GamePhase.FAILED);
		}
	}

	public void OnCancelButton()
	{
		m_GameManager.ChangePhase(GamePhase.FAILED);
	}
}
