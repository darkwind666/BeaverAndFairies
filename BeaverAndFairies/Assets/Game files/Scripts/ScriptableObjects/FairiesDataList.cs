using UnityEngine;
using System.Collections;


[System.Serializable]
public class FairiesDataList : ScriptableObject {

	public GameFairyModel[] dataArray;
	public ScoresCountInAppData[] inAppsDataArray;
	public int slowBonusPrice;
	public int damageBonusPrice;
	public float blockAdsInAppPrice;
}
