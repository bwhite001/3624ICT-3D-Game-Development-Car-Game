using UnityEngine;
using System.Collections;

public class PlayerColorChange : MonoBehaviour {
	public Material[] carMaterals;
	public Material[] arrowMaterals;
	public GameObject arrow;
	public GameObject body;
	 


	// Use this for initialization
	void Start () {
		int index = (int)Random.Range (0, carMaterals.Length - 1);
		setCarColor (index);
	}

	int getColorCombo(int index)
	{
		// 0 = Blue
		// 1 = Green
		// 2 = Orange
		// 3 = Purple
		// 4 = Red
		// 5 = Yellow

		switch(index)
		{
			//Blue + Orange
			case 0:
				return 2;
			//Green + Red
			case 1:
				return 4;
			//Orange + Blue
			case 2:
				return 0;
			//Purple + Yellow
			case 3:
				return 5;
			//Red + Green
			case 4:
				return 1;
			//Yellow + Purple
			case 5:
				return 3;

			
		}

		return 0;
	}

	public void setCarColor(int selectedColor)
	{
		int carColor = selectedColor;

		int arrowColor = getColorCombo(carColor);

		Debug.Log (carColor + " " + arrowColor);

		if(carMaterals[carColor] != null && arrowMaterals[arrowColor] != null)
		{
			body.renderer.material = carMaterals[carColor];
			arrow.renderer.material = arrowMaterals[arrowColor];
		}
	}
}
