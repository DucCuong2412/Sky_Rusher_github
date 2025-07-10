using UnityEngine;

public class BallScript : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		UnityEngine.Debug.Log(collision.gameObject.name);
		if (collision.gameObject.name == "Platform (8)")
		{
			base.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 20f), ForceMode2D.Impulse);
			HapticFeedback.DoHaptic(HapticFeedback.NotificationType.Success);
		}
		else
		{
			HapticFeedback.DoHaptic(HapticFeedback.HapticForce.Medium);
		}
	}
}
