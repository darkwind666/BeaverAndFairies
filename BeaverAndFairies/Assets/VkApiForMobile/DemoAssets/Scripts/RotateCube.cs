using UnityEngine;
using System.Collections;

public class RotateCube : MonoBehaviour {
	public int Degree_per_second;
	// Use this for initialization
	void Start () {
	    
	}
	
	void Update () {
		transform.Rotate (Vector3.up, Degree_per_second * Time.deltaTime);
	}
}
