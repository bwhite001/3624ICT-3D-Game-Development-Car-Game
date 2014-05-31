using UnityEngine;
using System.Collections;

public class CarColors {
	public static int currentCarColor;
	public static void setColor(int c)
	{
		currentCarColor = c;
	}

	public static int getColor()
	{
		if(currentCarColor != null)
		return currentCarColor;
		else
		return (int)Random.Range (0, 6 - 1);
	}


}
