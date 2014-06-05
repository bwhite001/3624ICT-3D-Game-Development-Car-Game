using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {

	//public Transform player; //Camera to use
	private Transform target; //Target to point at (you could set this to any gameObject dynamically)
	
	void Update() {
		transform.LookAt(target);
	}

	public void changeTarget(Transform newtarget)
	{
		target = newtarget;
	}
}
