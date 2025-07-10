using System.Collections;
using UnityEngine;

public class ScreenShaker : MonoBehaviour
{
	private static ScreenShaker _instance;

	private const float DEFAULT_SHAKE_INTENSITY = 0.5f;

	private const float DEFAULT_WAVE_INTENSITY = 2f;

	private const float DEFAULT_DURATION = 0.1f;

	private Camera mainCam;

	private Coroutine shakeCo;

	public static ScreenShaker instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject gameObject = new GameObject("_ScreenShaker");
				_instance = gameObject.AddComponent<ScreenShaker>();
				Object.DontDestroyOnLoad(gameObject);
			}
			return _instance;
		}
	}

	public void Shake(bool fadeOut = false)
	{
		Shake(0.5f, 0.1f, fadeOut);
	}

	public void Shake(float intensity, float duration, bool fadeOut = false)
	{
		if (!mainCam)
		{
			mainCam = Camera.main;
		}
		if (shakeCo != null)
		{
			StopCoroutine(shakeCo);
		}
		shakeCo = StartCoroutine(ShakeCo(intensity, duration, fadeOut));
	}

	private IEnumerator ShakeCo(float intensity, float duration, bool fadeOut = false)
	{
		float t = 0f;
		float _intensity = intensity;
		for (; t < duration; t += Time.deltaTime)
		{
			if (!(mainCam != null))
			{
				break;
			}
			Vector3 basePos = mainCam.transform.position;
			mainCam.transform.position = basePos + (Vector3)UnityEngine.Random.insideUnitCircle * _intensity;
			yield return new WaitForEndOfFrame();
			mainCam.transform.position = basePos;
			yield return null;
			if (fadeOut)
			{
				_intensity = Mathf.Lerp(intensity, 0f, t / duration);
			}
		}
	}

	public void Wave(bool fadeOut = true)
	{
		Wave(Vector3.right, 2f, 0.5f, fadeOut);
	}

	public void Wave(Vector3 axis, float intensity, float duration, bool fadeOut = true)
	{
		if (!mainCam)
		{
			mainCam = Camera.main;
		}
		if (shakeCo != null)
		{
			StopCoroutine(shakeCo);
		}
		shakeCo = StartCoroutine(WaveCo(axis, intensity, duration, fadeOut));
	}

	private IEnumerator WaveCo(Vector3 axis, float intensity, float duration, bool fadeOut = true)
	{
		float t = 0f;
		float _intensity = intensity;
		int sign = 1;
		while (t < duration && mainCam != null)
		{
			Quaternion baseRot = mainCam.transform.rotation;
			mainCam.transform.rotation = baseRot;
			mainCam.transform.Rotate(axis, _intensity * (float)sign, Space.Self);
			yield return new WaitForEndOfFrame();
			mainCam.transform.rotation = baseRot;
			yield return null;
			int seg = (int)Mathf.Floor(t / (duration / 4f));
			if (fadeOut)
			{
				_intensity = Mathf.Lerp(Mathf.Lerp(intensity, 0f, 0.25f * (float)seg), 0f, (t - duration / 4f * (float)seg) / (duration / 4f));
			}
			t += Time.deltaTime;
			sign = ((seg % 2 == 0) ? 1 : (-1));
		}
	}
}
