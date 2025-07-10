using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingletonMB<PoolManager>
{
	public class Pool
	{
		public GameObject m_Prefab;

		public Stack<GameObject> m_FreeInstances;
	}

	private Dictionary<string, Pool> m_Pools;

	public static void CreatePool(string _Name, GameObject _Prefab, int _Count)
	{
		SingletonMB<PoolManager>.Instance.CreatePool_Internal(_Name, _Prefab, _Count);
	}

	private void CreatePool_Internal(string _Name, GameObject _Prefab, int _Count)
	{
		if (m_Pools == null)
		{
			m_Pools = new Dictionary<string, Pool>();
		}
		if (m_Pools.ContainsKey(_Name))
		{
			UnityEngine.Debug.LogError("A pool already exists for the object " + _Name);
			return;
		}
		Pool pool = new Pool();
		pool.m_Prefab = _Prefab;
		pool.m_FreeInstances = new Stack<GameObject>();
		if (pool.m_Prefab != null)
		{
			for (int i = 0; i < _Count; i++)
			{
				GameObject gameObject = Object.Instantiate(pool.m_Prefab, base.transform);
				gameObject.SetActive(value: false);
				pool.m_FreeInstances.Push(gameObject);
			}
		}
		else
		{
			UnityEngine.Debug.LogWarning("There is no prefab for the pool " + _Name);
		}
		m_Pools.Add(_Name, pool);
	}

	public static void FreePool(string _Id)
	{
		SingletonMB<PoolManager>.Instance.FreePool_Internal(_Id);
	}

	private void FreePool_Internal(string _Id)
	{
		if (m_Pools != null && m_Pools.ContainsKey(_Id))
		{
			Pool pool = m_Pools[_Id];
			while (pool.m_FreeInstances.Count > 0)
			{
				UnityEngine.Object.Destroy(m_Pools[_Id].m_FreeInstances.Pop());
			}
			m_Pools.Remove(_Id);
		}
	}

	public static void FreeAllPools()
	{
		SingletonMB<PoolManager>.Instance.FreeAllPools_Internal();
	}

	private void FreeAllPools_Internal()
	{
		if (m_Pools != null && m_Pools.Count != 0)
		{
			Dictionary<string, Pool>.KeyCollection keys = m_Pools.Keys;
			foreach (string item in keys)
			{
				FreePool(item);
			}
		}
	}

	public static GameObject GetInstance(string _Id)
	{
		return SingletonMB<PoolManager>.Instance.GetInstance_Internal(_Id);
	}

	private GameObject GetInstance_Internal(string _Id)
	{
		if (m_Pools == null || !m_Pools.ContainsKey(_Id))
		{
			UnityEngine.Debug.LogError("There is no pool for the object " + _Id + " ! can't return any object");
			return null;
		}
		Pool pool = m_Pools[_Id];
		if (pool.m_FreeInstances.Count == 0)
		{
			return Object.Instantiate(pool.m_Prefab);
		}
		GameObject gameObject = pool.m_FreeInstances.Pop();
		gameObject.SetActive(value: true);
		return gameObject;
	}

	public static void FreeInstance(string _Id, GameObject _Instance)
	{
		SingletonMB<PoolManager>.Instance.FreeInstance_Internal(_Id, _Instance);
	}

	private void FreeInstance_Internal(string _Id, GameObject _Instance)
	{
		if (m_Pools == null || !m_Pools.ContainsKey(_Id))
		{
			UnityEngine.Debug.LogError("There is no pool for the object " + _Id + " ! can't free this object");
			return;
		}
		_Instance.SetActive(value: false);
		if (_Instance.transform.parent != base.transform)
		{
			_Instance.transform.parent = base.transform;
		}
		m_Pools[_Id].m_FreeInstances.Push(_Instance);
	}
}
