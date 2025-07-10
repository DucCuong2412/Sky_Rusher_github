using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMB<SoundManager>
{
	public bool m_SoundActivate = true;

	public AudioSource m_MainTheme;

	public List<SoundElement> m_Elements;

	public GameObject m_AudioSourcePrefab;

	private Dictionary<ESoundType, SoundElement> m_Sounds;

	private Dictionary<ESoundType, AudioSource> m_AudioSources;

	private void Awake()
	{
		m_Sounds = new Dictionary<ESoundType, SoundElement>();
		m_AudioSources = new Dictionary<ESoundType, AudioSource>();
		for (int i = 0; i < m_Elements.Count; i++)
		{
			m_Sounds.Add(m_Elements[i].m_SoundType, m_Elements[i]);
		}
		PoolManager.CreatePool("Audio", m_AudioSourcePrefab, 10);
	}

	private void Start()
	{
		if (m_SoundActivate)
		{
			m_MainTheme.Play();
		}
	}

	public void DeactivateSounds()
	{
		m_MainTheme.Stop();
		StopAllSounds();
		m_SoundActivate = false;
	}

	public void ActivateSounds()
	{
		m_MainTheme.Play();
		m_SoundActivate = true;
	}

	public void PlaySound(ESoundType _Type)
	{
		if (m_SoundActivate)
		{
			if (m_AudioSources.ContainsKey(_Type) && m_AudioSources[_Type] == null)
			{
				m_AudioSources.Remove(_Type);
			}
			AudioSource audioSource;
			if (m_AudioSources.ContainsKey(_Type))
			{
				audioSource = m_AudioSources[_Type];
			}
			else
			{
				GameObject instance = PoolManager.GetInstance("Audio");
				audioSource = instance.GetComponent<AudioSource>();
			}
			audioSource.volume = m_Sounds[_Type].m_Volume;
			audioSource.loop = m_Sounds[_Type].m_Loop;
			audioSource.clip = m_Sounds[_Type].m_Sound;
			audioSource.Play();
			if (!m_AudioSources.ContainsKey(_Type))
			{
				m_AudioSources.Add(_Type, audioSource);
			}
		}
	}

	public void StopSound(ESoundType _Type)
	{
		if (m_AudioSources.ContainsKey(_Type) && m_AudioSources[_Type] != null)
		{
			m_AudioSources[_Type].Stop();
			m_AudioSources.Remove(_Type);
		}
	}

	public void StopAllSounds()
	{
		StopSound(ESoundType.INTRO);
		StopSound(ESoundType.STARTAMBIANCE);
		StopSound(ESoundType.EXPLOSION);
		StopSound(ESoundType.ENDWOOSH);
		StopSound(ESoundType.BONUS);
	}
}
