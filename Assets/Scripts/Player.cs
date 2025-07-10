using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SingletonMB<Player>
{
	private const float c_LaunchDuration = 1f;

	private const int c_MaxCollisions = 20;

	private const float c_XDragSpeed = 2.5f;

	private const float c_YDragSpeed = 1.5f;

	private const float c_XRotationSpeed = 1f;

	private const float c_YRotationSpeed = 1f;

	private const float c_PosSensibiliy = 0.1f;

	private const float c_HitMove = 0.05f;

	private const float m_SpeedMulti = 1f;

	private const float c_MaxRotOffset = 0.5f;

	private const float c_MaxZAngle = 45f;

	private const float c_MaxXAngle = 45f;

	private const float c_RotSensibiliy = 0.2f;

	public Color m_BaseColor;

	public Color m_ActiveColor;

	public AnimationCurve m_ReactorColorCurve;

	public GameObject m_Ship;

	public MeshRenderer[] m_ShipRenderer;

	public ParticleSystem m_DieEffect;

	public ParticleSystem m_DieEffectGlow;

	public GameObject m_HitEffect;

	public GameObject m_CurrencyEffect;

	public GameObject m_EndEffect;

	public GameObject m_BoostEffect;

	public MeshRenderer[] m_FireRenderer;

	public AnimationCurve m_LaunchCurve;

	public Vector3 m_BonusExtents;

	public Vector3 m_HitExtents;

	public Vector3 m_ObstacleExtents;

	public List<TrailRenderer> m_Trails;

	private bool m_IsMoving;

	private Vector3 m_LastPos;

	private float m_ForwardSpeed;

	private Transform m_Transform;

	private MainCamera m_MainCamera;

	private GameManager m_GameManager;

	private HapticFeedback m_HapticManager;

	private bool m_IsDead;

	private Vector3 m_InputBuffer;

	private Vector3 m_PosBuffer;

	private Vector3 m_OffsetBuffer;

	private Quaternion m_RotBuffer;

	private int m_BonusMask;

	private int m_ObstacleMask;

	private int m_BoostMask;

	private Vector3 m_BasePosition;

	private RaycastHit[] m_CollisionBuffer;

	private float m_SpeedBuffer;

	private void Awake()
	{
		m_Transform = base.transform;
		m_BasePosition = new Vector3(0f, -3f, 0f);
		m_Transform.position = m_BasePosition;
		m_PosBuffer = m_BasePosition;
		for (int i = 0; i < m_Trails.Count; i++)
		{
			m_Trails[i].Clear();
		}
		m_MainCamera = SingletonMB<MainCamera>.Instance;
		m_GameManager = SingletonMB<GameManager>.Instance;
		m_HapticManager = SingletonMB<HapticFeedback>.Instance;
		m_BonusMask = LayerMask.GetMask("Currency");
		m_ObstacleMask = LayerMask.GetMask("Obstacle");
		m_BoostMask = LayerMask.GetMask("Boost");
		m_CollisionBuffer = new RaycastHit[20];
		m_GameManager.onGamePhaseChanged += OnGamePhaseChanged;
	}

	private void OnDestroy()
	{
		if (m_GameManager != null)
		{
			m_GameManager.onGamePhaseChanged -= OnGamePhaseChanged;
		}
	}

	private void OnGamePhaseChanged(GamePhase _Phase)
	{
		switch (_Phase)
		{
		case GamePhase.GAME:
			break;
		case GamePhase.MAIN_MENU:
			Init();
			m_MainCamera.Init();
			break;
		case GamePhase.INTRO:
			StartCoroutine(Launch());
			break;
		case GamePhase.SAVE_ME:
			Die();
			break;
		case GamePhase.CONTINUE_GAME:
			Revive();
			break;
		case GamePhase.SUCCESS:
			if (m_GameManager.GetVibrations())
			{
				Handheld.Vibrate();
			}
			Object.Instantiate(m_EndEffect, m_PosBuffer, Quaternion.identity);
			break;
		case GamePhase.FAILED:
			Die();
			break;
		}
	}

	public void Init()
	{
		m_IsDead = false;
		m_IsMoving = false;
		m_LastPos = Vector3.zero;
		m_Transform.position = m_BasePosition;
		m_Transform.rotation = Quaternion.identity;
		m_PosBuffer = m_Transform.position;
		m_OffsetBuffer = Vector3.zero;
		m_RotBuffer = Quaternion.identity;
		m_InputBuffer = Vector3.zero;
		m_Ship.SetActive(value: true);
		m_ForwardSpeed = Mathf.Clamp(50f + 0.2f * (float)m_GameManager.GetLevel(), 0f, 70f);
		m_SpeedBuffer = m_ForwardSpeed * 1f;
		for (int i = 0; i < m_Trails.Count; i++)
		{
			m_Trails[i].Clear();
		}
	}

	private void Revive()
	{
		m_IsDead = false;
		m_IsMoving = false;
		m_LastPos = Vector3.zero;
		m_PosBuffer.x = 0f;
		m_PosBuffer.y = 0f;
		m_Transform.position = m_PosBuffer;
		m_OffsetBuffer = Vector3.zero;
		m_RotBuffer = Quaternion.identity;
		m_InputBuffer = Vector3.zero;
		m_Ship.SetActive(value: true);
		for (int i = 0; i < m_Trails.Count; i++)
		{
			m_Trails[i].Clear();
		}
		StartCoroutine(RestoreSpeed());
	}

	private void Die()
	{
		if (!m_IsDead)
		{
			if (m_GameManager.GetVibrations())
			{
				Handheld.Vibrate();
			}
			m_IsDead = true;
			m_ForwardSpeed = 0f;
			m_Ship.SetActive(value: false);
			m_DieEffect.Play();
		}
	}

	private IEnumerator RestoreSpeed()
	{
		float time = 0f;
		while (time < 1f)
		{
			time += Time.deltaTime;
			m_ForwardSpeed = Mathf.Lerp(m_ForwardSpeed, m_SpeedBuffer, time / 2f);
			yield return null;
		}
	}

	private IEnumerator Launch()
	{
		float time = 0f;
		while (time < 1f)
		{
			time += Time.deltaTime / 1f;
			m_PosBuffer.y = Mathf.Lerp(m_BasePosition.y, 0f, m_LaunchCurve.Evaluate(time));
			yield return null;
		}
		m_GameManager.ChangePhase(GamePhase.GAME);
	}

	private void Update()
	{
		PlayerMovement();
	}

	private void PlayerMovement()
	{
		GamePhase currentPhase = m_GameManager.currentPhase;
		Vector3 position;
		Quaternion rotation;
		int num;
		int num2;
		switch (currentPhase)
		{
		case GamePhase.FAILED:
			return;
		case GamePhase.GAME:
		case GamePhase.CONTINUE_GAME:
		{
			Vector3 mousePosition = UnityEngine.Input.mousePosition;
			if (Input.GetMouseButtonDown(0))
			{
				m_IsMoving = true;
				m_LastPos = mousePosition;
			}
			if (m_IsMoving)
			{
				Vector3 vector = mousePosition - m_LastPos;
				m_InputBuffer.Set(vector.x, vector.y, 0f);
				m_LastPos = mousePosition;
			}
			else
			{
				m_InputBuffer = Vector3.zero;
			}
			if (Input.GetMouseButtonUp(0))
			{
				m_IsMoving = false;
				m_InputBuffer = Vector3.zero;
			}
			m_InputBuffer.x *= 1.25f;
			m_InputBuffer.y *= 0.75f;
			m_OffsetBuffer = (Vector3.forward * m_ForwardSpeed + m_InputBuffer) * Time.deltaTime;
			m_PosBuffer += m_OffsetBuffer;
			m_PosBuffer.x = Mathf.Clamp(m_PosBuffer.x, -3.5f, 3.5f);
			m_PosBuffer.y = Mathf.Clamp(m_PosBuffer.y, -3f, 3f);
			position = m_Transform.position;
			rotation = m_Transform.rotation;
			num = Physics.BoxCastNonAlloc(position, m_ObstacleExtents, Vector3.forward, m_CollisionBuffer, rotation, 1f, m_ObstacleMask);
			num2 = 0;
			goto IL_01ec;
		}
		case GamePhase.SUCCESS:
			{
				m_OffsetBuffer = Vector3.forward * m_ForwardSpeed * Time.deltaTime;
				m_PosBuffer += m_OffsetBuffer;
				break;
			}
			IL_01ec:
			if (num2 < num)
			{
				//if (m_GameManager.currentPhase == GamePhase.GAME)
				//{
				//	m_GameManager.ChangePhase(GamePhase.SAVE_ME);
				//}
				
				{
					m_GameManager.ChangePhase(GamePhase.FAILED);
				}
				SingletonMB<SoundManager>.Instance.PlaySound(ESoundType.EXPLOSION);
				return;
			}
			num = Physics.BoxCastNonAlloc(position, m_HitExtents, Vector3.forward, m_CollisionBuffer, rotation, 1f, m_ObstacleMask);
			for (num2 = 0; num2 < num; num2++)
			{
				GameObject gameObject = Object.Instantiate(m_HitEffect, m_Transform);
				Vector3 point = m_CollisionBuffer[num2].point;
				gameObject.transform.position = point;
				m_PosBuffer.x += Mathf.Sign(position.x - point.x) * 0.05f;
				m_Transform.position = new Vector3(m_PosBuffer.x, position.y, position.z);
				if (m_GameManager.GetVibrations())
				{
					m_HapticManager.DoHeavyImapactHaptic();
				}
			}
			num = Physics.BoxCastNonAlloc(position, m_BonusExtents, Vector3.forward, m_CollisionBuffer, rotation, 1f, m_BonusMask);
			for (num2 = 0; num2 < num; num2++)
			{
				GameObject gameObject2 = m_CollisionBuffer[num2].transform.gameObject;
				UnityEngine.Object.Destroy(gameObject2);
				m_GameManager.AddCurrency(1);
				Object.Instantiate(m_CurrencyEffect, m_Transform);
				SingletonMB<SoundManager>.Instance.PlaySound(ESoundType.BONUS);
				if (m_GameManager.GetVibrations())
				{
					m_HapticManager.DoHeavyImapactHaptic();
				}
			}
			num = Physics.BoxCastNonAlloc(position, m_BonusExtents, Vector3.forward, m_CollisionBuffer, rotation, 1f, m_BoostMask);
			for (num2 = 0; num2 < num; num2++)
			{
				GameObject gameObject3 = m_CollisionBuffer[num2].transform.gameObject;
				UnityEngine.Object.Destroy(gameObject3);
				Object.Instantiate(m_BoostEffect, m_Transform);
				StartCoroutine(Boost());
				if (m_GameManager.GetVibrations())
				{
					m_HapticManager.DoHeavyImapactHaptic();
				}
			}
			break;
			IL_01e6:
			num2++;
			goto IL_01ec;
		}
		m_Transform.position = Vector3.Lerp(m_Transform.position, m_PosBuffer, 0.1f);
		float num3 = (0f - Mathf.Clamp(m_OffsetBuffer.x, -0.5f, 0.5f)) / 0.5f;
		float num4 = (0f - Mathf.Clamp(m_OffsetBuffer.y, -0.5f, 0.5f)) / 0.5f;
		m_RotBuffer = Quaternion.Euler(num4 * 45f, 0f, num3 * 45f);
		m_Transform.rotation = Quaternion.Lerp(m_Transform.rotation, m_RotBuffer, 0.2f);
		if (currentPhase != GamePhase.GAME && currentPhase != GamePhase.CONTINUE_GAME)
		{
			return;
		}
		Vector3 position2 = m_Transform.position;
		if (position2.z > m_GameManager.totalDistance)
		{
			m_GameManager.ChangePhase(GamePhase.SUCCESS);
			if (m_GameManager.m_SpeedChallenge)
			{
				SingletonMB<ChallengeManager>.Instance.ChallengeHasCompleted(_Won: true);
			}
		}
	}

	public void SetColor(Color _Color)
	{
		ParticleSystem.MainModule main = m_DieEffectGlow.main;
		ParticleSystem.MinMaxGradient startColor = main.startColor;
		startColor.color = _Color;
		main.startColor = startColor;
		for (int i = 0; i < m_FireRenderer.Length; i++)
		{
			m_FireRenderer[i].material.color = _Color;
			m_FireRenderer[i].material.SetColor("_EmissionColor", _Color);
		}
		_Color.r *= 0.4f;
		_Color.g *= 0.4f;
		_Color.b *= 0.4f;
		for (int j = 0; j < m_ShipRenderer.Length; j++)
		{
			m_ShipRenderer[j].material.color = _Color;
		}
	}

	public void ChangePlayerSpeed(float _Speed)
	{
		m_ForwardSpeed = 50f * _Speed;
		m_SpeedBuffer = m_ForwardSpeed;
	}

	private IEnumerator Boost()
	{
		m_ForwardSpeed *= 4f;
		yield return new WaitForSeconds(0.2f);
		m_ForwardSpeed = m_SpeedBuffer;
	}
}
