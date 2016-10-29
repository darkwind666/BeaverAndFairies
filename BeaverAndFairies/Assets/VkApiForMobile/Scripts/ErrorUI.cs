using UnityEngine;
using System.Collections;

public class ErrorUI : MonoBehaviour {
	static ErrorUI Instance;
	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
		if (Instance == null) {
			Instance = this;
		} else {
			DestroyImmediate(gameObject);
		}

	}
}
