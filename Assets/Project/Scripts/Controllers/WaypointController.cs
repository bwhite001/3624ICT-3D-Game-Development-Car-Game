using UnityEngine;
using System.Collections;

public class WaypointController : MonoBehaviour {
	public Waypoint waypointPrefab;

	private Waypoint[] waypoints;

	public int ammount = 35;

	public int selectedWaypointIndex;

	public float largest = 500f;

	private string[] WaypontNames = {"Melazzo","Baden","Xhoris","Tuscaloosa","Saarbrï¿½cken","Marbella","Tuglie",
	                                 "Grezzana","Purmerend","Spokane","Barddhaman","Charleroi","Balclutha","Schiltigheim",
	                                 "Oppido Mamertina","Chï¿½tillon","Pont-Saint-Martin","Salt Lake City","Calestano",
	                                 "Sogliano Cavour","North Barrackpur","Hope","Santu Lussurgiu","Vellore",
	                                 "San Giovanni Suergiu","Andernach","Moncton","Eghezee","Saint-Martin","Barrhead",
	                                 "Schoonaarde","Pastena","Calvera","Elgin","Georgia","Rodengo/Rodeneck","Erpion",
	                                 "Tirunelveli","Bon Accord","Gwalior","Narcao","Flï¿½nu","Bearberry","Saint-Gï¿½ry",
	                                 "Jonquiï¿½re","Parbhani","Broxburn","Hay River","Joondalup","Wellington",
	                                 "Jerez de la Frontera","Kitscoty","Dindigul","Abingdon","Melle","Pickering",
	                                 "Pomarico","Bionaz","Essex","New Westminster","Biloxi","Limal","Alexandria",
	                                 "Alness","Bargagli","Subbiano","Ujjain","Rigolet","Coalhurst","Newport","Rotorua",
	                                 "Sant'Agapito","Milnathort","Cercepiccola","Busso","Cascavel","Hannover","Cercemaggiore",
	                                 "Castel Ritaldi","Mellery","Tufara","Montague","Blumenau","Cisterna di Latina","Marcq-en-Baroeul",
	                                 "Campagna","Nobressart","Caprino Bergamasco","Karlsruhe","Isnes","Rosciano","Levallois-Perret",
									 "Frauenkirchen","Tarsia","Mellet","Wha Ti","Santa Cruz de Tenerife","Penhold","Lamontzï¿½e","Nelson"};

	void createWaypoints (int ammount)
	{
		waypoints = new Waypoint[ammount];

		for (int i = 0; i<ammount; i++) {
			Vector3 pos = getRandomPos();

			Waypoint temp = Instantiate(waypointPrefab, pos, Quaternion.identity) as Waypoint;

			//set all waypoints to not selected
			temp.selected = false;

			int index = (int)Random.Range (0,99);
			string tmpName = WaypontNames[index];

			temp.waypointName = tmpName;

			//add to waypoints array
			waypoints[i] = temp;
		}
	}

	Vector3 getRandomPos() {
		float x = Random.Range (-largest, largest);
		float z = Random.Range (-largest, largest);
		Vector3 position = new Vector3(x, 0, z);

		return position;
	}

	void startWaypointSelection() {


	}

	void selectWaypoint() {

		selectedWaypointIndex = (int)Random.Range (0, ammount - 1);

		waypoints [selectedWaypointIndex].SendMessage ("setSelected", true);

		GameObject.FindGameObjectWithTag ("ArrowController").SendMessage ("changeTarget", waypoints [selectedWaypointIndex].transform);

	}
	
	public Waypoint getSelectedWaypoint()
	{
		return waypoints [selectedWaypointIndex];
	}
}
