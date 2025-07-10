using System.Collections;
using UnityEngine;

public class MainCamera : SingletonMB<MainCamera>
{
	private const float c_LaunchAnimDuration = 0.8f;

	private const float c_XLimit = 3f;

	private const float c_YLimit = 2.5f;

	private const float c_IntroY = -0.5f;

	private const float c_BaseY = 0.5f;

	private const float c_GameZOffset = 5f;

	private const float c_BrakeForce = 0.05f;

	public GameObject m_Speedlines;

	public GameObject m_SkinCameraView;

	public AnimationCurve m_SkinCameraAnim;

	private bool m_IsPlaying;

	private bool m_IsBraking;

	private Transform m_Transform;

	private Transform m_PlayerTr;

	private GameManager m_GameManager;

	private MainMenuView m_MainMenuView;

	private ScreenShaker m_ScreenShaker;

	private SkinManager m_SkinManager;

	private Vector3 m_PosBuffer;

	private float m_BrakeSpeed;

	private Vector3 m_CameraPos;

	private Quaternion m_CameraRot;

	private void Awake()
	{
		m_Transform = base.transform;
		m_GameManager = SingletonMB<GameManager>.Instance;
		m_MainMenuView = SingletonMB<MainMenuView>.Instance;
		m_ScreenShaker = GetComponent<ScreenShaker>();
		m_SkinManager = SingletonMB<SkinManager>.Instance;
		m_PosBuffer = Vector3.zero;
		m_SkinManager.onPlayerChanged += OnSkinChanged;
		m_GameManager.onGamePhaseChanged += OnGamePhaseChanged;
	}

	public void GoToMainView()
	{
		StartCoroutine(TransitionCam(m_SkinCameraView.transform, _ToSkinPos: false));
	}

	public void GoToSkinView()
	{
		m_CameraPos = m_Transform.position;
		m_CameraRot = m_Transform.rotation;
		StartCoroutine(TransitionCam(m_SkinCameraView.transform, _ToSkinPos: true));
	}

	private void OnGamePhaseChanged(GamePhase _Phase)
	{
		switch (_Phase)
		{
		case GamePhase.MAIN_MENU:
			m_IsPlaying = false;
			m_IsBraking = false;
			m_Speedlines.SetActive(value: false);
			break;
		case GamePhase.INTRO:
			StartCoroutine(Launch());
			break;
		case GamePhase.GAME:
			m_IsPlaying = true;
			m_Speedlines.SetActive(value: true);
			break;
		case GamePhase.SAVE_ME:
			Die();
			break;
		case GamePhase.CONTINUE_GAME:
			m_IsPlaying = true;
			m_Speedlines.SetActive(value: true);
			break;
		case GamePhase.SUCCESS:
			m_IsPlaying = false;
			m_BrakeSpeed = 50f;
			m_IsBraking = true;
			m_Speedlines.SetActive(value: false);
			break;
		case GamePhase.FAILED:
			Die();
			break;
		}
	}

	private void Die()
	{
		m_Speedlines.SetActive(value: false);
		m_ScreenShaker.Shake(0.5f, 0.3f);
		m_IsPlaying = false;
	}

	private void Update()
	{
		if (m_IsPlaying)
		{
			Move();
		}
		else if (m_IsBraking)
		{
			Brake();
		}
	}

	public void Init()
	{
		m_PlayerTr = SingletonMB<Player>.Instance.transform;
		ref Vector3 posBuffer = ref m_PosBuffer;
		Vector3 position = m_PlayerTr.position;
		posBuffer.Set(0f, -0.5f, position.z - 5f);
		m_Transform.position = m_PosBuffer;
	}

	private IEnumerator Launch()
	{
		float time = 0f;
		while (time < 1f)
		{
			time += Time.deltaTime / 0.8f;
			m_PosBuffer.y = Mathf.Lerp(-0.5f, 0.5f, time);
			m_Transform.position = m_PosBuffer;
			yield return null;
		}
	}

	private IEnumerator TransitionCam(Transform _CamTr, bool _ToSkinPos)
	{
		float time = 0f;
		while (time < 1f)
		{
			time += Time.deltaTime;
			float curveTime = m_SkinCameraAnim.Evaluate(time);
			if (_ToSkinPos)
			{
				m_Transform.position = Vector3.Lerp(m_CameraPos, _CamTr.position, curveTime);
				m_Transform.rotation = Quaternion.Lerp(m_CameraRot, _CamTr.rotation, curveTime);
			}
			else
			{
				m_Transform.position = Vector3.Lerp(_CamTr.position, m_CameraPos, curveTime);
				m_Transform.rotation = Quaternion.Lerp(_CamTr.rotation, m_CameraRot, curveTime);
			}
			yield return null;
		}
		if (!_ToSkinPos)
		{
			m_MainMenuView.ReturnToMainMenu();
		}
	}

	private void Move()
	{
		Vector3 position = m_PlayerTr.position;
		float x = position.x;
		Vector3 position2 = m_PlayerTr.position;
		float y = position2.y;
		ref Vector3 posBuffer = ref m_PosBuffer;
		float newX = (!(x < 0f)) ? (x / 3.5f * 3f) : (x / -3.5f * -3f);
		float newY = 0.5f + ((!(y < 0f)) ? (y / 3f * 2.5f) : (y / -3f * -2.5f));
		Vector3 position3 = m_PlayerTr.position;
		posBuffer.Set(newX, newY, position3.z - 5f);
		m_Transform.position = Vector3.Lerp(m_Transform.position, m_PosBuffer, 0.5f);
	}

	private void Brake()
	{
		m_BrakeSpeed = Mathf.Lerp(m_BrakeSpeed, 0f, 0.05f);
		m_PosBuffer.z += m_BrakeSpeed * Time.deltaTime;
		m_Transform.position = Vector3.Lerp(m_Transform.position, m_PosBuffer, 0.5f);
	}

	private void OnSkinChanged(Player _Player)
	{
		m_PlayerTr = _Player.transform;
	}
}
