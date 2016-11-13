using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameFairyModel {

	public Texture2D fairyTexture;
	public int fairyPrice;
	public int fairyCreateSlowBonusTime;
	public int fairyCreateDamageBonusTime;

}

[System.Serializable]
public class ScoresCountInAppData {

	public float price;
	public int scoresCount;

}
