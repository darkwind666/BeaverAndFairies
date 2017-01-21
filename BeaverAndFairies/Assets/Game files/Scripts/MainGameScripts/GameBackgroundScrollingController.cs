using UnityEngine;
using System.Collections;

public class GameBackgroundScrollingController : MonoBehaviour {

	public float scrollingSpeed;

	void Start () {
	
	}

	void Update () {
		
		Vector2 offset = new Vector2(0, Time.time * scrollingSpeed);
		Renderer renderer = GetComponent<Renderer>();
		renderer.material.mainTextureOffset = offset;

	}
}
