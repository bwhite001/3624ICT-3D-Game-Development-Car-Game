using UnityEngine;
using System.Collections;

public class PatrolPointController : MonoBehaviour {

	public Transform[] getPatrolPoints()
	{

		int children = transform.childCount;
		Transform[] patrolPoints = new Transform[children];
		for (int i = 0; i < children; ++i)
			patrolPoints[i] = transform.GetChild(i);

		return patrolPoints;
	}
}
