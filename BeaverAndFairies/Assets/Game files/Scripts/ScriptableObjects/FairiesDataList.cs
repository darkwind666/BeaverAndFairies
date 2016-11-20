using UnityEngine;
using System.Collections;


[System.Serializable]
public class FairiesDataList : ScriptableObject {

	public GameFairyModel[] dataArray;
	public ScoresCountInAppData[] inAppsDataArray;
	public int slowBonusPrice;
	public int damageBonusPrice;
	public float blockAdsInAppPrice;
	public int damageBonusRechargeTime;
	public int damageExplosionsBonusCount;
	public float bonusGameSpeedSlowdownRate;
	public int slowBonusMaxTime;
	public int slowBonusRechargeTime;
	public int finalChanceExplosionsBonusCount;
	public int showButtonTime;
}
