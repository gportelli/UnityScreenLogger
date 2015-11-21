using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	public float RotationSpeed = 70;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0, RotationSpeed * Time.deltaTime, 0));
	}
}
