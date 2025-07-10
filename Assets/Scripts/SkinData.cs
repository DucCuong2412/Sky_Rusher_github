using UnityEngine;

[CreateAssetMenu(fileName = "SkinData", menuName = "SkyRusher/Skins", order = 1)]
public class SkinData : ScriptableObject
{
	public string m_SaveId;

	public Sprite m_Preview;

	public GameObject m_Prefab;

	public ESkinCategory m_Category;

	public int m_Order;

	public int m_Price;
}
