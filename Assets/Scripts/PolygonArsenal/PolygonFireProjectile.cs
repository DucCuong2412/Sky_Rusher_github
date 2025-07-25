using UnityEngine;
using UnityEngine.EventSystems;

namespace PolygonArsenal
{
	public class PolygonFireProjectile : MonoBehaviour
	{
		private RaycastHit hit;

		public GameObject[] projectiles;

		public Transform spawnPosition;

		[HideInInspector]
		public int currentProjectile;

		public float speed = 1000f;

		private PolygonButtonScript selectedProjectileButton;

		private void Start()
		{
			selectedProjectileButton = GameObject.Find("Button").GetComponent<PolygonButtonScript>();
		}

		private void Update()
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.RightArrow))
			{
				nextEffect();
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.D))
			{
				nextEffect();
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.A))
			{
				previousEffect();
			}
			else if (UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow))
			{
				previousEffect();
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition), out hit, 100f))
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(projectiles[currentProjectile], spawnPosition.position, Quaternion.identity);
				gameObject.transform.LookAt(hit.point);
				gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * speed);
			}
			UnityEngine.Debug.DrawRay(Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition).origin, Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition).direction * 100f, Color.yellow);
		}

		public void nextEffect()
		{
			if (currentProjectile < projectiles.Length - 1)
			{
				currentProjectile++;
			}
			else
			{
				currentProjectile = 0;
			}
			selectedProjectileButton.getProjectileNames();
		}

		public void previousEffect()
		{
			if (currentProjectile > 0)
			{
				currentProjectile--;
			}
			else
			{
				currentProjectile = projectiles.Length - 1;
			}
			selectedProjectileButton.getProjectileNames();
		}

		public void AdjustSpeed(float newSpeed)
		{
			speed = newSpeed;
		}
	}
}
