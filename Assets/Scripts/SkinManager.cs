using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : SingletonMB<SkinManager>
{
	public delegate void OnPlayerChanged(Player _Player);

	public SkinData m_DefaultSkin;

	public GameObject m_SkinSlotPrefab;

	public Transform m_Content;

	private List<SkinData> m_Skins;

	private SkinView m_SkinView;

	private ZoningManager m_ZoningManager;

	private GameObject m_Player;

	private List<SkinComponent> m_SkinComponents;

	private GameManager m_GameManager;

	public event OnPlayerChanged onPlayerChanged;

	private void Awake()
	{
		m_SkinView = SingletonMB<SkinView>.Instance;
		m_ZoningManager = SingletonMB<ZoningManager>.Instance;
		m_GameManager = SingletonMB<GameManager>.Instance;
		m_Skins = new List<SkinData>();
		m_Skins.AddRange(Resources.LoadAll<SkinData>(Constants.c_SkinPath));
		m_Skins.Sort((SkinData x, SkinData y) => (x.m_Category == y.m_Category) ? x.m_Order.CompareTo(y.m_Order) : x.m_Category.CompareTo(y.m_Category));
		GameObject prefab = m_DefaultSkin.m_Prefab;
		string value = null;
		if (PlayerPrefs.HasKey(Constants.c_SelectedSkinSave))
		{
			value = PlayerPrefs.GetString(Constants.c_SelectedSkinSave);
		}
		else
		{
			PlayerPrefs.SetInt(m_DefaultSkin.m_SaveId, 1);
			PlayerPrefs.SetString(Constants.c_SelectedSkinSave, m_DefaultSkin.m_SaveId);
		}
		m_SkinComponents = new List<SkinComponent>();
		for (int i = 0; i < m_Skins.Count; i++)
		{
			GameObject gameObject = Object.Instantiate(m_SkinSlotPrefab, m_Content);
			SkinComponent component = gameObject.GetComponent<SkinComponent>();
			component.InitSkin(m_Skins[i]);
			gameObject.GetComponent<Image>().sprite = m_Skins[i].m_Preview;
			if (m_Skins[i].m_SaveId.Equals(value))
			{
				prefab = m_Skins[i].m_Prefab;
			}
			m_SkinComponents.Add(component);
		}
		ChangeSkin(prefab);
	}

	private void Start()
	{
		if (this.onPlayerChanged != null)
		{
			this.onPlayerChanged(m_Player.GetComponent<Player>());
		}
	}

	public void OnSkinSelected(SkinData _SkinData)
	{
		if (HasUnlockedSkin(_SkinData))
		{
			PlayerPrefs.SetString(Constants.c_SelectedSkinSave, _SkinData.m_SaveId);
			m_SkinView.DisablePrice();
		}
		else
		{
			m_SkinView.InitPrice(_SkinData.m_Price);
		}
		ChangeSkin(_SkinData.m_Prefab);
	}

	private void ChangeSkin(GameObject _Prefab)
	{
		if (m_Player != null)
		{
			UnityEngine.Object.DestroyImmediate(m_Player);
		}
		m_Player = Object.Instantiate(_Prefab);
		Player component = m_Player.GetComponent<Player>();
		component.SetColor(m_ZoningManager.zoningColor);
		component.Init();
		if (this.onPlayerChanged != null)
		{
			this.onPlayerChanged(component);
		}
	}

	public bool BuySkin(SkinData _SkinData)
	{
		if (m_GameManager.GetCurrency() >= _SkinData.m_Price)
		{
			m_GameManager.AddCurrency(-_SkinData.m_Price);
			UnlockSkin(_SkinData);
			return true;
		}
		return false;
	}

	public void ReturnMainMenu()
	{
		SkinData selectedSkin = GetSelectedSkin();
		ChangeSkin(selectedSkin.m_Prefab);
	}

	private bool HasUnlockedSkin(SkinData _SkinData)
	{
		if (PlayerPrefs.HasKey(_SkinData.m_SaveId))
		{
			return PlayerPrefs.GetInt(_SkinData.m_SaveId) == 1;
		}
		return false;
	}

	private void UnlockSkin(SkinData _SkinData)
	{
		PlayerPrefs.SetInt(_SkinData.m_SaveId, 1);
	}

	private SkinData GetSelectedSkin()
	{
		string @string = PlayerPrefs.GetString(Constants.c_SelectedSkinSave);
		SkinData result = null;
		for (int i = 0; i < m_Skins.Count; i++)
		{
			if (m_Skins[i].m_SaveId.Equals(@string))
			{
				result = m_Skins[i];
			}
		}
		return result;
	}

	public void SetColor(Color _Color)
	{
		m_SkinView.SetColor(_Color);
		for (int i = 0; i < m_SkinComponents.Count; i++)
		{
			m_SkinComponents[i].SetColor(_Color);
		}
	}
}
