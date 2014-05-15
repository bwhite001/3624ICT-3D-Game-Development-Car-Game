using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {
	public CarAI carPrefab;

	private CarAI[] cars;

	public float largest = 500f;

	public void createCars (int ammount)
	{

		cars = new CarAI[ammount];

		for (int i = 0; i<ammount; i++) {
			Vector3 pos = getRandomPos();

			CarAI temp = Instantiate(carPrefab, pos, Quaternion.identity) as CarAI;
			temp.transform.parent = transform;
			//add to waypoints array
			cars[i] = temp;

		}
	}

	Vector3 getRandomPos() {
		float x = Random.Range (-largest, largest);
		float z = Random.Range (-largest, largest);
		Vector3 position = new Vector3(x, 0, z);

		return position;
	}
}
