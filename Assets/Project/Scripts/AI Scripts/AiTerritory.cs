using UnityEngine;
using System.Collections;

public class AiTerritory : MonoBehaviour {

	public Vector3 terrySize = new Vector3(100,100,100);

	void OnDrawGizmos(){
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube (transform.position, terrySize);
	}
}
