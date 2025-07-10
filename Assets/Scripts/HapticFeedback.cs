using UnityEngine;

public class HapticFeedback : SingletonMB<HapticFeedback>
{
	public enum HapticForce
	{
		Light,
		Medium,
		Heavy
	}

	public enum NotificationType
	{
		Error,
		Success,
		Warning
	}

	public void DoNotificationHapticError()
	{
		UnityEngine.Debug.Log("HapticFeedback is not support on this platform");
	}

	public void DoNotificationHapticSuccess()
	{
		UnityEngine.Debug.Log("HapticFeedback is not support on this platform");
	}

	public void DoNotificationHapticWarning()
	{
		UnityEngine.Debug.Log("HapticFeedback is not support on this platform");
	}

	public void DoSelectionHaptic()
	{
		UnityEngine.Debug.Log("HapticFeedback is not support on this platform");
	}

	public void DoLightImapactHaptic()
	{
		UnityEngine.Debug.Log("HapticFeedback is not support on this platform");
	}

	public void DoMediumImapactHaptic()
	{
		UnityEngine.Debug.Log("HapticFeedback is not support on this platform");
	}

	public void DoHeavyImapactHaptic()
	{
		UnityEngine.Debug.Log("HapticFeedback is not support on this platform");
	}

	public static void DoHaptic()
	{
		UnityEngine.Debug.Log("HapticFeedback is not support on this platform");
	}

	public static void DoHaptic(HapticForce type)
	{
		UnityEngine.Debug.Log("HapticFeedback is not support on this platform");
	}

	public static void DoHaptic(NotificationType type)
	{
		UnityEngine.Debug.Log("HapticFeedback is not support on this platform");
	}

	public static void DoFallbackHapticNope()
	{
		UnityEngine.Debug.Log("HapticFeedback is not support on this platform");
	}
}
