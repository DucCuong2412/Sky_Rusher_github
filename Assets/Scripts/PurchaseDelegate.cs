using UnityEngine;
//using UnityEngine.Purchasing;

internal class PurchaseDelegate : SingletonMB<PurchaseDelegate>, IPurchaseDelegate
{
	protected override void AwakeSpecific()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		UnityEngine.Debug.Log("Registering delegate");
		//VoodooSauce.RegisterPurchaseDelegate(this);
	}

	public void Purchase(string _ProductId)
	{
		//VoodooSauce.Purchase(_ProductId);
	}

	public void RestorePurchase()
	{
		//VoodooSauce.RestorePurchases();
	}

	public void OnInitializeSuccess()
	{
	}

	//public void OnInitializeFailure(InitializationFailureReason _Reason)
	//{
	//	UnityEngine.Debug.LogError("Initialization failed. " + _Reason);
	//}

	public void OnPurchaseComplete(string _ProductId)
	{
		InfoView instance = SingletonMB<InfoView>.Instance;
		if (_ProductId.Equals(Constants.c_NoAdsBundleID))
		{
			EnableNoAds();
			instance.SetDescription("No ads successfully purchased!");
			instance.gameObject.SetActive(value: true);
		}
	}

	private void EnableNoAds()
	{
		//if (!VoodooSauce.IsPremium())
		//{
		//	VoodooSauce.EnablePremium();
		//	SingletonMB<MainMenuView>.Instance.RefreshNoAds();
		//}
	}

	//public void OnPurchaseFailure(string _ProductId, PurchaseFailureReason _Reason)
	//{
	//	UnityEngine.Debug.LogError("Can't purchase " + _ProductId + ", " + _Reason);
	//	InfoView instance = SingletonMB<InfoView>.Instance;
	//	instance.SetDescription("Purchase failed.");
	//	instance.gameObject.SetActive(value: true);
	//}
}
