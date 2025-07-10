using System.Collections;
using UnityEngine;

namespace PolygonArsenal
{
	public class PolygonLoopScript : MonoBehaviour
	{
		public GameObject chosenEffect;

		public float loopTimeLimit = 2f;

		private void Start()
		{
			PlayEffect();
		}

		public void PlayEffect()
		{
			StartCoroutine("EffectLoop");
		}

		private IEnumerator EffectLoop()
		{
			GameObject effectPlayer = UnityEngine.Object.Instantiate(chosenEffect);
			effectPlayer.transform.position = base.transform.position;
			yield return new WaitForSeconds(loopTimeLimit);
			UnityEngine.Object.Destroy(effectPlayer);
			PlayEffect();
		}
	}
}
