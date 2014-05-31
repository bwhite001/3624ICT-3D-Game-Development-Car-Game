using UnityEngine;
using System.Collections;

public class PatrolPoint : MonoBehaviour {
	
	void OnDrawGizmos () {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, 10.0f);
	}
}
