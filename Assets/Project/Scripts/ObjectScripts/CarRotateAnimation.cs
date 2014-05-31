using UnityEngine;
using System.Collections;

public class CarRotateAnimation : MonoBehaviour {

	public float spinSpeed = 1.0f;
	private Vector3 axis = new Vector3(0,1,0);

	// Use this for initialization
	void Start () {
		axis = new Vector3(0,1,0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(axis * Time.deltaTime * (spinSpeed * 360));
	}
}
