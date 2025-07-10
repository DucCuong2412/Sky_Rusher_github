using System.Collections;
using UnityEngine;

public class LevelView : SingletonMB<LevelView>
{
	private const float c_LaunchDuration = 0.6f;

	public TextMesh m_Level;

	private GameManager m_GameManager;

	private Color m_ColorBuffer;

	private void Awake()
	{
		m_GameManager = SingletonMB<GameManager>.Instance;
		m_ColorBuffer = m_Level.color;
		m_GameManager.onGamePhaseChanged += OnGamePhaseChanged;
	}

	public void SetColor(Color _Color)
	{
		m_Level.color = _Color;
		m_ColorBuffer = _Color;
	}

	private void OnGamePhaseChanged(GamePhase _Phase)
	{
		switch (_Phase)
		{
		case GamePhase.MAIN_MENU:
			m_Level.text = string.Empty;
			break;
		case GamePhase.INTRO:
			if (m_GameManager.isChallenge)
			{
				StartCoroutine(Launch("Challenge"));
			}
			else if (m_GameManager.m_IsBonusStage)
			{
				StartCoroutine(Launch("Bonus Stage"));
			}
			else
			{
				StartCoroutine(Launch("Level " + m_GameManager.GetLevel().ToString()));
			}
			break;
		}
	}

	private IEnumerator Launch(string _Text)
	{
		for (int i = 0; i <= _Text.Length; i++)
		{
			m_Level.text = _Text.Substring(0, i);
			yield return new WaitForSeconds(0.04f);
		}
		float randRem;
		for (float remTime = 0.6f; remTime > 0f; remTime -= randRem)
		{
			randRem = Random.Range(0.01f, 0.05f);
			m_ColorBuffer.a = Random.Range(0.3f, 1f);
			m_Level.color = m_ColorBuffer;
			yield return new WaitForSeconds(randRem);
		}
		m_Level.text = "GO !";
	}
}
