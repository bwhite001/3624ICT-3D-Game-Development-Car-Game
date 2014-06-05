using UnityEngine;
using System.Collections;

public class CarColors {
	public static int currentCarColor = -1;


	public static void setColor(int c)
	{
		currentCarColor = c;
	}

	public static int getColor()
	{
		if(currentCarColor >= 0)
			return currentCarColor;
		else
			return (int)Random.Range (0, 5);
	}


}
