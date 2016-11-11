using UnityEngine;
using System.Collections;


[System.Serializable]
public class FairiesDataList : ScriptableObject {

	public GameFairyModel[] dataArray;
	public int slowBonusPrice;
	public int damageBonusPrice;
}
